using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EstadoResultado
{
    public partial class DataMaint : Form
    {
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private SQLiteDataReader dreader;
        private ArrayList eerr;
        private Dictionary<string, string> items;
        private Dictionary<string, string[]> areas;
        private string action;

        public DataMaint(string invoker, EERRDataAndMethods whatever)
        {
            action = invoker;
            InitializeComponent();
            dgParams.Columns.Clear();
            if (invoker.Equals(Constants.INV_ITEMS))
            {
                DataGridViewColumn code = new DataGridViewTextBoxColumn();
                code.ReadOnly = false;
                code.HeaderText = "ITEM";
                DataGridViewColumn descr = new DataGridViewTextBoxColumn();
                descr.HeaderText = "DESCRIPCION";
                dgParams.Columns.AddRange(new DataGridViewColumn[] {code,descr});
                items = whatever.getItems();
                IEnumerator kItems = items.Keys.GetEnumerator();
                while (kItems.MoveNext())
                {
                    string key = (string)kItems.Current;
                    string value = (string)items[key];
                    dgParams.Rows.Add(key, value);

                }
            }
            else if (invoker.Equals(Constants.INV_AREAS))
            {
                DataGridViewColumn code = new DataGridViewTextBoxColumn();
                code.ReadOnly = true;
                code.HeaderText = "AREA";
                DataGridViewColumn marca = new DataGridViewTextBoxColumn();
                marca.HeaderText = "MARCA";
                DataGridViewColumn group = new DataGridViewTextBoxColumn();
                group.HeaderText = "AGRUPACION";
                dgParams.Columns.AddRange(new DataGridViewColumn[] { code, marca, group });
                areas = whatever.getAreas();
                IEnumerator kAreas = areas.Keys.GetEnumerator();
                while (kAreas.MoveNext())
                {
                    string key = (string)kAreas.Current;
                    string[] value = (string[])areas[key];
                    dgParams.Rows.Add(key, value[0], value[1]);

                }
            }
            else if (invoker.Equals(Constants.INV_LINEAS))
            {
                DataGridViewColumn code = new DataGridViewTextBoxColumn();
                code.ReadOnly = true;
                code.HeaderText = "PREFIJO";
                DataGridViewColumn descr = new DataGridViewTextBoxColumn();
                descr.HeaderText = "LINEA";
                dgParams.Columns.AddRange(new DataGridViewColumn[] { code, descr });
                eerr = whatever.getLineas();

                for(int i=0; i < eerr.Count; i++)
                {
                    string[] keyValue = (string[])eerr[i];
                    dgParams.Rows.Add(keyValue[0], keyValue[1]);

                }

            }
            dgParams.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Que tat?");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (action.Equals(Constants.INV_ITEMS))
            {
                //items.Clear();
                DataGridViewRowCollection dgrc = dgParams.Rows;
                for (int i = 0; i < dgrc.Count;i++)
                {
                    string cd = (string)dgrc[i].Cells[0].Value;
                }
            }
        }
    }
}
