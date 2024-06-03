using Gara_Management.DAO;
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
using System.Windows.Shapes;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrHome.xaml
    /// </summary>
    public partial class scrHome : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#079992");
        Color color5 = (Color)ColorConverter.ConvertFromString("#218c74");
        string gara;
        Account account;
        public scrHome(string gara, Account acc)
        {
            InitializeComponent();
            this.gara = gara;
            this.account = acc;
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

        private void bd_acceptCar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tiếp nhận sửa chữa 
             crdAccept crdAccept = new crdAccept(gara);
            crdAccept.ShowDialog();
        }

        private void bd_paymentReceipt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // lập phiếu thu tiền
            Staff staff = StaffDAO.Instance.GetStaffById(account.IDStaff);
            crdReceipt crdReceipt = new crdReceipt(gara,staff);
            crdReceipt.ShowDialog();
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Lập phiếu sửa chữa
            crdRepair crdRepair = new crdRepair();
            crdRepair.ShowDialog();
        }

        private void bd_stockIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tạo phiếu nhập kho
            crdStockIn crdStock = new crdStockIn(gara,account);
            crdStock.ShowDialog();
        }

        private void bd_reportCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tạo báo cáo
            cardRevenue revenue = new cardRevenue();
            revenue.Show();

        }

        private void bd_customerAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // thêm khách hàng
            crdCustomer crdCustomer = new crdCustomer(gara);    
            crdCustomer.ShowDialog();
        }

        private void bd_staffAdd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // thêm nhân viên
            cardInfoStaff crdstaff = new cardInfoStaff(gara);
            crdstaff.ShowDialog();
        }
    }
}
