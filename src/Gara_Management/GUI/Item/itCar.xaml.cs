using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Card;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itCar.xaml
    /// </summary>
    public partial class itCar : UserControl
    {
        string gara;
        Account account;
        Customer customer;
        public itCar(string gara, Account acc, Customer cus)
        {
            InitializeComponent();
            this.gara = gara;
            this.account = acc;
            this.customer = cus;
        }
        

        // phiếu thu tiền
        private void bd_paymentReceipt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Staff staff = StaffDAO.Instance.GetStaffById(account.IDStaff);
            crdReceipt crdReceipt = new crdReceipt(gara, staff,customer, int.Parse(txtb_total.Text));
            crdReceipt.ShowDialog();
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair("Mã phiếu");
            crdRepair.ShowDialog(); 
        }

   

        private void bd_acceptDetail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdAccept crdAccept = new crdAccept("MÃ phiếu", gara);
            crdAccept.ShowDialog();
        }
    }
}
