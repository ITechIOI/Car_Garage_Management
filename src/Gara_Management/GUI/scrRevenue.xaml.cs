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

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrRevenue.xaml
    /// </summary>
    public partial class scrRevenue : UserControl
    {
        public EventHandler changeMoneyScr;
        public scrRevenue()
        {
            InitializeComponent();
        }
        private void bt_money_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeMoneyScr?.Invoke(this, EventArgs.Empty);
        }
        private void bt_revenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardRevenue revenue = new cardRevenue();
            revenue.Show();
        }

        private void bt_inventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardInventoryReport inventory=new cardInventoryReport();
            inventory.Show();
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }

        }

       
    }
}
