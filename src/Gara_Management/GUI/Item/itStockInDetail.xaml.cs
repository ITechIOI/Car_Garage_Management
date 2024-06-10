using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Card;
using System;
using System.Collections;
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

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itStockInDetail.xaml
    /// </summary>
    public partial class itStockInDetail : UserControl, grnInterface
    {
        public GRNDetail detail;
        string gara;
        string id;


        public itStockInDetail( int i, string gara, string id, int price, int amount)
        {
            InitializeComponent();
            txtb_orderednum.Text = i.ToString();
            this.id = id;
            this.gara = gara;
            txtb_amount.Text = amount.ToString();
            txtb_price.Text = price.ToString();
            txtb_idStock.Text = id;
            object nameCom = DataProvider.Instance.ExecuteScalar("SELECT NAME_COM from CAR_COMPONENTS WHERE ID_COM='" + id + "'");
            txtb_name.Text = nameCom.ToString();
        }
        public itStockInDetail(GRNDetail detail, string gara, int i)
        {
            InitializeComponent();
            this.detail = detail;
            this.gara = gara;
            this.id = detail.IDCom;
            txtb_orderednum.Text=this.detail.GRNOrdinalNum.ToString();
            txtb_idStock.Text = this.detail.IDCom;
            txtb_price.Text =((int) this.detail.ComPrice).ToString();
            txtb_amount.Text=this.detail.ComQuantity.ToString();
            object nameCom = DataProvider.Instance.ExecuteScalar("SELECT NAME_COM from CAR_COMPONENTS WHERE ID_COM='" + this.detail.IDCom + "'");
            txtb_name.Text = nameCom.ToString();
            txtb_sumofmoney.Text=((int)this.detail.GRNTotalPayment).ToString();
        }

        private void txtb_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount;
            decimal price = 0;
            if (int.TryParse(txtb_amount.Text, out amount) && decimal.TryParse(txtb_price.Text, out price))
            {
                txtb_sumofmoney.Text = (amount * price).ToString();
            }

        }

        private void txtb_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount;
            decimal price = 0;
            if (int.TryParse(txtb_amount.Text, out amount) && decimal.TryParse(txtb_price.Text, out price))
            {
                txtb_sumofmoney.Text = (amount * price).ToString();
            }
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdComponent component = new crdComponent(this as grnInterface, gara, txtb_name.Text, txtb_amount.Text, txtb_price.Text);
            component.bd_save.MouseDown += Bd_save_MouseDown;
            if (this.detail != null)
            {
                component.bd_delete.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                component.bd_delete.MouseDown += Bd_delete_MouseDown;
            }    
            component.Show();
        }

        private void Bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int j = -1;
            foreach (UIElement item in ((Panel)this.Parent).Children)
            {
                if (item is itStockInDetail detail)
                {
                    if (detail.txtb_idStock.Text == this.txtb_idStock.Text)
                    {
                        j = int.Parse(detail.txtb_orderednum.Text);
                    }
                    if (j != -1 && int.Parse(detail.txtb_orderednum.Text) > j)
                    {
                        detail.txtb_orderednum.Text = (int.Parse(detail.txtb_orderednum.Text) - 1).ToString();
                    }
                }
            }
            ((Panel)this.Parent).Children.Remove(this);
        }

        private void Bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        public void ReceivedData(string idCom, int price, int quantity)
        {
            txtb_amount.Text = quantity.ToString();
            txtb_price.Text = price.ToString();
        }
    }
}
