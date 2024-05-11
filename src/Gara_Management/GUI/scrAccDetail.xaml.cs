﻿using Gara_Management.GUI.Card;
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
        public scrAccDetail()
        {
            InitializeComponent();
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
            cardUpdateInfo crdupdate = new cardUpdateInfo();
            crdupdate.ShowDialog();
        }

        private void bt_change_pass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardChangepass crdchange = new cardChangepass();
            crdchange.ShowDialog();
        }
    }
}