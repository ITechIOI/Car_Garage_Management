using Gara_Management.DAO;
using Gara_Management.DTO;
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
    public partial class itStockInDetail : UserControl
    {
        public GRNDetail detail;
        string gara;
        bool check = false;
        public itStockInDetail(int i, string gara)
        {
            InitializeComponent();
            txtb_orderednum.Text = i.ToString();
            this.gara = gara;
            check = true;
        }
        public itStockInDetail(GRNDetail detail, string gara)
        {
            InitializeComponent();
            this.detail = detail;
            this.gara = gara;
            txtb_orderednum.Text=this.detail.GRNOrdinalNum.ToString();
            txtb_idStock.Text = this.detail.IDCom;
            check = true;
            txtb_price.Text = this.detail.ComPrice.ToString();
            txtb_amount.Text=this.detail.ComQuantity.ToString();
            object nameCom = DataProvider.Instance.ExecuteScalar("SELECT NAME_COM from CAR_COMPONENTS WHERE ID_COM='" + this.detail.IDCom + "'");
            //txtb_name.Text = nameCom.ToString();
            txtb_sumofmoney.Text=this.detail.GRNTotalPayment.ToString();
        }

        private void txtb_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount;
            decimal price;
            if (int.TryParse(txtb_amount.Text, out amount) && decimal.TryParse(txtb_price.Text, out price))
            {
                txtb_sumofmoney.Text = (amount * price).ToString();
            }
               
        }

        private void txtb_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            int amount;
            decimal price;
            if (int.TryParse(txtb_amount.Text, out amount) && decimal.TryParse(txtb_price.Text, out price))
            {
                txtb_sumofmoney.Text = (amount * price).ToString();
            }
            
        }

        private void txtb_idStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (check && txtb_idStock.Text != null)
            {
                if (CarComponentDAO.Instance.GetCarComponentByID(txtb_idStock.Text, gara) != null)
                    txtb_name.Text = CarComponentDAO.Instance.GetCarComponentByID(txtb_idStock.Text, gara).NameCom;
                else
                    MessageBox.Show("Phụ tùng không tồn tại.");
            }
        }
    }
}
