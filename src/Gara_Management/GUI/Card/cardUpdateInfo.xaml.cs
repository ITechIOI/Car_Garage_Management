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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardUpdateInfo.xaml
    /// </summary>
    public partial class cardUpdateInfo : Window
    {
        Staff staff;//thông tin cũ
        Account account;
        Staff staff1;//chứa thông tin cần thay đổi
        public cardUpdateInfo(Staff staff, Account account, Staff staff1)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.staff = staff;
            this.account = account;
            this.staff1 = staff1;
            txtb_idStaff.Text = staff.IDStaff;
            txtb_name.Text = staff.NameStaff;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_password.Text != account.Password)
            {
                MessageBox.Show("Sai mật khẩu vui lòng thử lại");
            }    
            else
            {
                if (StaffDAO.Instance.UpdateStaff(staff1.IDStaff, staff1.NameStaff, staff1.BirthdayStaff.ToString("dd/MM/yyyy"),
                    staff1.AddressStaff, staff1.EmailStaff, staff1.PhoneNumberStaff, int.Parse(staff1.Salary.ToString()),
                    staff1.Position))
                {
                    MessageBox.Show("Cập nhật thông tin thành công.");
                    
                    this.Close();
                }    
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại. Vui lòng thử lại.");
                }    
            }    
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
