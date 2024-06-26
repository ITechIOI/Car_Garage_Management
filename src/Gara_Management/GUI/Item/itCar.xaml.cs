﻿using Gara_Management.DAO;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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
        string idRec;
        public itCar(string gara, Account acc, Customer cus, string idRec, int i)
        {
            InitializeComponent();
            this.gara = gara;
            this.account = acc;
            this.customer = cus;
            this.idRec = idRec;
            ReceptionForm recept = ReceptionFormDAO.Instance.LoadReceptionFormByID(idRec, gara);
            RepairPaymentBill bill = RepairPaymentBillDAO.Instance.GetRepairPaymentBillByIDRec(gara, idRec);
            if (bill != null) { txtb_total.Text =((int) bill.TotalPayment).ToString(); }
            else { txtb_total.Text = "0"; }
            txtb_ordinalNum.Text = i.ToString();
            txtb_carBrand.Text =CarBrandDAO.Instance.LoadCarBrandByID(recept.IDBrand, gara).NameBrand;
            txtb_numberPlate.Text = recept.NumberPlate;
            if (CustomerDAO.Instance.LoadCustomerByID(recept.IDCus, gara) != null)
            {
                txtb_cus.Text = CustomerDAO.Instance.LoadCustomerByID(recept.IDCus, gara).NameCus;
            }
        }
        

        // phiếu thu tiền
        private void bd_paymentReceipt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Staff staff = StaffDAO.Instance.GetStaffById(account.IDStaff);
            crdReceipt crdReceipt = new crdReceipt(gara, staff,customer, decimal.Parse(txtb_total.Text), idRec);
            crdReceipt.ShowDialog();
        }

        private void bd_repairInvoice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdRepair crdRepair = new crdRepair(idRec, gara, StaffDAO.Instance.GetStaffById(account.IDStaff));
            crdRepair.ShowDialog();
            string gr = gara;
            Account acc = account;
            Panel panel = ((Panel)this.Parent);
            panel.Children.Clear();
            int i = 1;
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtList(gr);
            foreach (ReceptionForm recept in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(recept.IDCus, gara);
                itCar it = new itCar(gr, acc, cus, recept.IDRec, i++);
                panel.Children.Add(it);
            }
        }

   

        private void bd_acceptDetail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            crdAccept crdAccept = new crdAccept(idRec, gara);
            crdAccept.ShowDialog();
            string gr = gara;
            Account acc = account;
            Panel panel = ((Panel)this.Parent);
            panel.Children.Clear();
            int i = 1;
            List<ReceptionForm> list = ReceptionFormDAO.Instance.LoadReceptionFormtList(gr);
            foreach (ReceptionForm recept in list)
            {
                Customer cus = CustomerDAO.Instance.LoadCustomerByID(recept.IDCus, gara);
                itCar it = new itCar(gr, acc, cus, recept.IDRec, i++);
                panel.Children.Add(it);
            }
        }
    }
}
