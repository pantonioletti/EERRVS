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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms;
using System.IO;


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
            ParamMaint itemMaint = new ParamMaint();
            itemMaint.Title = "Mantención de Items";
            itemMaint.Show();
        }

        private void btnPathIn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fldIn = new FolderBrowserDialog();
            fldIn.SelectedPath = "C:\\dev\\projects";
            fldIn.ShowDialog();
            PathIn.Text = fldIn.SelectedPath;
            try
            {
                IEnumerable<string> files = Directory.EnumerateFiles(PathIn.Text, "*.csv");
                IEnumerator<string> enFiles = files.GetEnumerator();
                while (enFiles.MoveNext())
                {
                    string fName = enFiles.Current;
                    if (fName.EndsWith(".csv"))
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
                StreamWriter log = new StreamWriter(s.ToString() + ".log");
                s.Append(".csv");
                StreamWriter sw = new StreamWriter(s.ToString());
                PathOut.Text = s.ToString();
                StringBuilder sb = new StringBuilder();
                System.Collections.IEnumerator idxEnum = ListInputFiles.SelectedItems.GetEnumerator();
                bool headers = false;
                while (idxEnum.MoveNext())
                {
                    string inFile = PathOut.Text + "\\" + (string)idxEnum.Current;
                    //string newInFile = eerrLib.convertXls2Xlsx(inFile, log);
                    //if (newInFile == null)
                    //    log.WriteLine("Error processing " + inFile + "\nMoving to next file.");
                    //else
                    //{
                    InputExcelReader ier = new InputExcelReader(inFile);
                    if (!headers)
                        ier.printHeaders(sw);
                    for (int i = 0; i < ier.sheetsCount(); i++)
                        ier.readSheet(i, sw, log);
                    //}
                }
                sw.Close();
                //eerrLib.convertCSV2Xlsx(s.ToString(), log);
                log.Close();
                System.Windows.MessageBox.Show("Proceso terminado");
            }
        }
        
    }
}
