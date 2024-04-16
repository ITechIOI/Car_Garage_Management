﻿using Gara_Management.GUI.Card;
using Gara_Management.GUI.Item;
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
    /// Interaction logic for scrAccount.xaml
    /// </summary>
    public partial class scrAccount : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        Color color5 = (Color)ColorConverter.ConvertFromString("#072D44");


        public event EventHandler changeToRepairCardScr;
        public scrAccount()
        {
            InitializeComponent();

            for (int i = 0; i < 9; i++)
            {
                itAccount itAcc1 = new itAccount();
                ds_acc.Children.Add(itAcc1);

            }
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

        // nút chuyển màn hình sang danh sách phiếu sửa chữa
        private void bt_repair_card_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToRepairCardScr?.Invoke(this, EventArgs.Empty);

        }

        private void bd_acceptCar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // tiếp nhận sửa chửa (giống bên home)
            crdAccept newCar = new crdAccept();
            newCar.ShowDialog();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // lọc 
        }
    }
}
