using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Principal;
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
    /// Interaction logic for crdRepair.xaml
    /// </summary>
    public partial class crdRepair : Window, repairDetailInterface
    {
        RepairPaymentBill rdi;
        string idCom;
        int quantity;
        decimal price, wage;
        private int stt = 1; /*biến để tạo stt*/
        string gara;
        private float totalPrice = 0; /*biến để tính tổng tiền*/
        private bool isChanged = false;
        private ReceptionForm receptionForm;
        private List<itRepairCardDetail> detailList = new List<itRepairCardDetail>();
        Customer cus;
        Staff staff;
        // tạo phiếu mới
        public crdRepair(string gara, Staff staff)
        {
            InitializeComponent();
            this.Opacity = 0;
            bd_add.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;
            this.gara = gara;
            this.staff = staff;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        // mở phiếu đã có
        public crdRepair(string maphieu, string gara, Staff staff)
        {
            InitializeComponent();
            this.gara = gara;
            this.staff = staff;
            tbx_add.Text = "Sửa";
            tbl_IDRec.IsReadOnly = true;
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

        

        //thêm dòng chi tiết sửa chữa
        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
<<<<<<< HEAD
            //if (!isChanged)
            //{
            //    isChanged = true;
            //    itRepairCardDetail item = new itRepairCardDetail();
            //    item.CreateNewRepairCardDetail(stt, tbl_IDRec.Text);
            //    stt++;
            //    ds_suachua.Children.Add(item);
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng lưu thông tin trước khi thêm bản ghi mới");
            //}
            if (tbx_NumberPlate.Text != "")
            {
                if (tbx_add.Text == "Sửa")// nghhĩa là phiếu đã có
                {
                    
                    btn_delete.Visibility = Visibility.Visible;
                    for (int i = 0; i < ds_suachua.Children.Count; i++)
                    {
                        itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                        child.EnableEditing();
                    }
                    // code 
                    tbx_pay.Text = "Lưu";
                    tbx_add.Text = "Thêm";
                }
                else // đang sửa phiếu
                {
                    
                    crdRepairComponent component = new crdRepairComponent(gara, this as repairDetailInterface);
                    component.bd_save.MouseDown += Bd_save_MouseDown;
                    component.bd_delete.MouseDown += Bd_delete_MouseDown;
                    component.ShowDialog();
                    for (int i = 0; i < ds_suachua.Children.Count; i++)
                    {
                        itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                        child.DisableEditing();
                    }
                    
                }
                
=======
            if (!isChanged)
            {
                isChanged = true;
                crdRepairComponent component = new crdRepairComponent(gara, this as repairDetailInterface);
                component.bd_save.MouseDown += Bd_save_MouseDown;
                component.bd_delete.MouseDown += Bd_delete_MouseDown;
                component.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng lưu thông tin trước khi thêm bản ghi mới");
>>>>>>> 8456ff37946a73bc816e53bd413f76c6881c5993
            }
        }

        public void Bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (idCom != "" && quantity > 0)
            {
                itRepairCardDetail it = new itRepairCardDetail(gara, tbl_IDRec.Text, idCom, stt, price, quantity, wage);
                stt++;
                ds_suachua.Children.Add(it);
            }
        }

        public void Bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //Nút lưu, sửa
        private void bd_modify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_pay.Text =="Lưu")
            {
                // code
                SaveRepairCardDetails();
                isChanged = false;
                tbx_add.Text = "Sửa";
                tbx_pay.Text = "Thanh toán";
            }   
            else
            {
                crdReceipt crdReceipt = new crdReceipt(gara,staff, cus, decimal.Parse(tbl_totalPayment.Text),tbl_IDRec.Text);
                crdReceipt.ShowDialog();
            }    
        }

        //Lấy thông tin từ database theo IDREC
        private void LoadRepairCardDetails(string id)
        {
            if (ReceptionFormDAO.Instance.LoadReceptionFormByID(id, gara) != null)
            {
                stt = 1;
                totalPrice = 0;
                string numberPlate = "", recDate = "";
                detailList = RepairPaymentDetailDAO.Instance.LoadItRepairCardDetail(id);
                receptionForm = ReceptionFormDAO.Instance.LoadReceptionFormByID(id, gara);
                tbx_NumberPlate.Text = receptionForm.NumberPlate;
                dpk_RecDate.Text = receptionForm.ReceptionDate.ToString();
                foreach (itRepairCardDetail item in detailList)
                {
                    ds_suachua.Children.Add(item);
                    totalPrice = totalPrice + float.Parse(item.tbx_total.Text.ToString());
                    stt++;
                }
                tbl_totalPayment.Text =((int) totalPrice).ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy mã phiếu sửa chữa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        //Lưu thông tin vào database
        private void SaveRepairCardDetails()
        {
            bool status = true;
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                if (child.isValid())
                {
                    if (child.isExist())
                    {
<<<<<<< HEAD
                        int amount, wage, price;
                        if (!(int.TryParse(child.tbx_quantity.Text, out amount) && int.TryParse(child.tbx_price.Text, out price) &&
                            int.TryParse(child.tbx_wage.Text, out wage)))
=======
                        int amount;
                        decimal wage, price;
                        if (!(int.TryParse(child.tbx_quantity.Text,out amount) && decimal.TryParse(child.tbx_price.Text, out price) &&
                            decimal.TryParse(child.tbx_wage.Text, out wage)))
>>>>>>> 8456ff37946a73bc816e53bd413f76c6881c5993
                        {
                            MessageBox.Show("Tiền công, đơn giá và số lượng phải là số nguyên dương.", "Thông báo");

                        }
                        else
                        {
                            if (wage <= 0 || amount <= 0 || price <= 0)
                            {
                                MessageBox.Show("Tiền công, đơn giá và số lượng phải là số nguyên dương.", "Thông báo");

                            }
                            else
                            {
                                if (!RepairPaymentDetailDAO.Instance.UpdateRepairCardDetail(child.GetRPDOrdinalNum(), child))
                                    status = false;
                            }
                        }
                    }
                    else
                    {
                        if (!RepairPaymentDetailDAO.Instance.InsertRepairCardDetail(tbl_IDRec.Text, child))
                            status = false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin vật tư ở dòng: " + (i + 1).ToString());
                }
            }
            if (status)
            {
                ds_suachua.Children.Clear();
                LoadRepairCardDetails(tbl_IDRec.Text);
                MessageBox.Show("Cập nhật phiếu sửa chữa thành công!","Thông báo");
                
            }
            else
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            ds_suachua.Children.Clear();
            LoadRepairCardDetails(tbl_IDRec.Text);
        }

        //Xóa các dòng dữ liệu đã chọn 
        private void DeleteRepairCardDetails()
        {
            bool status = true;
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                if(child.deleteThis == true)
                {
                    if (!RepairPaymentDetailDAO.Instance.DeleteRepairCardDetail(child.GetRPDOrdinalNum()))
                    {
                        MessageBox.Show("Lỗi ở dòng: " + (i + 1).ToString());
                        status = false; 
                    }
                }
            }
            if (status)
            {
                ds_suachua.Children.Clear();
                LoadRepairCardDetails(tbl_IDRec.Text);
<<<<<<< HEAD
                MessageBox.Show("Cập nhật phiếu sửa chữa thành công! ", "Thông báo");
                
=======
                isChanged = false;
                if (MessageBox.Show("Cập nhật phiếu sửa chữa thành công! Bạn có muốn in phiếu sửa chữa không?",
                    "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PrintRepairPaymentBill();
                    this.Close();
                }
>>>>>>> 8456ff37946a73bc816e53bd413f76c6881c5993
            }
            else
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            ds_suachua.Children.Clear();
            LoadRepairCardDetails(tbl_IDRec.Text);
            isChanged = false;
            bd_add.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;
            tbx_modify.Text = "Sửa";
        }

        //Sau khi nhập IDRec nhấn enter để load chi tiết sửa chữa tương ứng
        private void tbl_IDRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ds_suachua.Children.Clear();
                LoadRepairCardDetails(tbl_IDRec.Text);
            }
        }

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
<<<<<<< HEAD
            DeleteRepairCardDetails();

            tbx_pay.Text = "Thanh toán";
        }

        private void tbl_IDRec_TextChanged(object sender, TextChangedEventArgs e)
        {
            string idCus = ReceptionFormDAO.Instance.LoadReceptionFormByID(tbl_IDRec.Text).IDCus;
            cus = CustomerDAO.Instance.LoadCustomerByID(idCus, gara);
        }

        private void ds_suachua_LayoutUpdated(object sender, EventArgs e)
        {
            int total=0;
            List <itRepairCardDetail> list = RepairPaymentDetailDAO.Instance.LoadItRepairCardDetail(tbl_IDRec.Text);
            foreach (itRepairCardDetail item in list)
            {
                total += int.Parse(item.tbx_total.Text);
            }
            tbl_totalPayment.Text = total.ToString();
=======
            bool isSelected = false;
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                if (child.deleteThis == true)
                {
                    isSelected = true;
                }
            }
            if (!isSelected)
                return;
            MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa các chi tiết đã chọn không?", "Thông báo", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DeleteRepairCardDetails();
            }
>>>>>>> 8456ff37946a73bc816e53bd413f76c6881c5993
        }

        public void ReceivedData(string idCom, decimal price, int quantity, decimal wage)
        {
            this.idCom = idCom;
            this.price = price;
            this.quantity = quantity;
            this.wage = wage;
        }
    }
}
