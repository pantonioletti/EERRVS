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
        public ParamMaint(string param)
        {
            InitializeComponent();
            curParam = param;
        }
    }
}
