using Gara_Management.DAO;
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
            txtb_price.Text =((int) this.com.CurPrice).ToString();
            txtb_amount.Text = this.com.ComQuantity.ToString();
        }

        private void bd_addSupplies_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // hiển thị phiếu đăng kí mua vật tư
            crdStockIn crdStockIn = new crdStockIn(com.IDCom, gara, account);// ví dụ
            crdStockIn.ShowDialog();
            Account acc = account;
            string gr = gara;
            Panel panel = (Panel)this.Parent;
            panel.Children.Clear();
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gr);
            foreach (CarComponent com in list)
            {
                itSupplies it = new itSupplies(com, gara, acc);
                panel.Children.Add(it);
            }

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            cardComponentDetail componentDetail=new cardComponentDetail(com, gara);
            componentDetail.ShowDialog();
            Account acc = account;
            string gr = gara;
            Panel panel = (Panel)this.Parent;
            panel.Children.Clear();
            List<CarComponent> list = CarComponentDAO.Instance.LoadCarComponentList(gr);
            foreach (CarComponent com in list)
            {
                itSupplies it = new itSupplies(com, gara, acc);
                panel.Children.Add(it);
            }
        }
    }
}
