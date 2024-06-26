﻿using Gara_Management.DAO;
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
    public partial class crdStockIn : Window, grnInterface
    {
        GoodReceivedNote grn;
        string gara;
        Account acc;
        int i = 0;
        bool check = false;
        string id;
        int price, amount;
        List<GRNDetail> list = new List<GRNDetail>();
        


        // tùy vào cách dùng mà hiển thị sẽ khác nhau
        // phiếu mới
        public crdStockIn(string gara, Account acc)
        {
            InitializeComponent();
            this.Opacity = 0;
            i = 0;

            btn_delete.Visibility = Visibility.Hidden;
            this.gara = gara;
            this.acc = acc;
            txtb_idLot.Text = "LOT" + (GoodReceivedNoteDAO.Instance.GetMaxLotNumber() + 1);
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(acc.IDStaff).NameStaff;
            txtb_date.SelectedDate = DateTime.Now;
            ds_nhapkho.LayoutUpdated += Ds_nhapkho_LayoutUpdated;
        }

        private void Ds_nhapkho_LayoutUpdated(object sender, EventArgs e)
        {
            i = ds_nhapkho.Children.Count;

            List<itStockInDetail> list = getListItem(ds_nhapkho);
            int total = 0; int money;
            foreach (itStockInDetail item in list)
            {
                if (int.TryParse(item.txtb_sumofmoney.Text, out money))
                {
                    total += money;
                }
            }
            txtb_totalsum.Text = total.ToString();
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
            ds_nhapkho.LayoutUpdated += Ds_nhapkho_LayoutUpdated;
            txtb_idLot.Text = grn.LotNumber;
            txtb_date.Text = grn.ImportTime.ToString();
            txtb_date.IsEnabled = false;
            txtb_namesupplier.IsReadOnly = true;
            txtb_namesupplier.Text = grn.Supplier;
            this.gara = StaffDAO.Instance.GetStaffById(AccountDAO.Instance.GetIDStaffByIDAcc(grn.DataEntryStaff.ToString())).IDGara;
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(AccountDAO.Instance.GetIDStaffByIDAcc(grn.DataEntryStaff.ToString())).NameStaff;
            decimal price = 0;
            list.Clear();
            list = GRNDetailDAO.Instance.LoadGRNDetailListByLotNumber(grn.LotNumber);
            i = 1;
            int sl = 0;
            foreach (GRNDetail detail in list)
            {
                itStockInDetail it = new itStockInDetail(detail, gara, i++);
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
            btn_delete.Visibility = Visibility.Hidden;
            ds_nhapkho.LayoutUpdated += Ds_nhapkho_LayoutUpdated;
            txtb_idLot.Text = "LOT" + (GoodReceivedNoteDAO.Instance.GetMaxLotNumber() + 1);
            txtb_staff.Text = StaffDAO.Instance.GetStaffById(acc.IDStaff).NameStaff;
            txtb_date.SelectedDate = DateTime.Now;
            i ++ ;
            itStockInDetail it = new itStockInDetail( i++, gara, idComponent, 1000, 1);
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
            PrintGoodReceivedNote();

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
                crdComponent component = new crdComponent(gara, this as grnInterface);
                component.bd_save.MouseDown += Bd_save_MouseDown;
                component.bd_delete.MouseDown += Bd_delete_MouseDown;
                component.ShowDialog();
                
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn cập nhật phiếu nhập này?", "Thông báo", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    add_button.Text = "Thêm";
                    check = true;
                    txtb_namesupplier.IsReadOnly = false;
                    txtb_date.IsEnabled = true;
                    i = ds_nhapkho.Children.Count;
                    bd_pay.Visibility = Visibility.Visible;
                }
            }

        }

        public void Bd_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
               
        }
        
        public void Bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (id != "" && price!=0 && price!=0)
            {
                i++;
                itStockInDetail it = new itStockInDetail(i, gara, id, price, amount);
                ds_nhapkho.Children.Add(it);
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
                    List<itStockInDetail> list = getListItem(ds_nhapkho);
                    foreach (itStockInDetail item in list)
                    {
                        int amount, price;
                        if (!((int.TryParse(item.txtb_amount.Text, out amount)) && (int.TryParse(item.txtb_price.Text, out price))))
                        {
                            
                            MessageBox.Show("Số lượng và đơn giá phải là số nguyên dương.", "Thông báo");
                            return;
                        }
                        else
                        {
                            if (amount <= 0 || price <= 0)
                            {
                                MessageBox.Show("Số lượng và đơn giá phải là 1 số nguyên dương.", "Thông báo");
                                return;
                            }
                           
                        }

                    }
                    DateTime date = DateTime.Parse(txtb_date.SelectedDate.ToString());
                    bool res = GoodReceivedNoteDAO.Instance.InsertGoodReceivedNote(txtb_idLot.Text, txtb_namesupplier.Text, gara,
                        date.ToString("dd/MM/yyyy"), acc.IDAcc);
                    foreach (itStockInDetail item in list)
                    {
                        int amount, price;
                        if (!((int.TryParse( item.txtb_amount.Text, out amount)) && (int.TryParse(item.txtb_price.Text, out price))))
                        {
                            MessageBox.Show("Số lượng và đơn giá phải là số nguyên dương.", "Thông báo");
                        }
                        else
                        {
                            if (amount<=0 || price<=0)
                            {
                                MessageBox.Show("Số lượng và đơn giá phải là 1 số nguyên dương.", "Thông báo");
                            } 
                            else
                            {
                                res = res && GRNDetailDAO.Instance.InsertGRNDetail(txtb_idLot.Text,
                            CarComponentDAO.Instance.GetComponentIDByName(gara, item.txtb_name.Text),
                            decimal.Parse(item.txtb_price.Text), int.Parse(item.txtb_amount.Text));
                            }    
                        }
                        
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
            table.Columns.Add(new TableColumn() { Width = new GridLength(50) }) ; // Thêm cột
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
                    List<itStockInDetail> list = getListItem(ds_nhapkho);
                    foreach (itStockInDetail item in list)
                    {
                        int amount, price;
                        if (!((int.TryParse(item.txtb_amount.Text, out amount)) && (int.TryParse(item.txtb_price.Text, out price))))
                        {

                            MessageBox.Show("Số lượng và đơn giá phải là số nguyên dương.", "Thông báo");
                            return;
                        }
                        else
                        {
                            if (amount <= 0 || price <= 0)
                            {
                                MessageBox.Show("Số lượng và đơn giá phải là 1 số nguyên dương.", "Thông báo");
                                return;
                            }
                        }
                    }
                    DateTime date = DateTime.Parse(txtb_date.SelectedDate.ToString());
                    bool res = GoodReceivedNoteDAO.Instance.UpdateGoodReceivedNote(txtb_idLot.Text, txtb_namesupplier.Text, gara,
                        date.ToString("dd/MM/yyyy"), grn.DataEntryStaff);
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

        private void btn_delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa phiếu nhập này?", "Thông báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (GoodReceivedNoteDAO.Instance.DeleteGoodReceivedNote(txtb_idLot.Text))
                {
                    MessageBox.Show("Xóa phiếu nhập hàng thành công,", "Thông báo");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xóa phiếu nhập này không thành công. Vui lòng thử lại sau.", "Thông báo");
                }

            }
        }

        public void ReceivedData(string idCom, int price, int quantity)
        {
            this.id = idCom;
            this.price = price;
            this.amount = quantity;
        }
    }
}
