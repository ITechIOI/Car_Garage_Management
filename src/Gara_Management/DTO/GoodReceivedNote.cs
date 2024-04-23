using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class GoodReceivedNote
    {
        private string lotNumber;
        public string LotNumber { get => lotNumber; set => lotNumber = value; }

        private string supplier;
        public string Supplier { get => supplier; set => supplier = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime importTime;
        public DateTime ImportTime { get => importTime; set => importTime = value; }

        private string dataEntryStaff;
        public string DataEntryStaff { get => dataEntryStaff; set => dataEntryStaff = value; }

        private decimal totalPaymentGRN;
        public decimal TotalPaymentGRN { get => totalPaymentGRN; set => totalPaymentGRN = value; }

        private bool statusGRN;
        public bool StatusGRN { get => statusGRN; set => statusGRN = value; }

        public GoodReceivedNote(string lotNumber, string supplier, string iDGara, DateTime importTime, string dataEntryStaff, decimal totalPaymentGRN, bool statusGRN)
        {
            this.LotNumber = lotNumber;
            this.Supplier = supplier;
            this.IDGara = iDGara;
            this.ImportTime = importTime;
            this.DataEntryStaff = dataEntryStaff;
            this.TotalPaymentGRN = totalPaymentGRN;
            this.StatusGRN = statusGRN;
        }

        public GoodReceivedNote(DataRow row)
        {
            this.LotNumber = row["LOTNUMBER"].ToString();
            this.Supplier = row["SUPPLIER"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.ImportTime = Convert.ToDateTime(row["IMPORT_TIME"].ToString());
            this.DataEntryStaff = row["DATA_ENTRY_STAFF"].ToString();
            this.TotalPaymentGRN = Convert.ToDecimal(row["TOTAL_PAYMENT_GRN"].ToString());
            this.StatusGRN = Convert.ToBoolean(row["STATUS_GRN"]);
        }
    }
}
