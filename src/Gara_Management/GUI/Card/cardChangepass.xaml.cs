﻿using Gara_Management.DAO;
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
    /// Interaction logic for cardChangepass.xaml
    /// </summary>
    public partial class cardChangepass : Window
    {
        Staff staff;
        Account account;
        public cardChangepass(Staff staff, Account account)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.staff = staff;
            this.account = account;
            idStaffTextbox.Text = this.staff.IDStaff;
            nameStaffTextbox.Text = this.staff.NameStaff;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
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
            
            if (oldPasswordTextbox.Password == string.Empty || newPasswordTextbox.Password == string.Empty || retypeNewPasswordTextbox.Password == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
            }
            else
            {
                string idstaff = staff.IDStaff;
                if (oldPasswordTextbox.Password != account.Password)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng. Vui lòng thử lại.");
                }
                else
                {
                    if (newPasswordTextbox.Password.Length < 8)
                    {
                        MessageBox.Show("Mật khẩu mới phải hơn 8 kí tự.");
                    }
                    else
                    {
                        if (newPasswordTextbox.Password != retypeNewPasswordTextbox.Password)
                        {
                            MessageBox.Show("Mật khẩu mới không trùng khớp. Vui lòng thử lại.");
                        }
                        else
                        {
                            AccountDAO.Instance.ChangePassword(account.IDAcc, newPasswordTextbox.Password);
                            MessageBox.Show("Thay đổi mật khẩu thành công.");
                            this.Close();
                        }
                    }
                }
            }
        }

        private void retypeNewPasswordTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (retypeNewPasswordTextbox.Password != newPasswordTextbox.Password)
            {
                txtb_warning.Visibility = Visibility.Visible;
            }
            else
            {
                txtb_warning.Visibility = Visibility.Collapsed;
            }    
        }

        private void retypeNewPasswordTextbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (retypeNewPasswordTextbox.Password != newPasswordTextbox.Password)
            {
                txtb_warning.Visibility = Visibility.Visible;
            }
            else
            {
                txtb_warning.Visibility = Visibility.Collapsed;
            }
        }
    }
}
