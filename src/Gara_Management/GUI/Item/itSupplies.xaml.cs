using Gara_Management.DTO;
using Gara_Management.GUI.Card;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itSupplies.xaml
    /// </summary>
    public partial class itSupplies : UserControl
    {
        CarComponent com;
        string gara;
        Account account;
        public itSupplies(CarComponent com, string gara, Account account)
        {
            InitializeComponent();
            this.com = com;
            this.gara = gara;
            this.account = account;
            txtb_idComponent.Text = this.com.IDCom;
            txtb_nameComponent.Text = this.com.NameCom;
            txtb_price.Text = this.com.CurPrice.ToString();
            txtb_amount.Text = this.com.ComQuantity.ToString();
        }

        private void bd_addSupplies_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // hiển thị phiếu đăng kí mua vật tư
            crdStockIn crdStockIn = new crdStockIn(com.IDCom, gara, account);// ví dụ
            crdStockIn.ShowDialog();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardComponentDetail componentDetail=new cardComponentDetail();
            componentDetail.ShowDialog();
        }
    }
}
