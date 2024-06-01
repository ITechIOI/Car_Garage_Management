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
using System.Windows.Media.Animation;
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
            this.Opacity = 0;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        // xem phiếu đã có
        public crdStockIn(GoodReceivedNote grn)
        {
            InitializeComponent();
            Supplier supplier = SupplierDAO.Instance.GetSupplierByID(grn.Supplier);
            txtb_idLot.Text = grn.LotNumber;
            txtb_date.Text = grn.ImportTime.ToString();
            txtb_namesupplier.Text = supplier.NameSupplier;

            decimal price = 0;
            List<GRNDetail> list = GRNDetailDAO.Instance.LoadGRNDetailListByLotNumber(grn.LotNumber);
            foreach (GRNDetail detail in list)
            {
                itStockInDetail it = new itStockInDetail(detail);
                price = price + detail.GRNTotalPayment;
                ds_nhapkho.Children.Add(it);
            }
            txtb_totalsum.Text = price.ToString();
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
