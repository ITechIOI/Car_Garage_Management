using Gara_Management.DAO;
using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for crdCustomer.xaml
    /// </summary>
    public partial class crdCustomer : Window
    {

        bool isChanged = false;
        string gara;
        Customer customer;
        
        // thêm mới

        public crdCustomer(string gara)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            idCustomerBorder.Visibility = Visibility.Hidden;
            debtBorder.Visibility = Visibility.Hidden;
            tbx_save.Text = "Thêm";
        }

        public crdCustomer(string gara, string name, string phone)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            idCustomerBorder.Visibility = Visibility.Hidden;
            debtBorder.Visibility = Visibility.Hidden;
            txtb_name.Text = name;
            txtb_phone.Text = phone;
            tbx_save.Text = "Thêm";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        // xem khách hàng đã có
        public crdCustomer(Customer customer, string gara)
        {
            InitializeComponent();
            idCustomerBorder.Visibility = Visibility.Visible;
            this.customer = customer;
            this.gara = gara;
            txtb_name.IsEnabled = false;
            txtb_phone.IsEnabled = false;
            txtb_address1.IsEnabled = false;
            txtb_address2.IsEnabled = false;
            txtb_address3.IsEnabled = false;
            txtb_address4.IsEnabled = false;
            txtb_debt.IsEnabled = false;
            tbx_save.Text = "Sửa";
            // cho mấy textbox unenable, ấn sửa mới enable
            txtb_idCustomer.Text = this.customer.IDCus;
            txtb_name.Text = this.customer.NameCus;
            txtb_phone.Text = this.customer.PhoneNumberCus;
            string cutaddress = this.customer.AddressCus;
            string[] parts = cutaddress.Split(new string[] { ", " }, System.StringSplitOptions.None);
            txtb_address1.Text = parts[0];
            txtb_address2.Text = parts[1];
            txtb_address3.Text = parts[2];
            txtb_address4.Text = parts[3];
            txtb_debt.Text = this.customer.Debt.ToString();
        }




        // đã có thay đổi ở các text box
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged = true;
        }
        // move màn hình
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        // thoát
        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // kiểm tra xem cần làm gì trước khi đóng 
            this.Close();
        }
        // kiểm tra các thông tin đã hợp lệ chưa
        private bool isValid()
        {

            return true;
        }

        // lưu phiếu, sau khi lưu nút sẽ trở thành nút sửa, nếu mở phiếu đã có sẵn thì ban đầu sẽ là nút sửa ( 1 nút kiêm 2 chức năng)
        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_save.Text == "Thêm")
            {
                if (txtb_name.Text == "" || txtb_phone.Text == "" || txtb_address1.Text == "" || txtb_address2.Text == "" ||
                        txtb_address3.Text == "" || txtb_address4.Text == "" )
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                }
                else
                {
                    string address = txtb_address1.Text + ", " + txtb_address2.Text + ", " +
                            txtb_address3.Text + ", " + txtb_address4.Text;
                    if (CustomerDAO.Instance.InSertCustomer(gara, txtb_name.Text, txtb_phone.Text, address))
                    {
                        MessageBox.Show("Thêm khách hàng thành công.");
                        this.Close();
                    }    
                    else
                    {
                        MessageBox.Show("Thêm khách hàng thất bại. Vui lòng thử lại.");
                    }    
                }
            }
            else 
            {
                if (tbx_save.Text == "Sửa")
                {
                    txtb_name.IsEnabled = true;
                    txtb_phone.IsEnabled = true;
                    txtb_address1.IsEnabled = true;
                    txtb_address2.IsEnabled = true;
                    txtb_address3.IsEnabled = true;
                    txtb_address4.IsEnabled = true;
                    tbx_save.Text = "Lưu";
                }   
                else
                {
                    if (txtb_name.Text == "" || txtb_phone.Text == "" || txtb_address1.Text == "" || txtb_address2.Text == "" ||
                        txtb_address3.Text == "" || txtb_address4.Text == "" )
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    }
                    else
                    {
                        string address = txtb_address1.Text + ", " + txtb_address2.Text + ", " +
                            txtb_address3.Text + ", " + txtb_address4.Text;
                        if (CustomerDAO.Instance.UpdateCustomer(txtb_idCustomer.Text, txtb_name.Text, txtb_phone.Text, address))
                        {
                            MessageBox.Show("Cập nhật thông tin khách hàng thành công.");
                            txtb_name.IsEnabled = false;
                            txtb_phone.IsEnabled = false;
                            txtb_address1.IsEnabled = false;
                            txtb_address2.IsEnabled = false;
                            txtb_address3.IsEnabled = false;
                            txtb_address4.IsEnabled = false;
                            txtb_debt.IsEnabled = false;
                            tbx_save.Text = "Sửa";
                        }    
                        else
                        {
                            MessageBox.Show("Cập nhật thông tin khách hàng không thành công. Vui lòng thử lại");
                        }    
                    }    

                }    

            }
        }


    }

}

