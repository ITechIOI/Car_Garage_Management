using Gara_Management.DTO;
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
    /// Interaction logic for itCustomer.xaml
    /// </summary>
    public partial class itCustomer : UserControl
    {
        Customer customer;
        string gara;
        public itCustomer(Customer customer, string gara)
        {
            InitializeComponent();
            this.customer = customer;
            this.gara = gara;
        }

        // Hiển thị bảng thông tin khách hàng
        private void bd_customer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdCustomer crdCustomer = new crdCustomer(customer, gara);
            crdCustomer.ShowDialog();   
        }
    }
}
