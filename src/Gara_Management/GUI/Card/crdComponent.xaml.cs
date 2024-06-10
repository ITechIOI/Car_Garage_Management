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
    /// Interaction logic for crdComponent.xaml
    /// </summary>
    public partial class crdComponent : Window
    {
        bool delete = false;
        string gara;
        private static bool check = false;
        public crdComponent(string gara)
        {
            InitializeComponent();
            tbx_delete.Text = "Hủy";
            this.gara = gara;
            LoadListComponentToComboBox();
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

        }

        private void bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
              
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

        private void cbx_component_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string id  = CarComponentDAO.Instance.GetComponentIDByName(gara, cbx_component.SelectedItem.ToString());
            CarComponent component = CarComponentDAO.Instance.GetCarComponentByID(id, gara);
            if (component != null)
            {
                txtb_price.Text =((int) component.CurPrice).ToString();
                txtb_wage.Text =((int) component.Wage).ToString();
            }
        }
    }
}
