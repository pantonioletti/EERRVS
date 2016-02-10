using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EstadoResultadoWPF
{
    /*
     *  +---------------+----------+----------+
     *  |               | Input    | Output   |
     *  |   Type        | Position | Position |
     *  +---------------+----------+----------+
        | date          |    1     |  10      |
        | compte        |    2     |  11      |
        | type          |    3     |  12      |
        | comment       |    4     |  13      |
        | area          |    5     |  14*     |
        | cost_center   |    6     |  15      |
        | item          |    7     |  16*     |
        | eff_date      |    8     |  18      |
        | analisys_date |    9     |  19      |
        | reference     |   10     |  20      |
        | ref_date      |   11     |  21      |
        | exp_date      |   12     |  22      |
        | debit         |   13     |  23      |
        | credit        |   14     |  24      |
        | balance       |   15     |  25      |
        | branch        |   16     |  26      |
     *  +---------------+----------+----------+
     */
    class EERRCsvRW
    {
        const int C_IN_DATE = 1;
        const int C_IN_COMPTE = 2;
        const int C_IN_TYPE = 3;
        const int C_IN_COMMENT = 4;
        const int C_IN_AREA = 5;
        const int C_IN_COST_CENTER = 6;
        const int C_IN_ITEM = 7;
        const int C_IN_EFF_DATE = 8;
        const int C_IN_ANALISYS_DATE = 9;
        const int C_IN_REFERENCE = 10;
        const int C_IN_REF_DATE = 11;
        const int C_IN_EXP_DATE = 12;
        const int C_IN_DEBIT = 13;
        const int C_IN_CREDIT = 14;
        const int C_IN_BALANCE = 15;
        const int C_IN_BRANCH = 16;

        const int C_OUT_STAT = 1;
        const int C_OUT_CIA = 2;
        const int C_OUT_DESC_AREA = 3;
        const int C_OUT_BRAND = 4;
        const int C_OUT_EERR = 5;
        const int C_OUT_DET_EERR = 6;
        const int C_OUT_ACCT_NUM = 7;
        const int C_OUT_ACCT_DESC = 8;
        const int C_OUT_MONTH = 9;
        const int C_OUT_DATE = 10;
        const int C_OUT_COMPTE = 11;
        const int C_OUT_TYPE = 12;
        const int C_OUT_COMMENT = 13;
        const int C_OUT_AREA = 14;
        const int C_OUT_COST_CENT = 15;
        const int C_OUT_ITEM = 16;
        const int C_OUT_ITEM_DESC = 17;
        const int C_OUT_EFF_DATE = 18;
        const int C_OUT_ANALYSIS_DATE = 19;
        const int C_OUT_REF = 20;
        const int C_OUT_REF_DATE = 21;
        const int C_OUT_EXP_DATE = 22;
        const int C_OUT_DEBIT = 23;
        const int C_OUT_CREDIT = 24;
        const int C_OUT_BALANCE = 25;
        const int C_OUT_BRANCH = 26;
        const string C_COL_DEBIT = "W";
        const string C_COL_CREDIT = "X";

        string[] months ={ "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

        const string C_STR_IN_HEAD = "RCL SUDAMERICANA SOCIEDAD ANONIMA";
        const string C_STR_IN_ACCOUNT = "Cuenta Contable";
        char C_COL_SEPARATOR = ';';

        const string C_ERR_MSG_FILE_FMT_ERR = "File format incorrect";

        const string C_DATA_STATUS = "REAL";
        StreamReader in_fd;

        public StringBuilder readXls(string file, EERRDataAndMethods eerr, XSSFWorkbook twb, bool applyRate, Double rate)
        {
            StringBuilder retVal = new StringBuilder("");
            HSSFWorkbook wb;
            wb = new HSSFWorkbook(new FileStream(file, FileMode.Open));

            ISheet sheet = wb.GetSheetAt(0);
            IRow r = sheet.GetRow(0);
            ICell c = r.GetCell(0);
            string company = c.StringCellValue;
            XSSFSheet sh = (XSSFSheet)twb.GetSheet("Estado resultado");

            string acct = "";
            string acctDesc = "";
            for (int i = 1; i < sheet.LastRowNum; i++)
            {
                r = sheet.GetRow(i);
                if (r != null)
                {
                    c = r.GetCell(0);
                    if (c != null)
                    {
                        
                        if ((c.StringCellValue).StartsWith(C_STR_IN_ACCOUNT))
                        {
                            acct = c.StringCellValue;
                            acct = acct.Substring(C_STR_IN_ACCOUNT.Length).Trim();
                            int pos = acct.IndexOf(' ');
                            acctDesc = acct.Substring(pos + 1).Trim();
                            acct = acct.Substring(0,pos).Trim();
                            Console.WriteLine("Account: " + acct + " " + acctDesc);
                        }
                        else if (acct.Length > 0 && r.LastCellNum >= 16)
                        {
                            
                            //"Estado" 1,"Empresa" 2,"Agrupacion" 3,"Marca," 4,"EERR" 5,"Detalle EERR" 6,"Cuenta" 7,"Desc Cuenta" 8,
                            //"Mes" 9,"Fecha" 10,"# Compte" 11,"Tipo;Glosa" 12,"Area" 13,"C.Costo" 14,"Item" 15,"Desc Item" 16, "F.Efec" 17,
                            //"Analisis" 18,"Refer" 19,"Fch Ref" 20,"Fch Vto" 21,"DEBE" 22,"HABER" 23,"SALDO" 24,"Sucursal" 25
                            string s = "";
                            XSSFRow row = (XSSFRow)sh.CreateRow(sh.LastRowNum+1);

                            XSSFCell cell = (XSSFCell)row.CreateCell(C_OUT_STAT - 1);
                            cell.SetCellValue(C_DATA_STATUS);
                            cell = (XSSFCell)row.CreateCell(C_OUT_CIA - 1);
                            cell.SetCellValue(company);

                            c = r.GetCell(C_IN_AREA - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_DESC_AREA - 1);
                            cell.SetCellValue(eerr.getArea(c.ToString()));
                            cell = (XSSFCell)row.CreateCell(C_OUT_BRAND - 1);
                            cell.SetCellValue(eerr.getBrand(c.ToString()));
                            cell = (XSSFCell)row.CreateCell(C_OUT_DET_EERR - 1);
                            cell.SetCellValue(eerr.getLinea(acct));
                            cell = (XSSFCell)row.CreateCell(C_OUT_ACCT_NUM - 1);
                            cell.SetCellValue(acct);
                            cell = (XSSFCell)row.CreateCell(C_OUT_ACCT_DESC - 1);
                            cell.SetCellValue(acctDesc);

                            c = r.GetCell(C_IN_DATE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_DATE - 1);
                            cell.SetCellValue(c.ToString());

                            string sdate = c.ToString();
                            if (sdate.Length == 10)
                            {
                                short m = 0;
                                if (Int16.TryParse(sdate.Substring(3, 2), out m))
                                {
                                    cell = (XSSFCell)row.CreateCell(C_OUT_MONTH - 1);
                                    cell.SetCellValue(months[m - 1]);
                                }
                            }

                            c = r.GetCell(C_IN_COMPTE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_COMPTE - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_TYPE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_TYPE - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_COMMENT - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_COMMENT - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_AREA - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_AREA - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_COST_CENTER - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_COST_CENT - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_ITEM - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_ITEM - 1);
                            cell.SetCellValue(c.ToString());
                            cell = (XSSFCell)row.CreateCell(C_OUT_ITEM_DESC - 1);
                            cell.SetCellValue(eerr.getItem(c.ToString()));

                            c = r.GetCell(C_IN_EFF_DATE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_EFF_DATE - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_ANALISYS_DATE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_ANALYSIS_DATE - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_REFERENCE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_REF - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_REF_DATE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_REF_DATE - 1);
                            cell.SetCellValue(c.ToString());

                            c = r.GetCell(C_IN_EXP_DATE - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_EXP_DATE - 1);
                            cell.SetCellValue(c.ToString());

                            XSSFCell deb = null;
                            short doubleFormat = HSSFDataFormat.GetBuiltinFormat("#,##0");  //wb.CreateDataFormat().GetFormat("#,##0");
                            double v = 0;
                            c = r.GetCell(C_IN_DEBIT - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_DEBIT - 1);
                            deb = cell;
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                {
                                    //cell = (XSSFCell)row.CreateCell(C_OUT_DEBIT - 1);
                                    cell.SetCellValue((applyRate?rate:1)*v);
                                    cell.SetCellType(CellType.Numeric);
                                    cell.CellStyle.DataFormat = doubleFormat;
                                    
                                }
                            }

                            XSSFCell cred = null;
                            c = r.GetCell(C_IN_CREDIT - 1);
                            cell = (XSSFCell)row.CreateCell(C_OUT_CREDIT - 1);
                            cred = cell;
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                {
                                    //cell = (XSSFCell)row.CreateCell(C_OUT_CREDIT - 1);
                                    cell.SetCellValue((applyRate ? rate : 1) * v);
                                    cell.SetCellType(CellType.Numeric);
                                    cell.CellStyle.DataFormat = doubleFormat;
                                }
                            }
                            c = r.GetCell(C_IN_BALANCE - 1);
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                {
                                    cell = (XSSFCell)row.CreateCell(C_OUT_BALANCE - 1);
                                    cell.SetCellValue((applyRate ? rate : 1) * v);
                                    cell.SetCellType(CellType.Formula);
                                    cell.SetCellFormula(String.Format("{0}{1}-{2}{3}", C_COL_DEBIT, cell.Row.RowNum+1, C_COL_CREDIT, cell.Row.RowNum+1));
                                    cell.CellStyle.DataFormat = doubleFormat;
                                }
                            }
                            c = r.GetCell(C_IN_BRANCH - 1);
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s))
                                {
                                    cell = (XSSFCell)row.CreateCell(C_OUT_BRANCH - 1);
                                    cell.SetCellValue(eerr.getSucursal(s));
                                }
                            }
                        }
                    }

                }
            }
            return retVal;
        }

    }
}
