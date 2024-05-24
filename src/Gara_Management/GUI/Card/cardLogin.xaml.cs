﻿using Gara_Management.DAO;
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
    /// Interaction logic for cardLogin.xaml
    /// </summary>
    public partial class cardLogin : Window
    {
        public cardLogin()
        {
            InitializeComponent();
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void bt_login_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string username = usernameTextbox.Text;
            string password = passwordTextBox.Text;
            if (AccountDAO.Instance.CheckForLogin(username, password) != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng. Vui lòng thử lại.");
            }    
        }
    }
}
