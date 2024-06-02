using Gara_Management.GUI.Card;
using Gara_Management.GUI.Item;
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
    /// Interaction logic for scrRepairCard.xaml
    /// </summary>
    public partial class scrRepairCard : UserControl
    {
         Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");
        public EventHandler changeToCarsScr;
        string gara; 
        public scrRepairCard(string gara)
        {
            InitializeComponent();
            this.gara = gara;
            LoadListRepair();
        }

        private void bt_car_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToCarsScr?.Invoke(this, EventArgs.Empty);
        }
        private void bd_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color4);
        }

        private void bd_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color3);
        }

        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair();
            crdRepair.ShowDialog(); 
        }

        private void bd_filetr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Visible;
        }
        private void LoadListRepair()
        {
            for (int i = 0; i < 10; i++)
            {
                itRepairCard it = new itRepairCard();
                ds_phieuTN.Children.Add(it);
            }
        }
        // áp dụng lọc
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Hidden;
            //


        }
    }
}
