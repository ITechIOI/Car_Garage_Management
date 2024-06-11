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
using System.Windows.Media.Animation;
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
            this.Opacity = 0;
            staffIDBorder.Visibility = Visibility.Hidden;
            staffBirthdayPicker.SelectedDateFormat = new DatePickerFormat();
            LoadAccAuthor();
            LoadPosition();
            this.gara = gara;
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
            if (staffIDBorder.Visibility == Visibility.Hidden)
            {
                if (staffNameTextBox.Text == "" || staffBirthdayPicker.SelectedDate == null || 
                    staffAddressTextBox.Text == "" || staffEmailTextBox.Text == "" || staffPhoneTextbox.Text == ""
                    || cbx_position.SelectedItem == null || staffSalaryTextbox.Text =="")
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.","Thông báo");
                }    
                else
                {
                    int salary;
                    if (!int.TryParse(staffSalaryTextbox.Text,out salary))
                    {
                        MessageBox.Show("Lương phải là một số nguyên", "Thông báo");
                    }    
                    else
                    {
                        if (salary<=0)
                        {
                            MessageBox.Show("Lương phải là một số nguyên dương.", "Thông báo");
                            return;
                        }    
                        if (StaffDAO.Instance.CheckExistIDStaff(staffNameTextBox.Text, staffPhoneTextbox.Text, gara) != "")
                        {
                            MessageBox.Show("Nhân viên đã tồn tại.", "Thông báo");
                        }
                        else
                        {
                            DateTime birthday = DateTime.Parse(staffBirthdayPicker.SelectedDate.ToString());
                            bool res = StaffDAO.Instance.InsertStaff(staffNameTextBox.Text, 
                                birthday.ToString("dd/MM/yyyy"), staffAddressTextBox.Text,
                                staffEmailTextBox.Text, staffPhoneTextbox.Text, salary, 
                                cbx_position.SelectedItem.ToString(), gara);
                            if (usernameTextBox.Text != "")
                            {
                                if (AccountDAO.Instance.CheckExistedUsername(usernameTextBox.Text))
                                {
                                    MessageBox.Show("Tài khoản bị trùng vui lòng thử lại.", "Thông báo");
                                }
                                else
                                {
                                    string id = StaffDAO.Instance.CheckExistIDStaff(staffNameTextBox.Text,
                                        staffPhoneTextbox.Text, gara);
                                    bool author;
                                    if (cbx_accAuthor.SelectedItem.ToString() == "Admin") { author = false; }
                                    else { author = true;}
                                    res = res && AccountDAO.Instance.InsertAccount(usernameTextBox.Text, id, author);
                                }
                            }
                            if (res)
                            {
                                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Thêm nhân viên không thành công. Vui lòng thử lại!", "Thông báo");
                            }
                        }    
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
