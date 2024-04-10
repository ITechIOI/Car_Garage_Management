using Gara_Management.GUI;
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

namespace Gara_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int scr = 0;
        Color color4= (Color)ColorConverter.ConvertFromString("#064469");
        Color color5= (Color)ColorConverter.ConvertFromString("#072D44");

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new scrHome();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void menuHome_MouseEnter(object sender, MouseEventArgs e)
        {
            menuHome.Background= new SolidColorBrush(color4);
        }

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
        private void menuHome_MouseLeave(object sender, MouseEventArgs e)
        {
            if(scr!=1)
            menuHome.Background = new SolidColorBrush(color5);
        }

        private void menuCars_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCars.Background = new SolidColorBrush(color4);
        }

        private void menuCars_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 2;
            DataContext = new scrCars();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color4);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        private void menuCars_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 2)
                menuCars.Background = new SolidColorBrush(color5);

        }

        private void menuStore_MouseEnter(object sender, MouseEventArgs e)
        {
            menuStore.Background = new SolidColorBrush(color4);
        }

        private void menuStore_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 3;
            DataContext = new scrStore();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color4);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color5);
        }
        private void menuStore_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 3)
                menuStore.Background = new SolidColorBrush(color5);
        }
        private void menuRevenue_MouseEnter(object sender, MouseEventArgs e)
        {
            menuRevenue.Background = new SolidColorBrush(color4);
        }
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
        private void menuRevenue_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 4)
                menuRevenue.Background = new SolidColorBrush(color5);
        }

        private void menuCustomers_MouseEnter(object sender, MouseEventArgs e)
        {
            menuCustomers.Background = new SolidColorBrush(color4);
        }

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
        private void menuCustomers_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 5)
                menuCustomers.Background = new SolidColorBrush(color5);
        }
        private void menuAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            menuAccount.Background = new SolidColorBrush(color4);
        }

        private void menuAccount_MouseDown(object sender, MouseButtonEventArgs e)
        {
            scr = 6;
            DataContext = new scrAccount();
            menuHome.Background = new SolidColorBrush(color5);
            menuCars.Background = new SolidColorBrush(color5);
            menuStore.Background = new SolidColorBrush(color5);
            menuRevenue.Background = new SolidColorBrush(color5);
            menuCustomers.Background = new SolidColorBrush(color5);
            menuAccount.Background = new SolidColorBrush(color4);
        }

        private void menuAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scr != 6)
                menuAccount.Background = new SolidColorBrush(color5);
        }

        
    }
}
