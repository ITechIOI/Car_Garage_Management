using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Card;
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
    /// Interaction logic for scrCustomer.xaml
    /// </summary>
    public partial class scrCustomer : UserControl
    {
        Color color3 = (Color)ColorConverter.ConvertFromString("#5790AB");
        Color color4 = (Color)ColorConverter.ConvertFromString("#064469");
        string gara;
        int minDebt = -1;
        int maxDebt = -1;
        public scrCustomer(string gara)
        {
            InitializeComponent();
            this.gara = gara;
            LoadListCustomer();
            rangeSlider.Maximum = CustomerDAO.Instance.GetMaxDebt(gara);
            ckb_Debt.IsChecked = false;
                  
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

        // hiển thị bảng thêm 1 khách hàng
        private void bd_add_customer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdCustomer crdCustomer = new crdCustomer(gara);
            crdCustomer.ShowDialog();
            LoadListCustomer();
        }

        private void bd_filter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (minDebt == -1 && maxDebt == -1)
            {
                rangeSlider.LowerValue = rangeSlider.Minimum;
                rangeSlider.HigherValue = rangeSlider.Maximum;
                ckb_Debt.IsChecked = false;
                filter.Visibility = Visibility.Visible;
            }
            else
            {
                rangeSlider.LowerValue = minDebt;
                rangeSlider.HigherValue = maxDebt;
                ckb_Debt.IsChecked = true;
                filter.Visibility = Visibility.Visible;
            }
        }
        private void LoadListCustomer()
        {
            List<Customer> customers = CustomerDAO.Instance.LoadCustomerList(this.gara);
            ds_khachhang.Children.Clear();
            foreach (Customer customer in customers)
            {
                itCustomer it = new itCustomer(customer, this.gara);
                ds_khachhang.Children.Add(it);
            }
        }
        private void apply_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtb_findCustomer.Text = "";
            filter.Visibility = Visibility.Hidden;
            if (ckb_Debt.IsChecked == true)
            {
                maxDebt = (int)rangeSlider.HigherValue;
                minDebt = (int)rangeSlider.LowerValue;
                LoadListCustomerByDebt();
            }   
            else
            {
                maxDebt = -1;
                minDebt = -1;
                LoadListCustomer();
            }    


        }
        private void LoadListCustomerByDebt()
        {
            List<Customer> customers = CustomerDAO.Instance.LoadCustomerListByDebt(this.gara, minDebt, maxDebt);
            ds_khachhang.Children.Clear();
            foreach (Customer customer in customers)
            {
                itCustomer it = new itCustomer(customer, this.gara);
                ds_khachhang.Children.Add(it);
            }
        }
        private void LoadListCustomerByDebtAndName()
        {
            List<Customer> customers = CustomerDAO.Instance.LoadCustomerListByDebtAndName(this.gara, txtb_findCustomer.Text,
                minDebt, maxDebt);
            ds_khachhang.Children.Clear();
            foreach (Customer customer in customers)
            {
                itCustomer it = new itCustomer(customer, this.gara);
                ds_khachhang.Children.Add(it);
            }
        }
        private void LoadListCustomerByName()
        {
            List<Customer> customers = CustomerDAO.Instance.LoadCustomerListByName(this.gara, txtb_findCustomer.Text);
            ds_khachhang.Children.Clear();
            foreach (Customer customer in customers)
            {
                itCustomer it = new itCustomer(customer, this.gara);
                ds_khachhang.Children.Add(it);
            }
        }

        private void txtb_findCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtb_findCustomer.Text == "")
            {
                if (ckb_Debt.IsChecked == true)
                {
                    LoadListCustomerByDebt();
                }
                else
                {
                    LoadListCustomer();
                }
            }    
            else
            {
                if (ckb_Debt.IsChecked == true)
                {
                    LoadListCustomerByDebtAndName();
                }
                else
                {
                    LoadListCustomerByName();
                }
            }    
        }

        private void rangeSlider_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            ckb_Debt.IsChecked = true;
        }
    }
}
