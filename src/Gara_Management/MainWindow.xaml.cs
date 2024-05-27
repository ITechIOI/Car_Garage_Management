﻿using Gara_Management.DTO;
using Gara_Management.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using System.Xml.Serialization;

namespace Gara_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int scr = 1;
        Color color4= (Color)ColorConverter.ConvertFromString("#079992"); /*#064469*/
        Color color5= (Color)ColorConverter.ConvertFromString("#218c74");

        Account acc;

        public MainWindow(Account acc)
        {
            InitializeComponent();
            DataContext = new scrHome();
            this.acc = acc;
            
        }

  
        // di chuyển màn hình
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // move chuột vào nút màn hình Home 
        private void menuHome_MouseEnter(object sender, MouseEventArgs e)
        {
            menuHome.Background= new SolidColorBrush(color4);
        }
        // clik nút màn hình Home
        private void menuHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 1;
            DataContext = new scrHome();
            menuHome.Background = new SolidColorBrush(color4);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        // move chuột ra khỏi nút màn hình Home
        private void menuHome_MouseLeave(object sender, MouseEventArgs e)
        {
            if(scr!=1)
            menuHome.Background = new SolidColorBrush(color5);
        }

        // move chuột vào nút màn hình Cars
        private void menuCars_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCars.Background = new SolidColorBrush(color4);
        }
        // clik nút màn hình Cars
        private void menuCars_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 2;
            scrCars scrCars = new scrCars();
            DataContext = scrCars;
            scrCars.changeToRepairCardScr += scrCars_changeToRepairCardScr;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color4);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        // thay đổi từ màn hình repairCard sang Cars 
        private void scrRepairCard_changeToCardScr(object sender, EventArgs e)
        {
            scr = 2;
            scrCars scrCars = new scrCars();       
            DataContext = scrCars;
            scrCars.changeToRepairCardScr += scrCars_changeToRepairCardScr;
        }
        // thay đổi từ màn hình Cars sang repairCard
        public void scrCars_changeToRepairCardScr(object sender, EventArgs e)
        {
            scr = 2;
            scrRepairCard scrRepairCard = new scrRepairCard();
            DataContext = scrRepairCard;
            scrRepairCard.changeToCarsScr += scrRepairCard_changeToCardScr;
        }
        // move chuột ra khỏi nút màn hình Cars
        private void menuCars_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 2)
                menuCars.Background = new SolidColorBrush(color5);
        }

        // move chuột vào nút màn hình Store
        private void menuStore_MouseEnter(object sender, MouseEventArgs e)
        {
            menuStore.Background = new SolidColorBrush(color4);
        }
        // click nút màn hình Store
        private void menuStore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 3;
            scrStore scrStore = new scrStore();
            DataContext = scrStore;
            scrStore.changeToStockInScr += scrStore_changeToStockInScr;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color4);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        // thay đổi từ màn hình Store sang StockIn
        public void scrStore_changeToStockInScr(object sender, EventArgs e)
        {
            scr = 3;
            scrStockIn scrStockIn = new scrStockIn();
            DataContext = scrStockIn;
            scrStockIn.changeToStoreScr += scrStockIn_changeToStoreScr;
        }
        // thay đổi từ màn hình StockIn sang Store
        public void scrStockIn_changeToStoreScr(object sender, EventArgs e)
        {
            scr = 3;
            scrStore scrStore = new scrStore();
            DataContext = scrStore;
            scrStore.changeToStockInScr += scrStore_changeToStockInScr;
        }
        // move chuột ra khỏi nút màn hình Store
        private void menuStore_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 3)
                menuStore.Background = new SolidColorBrush(color5);
        }

        // move chuột vào nút màn hình Revenue
        private void menuRevenue_MouseEnter(object sender, MouseEventArgs e)
        {
            menuRevenue.Background = new SolidColorBrush(color4);
        }
        // click nút màn hình Revenue
        private void menuRevenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 4;
            DataContext = new scrRevenue();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color4);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        // move chuột ra khỏi nút màn hình Revenue
        private void menuRevenue_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 4)
                menuRevenue.Background = new SolidColorBrush(color5);
        }

        // move chuột vào nút màn hình Customers
        private void menuCustomers_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCustomers.Background = new SolidColorBrush(color4);
        }
        // click nút màn hình Customers
        private void menuCustomers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 5;
            DataContext = new scrCustomer();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color4);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        // move chuột ra khỏi nút màn hình Customers
        private void menuCustomers_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 5)
                menuCustomers.Background = new SolidColorBrush(color5);
        }

        // move chuột vào nút màn hình Account
        private void menuAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            menuAccount.Background = new SolidColorBrush(color4);
        }
        // click nút màn hình Account
        private void menuAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 6;
            scrAccDetail scraccdt = new scrAccDetail();
            DataContext = scraccdt;
            scraccdt.returntoListacc += ChangetoscrListacc;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color4);
        }
        // chuyển sang màn hình danh sách các nhân viên (chỉ Admin mới làm đc)
        public void ChangetoscrListacc(object sender, EventArgs e)
        {
            scr = 6;
            scrAccount scracc = new scrAccount();
            DataContext = scracc;
            scracc.returntoDetailAcc += ChangetoscrDetailAcc;
        }

        // chuyển trở về màn hình chi tiết thông tin cá nhân của người đăng nhập

        public void ChangetoscrDetailAcc(object sender, EventArgs e)
        {
            scr = 6;
            scrAccDetail scrAccDetail = new scrAccDetail();
            DataContext = scrAccDetail;
            scrAccDetail.returntoListacc += ChangetoscrListacc;
        }


        // move chuột ra khỏi nút màn hình Account
        private void menuAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 6)
                menuAccount.Background = new SolidColorBrush(color5);
        }

        private void menuStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            menuStaff.Background = new SolidColorBrush(color4);
        }

        private void menuStaff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 7;
            DataContext = new scrAccount();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color4);
        }

        private void menuStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 7)
                menuStaff.Background = new SolidColorBrush(color5);

        }
    }
}
