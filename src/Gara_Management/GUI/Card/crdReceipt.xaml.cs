using Gara_Management.DAO;
using Gara_Management.DTO;
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
    /// Interaction logic for crdReceipt.xaml
    /// </summary>
    public partial class crdReceipt : Window
    {
        /* biếm kiểm tra các thông tin đã thay đổi hay chưa*/
        bool isChanged = false;
        string gara;
        Customer cus;
        Staff staff;
        private PrintDialog printDialog;
        public crdReceipt(string gara, Staff staff)
        {
            InitializeComponent();
            this.Opacity = 0;
            pk_dateReceipt.SelectedDate = DateTime.Now;
            this.gara = gara;
            this.staff = staff;
            txtb_bill.Text = "0";
            txtb_idReceipt.Text = DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM")
                + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm")
                + DateTime.Now.ToString("ss");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy storyboard từ resource
            Storyboard fadeInStoryboard = (Storyboard)this.Resources["FadeInStoryboard"];
            // Bắt đầu storyboard
            fadeInStoryboard.Begin(this);
        }

        public crdReceipt(string gara, Staff staff, Customer cus, decimal bill)
        {
            InitializeComponent();
            pk_dateReceipt.SelectedDate = DateTime.Now;
            this.staff = staff;
            this.gara = gara;
            this.cus = cus;
            txtb_nameCus.IsReadOnly = true;
            txtb_phoneCus.IsReadOnly = true;
            txtb_idReceipt.Text = DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM")
                + DateTime.Now.ToString("YYYY") + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm")
                + DateTime.Now.ToString("ss");
            txtb_nameCus.Text = cus.NameCus;
            txtb_phoneCus.Text = cus.PhoneNumberCus;
            txtb_debtCus.Text = cus.Debt.ToString();
            txtb_bill.Text = bill.ToString();
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
        // kiểm tra các thông tin có hợp lệ
        private bool isValid()
        {

            return true;
        }
        private void bd_save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int proceeds;
            if (txtb_nameCus.Text == "" || txtb_proceeds.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo");
            }
            else
            {
                if (!int.TryParse(txtb_proceeds.Text, out proceeds))
                {
                    MessageBox.Show("Số tiền thu phải là một số nguyên", "Thông báo");
                }    
                else
                {
                    decimal debt = decimal.Parse(txtb_debtCus.Text) + decimal.Parse(txtb_bill.Text);
                    if (proceeds > debt)
                    {
                        MessageBox.Show("Số tiền thu không được lớn hơn số tiền nợ " + debt, "Thông báo");
                    }    
                    else
                    {
                        CustomerDAO.Instance.UpdateDebtOfCustomer(gara, cus.IDCus, decimal.Parse(txtb_bill.Text));
                        DateTime date = DateTime.Parse(pk_dateReceipt.SelectedDate.ToString());
                        if (ReceiptDAO.Instance.InsertReceipt(txtb_idReceipt.Text, cus.IDCus, gara,
                            date.ToString("dd/MM/yyyy HH:mm:ss"), staff.IDStaff, proceeds))
                        {
                            if (MessageBox.Show("Lập phiếu thu tiền thành công. Bạn có muốn in phiếu thu?", "Thông báo", 
                                MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                            {
                                PrintReceipt();
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lập phiếu thu tiền không thành công vui lòng thử lại", "Thông báo");
                        }
                    }    
                }    
            }
        }

        

        private void txtb_phoneCus_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Customer> listCustomer = CustomerDAO.Instance.GetCustomerByPhone(txtb_phoneCus.Text, gara);
            if (listCustomer.Count == 0)
            {
                MessageBox.Show("Khách hàng không tồn tại!");
            } else
            {
                this.cus = listCustomer[0];
            }
            txtb_nameCus.Text = cus.NameCus;
            txtb_debtCus.Text = cus.Debt.ToString();
        }
        private void PrintReceipt()
        {
            CarGara carGara = CarGaraDAO.Instance.GetCarGaraByID(this.gara);
            FlowDocument flowDocument = new FlowDocument();
            Paragraph gara = new Paragraph();
            gara.Inlines.Add(new Run("GARA OTO\nĐịa chỉ: " + carGara.AddressGara + "\nSố điện thoại: " + carGara.PhoneNumberGara));
            gara.TextAlignment = TextAlignment.Center;
            gara.FontSize = 15;
            flowDocument.Blocks.Add(gara);

            Paragraph title = new Paragraph();
            title.Inlines.Add(new Run("PHIẾU THU TIỀN"));
            title.FontSize = 20;
            title.TextAlignment = TextAlignment.Center;
            title.FontWeight = FontWeights.Bold;
            flowDocument.Blocks.Add(title);

            Paragraph info = new Paragraph();
            DateTime date = DateTime.Parse(pk_dateReceipt.SelectedDate.ToString());
            string sInfo = "Mã phiếu: " +txtb_idReceipt.Text + "\nHọ tên: " + cus.NameCus + "\nSố điện thoại: " + cus.PhoneNumberCus + "\nĐịa chỉ: " + cus.AddressCus
                + "\nNgày thu tiền: " + date.ToString("dd/MM/yyyy") + "\nSố tiền thu: " + txtb_proceeds.Text;
            info.Inlines.Add(sInfo);
            info.Margin = new Thickness(15);
            flowDocument.Blocks.Add(info);


            // Mở hộp thoại chọn máy in
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // In hóa đơn
                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "Hóa đơn"); 
                
            }
        }
    }
}
