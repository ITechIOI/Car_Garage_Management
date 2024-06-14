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
    /// Interaction logic for itRepairCard.xaml
    /// </summary>
    public partial class itRepairCard : UserControl
    {
        ReceptionForm recept;
        RepairPaymentBill bill;
        Staff staff;
        public itRepairCard()
        {
            InitializeComponent();
        }
        public itRepairCard(ReceptionForm recept, RepairPaymentBill bill, Staff staff)
        {
            InitializeComponent();
            this.recept = recept;
            this.staff = staff;
            this.bill = bill;
            txtb_idRec.Text = bill.IDRec;
            txtb_numberPlate.Text = recept.NumberPlate;
            if (bill.CompletionDate.ToString("dd/MM/yyyy") != "01/01/1900")
            {
                txtb_completeDate.Text = bill.CompletionDate.ToString("dd/MM/yyyy");
            }
            else
            {
                txtb_completeDate.Text = "";
            }    
            txtb_totalBill.Text =((int) bill.TotalPayment).ToString();
        }

        // xem chi tiết phiếu sửa chữa
        private void bd_repair_detail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair(bill.IDRec,recept.IDGara, staff);
            crdRepair.ShowDialog();
            string gara = recept.IDGara;
            Panel panel = ((Panel)this.Parent);
            panel.Children.Clear();
            List<RepairPaymentBill> list = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillList(gara);
            foreach (RepairPaymentBill bill in list)
            {
                ReceptionForm recept1 = ReceptionFormDAO.Instance.LoadReceptionFormByID(bill.IDRec, recept.IDGara);
                itRepairCard it = new itRepairCard(recept1, bill,staff);

                panel.Children.Add(it);
            }
        }
    }
}
