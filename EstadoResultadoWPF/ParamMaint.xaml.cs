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
using System.Windows.Shapes;

namespace EstadoResultadoWPF
{
    /// <summary>
    /// Interaction logic for ParamMaint.xaml
    /// </summary>
    public partial class ParamMaint : Window
    {
        private string curParam;
        private EERRDataAndMethods eerr;

        public ParamMaint(string param, EERRDataAndMethods eerr)
        {
            InitializeComponent();
            curParam = param;
            this.eerr = eerr;
            initialize();
        }

        private void initialize()
        {
            if (curParam.Equals(Constants.INV_ITEMS))
            {
                
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "Cod";
                c1.Binding = new Binding("Cod");
                c1.Width = 110;
                dgParamsData.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Item";
                c2.Width = 110;
                c2.Binding = new Binding("Item");
                dgParamsData.Columns.Add(c2);

                dgParamsData.Items.Add(new string[2] { "6303", "Estacionamientos" });
                dgParamsData.Items.Add(new string[2] { "6305", "lxhADSLHa" });
                dgParamsData.Items.Add(new string[2] { "6333", "dcksahkah" });

                 

            }
            else if (curParam.Equals(Constants.INV_AREAS))
            {

            }
            else if (curParam.Equals(Constants.INV_LINEAS))
            {

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
