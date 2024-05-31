using System;
using Gara_Management.GUI.Card;
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
using Gara_Management.DTO;
using Gara_Management.DAO;

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itStockIn.xaml
    /// </summary>
    public partial class itStockIn : UserControl
    {
        GoodReceivedNote grn;
        public itStockIn(GoodReceivedNote grn)
        {
            InitializeComponent();
            this.grn = grn;
            Supplier supplier = SupplierDAO.Instance.GetSupplierByID(grn.Supplier);
        }

        private void bd_StockInInfor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn(grn);
            crdStockIn.ShowDialog();
        }
    }
}
