using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class IncurredCost
    {
        private int cOOrdinalNum;
        public int COOrdinalNum { get => cOOrdinalNum; set => cOOrdinalNum = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime incurredCostDate;
        public DateTime IncurredCostDate { get => incurredCostDate; set => incurredCostDate = value; }

        private string statusDescription;
        public string StatusDescription { get => statusDescription; set => statusDescription = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        private decimal incurredCostTTPrice;
        public decimal IncurredCostTTPrice { get => incurredCostTTPrice; set => incurredCostTTPrice = value; }

        private bool statusIC;
        public bool StatusIC { get => statusIC; set => statusIC = value; }

        public IncurredCost(int cOOrdinalNum, string iDCom, string iDGara, DateTime incurredCostDate, string statusDescription, int comQuantity, decimal incurredCostTTPrice, bool statusIC)
        {
            this.COOrdinalNum = cOOrdinalNum;
            this.IDCom = iDCom;
            this.IDGara = iDGara;
            this.IncurredCostDate = incurredCostDate;
            this.StatusDescription = statusDescription;
            this.ComQuantity = comQuantity;
            this.IncurredCostTTPrice = incurredCostTTPrice;
            this.StatusIC = statusIC;
        }

        public IncurredCost(DataRow row)
        {
            this.COOrdinalNum = int.Parse(row["CO_ORDINAL_NUM"].ToString());
            this.IDCom = row["ID_COM"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.IncurredCostDate = Convert.ToDateTime(row["INCURRED_COSTS_DATE"].ToString());
            this.StatusDescription = row["STATUS_DESCRIPTION"].ToString();
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
            this.IncurredCostTTPrice = Convert.ToDecimal(row["INCURRED_COSTS_TTPRICE"].ToString());
            this.StatusIC = Convert.ToBoolean(row["STATUS_IC"].ToString());
        }
    }
}
