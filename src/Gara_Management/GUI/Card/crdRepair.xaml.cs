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
using System.Windows.Media.Animation;
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
        string gara;
        private float totalPrice = 0; /*biến để tính tổng tiền*/
        private bool isChanged = false;
        private ReceptionForm receptionForm;
        private List<itRepairCardDetail> detailList = new List<itRepairCardDetail>();

        // tạo phiếu mới
        public crdRepair(string gara)
        {
            InitializeComponent();
            this.Opacity = 0;
            bd_add.Visibility = Visibility.Hidden;
            btn_delete.Visibility = Visibility.Hidden;
            this.gara = gara;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        // mở phiếu đã có
        public crdRepair(string maphieu, string gara)
        {
            InitializeComponent();
            this.gara = gara;
            bd_add.Visibility = Visibility.Hidden;
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
            if (tbx_NumberPlate.Text != "")
            {
                if (tbx_modify.Text == "Sửa")// nghhĩa là phiếu đã có
                {
                    bd_add.Visibility = Visibility.Visible;
                    btn_delete.Visibility = Visibility.Visible;
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
                    for (int i = 0; i < ds_suachua.Children.Count; i++)
                    {
                        itRepairCardDetail child = (itRepairCardDetail)ds_suachua.Children[i];
                        child.DisableEditing();
                    }
                    // code
                    SaveRepairCardDetails();
                    isChanged = false;
                    bd_add.Visibility = Visibility.Hidden;
                    btn_delete.Visibility= Visibility.Hidden;
                    tbx_modify.Text = "Sửa";
                }
            }
        }

        //Lấy thông tin từ database theo IDREC
        private void LoadRepairCardDetails(string id)
        {
            if (ReceptionFormDAO.Instance.LoadReceptionFormByID(id) != null)
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
            else
            {
                MessageBox.Show("Không tìm thấy mã phiếu sửa chữa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        int amount, wage, price;
                        if (!(int.TryParse(child.tbx_quantity.Text,out amount) && int.TryParse(child.tbx_price.Text, out price) && 
                            int.TryParse(child.tbx_wage.Text, out wage)))
                        {
                            MessageBox.Show("Tiền công, đơn giá và số lượng phải là số nguyên dương.", "Thông báo");
                            return;
                        }    
                        if (wage<=0|| amount<=0 || price<=0)
                        {
                            MessageBox.Show("Tiền công, đơn giá và số lượng phải là số nguyên dương.", "Thông báo");
                            return;
                        }    
                        if (!RepairPaymentDetailDAO.Instance.UpdateRepairCardDetail(child.GetRPDOrdinalNum(), child))
                            status = false;
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
                if (MessageBox.Show("Cập nhật phiếu sửa chữa thành công! Bạn có muốn in phiếu sửa chữa không?", 
                    "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PrintRepairPaymentBill();
                    this.Close();
                }    
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
                        status = false;
                }
            }
            if (status)
            {
                ds_suachua.Children.Clear();
                LoadRepairCardDetails(tbl_IDRec.Text);
                if (MessageBox.Show("Cập nhật phiếu sửa chữa thành công! Bạn có muốn in phiếu sửa chữa không?",
                    "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PrintRepairPaymentBill();
                    this.Close();
                }
            }
            else
                MessageBox.Show("Cập nhật phiếu sửa chữa thất bại!");
            ds_suachua.Children.Clear();
            LoadRepairCardDetails(tbl_IDRec.Text);
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
                DeleteRepairCardDetails();
                bd_add.Visibility = Visibility.Hidden;
                btn_delete.Visibility = Visibility.Hidden;
                tbx_modify.Text = "Sửa";
            }
        }
    }
