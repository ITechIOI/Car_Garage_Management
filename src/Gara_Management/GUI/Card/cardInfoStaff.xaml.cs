using Gara_Management.DAO;
using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for cardInfoStaff.xaml
    /// </summary>
    public partial class cardInfoStaff : Window
    {
        string gara;
        public cardInfoStaff(string gara)
        {
            InitializeComponent();
            staffIDBorder.Visibility = Visibility.Hidden;
            staffBirthdayPicker.SelectedDateFormat = new DatePickerFormat();
            LoadAccAuthor();
            LoadPosition();
            this.gara = gara;
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (staffIDBorder.Visibility == Visibility.Hidden)
            {
                string name = staffNameTextBox.Text;
                DateTime birthday = DateTime.Parse(staffBirthdayPicker.SelectedDate.ToString());
                string address = staffAddressTextBox.Text;
                string email = staffEmailTextBox.Text;
                string phone = staffPhoneTextbox.Text;
                int salary = int.Parse(staffSalaryTextbox.Text);
                string position = cbx_position.SelectedItem.ToString();
                if (StaffDAO.Instance.CheckExistIDStaff(name, phone, gara) != "")
                {
                    MessageBox.Show("Nhân viên đã tồn tại.");
                }
                else
                {
                    bool res = StaffDAO.Instance.InsertStaff(name, birthday.ToString("dd/MM/yyyy"), address, email, phone, salary, position, gara);
                    if (usernameTextBox.Text != String.Empty)
                    {
                        string id = StaffDAO.Instance.CheckExistIDStaff(name, phone, gara);
                        bool author;
                        if (cbx_accAuthor.SelectedItem.ToString() == "Admin")
                        {
                            author = false;
                        }    
                        else
                        {
                            author = true;
                        }
                        if (AccountDAO.Instance.CheckExistedUsername(usernameTextBox.Text))
                        {
                            MessageBox.Show("Tài khoản bị trùng vui lòng thử lại.");
                        }
                        else
                        {
                            res = res && AccountDAO.Instance.InsertAccount(usernameTextBox.Text, id, author);
                        }
                    }
                    if (res)
                    {
                        MessageBox.Show("Thêm nhân viên thành công!");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm nhân viên không thành công. Vui lòng thử lại!");
                    }
                }
            }
            //else
            //{
            //    string id = staffIDTextBlock.Text;
            //    string name = staffNameTextBox.Text;
            //    DateTime birthday = DateTime.Parse(staffBirthdayPicker.SelectedDate.ToString());
            //    string address = staffAddressTextBox.Text;
            //    string email = staffEmailTextBox.Text;
            //    string phone = staffPhoneTextbox.Text;
            //    int salary = int.Parse(staffSalaryTextbox.Text);
            //    string position = cbx_position.SelectedItem.ToString();
            //    if (StaffDAO.Instance.UpdateStaff(id, name, birthday.ToString("dd/MM/yyyy"), address, email, phone, salary, position))
            //    {
            //        MessageBox.Show("Chỉnh sửa thông tin nhân viên thành công!");
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Thêm nhân viên không thành công. Vui lòng thử lại!");
            //    }
            //}    
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

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
    }
}
