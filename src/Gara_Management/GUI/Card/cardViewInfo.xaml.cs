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
    /// Interaction logic for cardViewInfo.xaml
    /// </summary>
    public partial class cardViewInfo : Window
    {
        Staff staff;
        Account account;
        public cardViewInfo(Staff staff, Account account)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.staff = staff;
            this.account = account;
            SetComponentReadOnly(true);
            LoadAccAuthor();
            LoadPosition();
            LoadStaffInfo();
            txtb_idStaff.Text = this.staff.IDStaff;
            txtb_fullname.Text=this.staff.NameStaff;
            txtb_birthdate.Text = this.staff.BirthdayStaff.ToString();
            txtb_address.Text = this.staff.AddressStaff;
            txtb_email.Text = this.staff.EmailStaff;
            txtb_phonenumber.Text = this.staff.PhoneNumberStaff;
            cbx_position.Text = this.staff.Position;
            tbtx_salary.Text =((int) this.staff.Salary).ToString();
            if (this.account != null)
            {
                txtb_account.Text = this.account.UserName;
                if (this.account.AccAuthorization == false)
                {
                    cbx_accAuthor.Text = "Quản lý";
                }
                else
                {
                    cbx_accAuthor.Text = "Nhân viên";
                }
            }    
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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn_edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_edit.Text=="Chỉnh sửa")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn chỉnh sửa thông tin nhân viên?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Kiểm tra xem người dùng đã chọn Yes hay không
                if (result == MessageBoxResult.Yes)
                {
                    SetComponentReadOnly(false);
                    txtb_edit.Text = "Hủy";
                    txtb_delete.Text = "Cập nhật";
                }
            }
            else
            {
                SetComponentReadOnly(true);
                LoadStaffInfo();
                txtb_edit.Text = "Chỉnh sửa";
                txtb_delete.Text = "Xóa";
            }

        }

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_delete.Text == "Xóa")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa nhân viên này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool res = true;
                    if (account != null)
                    {
                        res = AccountDAO.Instance.DeleteAccount(account.IDAcc);
                    }
                    if (res && StaffDAO.Instance.DeleteStaff(staff.IDStaff))
                    {
                        MessageBox.Show("Xóa nhân viên thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại.");
                    }    
                }    
            }
            else
            {
                if (txtb_fullname.Text == "" || txtb_address.Text == "" || txtb_email.Text == "" 
                    || tbtx_salary.Text == "" || txtb_birthdate.SelectedDate == null || 
                    cbx_position.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
                else
                {
                    if (account != null && txtb_account.Text != "")
                    {
                        bool res = true;
                        bool author;
                        if (cbx_accAuthor.SelectedItem.ToString() == "Admin")
                            author = false;
                        else author = true;
                        res = AccountDAO.Instance.ChangeAccAuthorization(account.IDAcc, author);
                        DateTime birthday = DateTime.Parse(txtb_birthdate.SelectedDate.ToString());
                        if (res && StaffDAO.Instance.UpdateStaff(txtb_idStaff.Text, txtb_fullname.Text,
                            birthday.ToString("dd/MM/yyyy"), txtb_address.Text, txtb_email.Text,
                            txtb_phonenumber.Text, int.Parse(tbtx_salary.Text), cbx_position.SelectedItem.ToString()))
                        {
                            MessageBox.Show("Cập nhật thông tin nhân viên thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin không thành công.");
                        }
                        
                    }
                    else
                    {
                        if (account == null && txtb_account.Text != "")
                        {
                            if (cbx_accAuthor.SelectedItem == null)
                            {
                                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
                                return;
                            }    
                            bool author;
                            if (cbx_accAuthor.SelectedItem != null && cbx_accAuthor.SelectedItem.ToString() == "Admin")
                                author = false;
                            else author = true;
                            bool res = AccountDAO.Instance.InsertAccount(txtb_account.Text, staff.IDStaff, author);
                            DateTime birthday = DateTime.Parse(txtb_birthdate.SelectedDate.ToString());
                            if (res && StaffDAO.Instance.UpdateStaff(txtb_idStaff.Text, txtb_fullname.Text,
                                birthday.ToString("dd/MM/yyyy"), txtb_address.Text, txtb_email.Text,
                                txtb_phonenumber.Text, int.Parse(tbtx_salary.Text), cbx_position.SelectedItem.ToString()))
                            {
                                MessageBox.Show("Thêm tài khoản mới và cập nhật thông tin nhân viên thành công.");
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thông tin không thành công.");
                            }
                        }
                    }
                }
                
            }    
        }
        private void LoadAccAuthor()
        {
            cbx_accAuthor.Items.Clear();
            cbx_accAuthor.Items.Add("Quản lý");
            cbx_accAuthor.Items.Add("Nhân viên");
        }

        private void LoadPosition()
        {
            List<string> positions = StaffDAO.Instance.LoadListPosition();
            cbx_position.Items.Clear();
            foreach (string position in positions)
            {
                cbx_position.Items.Add(position);
            }
        }

        private void LoadStaffInfo()
        {
            if (account != null)
            {
                txtb_account.IsReadOnly = true;
            }    
            else
            {
                txtb_account.IsReadOnly= false;
            }    
        }
        private void SetComponentReadOnly(bool bo)
        {

            txtb_fullname.IsReadOnly = bo;
            txtb_address.IsReadOnly = bo;
            txtb_email.IsReadOnly = bo;
            txtb_phonenumber.IsReadOnly = bo;
            tbtx_salary.IsReadOnly = bo;
        }

        private void btn_resetpass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (AccountDAO.Instance.ResetPassword(account.IDAcc))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công.");
            }    
            else
            {
                MessageBox.Show("Đặt lại mật khẩu không thành công. Vui lòng thử lại.");
            }    
        }

        private void txtb_account_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtb_account.Text == "")
            {
                cbx_accAuthor.IsEnabled = false;
            }
            else
            {
                cbx_accAuthor.IsEnabled = true;
            }
        }
    }
}
