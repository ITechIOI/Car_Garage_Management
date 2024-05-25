using Gara_Management.DAO;
using Gara_Management.DTO;
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

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardChangepass.xaml
    /// </summary>
    public partial class cardChangepass : Window
    {
        public cardChangepass()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //string idStaff = idStaffTextbox.Text;
            //Account acc = AccountDAO.Instance.GetAccountByIDStaff(idStaff);
            //string password = oldPasswordTextbox.Text;
            //if (password != acc.Password)
            //{
            //    MessageBox.Show("Mật khẩu cũ không đúng. Vui lòng thử lại.");
            //}
            //else
            //{
            //    if (newPasswordTextbox.Text != retypeNewPasswordTextbox.Text)
            //    {
            //        MessageBox.Show("Mật khẩu mới không trùng khớp. Vui lòng thử lại.");
            //    }
            //    else
            //    {
            //        AccountDAO.Instance.ChangePassword(acc.IDAcc, newPasswordTextbox.Text);
            //        MessageBox.Show("Thay đổi mật khẩu thành công.");
            //        this.Close();
            //    }    
            //}
        }
    }
}
