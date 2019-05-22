using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstadoResultado
{
    public partial class Main : Form
    {
        private EERRDataAndMethods eerrLib;
        
        public Main()
        {
            string[] cmdLn = Environment.GetCommandLineArgs();
            if (cmdLn.Length < 2)
                System.Windows.Forms.MessageBox.Show("El aplicativo require archivo de configuración (.ini).");
            else
                eerrLib = new EERRDataAndMethods(cmdLn[1]);
            InitializeComponent();
            
        }


        private void btInFolder_Click(object sender, EventArgs e)
        {
            dgFolderSelect.ShowDialog();
            lbInputPath.Text = dgFolderSelect.SelectedPath;
            try
            {
                IEnumerable<string> files = Directory.EnumerateFiles(lbInputPath.Text,"*.xls");
                IEnumerator<string> enFiles = files.GetEnumerator();
                while (enFiles.MoveNext())
                {
                    string fName = enFiles.Current;
                    if (fName.EndsWith(".xls"))
                        lstInputFiles.Items.Add(fName.Replace((lbInputPath.Text).Insert((lbInputPath.Text).Length,"\\"), ""));
                }
            }
            catch (Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message);
            }

        }

        private void btOutputFolder_Click(object sender, EventArgs e)
        {
            dgFolderSelect.ShowDialog();
            lbOutputPath.Text = dgFolderSelect.SelectedPath;

        }


        private void btProcesar_Click(object sender, EventArgs e)
        {
            if (lbInputPath.Text.Length == 0 || lstInputFiles.SelectedItems.Count == 0)
                MessageBox.Show("Debe haber archivos de entrada seleccionados");
            else if (lbOutputPath.Text.Length == 0)
                MessageBox.Show("Debe seleccionar carpeta e salida");
            else{
                StreamReader sr;
                StringBuilder s = new StringBuilder();
                s.Append(lbOutputPath.Text);
                s.Append("\\eerr_");
                s.Append(System.DateTime.Now.Ticks.ToString());
                StreamWriter log = new StreamWriter(s.ToString() + ".log");
                s.Append(".csv");
                StreamWriter sw = new StreamWriter(s.ToString());
                lbOutputFile.Text = s.ToString();
                StringBuilder sb = new StringBuilder();
                IEnumerator idxEnum = lstInputFiles.SelectedItems.GetEnumerator();
                bool headers = false;
                while (idxEnum.MoveNext())
                {
                    string inFile = lbInputPath.Text + "\\"+ (string)idxEnum.Current;
                    string newInFile = eerrLib.convertXls2Xlsx(inFile, log);
                    if (newInFile == null)
                        log.WriteLine("Error processing " + inFile + "\nMoving to next file.");
                    else
                    {
                        InputExcelReader ier = new InputExcelReader(newInFile);
                        if (!headers)
                            ier.printHeaders(sw);
                        for (int i = 0; i < ier.sheetsCount(); i++)
                            ier.readSheet(i, sw, log);
                    }
                }
                sw.Close();
                eerrLib.convertCSV2Xlsx(s.ToString(), log);
                log.Close();
                MessageBox.Show("Proceso terminado");
            }
        }

        private void manteciónCódigosDeEERRToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataMaint fDMaint = new DataMaint(Constants.INV_ITEMS, eerrLib);
            fDMaint.Show(this);
        }
        private void lstInputFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lineasDeEstadoResultadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DataMaint fDMaint = new DataMaint(Constants.INV_LINEAS, eerrLib);
            fDMaint.Show(this);
        }

        private void areasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataMaint fDMaint = new DataMaint(Constants.INV_AREAS, eerrLib);
            fDMaint.Show(this);
        }

    }
}
