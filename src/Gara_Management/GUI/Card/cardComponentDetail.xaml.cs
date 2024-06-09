using Gara_Management.DAO;
using Gara_Management.DTO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardComponentDetail.xaml
    /// </summary>
    public partial class cardComponentDetail : Window
    {
        CarComponent com;
        string gara;
        //Thêm component mới
        public cardComponentDetail(string gara)
        { 
            InitializeComponent();
            this.gara = gara;
            btn_edit.Visibility = Visibility.Hidden;
            txtb_delete.Text = "Thêm";
            txtb_idcom.Visibility = Visibility.Collapsed;
            txtb_price.IsReadOnly = true;
            txtb_price.Text = "0";
            txtb_amount.IsReadOnly = true;
            txtb_amount.Text = "0";
        }
            //Hiển thị component đã có
            public cardComponentDetail(CarComponent com, string gara)
        {
            
            InitializeComponent();
            this.com = com;
            this.gara = gara;
            txtb_idcom.Text = com.IDCom;
            txtb_namecom.IsReadOnly = true;
            txtb_namecom.Text = com.NameCom;
            txtb_wage.IsReadOnly = true;
            txtb_wage.Text = com.Wage.ToString();
            txtb_price.IsReadOnly = true;
            txtb_price.Text = com.CurPrice.ToString();
            txtb_amount.IsReadOnly = true;
            txtb_amount.Text = com.ComQuantity.ToString();
            this.Opacity = 0;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        private void bt_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();   
        }

        private void btn_edit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_edit.Text == "Chỉnh sửa")
            {
                if (MessageBox.Show("Bạn muốn sửa thông tin của phụ tùng này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    txtb_edit.Text = "Hủy";
                    txtb_delete.Text = "Lưu";
                    txtb_namecom.IsReadOnly = false;
                    txtb_wage.IsReadOnly = false;
                    txtb_price.IsReadOnly = false;
                }
            }
            else
            {
                txtb_edit.Text = "Chỉnh sửa";
                txtb_delete.Text = "Xóa";
                txtb_namecom.IsReadOnly = true;
                txtb_wage.IsReadOnly = true;
                txtb_price.IsReadOnly = true;
               
            }
        }

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_delete.Text =="Xóa")
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phụ tùng này?", "Thông báo", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                {
                    if (CarComponentDAO.Instance.DeleteCarComponent(txtb_idcom.Text,  gara))
                    {
                        MessageBox.Show("Xóa phụ tùng thành công.", "Thông báo");
                        this.Close();
                    }    
                    else
                    {
                        MessageBox.Show("Xóa phụ tùng thất bại. Vui lòng thử lại.", "Thông báo");
                    }    
                }    
            }   
            else
            {
                if (txtb_delete.Text =="Thêm")
                {
                    if (txtb_namecom.Text == ""|| txtb_wage.Text == "")
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
                    }    
                    else
                    {
                        decimal wage;
                        if (!decimal.TryParse(txtb_wage.Text, out wage))
                        {
                            MessageBox.Show("Tiền công phải là một con số.", "Thông báo");
                        }    
                        else
                        {
                            if (CarComponentDAO.Instance.InsertCarComponent(txtb_namecom.Text, gara, wage))
                            {
                                MessageBox.Show("Thêm phụ tùng mới thành công.", "Thông báo");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Thêm phụ tùng mới thất bại vui lòng thử lại.", "Thông báo");
                            }    
                        }    
                    }    
                }   
                else
                {
                    if (txtb_namecom.Text == "" || txtb_wage.Text == "" || txtb_price.Text == "")
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
                    }
                    else
                    {
                        decimal wage;
                        if (!decimal.TryParse(txtb_wage.Text, out wage))
                        {
                            MessageBox.Show("Tiền công phải là một con số.", "Thông báo");
                        }
                        else
                        {
                            decimal price;
                            if (!decimal.TryParse(txtb_price.Text, out price))
                            {
                                MessageBox.Show("Giá tiền phải là một con số.");
                            }
                            else
                            {
                                if (CarComponentDAO.Instance.UpdateCarComponent(txtb_idcom.Text, txtb_namecom.Text, gara, wage, price))
                                {
                                    MessageBox.Show("Thêm phụ tùng mới thành công.", "Thông báo");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm phụ tùng mới thất bại vui lòng thử lại.", "Thông báo");
                                }
                            }
                        }
                    }
                }    
            }    
        }
    }
}
