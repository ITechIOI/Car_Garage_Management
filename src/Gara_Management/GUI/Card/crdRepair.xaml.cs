using Gara_Management.DAO;
using Gara_Management.DTO;
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
        private bool isChanged = false;
        private ReceptionForm receptionForm;
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
            bd_add.Visibility = Visibility.Hidden;
            tbl_IDRec.Text = maphieu;
            LoadRepairCardDetails(tbl_IDRec.Text);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void bd_exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isChanged)
            {
                MessageBoxResult result = MessageBox.Show("Thoát và không lưu?", "Thông tin chưa được lưu!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                    this.Close();
                return;
            }
            this.Close();
        }

        //
        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //thêm dòng chi tiết sửa chữa
        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isChanged)
            {
                isChanged = true;
                itRepairCardDetail item = new itRepairCardDetail();
                item.CreateNewRepairCardDetail(stt, tbl_IDRec.Text);
                stt++;
                ds_suachua.Children.Add(item);
            }
            else
            {
                MessageBox.Show("Vui lòng lưu thông tin trước khi thêm bản ghi mới");
            }
        }

        //Nút lưu, sửa
        private void bd_modify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_modify.Text == "Sửa")// nghhĩa là phiếu đã có
            {
                bd_add.Visibility = Visibility.Visible;
                for (int i = 0; i < ds_suachua.Children.Count; i++)
                {
                    itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                    child.EnableEditing();
                }
                // code 
                tbx_modify.Text = "Lưu";
            }
            else // đang sửa phiếu
            {
                bd_add.Visibility = Visibility.Hidden;
                for (int i = 0; i < ds_suachua.Children.Count; i++)
                {
                    itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                    child.DisableEditing();
                }
                // code
                SaveRepairCardDetails();
                isChanged = false;
                tbx_modify.Text = "Sửa";
            }
        }

        //Lấy thông tin từ database theo IDREC
        private void LoadRepairCardDetails(string id)
        {
            stt = 1;
            totalPrice = 0;
            string numberPlate = "", recDate = "";
            detailList = RepairPaymentDetailDAO.Instance.LoadItRepairCardDetail(id);
            receptionForm = ReceptionFormDAO.Instance.LoadReceptionFormByID(id);
            tbx_NumberPlate.Text = receptionForm.NumberPlate;
            dpk_RecDate.Text = receptionForm.ReceptionDate.ToString();
            foreach (itRepairCardDetail item in detailList)
            {
                ds_suachua.Children.Add(item);
                totalPrice = totalPrice + float.Parse(item.tbx_total.Text.ToString());
                stt++;
            }
            tbl_totalPayment.Text = totalPrice.ToString("N");
        }


        //Lưu thông tin vào database
        public void SaveRepairCardDetails()
        {
            bool status = true;
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                if (child.isValid())
                {
                    if (child.isExist())
                    {
                        if (!RepairPaymentDetailDAO.Instance.UpdateRepairCardDetail(child.GetRPDOrdinalNum(), child))
                            status = false;
                    }
                    else
                    {
                        if (!RepairPaymentDetailDAO.Instance.UpdateRepairCardDetail(tbl_IDRec.Text, child))
                            status = false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin vật tư ở dòng: " + (i + 1).ToString());
                }
            }
            if (status)
                MessageBox.Show("Cập nhật phiếu sửa chữa thành công!");
            else
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            ds_suachua.Children.Clear();
            LoadRepairCardDetails(tbl_IDRec.Text);
        }
    }
}
