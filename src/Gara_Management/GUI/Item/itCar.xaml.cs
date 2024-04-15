using Gara_Management.GUI.Card;
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

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itCar.xaml
    /// </summary>
    public partial class itCar : UserControl
    {

        public itCar()
        {
            InitializeComponent();
        }
        
        // phiếu thu tiền
        private void bd_paymentReceipt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdReceipt crdReceipt = new crdReceipt();
            crdReceipt.ShowDialog();
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair();
            crdRepair.ShowDialog(); 
        }
    }
}
