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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrAccDetail.xaml
    /// </summary>
    public partial class scrAccDetail : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");
        public EventHandler returntoListacc;
        string gara;
        Staff staff;
        Account account;
        public scrAccDetail(Staff staff, Account account, string gara)
        {
            InitializeComponent();
            this.staff = staff;
            this.account = account;
            this.gara = gara;
            txtb_idStaff.Text = this.staff.IDStaff;
            txtb_username.Text = this.account.UserName;
            txtb_name.Text = this.staff.NameStaff;
            txtb_birthday.Text = this.staff.BirthdayStaff.ToString();
            txtb_address.Text = this.staff.AddressStaff;
            txtb_email.Text = this.staff.EmailStaff;
            txtb_phone.Text = this.staff.PhoneNumberStaff;
            txtb_position.Text = this.staff.Position;
            if (this.account.AccAuthorization == false)
            {
                txtb_author.Text = "Quản lý";
            }
            else
            {
                txtb_author.Text = "Nhân viên";
            }
            txtb_salary.Text =((int) this.staff.Salary).ToString();
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

        private void bt_addAcc_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bt_return_MouseDown(object sender, MouseButtonEventArgs e)
        {
            returntoListacc?.Invoke(this, new EventArgs());

        }

        private void bt_update_info_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Staff staff1 = new Staff(txtb_idStaff.Text, txtb_name.Text, DateTime.Parse(txtb_birthday.Text), txtb_address.Text,
                txtb_email.Text, txtb_phone.Text, decimal.Parse(txtb_salary.Text), txtb_position.Text, gara, false);
            cardUpdateInfo crdupdate = new cardUpdateInfo(staff, account, staff1);
            crdupdate.ShowDialog();
        }

        private void bt_change_pass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardChangepass crdchange = new cardChangepass(staff, account);
            crdchange.ShowDialog();
            account = AccountDAO.Instance.GetAccountByIDStaff(staff.IDStaff);
        }
    }
}
