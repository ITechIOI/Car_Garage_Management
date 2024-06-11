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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Card
{
    /// <summary>
    /// Interaction logic for crdRepairComponent.xaml
    /// </summary>
    public partial class crdRepairComponent : Window
    {
        private readonly repairDetailInterface rdi;
        string gara;

        public crdRepairComponent(string gara, repairDetailInterface rdi)
        {
            InitializeComponent();
            tbx_delete.Text = "Hủy";
            this.gara = gara;
            this.rdi = rdi;
            LoadListComponentToComboBox();
        }

        public crdRepairComponent(repairDetailInterface rdi, string gara, string name, string quantity, string price, string wage)
        {
            InitializeComponent();
            tbx_delete.Text = "Xóa";
            tbx_save.Text = "Cập nhật";
            this.gara = gara;
            this.rdi = rdi;
            LoadListComponentToComboBox();
            cbx_component.SelectedItem = name;
            cbx_component.IsEnabled = false;
            bd_save.Visibility = Visibility.Collapsed;
            txtb_quantity.Text = quantity;
            txtb_price.Text = price;
            txtb_wage.Text = wage;
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

        private void LoadListComponentToComboBox()
        {
            List<CarComponent> carComponents = CarComponentDAO.Instance.LoadCarComponentList(gara);
            cbx_component.Items.Clear();
            foreach (CarComponent carComponent in carComponents)
            {
                cbx_component.Items.Add(carComponent.NameCom);
            }
        }

        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cbx_component.SelectedItem == null || txtb_quantity.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
            }
            else
            {
                int quantity;
                if (!int.TryParse(txtb_quantity.Text, out quantity))
                {
                    MessageBox.Show("Số lượng phải là một số nguyên", "Thông báo");
                }
                else
                {
                    if (quantity <= 0)
                    {
                        MessageBox.Show("Số lượng phải là một số nguyên dương.", "Thông báo");
                    }
                    else
                    {
                        string idCom = CarComponentDAO.Instance.GetComponentIDByName(gara, cbx_component.SelectedItem.ToString()).Trim();
                        rdi.ReceivedData(idCom, decimal.Parse(txtb_price.Text), int.Parse(txtb_quantity.Text), decimal.Parse(txtb_wage.Text));
                        this.Close();
                    }
                }
            }
        }

        private void bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void cbx_component_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = cbx_component.SelectedItem.ToString();
            CarComponent component = CarComponentDAO.Instance.LoadCarComponentListByName(gara, name)[0];
            txtb_price.Text = component.CurPrice.ToString();
            txtb_wage.Text = component.Wage.ToString();
        }
    }
}
