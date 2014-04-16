using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadZip;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Collections;


namespace EstadoResultadoWPF
{
    public class InputExcelReader : XlReader
    {
        private const string PROP_FILE_NAME = "EstadoResultado.properties";
        private const string PROP_ITEMS = "items";
        private const string PROP_AREAS = "areas";
        private const string PROP_EERR = "eerr";
        private const string PROP_FMT = "format";

        private const string DATA_DATE = "date";
        private const string DATA_COMPTE = "compte";
        private const string DATA_TYPE = "type";
        private const string DATA_COMMENT = "comment";
        private const string DATA_AREA = "area";
        private const string DATA_COST_CENTER = "cost_center";
        private const string DATA_ITEM = "item";
        private const string DATA_EFF_DATE = "eff_date";
        private const string DATA_ANALISYS_DATE = "analisys_date";
        private const string DATA_REFERENCE = "reference";
        private const string DATA_REF_DATE = "ref_date";
        private const string DATA_EXP_DATE = "exp_date";
        private const string DATA_DEBIT = "debit";
        private const string DATA_CREDIT = "credit";
        private const string DATA_BALANCE = "balance";
        private const string DATA_BRANCH = "branch";
        private const string DATA_ACCT_NUM = "acct_num";
        private const string DATA_ACCT_DESC = "acct_desc";
        private const string DATA_ITEM_DESC = "item_desc";
        private const string DATA_MONTH = "month";
        private const string DATA_STATUS = "status";
        private const string DATA_COMPANY = "company";
        private const string DATA_DESC_AREA = "desc_area";
        private const string DATA_BRAND = "brand";
        private const string DATA_DET_EERR = "det_eerr";
        private const string DATA_EERR = "eerr";
        private const string ACCT_PREFIX = "account_prefix";

        private string acctPrefixValue = "";
        private Hashtable items = new Hashtable();
        private Hashtable areas = new Hashtable();
        private Hashtable eerrs = new Hashtable();
        private Hashtable fmtInput = new Hashtable();
        private Hashtable fmtOutput = new Hashtable();
        private Hashtable fmtHeaders = new Hashtable();
        private string[] ARR_MONTH = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        public InputExcelReader(string inputFile)
            : base(inputFile)
        {
            loadConfData();
        }

        public void printHeaders(StreamWriter outFile)
        {
            try
            {
                string[] sHead = new string[fmtOutput.Count];
                foreach (string item in fmtOutput.Keys)
                    sHead[(int)fmtOutput[item]] = (string)fmtHeaders[item];
                for (int i = 0; i < sHead.Length; i++)
                    outFile.Write(sHead[i] + ";");
                outFile.WriteLine("");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
        }

        public int sheetsCount()
        {
            return mySheets.Length;
        }

        public void readSheet(int sheetIdx, StreamWriter outFile, StreamWriter log)
        {

            SheetData sd = mySheets[sheetIdx].ws.Worksheet.Elements<SheetData>().First();

            string company = ""; // row[0].Trim();
            string status = "REAL";
            string currentAcct = null;
            string currAcctDesc = null;
            string currentEerrDet = null;
            object[] curRow;
            StringBuilder line = new StringBuilder();
            log.WriteLine("Start processing sheet: " + mySheets[sheetIdx].name);
            foreach (Row r in sd.Elements<Row>())
            {
                if (r.GetFirstChild<Cell>().CellReference.InnerText[0] == 'A')
                {
                    curRow = new object[fmtOutput.Count];
                    curRow[(int)fmtOutput[DATA_COMPANY]] = company;
                    curRow[(int)fmtOutput[DATA_ACCT_NUM]] = currentAcct;
                    curRow[(int)fmtOutput[DATA_ACCT_DESC]] = currAcctDesc;
                    curRow[(int)fmtOutput[DATA_STATUS]] = status;
                    curRow[(int)fmtOutput[DATA_DET_EERR]] = currentEerrDet;
                    bool print = true;
                    foreach (Cell c in r.Elements<Cell>())
                    {
                        string value = c.CellValue.InnerText;
                        string col = c.CellReference.InnerText.Substring(0, 1);
                        CellValues dt = c.DataType == null ? CellValues.Number : c.DataType.Value;
                        bool isDate = false;
                        if (dt.Equals(CellValues.SharedString))
                        {
                            value = sst[int.Parse(value)].InnerText;
                        }
                        else if (dt.Equals(CellValues.Number))
                        {
                            if (c.StyleIndex != null)
                            {
                                string fmtId = myFormats[c.StyleIndex].NumberFormatId.InnerText;
                                if (fmtId.Equals("14") || fmtId.Equals("15") || fmtId.Equals("16") || fmtId.Equals("17"))
                                {
                                    value = DateTime.FromOADate(Double.Parse(value)).ToShortDateString();
                                    isDate = true;
                                }
                            }
                        }

                        if (c.CellReference.InnerText.Equals("A1"))
                        {
                            company = value;
                            print = false;
                            break;
                        }
                        else if (col[0] == 'A' && value.Trim().StartsWith(acctPrefixValue))
                        {
                            currentAcct = value.Substring(acctPrefixValue.Length).Trim();
                            int descPos = currentAcct.IndexOf(" ");
                            currAcctDesc = currentAcct.Substring(descPos + 1);
                            currentAcct = currentAcct.Substring(0, currentAcct.Length - (currAcctDesc.Length + 1));
                            int i = 1;
                            for (; i <= currentAcct.Length; i++)
                                if (eerrs.Contains(currentAcct.Substring(0, i)))
                                    break;
                            if (i > currentAcct.Length)
                                currentEerrDet = "N/A";
                            else
                                currentEerrDet = ((string[])eerrs[currentAcct.Substring(0, i)])[1];
                            print = false;
                            break;
                        }
                        else if (((string)fmtInput[col]).Equals(DATA_AREA))
                        {
                            if (areas.Contains(value))
                            {
                                curRow[(int)fmtOutput[DATA_DESC_AREA]] = ((string[])areas[value])[2];
                                curRow[(int)fmtOutput[DATA_BRAND]] = ((string[])areas[value])[1];
                            }
                            else
                            {
                                curRow[(int)fmtOutput[DATA_DESC_AREA]] = "N/A";
                                curRow[(int)fmtOutput[DATA_BRAND]] = "N/A";
                            }
                        }
                        else if(((string)fmtInput[col]).Equals(DATA_DATE) && isDate)
                        {
                            curRow[(int)fmtOutput[DATA_MONTH]] = ARR_MONTH[DateTime.Parse(value).Month-1];
                            curRow[(int)fmtOutput[(string)fmtInput[col]]] = value;

                        }
                        else
                        {
                            //int idx = (int)fmtOutput[fmtInput[col]]
                            curRow[(int)fmtOutput[(string)fmtInput[col]]] = value;
                        }
                    }

                    if (print && curRow[0] != null && curRow[(int)fmtOutput[DATA_ACCT_NUM]] != null)
                    {
                        if (curRow[(int)fmtOutput[DATA_BALANCE]] != null)
                        {
                            double cr = 0;
                            double db = 0;
                            if (curRow[(int)fmtOutput[DATA_CREDIT]] != null)
                                cr = Double.Parse((string)curRow[(int)fmtOutput[DATA_CREDIT]]);
                            if (curRow[(int)fmtOutput[DATA_DEBIT]] != null)
                                db = Double.Parse((string)curRow[(int)fmtOutput[DATA_DEBIT]]);
                            curRow[(int)fmtOutput[DATA_BALANCE]] = (db - cr).ToString();
                        }

                        for (int i = 0; i < curRow.Length; i++)
                        {
                            line.Append(curRow[i] == null ? "" : curRow[i]);
                            line.Append(";");
                        }
                        if (line.ToString().Trim().Length > 0)
                        {
                            outFile.WriteLine(line);
                            line = new StringBuilder();
                        }
                    }
                }
            }
        }

        private void loadPropFile(string fileName, Hashtable ht)
        {
            string s;
            StreamReader sr;

            try
            {
                sr = File.OpenText(fileName);
                do
                {
                    s = (sr.ReadLine()).Trim();
                    if (!s.StartsWith("#") && s.Length > 0)
                    {
                        string[] param = s.Split('=');
                        if (param.Length == 2)
                        {
                            if (ht.ContainsKey(param[0]))
                                ht[param[0]] = param[1];
                            else
                                ht.Add(param[0], param[1]);
                        }
                    }
                }
                while (!sr.EndOfStream);

                sr.Close();

            }
            catch (Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message);
            }
        }

