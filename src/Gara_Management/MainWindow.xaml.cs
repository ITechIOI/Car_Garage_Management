﻿using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI;
using Gara_Management.GUI.Card;
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
using System.Windows.Media.Animation;
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
        string gara;
        Staff staff;

        public MainWindow(Account acc)
        {
            InitializeComponent();
            if (acc.AccAuthorization)
            {
                menuRevenue.Visibility = Visibility.Collapsed;
                menuStaff.Visibility = Visibility.Collapsed;
            }    
            this.Opacity = 0;
            this.acc = acc;
            gara = StaffDAO.Instance.GetIDGaraByIDStaff(this.acc.IDStaff);
            DataContext = new scrHome(gara, acc); 
            staff = StaffDAO.Instance.GetStaffById(this.acc.IDStaff);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
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

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // clik nút màn hình Home
        private void menuHome_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 1;
            DataContext = new scrHome(gara, acc);
            menuHome.Background = new SolidColorBrush(color4);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color5);


            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // move chuột ra khỏi nút màn hình Home
        private void menuHome_MouseLeave(object sender, MouseEventArgs e)
        {
            if(scr!=1)
            menuHome.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        // move chuột vào nút màn hình Cars
        private void menuCars_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCars.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // clik nút màn hình Cars
        private void menuCars_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 2;
            scrCars scrCars = new scrCars(gara, acc);
            DataContext = scrCars;
            scrCars.changeToRepairCardScr += scrCars_changeToRepairCardScr;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color4);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // thay đổi từ màn hình repairCard sang Cars 
        private void scrRepairCard_changeToCardScr(object sender, EventArgs e)
        {
            scr = 2;
            scrCars scrCars = new scrCars(gara,acc);       
            DataContext = scrCars;
            scrCars.changeToRepairCardScr += scrCars_changeToRepairCardScr;
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // thay đổi từ màn hình Cars sang repairCard
        public void scrCars_changeToRepairCardScr(object sender, EventArgs e)
        {
            scr = 2;
            scrRepairCard scrRepairCard = new scrRepairCard(gara, staff);
            DataContext = scrRepairCard;
            scrRepairCard.changeToCarsScr += scrRepairCard_changeToCardScr;
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // move chuột ra khỏi nút màn hình Cars
        private void menuCars_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 2)
                menuCars.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        // move chuột vào nút màn hình Store
        private void menuStore_MouseEnter(object sender, MouseEventArgs e)
        {
            menuStore.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // click nút màn hình Store
        private void menuStore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 3;
            scrStore scrStore = new scrStore(gara,acc);
            DataContext = scrStore;
            scrStore.changeToStockInScr += scrStore_changeToStockInScr;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color4);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // thay đổi từ màn hình Store sang StockIn
        public void scrStore_changeToStockInScr(object sender, EventArgs e)
        {
            scr = 3;
            scrStockIn scrStockIn = new scrStockIn(gara, acc);
            DataContext = scrStockIn;
            scrStockIn.changeToStoreScr += scrStockIn_changeToStoreScr;
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // thay đổi từ màn hình StockIn sang Store
        public void scrStockIn_changeToStoreScr(object sender, EventArgs e)
        {
            scr = 3;
            scrStore scrStore = new scrStore(gara, acc);
            DataContext = scrStore;
            scrStore.changeToStockInScr += scrStore_changeToStockInScr;
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // move chuột ra khỏi nút màn hình Store
        private void menuStore_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 3)
                menuStore.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        // move chuột vào nút màn hình Revenue
        private void menuRevenue_MouseEnter(object sender, MouseEventArgs e)
        {
            menuRevenue.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // click nút màn hình Revenue
        private void menuRevenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 4;
            scrRevenue scrRevenue = new scrRevenue(gara);
            DataContext = scrRevenue;
        
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color4);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
   
      
        // move chuột ra khỏi nút màn hình Revenue
        private void menuRevenue_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 4)
                menuRevenue.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        // move chuột vào nút màn hình Customers
        private void menuCustomers_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCustomers.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // click nút màn hình Customers
        private void menuCustomers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 5;
            DataContext = new scrCustomer(gara);
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color4);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // move chuột ra khỏi nút màn hình Customers
        private void menuCustomers_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 5)
                menuCustomers.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        // move chuột vào nút màn hình Account
        private void menuAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            menuAccount.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }
        // click nút màn hình Account
        private void menuAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 6;
            scrAccDetail scraccdt = new scrAccDetail(staff, acc, gara);
            DataContext = scraccdt;
            scraccdt.returntoListacc += ChangetoscrListacc;
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color4);
            menuStaff.Background = new SolidColorBrush(color5);

            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);

        }
        // chuyển sang màn hình danh sách các nhân viên (chỉ Admin mới làm đc)
        public void ChangetoscrListacc(object sender, EventArgs e)
        {
            scr = 6;
            scrAccount scracc = new scrAccount(gara);
            DataContext = scracc;
            scracc.returntoDetailAcc += ChangetoscrDetailAcc;
            acc = AccountDAO.Instance.GetAccountByIDStaff(staff.IDStaff);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);

        }

        // chuyển trở về màn hình chi tiết thông tin cá nhân của người đăng nhập

        public void ChangetoscrDetailAcc(object sender, EventArgs e)
        {
            scr = 6;
            scrAccDetail scrAccDetail = new scrAccDetail(staff, acc, gara);
            DataContext = scrAccDetail;
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
            scrAccDetail.returntoListacc += ChangetoscrListacc;
            acc = AccountDAO.Instance.GetAccountByIDStaff(staff.IDStaff);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }


        // move chuột ra khỏi nút màn hình Account
        private void menuAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 6)
                menuAccount.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        private void menuStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            menuStaff.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        private void menuStaff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 7;
            DataContext = new scrAccount(gara);
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
            menuStaff.Background = new SolidColorBrush(color4);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);
        }

        private void menuStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 7)
                menuStaff.Background = new SolidColorBrush(color5);
            staff = StaffDAO.Instance.GetStaffById(staff.IDStaff);

        }

        private void logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất tài khoản này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                cardLogin cardLogin = new cardLogin();
                cardLogin.Show();
                this.Close();
            }
        }

        private void logout_MouseEnter(object sender, MouseEventArgs e)
        {
            logout.Background= new SolidColorBrush(color4);
        }

        private void logout_MouseLeave(object sender, MouseEventArgs e)
        {
            logout.Background = new SolidColorBrush(color5);

        }
    }
}
