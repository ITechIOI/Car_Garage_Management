using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class RepairPaymentDetail
    {
        private int rPDOrdinalNum;
        public int RPDOrdinalNum { get => rPDOrdinalNum; set => rPDOrdinalNum = value; }

        private string iDBill;
        public string IDBill { get => iDBill; set => iDBill = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string repairDescription;
        public string RepairDescription { get => repairDescription; set => repairDescription = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        private decimal totalPrice;
        public decimal TotalPrice { get => totalPrice; set => totalPrice = value; }

        private bool statusRPD;
        public bool StatusRPD { get => statusRPD; set => statusRPD = value; }

        public RepairPaymentDetail(int rPDOrdinalNum, string iDBill, string iDCom, string repairDescription, int comQuantity, decimal totalPrice, bool statusRPD)
        {
            this.RPDOrdinalNum = rPDOrdinalNum;
            this.IDBill = iDBill;
            this.IDCom = iDCom;
            this.RepairDescription = repairDescription;
            this.ComQuantity = comQuantity;
            this.TotalPrice = totalPrice;
            this.StatusRPD = statusRPD;
        }

        public RepairPaymentDetail(DataRow row)
        {
            this.RPDOrdinalNum = int.Parse(row["RPD_ORDINAL_NUM"].ToString());
            this.IDBill = row["ID_BILL"].ToString();
            this.IDCom = row["ID_COM"].ToString();
            this.RepairDescription = row["REPAIR_DESCRIPTION"].ToString();
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
            this.TotalPrice = Convert.ToDecimal(row["TOTAL_PRICE"].ToString());
            this.statusRPD = Convert.ToBoolean(row["STATUS_RPD"].ToString());
        }
    }
}