        private void loadKeyedFiles(string fileName, Hashtable ht)
        {
            string s;
            StreamReader sr;

            try
            {
                sr = File.OpenText(fileName);
                do
                {
                    s = (sr.ReadLine()).Trim();
                    if (!s.StartsWith("#") && s.Length > 0)
                    {
                        string[] param = s.Split(';');
                        if (param.Length > 1)
                            ht.Add(param[0], param);
                    }
                }
                while (!sr.EndOfStream);

                sr.Close();

            }
            catch (Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message);
            }
        }

        private void loadConfData()
        {
            Hashtable ht = new Hashtable();
            ht.Add(PROP_ITEMS, "");
            ht.Add(PROP_AREAS, "");
            ht.Add(PROP_EERR, "");
            ht.Add(PROP_FMT, "");

            try
            {
                loadPropFile(PROP_FILE_NAME, ht);
                loadKeyedFiles(string.Concat("rcl\\", ht[PROP_ITEMS]), items);
                loadKeyedFiles(string.Concat("rcl\\", ht[PROP_AREAS]), areas);
                loadKeyedFiles(string.Concat("rcl\\", ht[PROP_EERR]), eerrs);

                Hashtable fProps = new Hashtable();
                loadPropFile(string.Concat("rcl\\", ht[PROP_FMT]), fProps);

                IEnumerator propsEnum = fProps.Keys.GetEnumerator();
                string key;
                string value;
                while (propsEnum.MoveNext())
                {
                    key = (string)propsEnum.Current;
                    value = (string)fProps[key];
                    if (key.StartsWith("output_format_"))
                    {
                        key = key.Substring(14);
                        if (fmtOutput.ContainsKey(key))
                            fmtOutput[key] = int.Parse(value);
                        else
                            fmtOutput.Add(key, int.Parse(value));
                    }
                    else if (key.StartsWith("input_"))
                    {
                        key = key.Substring(6);
                        if (fmtInput.ContainsKey(key))
                            fmtInput[key] = value;
                        else
                            fmtInput.Add(key, value);

                    }
                    else if (key.StartsWith("head_"))
                    {
                        key = key.Substring(5);
                        if (fmtHeaders.ContainsKey(key))
                            fmtHeaders[key] = value;
                        else
                            fmtHeaders.Add(key, value);
                    }
                    else if (key.Equals(ACCT_PREFIX))
                        acctPrefixValue = value;
                }
                key = "";

            }
            catch (Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message);
            }
        }

    }
}
