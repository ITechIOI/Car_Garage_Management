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
using Gara_Management.DTO;
using Gara_Management.DAO;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrCars.xaml
    /// </summary>
    public partial class scrCars : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");



        public event EventHandler changeToRepairCardScr;
        string gara;
        Account account;
        public scrCars(string gara, Account account)
        {
            InitializeComponent();
            this.gara = gara;
            this.account = account;
            LoadListReceipt();
           
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
   
        // nút chuyển màn hình sang danh sách phiếu sửa chữa
        private void bt_repair_card_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToRepairCardScr?.Invoke(this, EventArgs.Empty);
          
        }

        private void bd_acceptCar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tiếp nhận sửa chửa (giống bên home)
            crdAccept newCar = new crdAccept(gara);
            newCar.ShowDialog();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // lọc 
            filter.Visibility = Visibility.Visible;
        }
        private void LoadListReceipt()
        {
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtList(gara);
            foreach (ReceptionForm item in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(item.IDCus, gara);
                itCar itCar1 = new itCar(gara, account, cus, item.IDRec);
                ds_acc.Children.Add(itCar1);

            }
        }

        private void filter_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        // áp dụng lọc
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Hidden;
            //


        }
    }
}
