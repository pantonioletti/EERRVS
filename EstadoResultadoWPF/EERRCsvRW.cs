using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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

        /*input_date=1
input_compte=2
input_type=3
input_comment=4
input_area=5
input_cost_center=6
input_item=7
input_eff_date=8
input_analisys_date=9
input_reference=10
input_ref_date=11
input_exp_date=12
input_debit=13
input_credit=14
input_balance=15
input_branch=16

output_format_01=status
output_format_02=company
output_format_03=desc_area
output_format_04=brand
output_format_05=eerr
output_format_06=det_eerr
output_format_07=acct_num
output_format_08=acct_desc
output_format_09=month
output_format_10=date
output_format_11=compte
output_format_12=type
output_format_13=comment
output_format_14=area
output_format_15=cost_center
output_format_16=item
output_format_17=item_desc
output_format_18=eff_date
output_format_19=analisys_date
output_format_20=reference
output_format_21=ref_date
output_format_22=exp_date
output_format_23=debit
output_format_24=credit
output_format_25=balance
output_format_26=branch*/
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

        const string C_STR_IN_HEAD = "RCL SUDAMERICANA SOCIEDAD ANONIMA";
        const string C_STR_IN_ACCOUNT = "Cuenta Contable";
        char C_COL_SEPARATOR = ';';

        const string C_ERR_MSG_FILE_FMT_ERR = "File format incorrect";

        const string C_DATA_STATUS = "REAL";
        StreamReader in_fd;

        public StringBuilder readXls(string file, EERRDataAndMethods eerr, SLDocument ws)
        {
            StringBuilder retVal = new StringBuilder("");
            HSSFWorkbook wb;
            //FileStream fs = new FileStream(file, FileMode.Open);
            wb = new HSSFWorkbook(new FileStream(file, FileMode.Open));

            ISheet sheet = wb.GetSheetAt(0);
            //if (sheet.LastRowNum > 1)
            IRow r = sheet.GetRow(0);
            ICell c = r.GetCell(0);
            string company = c.StringCellValue;
            ws.SelectWorksheet("Estado resultado");
            ws.InsertRow(2,50);
            int currR = 2;

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
                            ws.SetCellValue(currR, C_OUT_STAT, C_DATA_STATUS);                                 //  1
                            ws.SetCellValue(currR, C_OUT_CIA, company);                                        //  2
                            ws.SetCellValue(currR, C_OUT_DESC_AREA, eerr.getArea(c.ToString()));               //  3
                            c = r.GetCell(C_IN_AREA - 1);
                            ws.SetCellValue(currR, C_OUT_BRAND, eerr.getBrand(c.ToString()));                  //  4
                            ws.SetCellValue(currR, C_OUT_DET_EERR, eerr.getLinea(acct));                       //  6
                            ws.SetCellValue(currR, C_OUT_ACCT_NUM, acct);                                      //  7
                            ws.SetCellValue(currR, C_OUT_ACCT_DESC, acctDesc);                                 //  8
                            c = r.GetCell(C_IN_DATE - 1);
                            ws.SetCellValue(currR, C_OUT_DATE, c.ToString());                                  // 10
                            c = r.GetCell(C_IN_COMPTE - 1);
                            ws.SetCellValue(currR, C_OUT_COMPTE, c.ToString());                                // 11
                            c = r.GetCell(C_IN_TYPE - 1);
                            ws.SetCellValue(currR, C_OUT_TYPE, c.ToString());                                  // 12
                            c = r.GetCell(C_IN_COMMENT - 1);
                            ws.SetCellValue(currR, C_OUT_COMMENT, c.ToString());                               // 13
                            c = r.GetCell(C_IN_AREA - 1);
                            ws.SetCellValue(currR, C_OUT_AREA, c.ToString());                                  // 14
                            c = r.GetCell(C_IN_COST_CENTER - 1);
                            ws.SetCellValue(currR, C_OUT_COST_CENT, c.ToString());                             // 15
                            s = c.ToString();
                            double v = 0;
                            c = r.GetCell(C_IN_DEBIT - 1);
                            if (c != null) {
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                    ws.SetCellValueNumeric(currR, C_OUT_DEBIT, c.ToString());                      // 23
                            }
                            c = r.GetCell(C_IN_CREDIT - 1);
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                    ws.SetCellValueNumeric(currR, C_OUT_CREDIT, c.ToString());                     // 24
                            }
                            c = r.GetCell(C_IN_BALANCE - 1);
                            if (c != null)
                            {
                                s = c.ToString();
                                if (!string.IsNullOrEmpty(s) && Double.TryParse(s, out v))
                                    ws.SetCellValueNumeric(currR, C_OUT_BALANCE, c.ToString());                    // 25
                            }

                            if (++currR % 50 == 1)
                                ws.InsertRow(currR, 50);
                        }
                    }

                }
            }
            //ws.Save();
            return retVal;
        }
        public StringBuilder readCsv(string file, EERRDataAndMethods eerr, SLDocument ws)
        {
            StringBuilder retVal = new StringBuilder();
            string errMsg = "";
            string acct = "";
            string descAcct = "";
            string company = "";
            int row = 0;
            try
            {
                in_fd = new StreamReader(File.Open(file, FileMode.Open));
                string line = in_fd.ReadLine();
                string[] splitedline;
                if (line.Length > 0)
                {
                    splitedline = line.Split(C_COL_SEPARATOR);
                    company = splitedline[0].Trim();
                }
                else
                {
                    errMsg = C_ERR_MSG_FILE_FMT_ERR;
                    retVal = null; ;

                }

                if (retVal == null)
                    return retVal;

                while (in_fd.Peek() >= 0)
                {
                    line = in_fd.ReadLine();
                    if (line.StartsWith(C_STR_IN_ACCOUNT))
                    {
                        line = line.Substring(C_STR_IN_ACCOUNT.Length);
                        splitedline = (line.Trim()).Split(' ');
                        acct = splitedline[0].Trim();
                        descAcct = splitedline[1].Trim();
                    }
                    else
                        if (!acct.Equals(""))
                        {
                            splitedline = line.Split(C_COL_SEPARATOR);
                            if (splitedline[0].Trim().Length > 0)
                            {
                                if (splitedline.Length >= 16)
                                {

                                    /*
                                        0=date
                                        1=compte
                                        2=type
                                        3=comment
                                        4=area
                                        5=cost_center
                                        6=item
                                        7=eff_date
                                        8=analisys_date
                                        9=reference
                                        10=ref_date
                                        11=exp_date
                                        12=debit
                                        13=credit
                                        14=balance
                                        15=branch
                                     */
                                    int col = 1;
                                    ws.SetCellValue(++row, col++, C_DATA_STATUS);
                                    ws.SetCellValue(row, col++, company);
                                    StringBuilder sb = new StringBuilder(C_DATA_STATUS); // Status
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(company); // Company
                                    sb.Append(C_COL_SEPARATOR);
                                    string code = splitedline[C_IN_AREA];
                                    string data = eerr.getArea(code);
                                    sb.Append(data); // Area descrip
                                    ws.SetCellValue(row, col++, data);
                                    sb.Append(C_COL_SEPARATOR);
                                    data = eerr.getBrand(code);
                                    sb.Append(data); // Brand
                                    ws.SetCellValue(row, col++, data);
                                    sb.Append(C_COL_SEPARATOR);
                                    data = eerr.getLinea(acct);
                                    if (data == null)
                                        data = "Cuenta no asociada a linea de EERR";
                                    sb.Append(data); // Get EERR det
                                    ws.SetCellValue(row, col++, data);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(acct); // Account
                                    ws.SetCellValue(row, col++, acct);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(descAcct); // Account descrip
                                    ws.SetCellValue(row, col++, descAcct);
                                    sb.Append(C_COL_SEPARATOR);
                                    data = splitedline[0];
                                    int x = data.IndexOf('/');
                                    if (x != -1)
                                    {
                                        int y = data.IndexOf('/', x + 1);
                                        if (y != -1)
                                        {
                                            data = data.Substring(x+1, y-x-1);
                                            try
                                            {
                                                x = int.Parse(data);
                                                data = Constants.ARR_MONTH[x - 1];
                                            }
                                            catch (Exception pe)
                                            {
                                                data = "Formato de fecha erroneo";
                                            }
                                        }
                                    }
                                    sb.Append(data); // Month
                                    ws.SetCellValue(row, col++, data);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[0]); // Date
                                    ws.SetCellValue(row, col++, splitedline[0]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[1]); // Compte
                                    ws.SetCellValue(row, col++, splitedline[1]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[2]); // Type
                                    ws.SetCellValue(row, col++, splitedline[2]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[3]); // Comment
                                    ws.SetCellValue(row, col++, splitedline[3]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[4]); // Area code
                                    ws.SetCellValue(row, col++, splitedline[4]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[5]); // Cost center
                                    ws.SetCellValue(row, col++, splitedline[5]);
                                    sb.Append(C_COL_SEPARATOR);
                                    ws.SetCellValue(row, col++, splitedline[6]);
                                    sb.Append(eerr.getItem(splitedline[6])); // Item
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[7]); // Eff date
                                    ws.SetCellValue(row, col++, splitedline[7]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[8]); // Analysis date
                                    ws.SetCellValue(row, col++, splitedline[8]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[9]); // Reference
                                    ws.SetCellValue(row, col++, splitedline[9]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[10]); // Ref date
                                    ws.SetCellValue(row, col++, splitedline[10]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[11]); // Exp date
                                    ws.SetCellValue(row, col++, splitedline[11]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[12]); // Debit
                                    ws.SetCellValue(row, col++, splitedline[12]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[13]); // Credit
                                    ws.SetCellValue(row, col++, splitedline[13]);
                                    sb.Append(C_COL_SEPARATOR);
                                    sb.Append(splitedline[14]); // Balance
                                    ws.SetCellValue(row, col++, splitedline[14]);
                                    sb.Append(C_COL_SEPARATOR); 
                                    sb.Append(splitedline[15]); // Branch
                                    ws.SetCellValue(row, col++, splitedline[15]);
                                    sb.Append('\n');
                                    retVal.Append(sb);
                                }
                            }
                        }
                
                }
                in_fd.Close();
                Console.WriteLine("File closed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return retVal;
        }

    }
}
