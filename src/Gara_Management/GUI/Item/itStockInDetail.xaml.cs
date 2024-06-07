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
        GRNDetail detail;
        public itStockInDetail()
        {
            InitializeComponent();
        }
        public itStockInDetail(GRNDetail detail)
        {
            InitializeComponent();
            this.detail = detail;
            txtb_orderednum.Text=this.detail.GRNOrdinalNum.ToString();
            txtb_idStock.Text = this.detail.IDCom;
            txtb_price.Text = this.detail.ComPrice.ToString();
            txtb_amount.Text=this.detail.ComQuantity.ToString();
            object nameCom = DataProvider.Instance.ExecuteScalar("SELECT NAME_COM from CAR_COMPONENTS WHERE ID_COM='" + this.detail.IDCom + "'");
            //txtb_name.Text = nameCom.ToString();
            txtb_sumofmoney.Text=this.detail.GRNTotalPayment.ToString();
        }
    }
}
