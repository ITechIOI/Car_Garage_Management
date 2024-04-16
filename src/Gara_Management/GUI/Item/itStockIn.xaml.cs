using System;
using Gara_Management.GUI.Card;
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

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itStockIn.xaml
    /// </summary>
    public partial class itStockIn : UserControl
    {
        public itStockIn()
        {
            InitializeComponent();
        }

        private void bd_StockInInfor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn("ma","a");
            crdStockIn.ShowDialog();
        }
    }
}
