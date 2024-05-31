using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Item;
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
    /// Interaction logic for crdStockIn.xaml
    /// </summary>
    public partial class crdStockIn : Window
    {

        // tùy vào cách dùng mà hiển thị sẽ khác nhau
        // phiếu mới
        public crdStockIn()
        {
            InitializeComponent();

        }
        // xem phiếu đã có
        public crdStockIn(GoodReceivedNote grn)
        {
            InitializeComponent();
            Supplier supplier = SupplierDAO.Instance.GetSupplierByID(grn.Supplier);
            List<GRNDetail> list = GRNDetailDAO.Instance.LoadGRNDetailListByLotNumber(grn.LotNumber);
            foreach (GRNDetail detail in list)
            {
                itStockInDetail it = new itStockInDetail(detail);
                ds_nhapkho.Children.Add(it);
            }
        }
        // hiển thị khi nhấn vào thêm 1 vật tư nào đó
        public crdStockIn(string maVatTu, string gara)
        {
            InitializeComponent();

            itStockInDetail it = new itStockInDetail();
            // thêm 
            ds_nhapkho.Children.Add(it);

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

        //
        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {


        }

        private void bd_pay_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        
    }
}
