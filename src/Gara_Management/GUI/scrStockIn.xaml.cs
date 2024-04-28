using Gara_Management.GUI.Item;
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
    /// Interaction logic for scrStockIn.xaml
    /// </summary>
    public partial class scrStockIn : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");

        public EventHandler changeToStoreScr;
        public scrStockIn()
        {
            InitializeComponent();
            for(int i = 0; i < 10; i++)
            {
                itStockIn it = new itStockIn();
                ds_phieunhap.Children.Add(it);
            }
        }
        private void bd_exit_MouseEnter(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color4);
        }

        private void bd_exit_MouseLeave(object sender, MouseEventArgs e)
        {
            bd_exit.Background = new SolidColorBrush(color3);
        }
        // nút thoát ứng dụng
        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra xem người dùng đã chọn Yes hay không
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }
        // chuyển sang màn hình kho
        private void bd_store_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToStoreScr?.Invoke(this, EventArgs.Empty);  
        }
        // tạo phiêu nhập kho
        private void bd_stockIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn();
            crdStockIn.ShowDialog();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Visible;
        }
    }
}
