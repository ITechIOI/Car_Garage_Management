using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD
=======
using System.Net.Http;
>>>>>>> hhoang
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
<<<<<<< HEAD
=======
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
>>>>>>> hhoang

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for crdCustomer.xaml
    /// </summary>
    public partial class crdCustomer : Window
    {
<<<<<<< HEAD
=======
        bool isChanged = false;
        // thêm mới
>>>>>>> hhoang
        public crdCustomer()
        {
            InitializeComponent();
        }
<<<<<<< HEAD
    }
}
=======
        // xem khách hàng đã có
        public crdCustomer(string customerId)
        {
            InitializeComponent();
            tbx_save.Text = "Sửa";
            // cho mấy textbox unenable, ấn sửa mới enable
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
            if (!isChanged)
            {




            }
            else
            {


            }
        }


    }
  
}


>>>>>>> hhoang
