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
        public itRepairCard()
        {
            InitializeComponent();
        }
        public itRepairCard(ReceptionForm recept, RepairPaymentBill bill)
        {
            InitializeComponent();
            this.recept = recept;
            this.bill = bill;
            txtb_idRec.Text = bill.IDRec;
            txtb_numberPlate.Text = recept.NumberPlate;
            txtb_completeDate.Text = bill.CompletionDate.ToString("dd/MM/yyyy");
            txtb_totalBill.Text = bill.TotalPayment.ToString();
        }

        // xem chi tiết phiếu sửa chữa
        private void bd_repair_detail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair(bill.IDRec, recept.IDGara);
            crdRepair.ShowDialog();
        }
    }
}
