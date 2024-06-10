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

        string gara;
        GoodReceivedNote grn;
        public itStockIn(GoodReceivedNote grn, string gara)
        {
            InitializeComponent();
            this.grn = grn;
            this.gara = gara;
            txtb_idLot.Text = this.grn.LotNumber;
            txtb_namesupplier.Text = grn.Supplier;
            txtb_date.Text = this.grn.ImportTime.ToString();
            txtb_sumofmoney.Text =((int) this.grn.TotalPaymentGRN).ToString();
            
        }

        private void bd_StockInInfor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdStockIn crdStockIn = new crdStockIn(grn);
            crdStockIn.ShowDialog();
            Panel panel = ((Panel)this.Parent);
            panel.Children.Clear();
            List<GoodReceivedNote> list = GoodReceivedNoteDAO.Instance.LoadGoodReceivedNoteList(gara);
            foreach (GoodReceivedNote note in list)
            {
                itStockIn it = new itStockIn(note, gara);
                panel.Children.Add(it);
            }

        }
    }
}
