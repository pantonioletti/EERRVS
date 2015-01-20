using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Windows.Forms;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;


namespace EstadoResultadoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WEstadoResultado : Window
    {
        private EERRDataAndMethods eerrLib;

        public WEstadoResultado()
        {
            InitializeComponent();
            string[] cmdLn = Environment.GetCommandLineArgs();
            if (cmdLn.Length < 2)
                System.Windows.Forms.MessageBox.Show("El aplicativo require archivo de configuración (.ini).");
            else
                eerrLib = new EERRDataAndMethods(cmdLn[1]);

        }
        private void mnItem_Click(object sender, RoutedEventArgs e)
        {
            ParamMaint itemMaint = new ParamMaint(Constants.INV_ITEMS,eerrLib);
            itemMaint.Title = "Mantención de Items";
            itemMaint.Show();
        }

        private void btnPathIn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fldIn = new FolderBrowserDialog();
            fldIn.SelectedPath = "C:\\dev\\projects";
            fldIn.ShowDialog();
            //PathIn.Clear();
            ListInputFiles.Items.Clear();
            PathIn.Text = fldIn.SelectedPath;
            try
            {
                IEnumerable<string> files = Directory.EnumerateFiles(PathIn.Text);//, "*.csv,*.xls");
                IEnumerator<string> enFiles = files.GetEnumerator();
                while (enFiles.MoveNext())
                {
                    string fName = enFiles.Current;
                    if (fName.EndsWith(".csv")||fName.EndsWith(".xls"))
                        ListInputFiles.Items.Add(fName.Replace((PathIn.Text).Insert((PathOut.Text).Length, "\\"), ""));
                }
            }
            catch (Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message);
            }
        }
        private void btnPathOut_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fldOut = new FolderBrowserDialog();
            fldOut.SelectedPath = "C:\\dev\\projects";
            fldOut.ShowDialog();
            PathOut.Text = fldOut.SelectedPath;
        }

        private void btnProc_Click(object sender, RoutedEventArgs e)
        {
            if (PathIn.Text.Length == 0 || ListInputFiles.SelectedItems.Count == 0)
                System.Windows.MessageBox.Show("Debe haber archivos de entrada seleccionados");
            else if (PathOut.Text.Length == 0)
                System.Windows.MessageBox.Show("Debe seleccionar carpeta e salida");
            else
            {
                StreamReader sr;
                StringBuilder s = new StringBuilder();
                s.Append(PathOut.Text);
                s.Append("\\eerr_");
                s.Append(System.DateTime.Now.Ticks.ToString());
                string outPath = s.ToString();
                StreamWriter log = new StreamWriter(s.ToString() + ".log");
                SLDocument xlDoc = new SLDocument(); //s.ToString() + ".xlsx","Estado resultado");

                xlDoc.AddWorksheet("Estado resultado");
                xlDoc.SelectWorksheet("Estado resultado");
                string[] headers =new string[]{ "Estado","Empresa","Agrupacion","Marca","EERR","Detalle EERR","Cuenta","Desc Cuenta","Mes","Fecha","# Compte","Tipo", "Glosa","Area","C.Costo","Item","Desc Item","F.Efec","Analisis","Refer","Fch Ref","Fch Vto","DEBE","HABER","SALDO","Sucursal"};
                int col = 1;
                foreach(string h in headers)
                {
                    xlDoc.SetCellValue(1, col++, h);
                }
                //xlDoc.Save();
                
                s.Append(".csv");
                StreamWriter sw = new StreamWriter(s.ToString());
                PathOut.Text = s.ToString();
                StringBuilder sb = new StringBuilder();
                System.Collections.IEnumerator idxEnum = ListInputFiles.SelectedItems.GetEnumerator();
                //bool headers = false;
                EERRCsvRW csvrw = new EERRCsvRW();
                while (idxEnum.MoveNext())
                {
                    string inFile = (string)idxEnum.Current;
                    if (inFile.EndsWith(".xls"))
                        s = csvrw.readXls(inFile, eerrLib, xlDoc);
                    else
                        s = csvrw.readCsv(inFile, eerrLib, xlDoc);
                    sw.WriteLine(s.ToString());
                }
                sw.Close();
                xlDoc.SaveAs(outPath + ".xlsx");
                //xlDoc.Save();
                //eerrLib.convertCSV2Xlsx(s.ToString(), log);
                log.Close();
                System.Windows.MessageBox.Show("Proceso terminado");
            }
        }
        
    }
}
