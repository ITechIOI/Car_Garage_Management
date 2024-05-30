using Gara_Management.DAO;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for crdRepair.xaml
    /// </summary>
    public partial class crdRepair : Window
    {
        private int stt = 1; /*biến để tạo stt*/
        private float totalPrice = 0; /*biến để tính tổng tiền*/
        private bool isEditing = false;
        private List<itRepairCardDetail> detailList = new List<itRepairCardDetail>();

        // tạo phiếu mới
        public crdRepair()
        {
            InitializeComponent();
            bd_add.Visibility = Visibility.Hidden;
            tbl_IDRec.Text = "REC1";
            LoadRepairCardDetails(tbl_IDRec.Text);
        }

        // mở phiếu đã có
        public crdRepair(string maphieu)
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                itRepairCardDetail it = new itRepairCardDetail();
                ds_suachua.Children.Add(it);
            }
            ds_suachua.IsEnabled = false;
            bd_add.Visibility = Visibility.Hidden;

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

        //thêm dòng chi tiết sửa chữa
        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];

                if (child != null && !child.isValid())
                {
                    MessageBox.Show("thông báo gì đấy");
                    return;
                }
            }
            itRepairCardDetail item = new itRepairCardDetail();
            item.CreateNewRepairCardDetail(stt);
            stt++;
            ds_suachua.Children.Add(item);
        }

        private void bd_modify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_modify.Text == "Sửa")// nghhĩa là phiếu đã có
            {
                bd_add.Visibility = Visibility.Visible;
                // code 

                tbx_modify.Text = "Lưu";
            }
            else // đang sửa phiếu
            {
                bd_add.Visibility = Visibility.Hidden;
                // code

                tbx_modify.Text = "Sửa";
            }
        }

        //Lấy thông tin từ database theo IDREC
        private void LoadRepairCardDetails(string id)
        {
            string numberPlate = "", recDate = "";
            detailList = RepairPaymentDetailDAO.LoadItRepairCardDetail(id);
            tbx_NumberPlate.Text = detailList[0].numberPlate;
            dpk_RecDate.Text = detailList[0].recDate;
            foreach (itRepairCardDetail item in detailList)
            {
                ds_suachua.Children.Add(item);
                totalPrice = totalPrice + float.Parse(item.tbx_total.Text.ToString());
                stt++;
            }
            tbl_totalPayment.Text = totalPrice.ToString("N");
        }
    }
}
