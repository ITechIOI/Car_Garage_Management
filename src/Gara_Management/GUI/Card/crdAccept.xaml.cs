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
    /// Interaction logic for crdAccept.xaml
    /// </summary>
    public partial class crdAccept : Window
    {
        private bool isChanged=false;  /* biếm kiểm tra các thông tin đã thay đổi hay chưa*/
        // khi khởi tạo tự động có mã phiếu ( với phiếu mới)
        public crdAccept()
        {
            InitializeComponent();
        }

        // hiển thị lên phiếu đã có
        public crdAccept(string a)
        {
            InitializeComponent();
            tbx_save.Text = "Sửa";
            // cho các textBox unenable

            // các hàm lấy thông tin và gán vào UI

        }
        // đã có thay đổi ở các text box
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isChanged=true;
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
        // in phiếu
        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // lưu phiếu mới hoặc lưu các thông tin đã sửa trước khi in 
            if(!isChanged && isValid()) {
            
            }
        }

        
    }
}
