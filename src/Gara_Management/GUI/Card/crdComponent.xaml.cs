using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for crdComponent.xaml
    /// </summary>
    public partial class crdComponent : Window
    {
        private readonly grnInterface grn;

        string gara;
        public crdComponent(string gara, grnInterface grnInterface)
        {
            InitializeComponent();
            tbx_delete.Text = "Hủy";
            this.gara = gara;
            this.grn = grnInterface;
            LoadListComponentToComboBox();
        }
        public crdComponent(grnInterface grn, string gara, string id, string amount, string price)
        {
            InitializeComponent();
            tbx_delete.Text = "Xóa";
            tbx_save.Text = "Cập nhật";
            this.gara = gara;
            this.grn = grn;
            LoadListComponentToComboBox();
            cbx_component.SelectedItem = id;
            cbx_component.IsEnabled = false;
            bd_save.Visibility = Visibility.Collapsed;
            txtb_amount.Text = amount;
            txtb_price.Text = price;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cbx_component.SelectedItem == null || txtb_amount.Text == "" || txtb_price.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
            }
            else
            {
                int amount;
                if (!int.TryParse(txtb_amount.Text, out amount))
                {
                    MessageBox.Show("Số lượng phải là một số nguyên");

                }
                else
                {
                    int price;
                    if (!int.TryParse(txtb_price.Text, out price))
                    {
                        MessageBox.Show("Đơn giá phải là một số nguyên");

                    }
                    else
                    {
                        string idCom = CarComponentDAO.Instance.GetComponentIDByName(gara, cbx_component.SelectedItem.ToString());
                        grn.ReceivedData(idCom, price, amount);
                        this.Close();
                    }    
                }
            }
            
        }

        private void bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_delete.Text == "Xóa")
            {
                if (MessageBox.Show("Bạn có muốn xóa phụ tùng này trong danh sách nhập không?", "Thông báo", MessageBoxButton.YesNo)
                    == MessageBoxResult.Yes)
                {
                    string idCom = CarComponentDAO.Instance.GetComponentIDByName(gara, cbx_component.SelectedItem.ToString());
                    grn.ReceivedData(idCom, 0, 0);
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }    

        }
        private void LoadListComponentToComboBox()
        {
            List<CarComponent> carComponents = CarComponentDAO.Instance.LoadCarComponentList(gara);
            cbx_component.Items.Clear();
            foreach (CarComponent carComponent in carComponents)
            {
                cbx_component.Items.Add(carComponent.NameCom);
            }
        }

        private void txtb_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            bd_save.Visibility = Visibility.Visible;
            if (txtb_price.Text!="" && txtb_amount.Text != "")
            {
                int price, amount;
                if (int.TryParse(txtb_amount.Text, out amount) && int.TryParse(txtb_price.Text, out price))
                {
                    txtb_total.Text = (price * amount).ToString(); 
                }    
            }    
        }
    }
}
