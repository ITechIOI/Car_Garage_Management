using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        GoodReceivedNote grn;
        string gara;
        Account acc;
        int i = 0;
        bool check = false;
        List<GRNDetail> list = new List<GRNDetail>();

        // tùy vào cách dùng mà hiển thị sẽ khác nhau
        // phiếu mới
        public crdStockIn(string gara, Account acc)
        {
            InitializeComponent();
            this.Opacity = 0;
            this.gara = gara;
            this.acc = acc;
            txtb_idLot.Text = "LOT" + (GoodReceivedNoteDAO.Instance.GetMaxLotNumber() + 1);
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(acc.IDStaff).NameStaff;
            txtb_date.SelectedDate = DateTime.Now;
            
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
            this.grn = grn;
            add_button.Text = "Sửa";
            bd_pay.Visibility = Visibility.Hidden;
            txtb_idLot.Text = grn.LotNumber;
            txtb_date.Text = grn.ImportTime.ToString();
            txtb_namesupplier.Text = grn.Supplier;
            this.gara = StaffDAO.Instance.GetStaffById(AccountDAO.Instance.GetIDStaffByIDAcc(grn.DataEntryStaff.ToString())).IDGara;
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(AccountDAO.Instance.GetIDStaffByIDAcc(grn.DataEntryStaff.ToString())).NameStaff;
            decimal price = 0;
            list.Clear();
            list = GRNDetailDAO.Instance.LoadGRNDetailListByLotNumber(grn.LotNumber);
            foreach (GRNDetail detail in list)
            {
                itStockInDetail it = new itStockInDetail(detail, gara);
                ds_nhapkho.Children.Add(it);
                price = price + detail.GRNTotalPayment;
            }
            txtb_totalsum.Text = price.ToString();
            i = ds_nhapkho.Children.Count;
        }
        // hiển thị khi nhấn vào thêm 1 vật tư nào đó
        public crdStockIn(string idComponent, string gara, Account acc)
        {
            InitializeComponent();
            this.gara = gara;
            this.acc = acc;
            txtb_idLot.Text = "LOT" + (GoodReceivedNoteDAO.Instance.GetMaxLotNumber() + 1);
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(acc.IDStaff).NameStaff;
            txtb_date.SelectedDate = DateTime.Now;
            i++;
            GRNDetail detail = new GRNDetail(i, txtb_idLot.Text, idComponent, 1000, 1, 1000, false);
            list.Clear();
            list.Add(detail);
            itStockInDetail it = new itStockInDetail(detail, gara);
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
            if (!check)
                InsertGoodReceivedNote();
            else
                UpdateGoodReceivedNote();
        }

        private void bd_add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (add_button.Text == "Thêm")
            {
                i++;
                itStockInDetail it = new itStockInDetail(i, gara);
                ds_nhapkho.Children.Add(it);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn cập nhật phiếu nhập này?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    add_button.Text = "Thêm";
                    check = true;
                    i = ds_nhapkho.Children.Count;
                    bd_pay.Visibility = Visibility.Visible;
                }
            }
        }
        public List<itStockInDetail> getListItem(StackPanel stack)
        {
            var list = new List<itStockInDetail>();
            foreach (UIElement item in stack.Children)
            {
                if (item is itStockInDetail detail)
                {
                    list.Add(detail);
                }   
            }
            return list;
        }
        private void InsertGoodReceivedNote()
        {
            if (txtb_namesupplier.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Cảnh báo");
            }    
            else
            {
                if (ds_nhapkho.Children.Count == 0)
                {
                    MessageBox.Show("Phiếu nhập phải có ít nhất một mặt hàng.", "Cảnh báo");
                }    
                else
                {
                    DateTime date = DateTime.Parse(txtb_date.SelectedDate.ToString());
                    bool res = GoodReceivedNoteDAO.Instance.InsertGoodReceivedNote(txtb_idLot.Text, txtb_namesupplier.Text, gara,
                        date.ToString("dd/MM/yyyy"), acc.IDAcc);
                    List<itStockInDetail> list = getListItem(ds_nhapkho);
                    foreach (itStockInDetail item in list)
                    {
                        res = res && GRNDetailDAO.Instance.InsertGRNDetail(txtb_idLot.Text,
                            CarComponentDAO.Instance.GetComponentIDByName(gara, item.txtb_name.Text),
                            decimal.Parse(item.txtb_price.Text), int.Parse(item.txtb_amount.Text));
                    }
                    if (res)
                    {
                        if (MessageBox.Show("Thêm phiếu nhập thành công. Bạn có muốn in phiếu nhập kho không?", 
                            "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            PrintGoodReceivedNote();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm phiếu nhập thất bại. Vui lòng thử lại.", "Thông báo");
                    }    

                }    
            }
        }
        private void PrintGoodReceivedNote()
        {
            CarGara carGara = CarGaraDAO.Instance.GetCarGaraByID(this.gara);
            FlowDocument flowDocument = new FlowDocument();
            Paragraph gara = new Paragraph();
            gara.Inlines.Add(new Run("GARA OTO\nĐịa chỉ: " + carGara.AddressGara + "\nSố điện thoại: " + carGara.PhoneNumberGara));
            gara.TextAlignment = TextAlignment.Center;
            gara.FontSize = 15;
            flowDocument.Blocks.Add(gara);

            Paragraph title = new Paragraph();
            title.Inlines.Add(new Run("PHIẾU NHẬP KHO"));
            title.FontSize = 20;
            title.TextAlignment = TextAlignment.Center;
            title.FontWeight = FontWeights.Bold;
            flowDocument.Blocks.Add(title);

            Paragraph info = new Paragraph();
            DateTime date = DateTime.Parse(txtb_date.SelectedDate.ToString());
            string sInfo = "Mã lô: " + txtb_idLot.Text +"\nNgày nhập: " + date.ToString("dd/MM/yyyy") + "\nNhà cung cấp: " + 
                txtb_namesupplier.Text + "\nNgười kí nhận: " + txtb_staff.Text + "\nTổng tiền: " + txtb_totalsum.Text;            
            info.Inlines.Add(sInfo);
            info.Margin = new Thickness(15);
            flowDocument.Blocks.Add(info);

            Table table = new Table();
            table.BorderThickness = new Thickness(1);
            table.BorderBrush = new SolidColorBrush(Colors.Black);
            table.Columns.Add(new TableColumn() { Width = new GridLength(50) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(220) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(200) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(100) }); // Thêm cột
            table.Columns.Add(new TableColumn() { Width = new GridLength(250) }); // Thêm cột            
            TableRowGroup gr = new TableRowGroup();
            TableRow titleRow = new TableRow();
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("STT")))); // Ô đầu tiên
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Tên phụ tùng")))); // Ô thứ hai
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Đơn giá"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Số lượng"))));
            titleRow.Cells.Add(new TableCell(new Paragraph(new Run("Thành tiền"))));
            gr.Rows.Add(titleRow);
            // Tạo Hàng và Ô
            List<itStockInDetail> list = getListItem(ds_nhapkho);
            foreach (itStockInDetail item in list)
            {
                TableRow row = new TableRow();
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.txtb_orderednum.Text)))); 
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.txtb_name.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.txtb_price.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.txtb_amount.Text))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.txtb_sumofmoney.Text))));

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
                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "PHIẾU NHẬP KHO");

            }
        }
        private void UpdateGoodReceivedNote()
        {
            if (txtb_namesupplier.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Cảnh báo");
            }
            else
            {
                if (ds_nhapkho.Children.Count == 0)
                {
                    MessageBox.Show("Phiếu nhập phải có ít nhất một mặt hàng.", "Cảnh báo");
                }
                else
                {
                    
                    DateTime date = DateTime.Parse(txtb_date.SelectedDate.ToString());
                    bool res = GoodReceivedNoteDAO.Instance.UpdateGoodReceivedNote(txtb_idLot.Text, txtb_namesupplier.Text, gara,
                        date.ToString("dd/MM/yyyy"), grn.DataEntryStaff);
                    List<itStockInDetail> list = getListItem(ds_nhapkho);
                    foreach (itStockInDetail item in list)
                    {
                        if (GRNDetailDAO.Instance.CheckExistedGRNDetail(txtb_idLot.Text, item.txtb_idStock.Text))
                        {
                            res = res && GRNDetailDAO.Instance.UpdateGRNDetail(txtb_idLot.Text, 
                                CarComponentDAO.Instance.GetComponentIDByName(gara, item.txtb_name.Text),
                            decimal.Parse(item.txtb_price.Text), int.Parse(item.txtb_amount.Text));
                        }    
                        else
                        {
                            res = res && GRNDetailDAO.Instance.InsertGRNDetail(txtb_idLot.Text, item.txtb_idStock.Text,
                            decimal.Parse(item.txtb_price.Text), int.Parse(item.txtb_amount.Text));
                        }    
                    }
                    if (res)
                    {
                        if (MessageBox.Show("Cập nhật phiếu nhập thành công. Bạn có muốn in lại phiếu nhập kho đã thay đổi không", 
                            "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            PrintGoodReceivedNote();
                            
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật phiếu nhập thất bại. Vui lòng thử lại.", "Thông báo");
                    }

                }
            }
        }

        
    }
}
