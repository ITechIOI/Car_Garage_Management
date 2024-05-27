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
    /// Interaction logic for cardViewInfo.xaml
    /// </summary>
    public partial class cardViewInfo : Window
    {
        public cardViewInfo()
        {
            InitializeComponent();
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void btn_edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_edit.Text=="Chỉnh sửa")
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn chỉnh sửa thông tin nhân viên?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Kiểm tra xem người dùng đã chọn Yes hay không
                if (result == MessageBoxResult.Yes)
                {
                    txtb_fullname.IsReadOnly = false;
                    txtb_birthdate.IsReadOnly = false;
                    txtb_address.IsReadOnly = false;
                    txtb_email.IsReadOnly = false;
                    txtb_phonenumber.IsReadOnly = false;
                    txtb_edit.Text = "Hủy";
                }
            }
            else
            {
                txtb_fullname.IsReadOnly = true;
                txtb_birthdate.IsReadOnly = true;
                txtb_address.IsReadOnly = true;
                txtb_email.IsReadOnly = true;
                txtb_phonenumber.IsReadOnly = true;
                txtb_edit.Text = "Chỉnh sửa";

            }

        }

        private void btn_update_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
