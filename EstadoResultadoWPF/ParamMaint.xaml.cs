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
                
                dgParamsData.Width = 360;
                this.Width = 390;
                
                List<ItemData> lItem = eerr.getItems();
                dgParamsData.ItemsSource = lItem;

                 

            }
            else if (curParam.Equals(Constants.INV_AREAS))
            {
                /*DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Header = "Area";
                c1.Binding = new Binding("Area");
                c1.Width = 110;
                dgParamsData.Columns.Add(c1);
                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Header = "Marca";
                c2.Width = 110;
                c2.Binding = new Binding("Marca");
                dgParamsData.Columns.Add(c2);
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Header = "Agrupación";
                c3.Width = 110;
                c3.Binding = new Binding("Agrupacion");
                dgParamsData.Columns.Add(c3);
                */
                dgParamsData.Width = 360;
                this.Width = 390;
                
                List<AreaData> lArea = eerr.getAreas();
                dgParamsData.ItemsSource = lArea;

            }
            else if (curParam.Equals(Constants.INV_LINEAS))
            {

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
        	if (curParam.Equals(Constants.INV_AREAS))
        	{
        		dgParamsData.Items.MoveCurrentToFirst();
        		
        		while(!dgParamsData.Items.IsCurrentAfterLast)
        		{
        			AreaData area = (AreaData)dgParamsData.Items.CurrentItem;
        			dgParamsData.Items.MoveCurrentToNext();
        			string[] recArea = eerr.getArea(area.Area);
        			if (recArea == null || !area.Marca.Equals(recArea[0]) || !area.Agrupacion.Equals(recArea[1])) 
        					eerr.setArea(area);
        		}
        	}
        	else if (curParam.Equals(Constants.INV_ITEMS))
        	{
        		dgParamsData.Items.MoveCurrentToFirst();
        		
        		while(!dgParamsData.Items.IsCurrentAfterLast)
        		{
        			ItemData item = (ItemData)dgParamsData.Items.CurrentItem;
        			dgParamsData.Items.MoveCurrentToNext();
        			
        		}
        	}
        	else if (curParam.Equals(Constants.INV_LINEAS))
        	{
        		
        	}
        }
    }
}
