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
        int stt = 1;
        float totalPrice = 0;
        // tạo phiếu mới
        public crdRepair()
        {
            InitializeComponent();
            dpk_RecDate.Text = "22/03/2024";
            bd_modify.Visibility = Visibility.Hidden;
            LoadRepairCardDetails(tbl_IDRec.Text, tbx_NumberPlate.Text, Convert.ToDateTime(dpk_RecDate.ToString()));
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

        //Lấy thông tin từ database theo IDREC, Number plate, ngày tiếp nhận
        private void LoadRepairCardDetails(string id, string numberPlate, DateTime date)
        {
            string query = "EXEC USP_LOAD_REPAIR_CARD_DETAILS @ID_REC , @NUMBER_PLATES , @RECEPTION_DATE";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query, new object[] { id, numberPlate, date });
            foreach (DataRow row in dataTable.Rows)
            {
                itRepairCardDetail item = new itRepairCardDetail();
                item.SetValue(stt, row);
                ds_suachua.Children.Add(item);
                totalPrice = totalPrice + float.Parse(row["TOTAL_PRICE"].ToString());
                stt++;
            }
            tbl_totalPayment.Text = totalPrice.ToString("N");
        }
    }
}
