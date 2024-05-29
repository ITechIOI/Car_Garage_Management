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
    /// Interaction logic for cardViewInfo.xaml
    /// </summary>
    public partial class cardViewInfo : Window
    {
        Staff staff;
        Account account;
        public cardViewInfo(Staff staff, Account account)
        {
            InitializeComponent();
            this.staff = staff;
            this.account = account;
            SetComponentReadOnly(true);
            LoadAccAuthor();
            LoadPosition();
            LoadStaffInfo();  
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
                        bool author;
                        if (cbx_accAuthor.SelectedItem.ToString() == "Admin")
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
        private void LoadAccAuthor()
        {
            cbx_accAuthor.Items.Clear();
            cbx_accAuthor.Items.Add("Admin");
            cbx_accAuthor.Items.Add("Staff");
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
    }
}
