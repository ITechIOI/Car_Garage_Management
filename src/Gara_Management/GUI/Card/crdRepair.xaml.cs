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
            tbl_IDRec.IsReadOnly = true;
            tbl_IDRec.Text = maphieu;
            LoadRepairCardDetails(tbl_IDRec.Text);
            if (detailList.Count == 0)
            {
                tbx_add.Text = "Thêm";
                tbx_pay.Text = "Lưu";
            }
            else
            {
                tbx_add.Text = "Sửa";
                tbx_pay.Text = "Thanh toán";
            }    

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
                    //isChanged = false;
                    //crdRepairComponent component = new crdRepairComponent(gara, this as repairDetailInterface);
                    //component.bd_save.MouseDown += Bd_save_MouseDown;
                    //component.bd_delete.MouseDown += Bd_delete_MouseDown;
                    //component.ShowDialog();
                    ////for (int i = 0; i < ds_suachua.children.count; i++)
                    ////{
                    ////    itrepaircarddetail child = (itrepaircarddetail)ds_suachua.children[i];
                    ////    child.disableediting();
                    ////}
                    if (!isChanged)
                    {
                        isChanged = false;
                        crdRepairComponent component1 = new crdRepairComponent(gara, this as repairDetailInterface);
                        component1.bd_save.MouseDown += Bd_save_MouseDown;
                        component1.bd_delete.MouseDown += Bd_delete_MouseDown;
                        component1.ShowDialog();
                        for (int i = 0; i < ds_suachua.Children.Count; i++)
                        {
                            itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                            child.DisableEditing();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng lưu thông tin trước khi thêm bản ghi mới");

                    }

                }
                
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
            isChanged = true;
        }

        public void Bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadRepairCardDetails(tbl_IDRec.Text);
            isChanged = true;
        }

        //Nút thanh toán, lưu
        private void bd_modify_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (tbx_pay.Text == "Lưu")
            {
                // code
                SaveRepairCardDetails();
                isChanged = false;
                tbx_add.Text = "Sửa";
                tbx_pay.Text = "Thanh toán";
            }
            else
            {
                if (MessageBox.Show("Bạn muốn thanh toán ngay hiện tại?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    crdReceipt crdReceipt = new crdReceipt(gara, staff, cus, decimal.Parse(tbl_totalPayment.Text), tbl_IDRec.Text);
                    crdReceipt.ShowDialog();
                    this.Close();
                }
                else
                {
                    RepairPaymentBillDAO.Instance.SetFinishBill(tbl_IDRec.Text);
                    if (MessageBox.Show("Lưu bill thành công. Bạn có muốn hóa đơn?", "Thông báo",
                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PrintRepairPaymentBill();
                    }
                    this.Close();
                }    
            }
        }
        private void PrintRepairPaymentBill()
        {
            CarGara carGara = CarGaraDAO.Instance.GetCarGaraByID(this.gara);
            FlowDocument flowDocument = new FlowDocument();
            Paragraph gara = new Paragraph();
            gara.Inlines.Add(new Run("GARA OTO\nĐịa chỉ: " + carGara.AddressGara + "\nSố điện thoại: " + carGara.PhoneNumberGara));
            gara.TextAlignment = TextAlignment.Center;
            gara.FontSize = 15;
            flowDocument.Blocks.Add(gara);

            Paragraph title = new Paragraph();
            title.Inlines.Add(new Run("PHIẾU SỬA CHỮA"));
            title.FontSize = 20;
            title.TextAlignment = TextAlignment.Center;
            title.FontWeight = FontWeights.Bold;
            flowDocument.Blocks.Add(title);

            Paragraph info = new Paragraph();
            DateTime date = DateTime.Parse(dpk_RecDate.SelectedDate.ToString());
            string sInfo = "Mã phiếu: " + tbl_IDRec.Text + "\nNgày sửa: " + dpk_RecDate.Text
                + "\nBiển số xe: " + tbx_NumberPlate.Text + "\nTổng tiền: " + tbl_totalPayment.Text;
            /*"Mã lô: " + txtb_idLot.Text + "\nNgày nhập: " + date.ToString("dd/MM/yyyy") + "\nNhà cung cấp: " +
            txtb_namesupplier.Text + "\nNgười kí nhận: " + txtb_staff.Text + "\nTổng tiền: " + txtb_totalsum.Text;*/
            info.Inlines.Add(sInfo);
            info.Margin = new Thickness(15);
            flowDocument.Blocks.Add(info);

            Table table = new Table();
            table.FontSize = 14;
            table.Columns.Add(new TableColumn() { Width = new GridLength(40) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(180) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(140) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(110) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(70) }); // Thêm cột            
            table.Columns.Add(new TableColumn() { Width = new GridLength(110) }); // Thêm cột            
            table.Columns.Add(new TableColumn() { Width = new GridLength(110) }); // Thêm cột            
            TableRowGroup gr = new TableRowGroup();
            TableRow titleRow = new TableRow();
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("STT")))); // Ô đầu tiên
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Nội dung")))); // Ô thứ hai
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Vật tư"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Tiền công"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Thành tiền"))));
            gr.Rows.Add(titleRow);
            // Tạo Hàng và Ô
            //List<itStockInDetail> list = new List<itStockInDetail>();
            foreach (itRepairCardDetail item in detailList)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbl_stt.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_description.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_name.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_price.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_quantity.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_wage.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.tbx_total.Text))));

                gr.Rows.Add(row);
            }
            table.RowGroups.Add(gr);
            flowDocument.ColumnWidth = 830;
            flowDocument.Blocks.Add(table);

            // Mở hộp thoại chọn máy in
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // In hóa đơn
                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "PHIẾU SỬA CHỮA");

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
                tbl_totalPayment.Text = ((int)totalPrice).ToString();
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
                if (!child.isValid())
                {
                    MessageBox.Show("Không tìm thấy thông tin vật tư ở dòng: " + (i + 1).ToString(), "Thông báo");
                }
                else
                {
                    if (child.isExist())
                    {
                        int amount, wage, price;
                        if (!(int.TryParse(child.tbx_quantity.Text, out amount) && int.TryParse(child.tbx_price.Text, out price) &&
                            int.TryParse(child.tbx_wage.Text, out wage)))

                        {
                            MessageBox.Show("Tiền công, đơn giá và số lượng phải là số nguyên dương.", "Thông báo");

                        }
                        else
                        {
                            if (wage <= 0 || amount <= 0 || price < 0)
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
            }
            if (status)
            {
                ds_suachua.Children.Clear();
                LoadRepairCardDetails(tbl_IDRec.Text);
                MessageBox.Show("Cập nhật phiếu sửa chữa thành công!", "Thông báo");
            }
            else
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            
        }

        //Xóa các dòng dữ liệu đã chọn 
        private void DeleteRepairCardDetails()
        {
            bool status = true;
            for (int i = 0; i < ds_suachua.Children.Count; i++)
            {
                itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                if (child.deleteThis == true)
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
                MessageBox.Show("Cập nhật phiếu sửa chữa thành công! ", "Thông báo");
                isChanged = false;
            }
            else
            {
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            }

            ds_suachua.Children.Clear();
            LoadRepairCardDetails(tbl_IDRec.Text);
            isChanged = false;
            tbx_pay.Text = "Thanh toán";
            tbx_add.Text = "Sửa";
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
            {
                if (MessageBox.Show("Bạn có muốn xóa bill này không?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string idBill = RepairPaymentBillDAO.Instance.GetIDBillByIDRec(tbl_IDRec.Text);
                    if (RepairPaymentBillDAO.Instance.DeleteRepairPaymentBill(idBill))
                    {
                        MessageBox.Show("Xóa bill này thành công", "Thông báo");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Xóa bill không thành công hoặc bill này không tồn tại. Vui lòng thử lại.");
                    }
                }

            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa các chi tiết đã chọn không?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteRepairCardDetails();
                }
            }
        }

        private void tbl_IDRec_TextChanged(object sender, TextChangedEventArgs e)
        {
            string idCus = ReceptionFormDAO.Instance.LoadReceptionFormByID(tbl_IDRec.Text, gara).IDCus;
            cus = CustomerDAO.Instance.LoadCustomerByID(idCus, gara);
        }

        private void ds_suachua_LayoutUpdated(object sender, EventArgs e)
        {
            int total = 0;
            
            foreach (UIElement item in ds_suachua.Children)
            {
                if (item is itRepairCardDetail detail)
                {
                    total += int.Parse(detail.tbx_total.Text);
                }    
                
            }
            tbl_totalPayment.Text = total.ToString();
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

