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
        public crdReceipt(string gara, Staff staff)
        {
            InitializeComponent();
            pk_dateReceipt.SelectedDate = DateTime.Now;
            this.gara = gara;
            this.staff = staff;
            txtb_bill.Text = "0";
            txtb_idReceipt.Text = DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM")
                + DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("HH") + DateTime.Now.ToString("mm")
                + DateTime.Now.ToString("ss");
        }

        public crdReceipt(string gara, Staff staff, Customer cus, int bill)
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
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
            }
            else
            {
                if (!int.TryParse(txtb_proceeds.Text, out proceeds))
                {
                    MessageBox.Show("Số tiền thu phải là một số nguyên");
                }    
                else
                {
                    decimal debt = decimal.Parse(txtb_debtCus.Text) + decimal.Parse(txtb_bill.Text);
                    if (proceeds > debt)
                    {
                        MessageBox.Show("Số tiền thu không được lớn hơn số tiền nợ " + debt);
                    }    
                    else
                    {
                        CustomerDAO.Instance.UpdateDebtOfCustomer(gara, cus.IDCus, int.Parse(txtb_bill.Text));
                        DateTime date = DateTime.Parse(pk_dateReceipt.SelectedDate.ToString());
                        if (ReceiptDAO.Instance.InsertReceipt(txtb_idReceipt.Text, cus.IDCus, gara,
                            date.ToString("dd/MM/yyyy HH:mm:ss"), staff.IDStaff, proceeds))
                        {
                            MessageBox.Show("Lập phiếu thu tiền thành công");

                        }
                        else
                        {
                            MessageBox.Show("Lập phiếu thu tiền không thành công vui lòng thử lại");
                        }
                    }    
                }    
            }
        }

        private void bd_print_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtb_phoneCus_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.cus = CustomerDAO.Instance.GetCustomerByPhone(txtb_phoneCus.Text, gara)[0];
            txtb_nameCus.Text = cus.NameCus;
            txtb_debtCus.Text = cus.Debt.ToString();
        }
    }
}
