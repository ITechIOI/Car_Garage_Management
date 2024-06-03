﻿using Gara_Management.GUI.Item;
using Gara_Management.GUI.Card;
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
using Gara_Management.DTO;
using Gara_Management.DAO;

namespace Gara_Management.GUI
{
    /// <summary>
    /// Interaction logic for scrStockIn.xaml
    /// </summary>
    public partial class scrStockIn : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        string gara;
        Account acc;
        public EventHandler changeToStoreScr;
        public scrStockIn(string gara, Account acc)
        {
            InitializeComponent();
            this.gara = gara;
            this.acc = acc;
            LoadListGoodReceivedNote();
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
        // chuyển sang màn hình kho
        private void bd_store_scr_MouseDown(object sender, MouseButtonEventArgs e)
        {
            changeToStoreScr?.Invoke(this, EventArgs.Empty);  
        }
        // tạo phiêu nhập kho
        private void bd_stockIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn(gara, acc);
            crdStockIn.ShowDialog();
            LoadListGoodReceivedNote();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Visible;
        }
        private void LoadListGoodReceivedNote()
        {
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteList(gara);
            foreach (GoodReceivedNote good in list) 
            {
                itStockIn it = new itStockIn(good);
                ds_phieunhap.Children.Add(it);
            }
        }
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            filter.Visibility = Visibility.Hidden;
            //


        }
    }
}
