using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    internal class Receipt
    {
        private string idReceipt;
        public string IDReceipt { get => idReceipt; set => idReceipt = value; }
        private string idCus;
        public string IDCus { get => idCus; set => idCus = value; }
        private string idGara;
        public string IDGara { get => idGara; set => idGara = value; }
        private string idStaff;
        public string IDStaff { get => idStaff; set => idStaff = value; }
        private DateTime receiptDate;
        public DateTime ReceiptDate { get => receiptDate; set => receiptDate = value; }
        private decimal proceeds;
        public decimal Proceeds { get => proceeds; set => proceeds = value; }

        public Receipt(string idReceipt, string idCus, string gara, string staff, DateTime receiptDate, decimal proceed)
        {
            this.IDReceipt = idReceipt;
            this.IDCus = idCus;
            this.IDGara = gara;
            this.IDStaff = staff;
            this.ReceiptDate = receiptDate;
            this.Proceeds = proceed;
        }
        public Receipt(DataRow row)
        {
            this.IDReceipt = row["ID_RECEIPT"].ToString();
            this.IDCus = row["ID_CUS"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.IDStaff = row["RECEIPT_STAFF"].ToString();
            this.ReceiptDate = Convert.ToDateTime(row["RECEIPT_DATE"].ToString());
            this.Proceeds = Convert.ToDecimal(row["PROCEEDS"].ToString());
        }
    }
}
