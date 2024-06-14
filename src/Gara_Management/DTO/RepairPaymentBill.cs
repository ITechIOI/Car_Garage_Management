using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class RepairPaymentBill
    {
        private string iDBill;
        public string IDBill { get => iDBill; set => iDBill = value; }

        private string iDRec;
        public string IDRec { get => iDRec; set => iDRec = value; }

        private DateTime completionDate;
        public DateTime CompletionDate { get => completionDate; set => completionDate = value; }

        private decimal totalPayment;
        public decimal TotalPayment { get => totalPayment; set => totalPayment = value; }

        private decimal paid;
        public decimal Paid { get => paid; set => paid = value; }

        private bool statusBill;
        public bool StatusBill { get => statusBill; set => statusBill = value; }

        public RepairPaymentBill(string iDBill, string iDRec, DateTime completionDate, decimal totalPayment, decimal paid, bool statusBill)
        {
            this.IDBill = iDBill;
            this.IDRec = iDRec;
            this.CompletionDate = completionDate;
            this.TotalPayment = totalPayment;
            this.Paid = paid;
            this.StatusBill = statusBill;
        }

        public RepairPaymentBill(DataRow row)
        {
            this.IDBill = row["ID_BILL"].ToString();
            this.IDRec = row["ID_REC"].ToString();
              
            this.CompletionDate = Convert.ToDateTime(row["COMPLETION_DATE"].ToString());
            this.TotalPayment = Convert.ToDecimal(row["TOTAL_PAYMENT"].ToString());
            this.Paid = Convert.ToDecimal(row["PAID"].ToString());
            this.StatusBill = Convert.ToBoolean(row["STATUS_BILL"].ToString());
        }
    }
}
