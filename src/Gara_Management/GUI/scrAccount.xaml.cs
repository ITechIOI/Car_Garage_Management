using Gara_Management.DAO;
using Gara_Management.DTO;
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
    /// Interaction logic for scrAccount.xaml
    /// </summary>
    public partial class scrAccount : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#218c74");
        string gara;

        public event EventHandler returntoDetailAcc;
        public scrAccount(string gara)
        {
            InitializeComponent();
            this.gara = gara;
            LoadListStaff();

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

        private void bt_add_staff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardInfoStaff crdstaff = new cardInfoStaff(gara);
            crdstaff.ShowDialog();
            LoadListStaff();

        }

        private void bt_detail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            returntoDetailAcc?.Invoke(this, new EventArgs());
        }
        private void LoadListStaff()
        {
            List<Staff> staffs = StaffDAO.Instance.LoadStaffList(this.gara);
            ds_acc.Children.Clear();
            Account acc = null;
            foreach (Staff item in staffs)
            {
                acc = AccountDAO.Instance.GetAccountByIDStaff(item.IDStaff);
                itAccount itAcc1 = new itAccount(item, acc);
                ds_acc.Children.Add(itAcc1);
            }
        }
        private void LoadListStaffByName()
        {
            List<Staff> staffs = StaffDAO.Instance.LoadStaffListByName(this.gara, txtb_findStaff.Text);
            ds_acc.Children.Clear();
            Account acc = null;
            foreach (Staff item in staffs)
            {
                acc = AccountDAO.Instance.GetAccountByIDStaff(item.IDStaff);
                itAccount itAcc1 = new itAccount(item, acc);
                ds_acc.Children.Add(itAcc1);
            }
        }

        private void txtb_findStaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtb_findStaff.Text == "")
            {
                LoadListStaff();
            }    
            else
            {
                LoadListStaffByName();
            }    
        }
    }
}
