using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Data;
using System.Diagnostics;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


namespace EstadoResultadoWPF
{
    public class EERRDataAndMethods
    {
        private SQLiteConnection sqlite;
        private Dictionary<string, string> items = new Dictionary<string, string>();
        private Dictionary<string, string[]> area = new Dictionary<string, string[]>();
        Dictionary<string, string> confKeyValuePairs = new Dictionary<string, string>();
        //private ArrayList eerr = new ArrayList();
        private Object[] arrEERR;

        public EERRDataAndMethods(string confFile)
        {
            loadConf(confFile);
        }

        private void loadConf(string confFile)
        {
            //Dictionary<string, string> confKeyValuePairs = new Dictionary<string, string>();
            confKeyValuePairs.Clear();
            string db_file = "";
            // Load conf file
            try
            {
                StreamReader sr = new StreamReader(confFile);

                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    if (line.Contains("="))
                    {
                        string[] keyValue = line.Split('=');
                        confKeyValuePairs.Add(keyValue[0].Trim(), keyValue[1].Trim());
                    }
                }
                sr.Close();
                if (!confKeyValuePairs.TryGetValue(Constants.DBFILE, out db_file))
                {
                    System.Windows.Forms.MessageBox.Show("No se encontraron datos de EERR");
                    return;
                }
            }
            catch (FileNotFoundException ex)
            {

                System.Windows.Forms.MessageBox.Show("El aplicativo require archivo de configuración (.ini).");
                System.Console.WriteLine(ex.Message);
            }
            sqlite = new SQLiteConnection("Data Source=" + db_file);
            SQLiteDataAdapter ad;
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db

                // First load ITEMS
                cmd = sqlite.CreateCommand();
                cmd.CommandText = Constants.QUERY_ITEMS;
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
                DataRow[] rows = dt.Select();
                for (int i = 0; i < rows.Length; i++)
                {
                    items.Add((string)rows[i][Constants.ITEMS_1], (string)rows[i][Constants.ITEMS_2]);
                }
                ad.Dispose();
                dt.Dispose();
                rows = null;

                // Second load AREA
                cmd = sqlite.CreateCommand();
                cmd.CommandText = Constants.QUERY_AREA;
                ad = new SQLiteDataAdapter(cmd);
                dt = new System.Data.DataTable();
                ad.Fill(dt); //fill the datasource
                rows = dt.Select();
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] marca_agrup = new string[2];
                    marca_agrup[0] = (string)rows[i][Constants.AREA_2];
                    marca_agrup[1] = (string)rows[i][Constants.AREA_3];
                    area.Add((string)rows[i][Constants.AREA_1], marca_agrup);
                }
                ad.Dispose();

                // Third load EERR
                ArrayList eerr = new ArrayList();
                cmd = sqlite.CreateCommand();
                cmd.CommandText = Constants.QUERY_EERR;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                dt = new System.Data.DataTable();
                ad.Fill(dt); //fill the datasource
                rows = dt.Select();
                for (int i = 0; i < rows.Length; i++)
                {
                    string[] prefix_desc = new string[2];
                    prefix_desc[0] = (string)rows[i][Constants.EERR_1];
                    prefix_desc[1] = (string)rows[i][Constants.EERR_2];
                    eerr.Add(prefix_desc);
                }
                arrEERR = eerr.ToArray();
                ad.Dispose();
            }
            catch (SQLiteException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            sqlite.Close();
        }

        public string getIniParam(string key)
        {
            string retVal = null;

            if (!string.IsNullOrEmpty(key) && confKeyValuePairs.ContainsKey(key))
                retVal = confKeyValuePairs[key];
            return retVal;
        }
 
        public Dictionary<string, string> getItems()
        {
            return items;
        }

        public Dictionary<string, string[]> getAreas()
        {
            return area;
        }

        public string getArea(string code)
        {
            string retVal = "N/A";
            if (area.ContainsKey(code))
            {
                retVal = area[code][1];
            }
            return retVal;
        }

        public string getBrand(string code)
        {
            string retVal = "N/A";
            if (!string.IsNullOrEmpty(code) && area.ContainsKey(code))
            {
                retVal = area[code][0];
            }
            return retVal;
        }

        public Object[] getLineas()
        {
            return arrEERR;
        }

        public string getLinea(string acct)
        {
            string retVal = null;
            for (int i = 0; i < arrEERR.Length; i++)
                if (acct.StartsWith(((string[])arrEERR[i])[0]))
                {
                    retVal = ((string[])arrEERR[i])[1];
                    break;
                }
            return retVal;
        }

        public string getItem(string code)
        {
            string retVal = "N/A";
            if (!string.IsNullOrEmpty(code) && items.ContainsKey(code))
            {
                retVal = items[code];
            }
            return retVal;
        }

        public SpreadsheetDocument buildSpreadsheet(string filename)
        {
            SpreadsheetDocument xlDoc;
            try
            {
                xlDoc = SpreadsheetDocument.Create(filename + ".xlsx", SpreadsheetDocumentType.Workbook);
                WorkbookPart wbp = xlDoc.AddWorkbookPart();
                wbp.Workbook = new Workbook();

                wbp.AddNewPart<WorksheetPart>();
                //wbp.WorksheetParts.First<WorksheetPart>();//.Worksheet = new Worksheet();
                //wsp.Worksheet = new Worksheet(new SheetData());

                Sheets shts = wbp.Workbook.AppendChild<Sheets>(new Sheets());
                //Sheet sht = new Sheet() { Id = wbp.GetIdOfPart(wsp), SheetId = 1, Name = "Estado resultado"};
                //shts.Append(sht);
                wbp.Workbook.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                xlDoc = null;
            }
            return xlDoc;
        }


    }
}