using Gara_Management.DAO;
using Gara_Management.DTO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for cardCarBrand.xaml
    /// </summary>
    public partial class cardCarBrand : Window
    {
        string gara;
        public cardCarBrand(string gara)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            LoadListCarBrand();
            txtb_id.IsReadOnly = true;
            txtb_name.IsReadOnly = true;
        }

        private void CarBrand_Loaded(object sender, RoutedEventArgs e)
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

        private void btn_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (txtb_name.IsReadOnly)
            {
                bd_id.Visibility = Visibility.Collapsed;
                btn_delete.Visibility = Visibility.Collapsed;
                bd_idBrand.Visibility = Visibility.Collapsed;
                txtb_name.IsReadOnly =false;
                txtb_name.Text = "";
            }
            else
            {
                if (txtb_name.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                }
                else
                {
                    if (CarBrandDAO.Instance.GetIDBrandByName(txtb_name.Text, gara) != "")
                    {
                        MessageBox.Show("Hãng xe này đã tồn tại. Vui lòng thử lại", "Thông báo");
                    }
                    else
                    {
                        if (CarBrandDAO.Instance.InsertCarBrand(txtb_name.Text, gara))
                        {
                            MessageBox.Show("Thêm hãng xe mới thành công", "Thông báo");                        
                            LoadListCarBrand();
                        }
                        else
                        {
                            MessageBox.Show("Thêm hãng xe mới thất bại. Vui lòng thử lại.", "Thông báo");
                        }
                    }
                }
            }
            
        }

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hãng xe này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (CarBrandDAO.Instance.DeleteCarBrand(txtb_id.Text, gara))
                {

                    MessageBox.Show("Xóa hãng xe thành công.", "Thông báo");
                    LoadListCarBrand();
                }
                else
                {
                    MessageBox.Show("Xóa hãng xe thất bại.");
                }    
            }    
            
        }
        private void LoadListCarBrand()
        {
            List<CarBrand> list = CarBrandDAO.Instance.LoadCarBrandList(gara);
            ds_carbrand.Children.Clear();
            bd_id.Visibility = Visibility.Visible;
            btn_delete.Visibility = Visibility.Visible;
            bd_idBrand.Visibility = Visibility.Visible;
            txtb_id.Text = list[0].IDBrand;
            txtb_name.Text = list[0].NameBrand;
            foreach (CarBrand carBrand in list)
            {
                itCarBrand it = new itCarBrand(carBrand);
                ds_carbrand.Children.Add(it);
                it.MouseDown += brand_MouseDown;
            }
        }

        private void brand_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtb_id.IsReadOnly = true;
            txtb_name.IsReadOnly = true;
            txtb_id.Text = (sender as itCarBrand).txtb_idCarBrand.Text;
            txtb_name.Text = (sender as itCarBrand).txtb_nameBrand.Text;
            bd_id.Visibility = Visibility.Visible;
        }
    }
}
