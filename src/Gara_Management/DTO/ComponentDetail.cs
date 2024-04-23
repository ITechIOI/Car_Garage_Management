using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class ComponentDetail
    {
        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private decimal wage;
        public decimal Wage { get => wage; set => wage = value; }

        private decimal curPrice;
        public decimal CurPrice { get => curPrice; set => curPrice = value; }

        private bool statusDetail;
        public bool StatusDetail { get => statusDetail; set => statusDetail = value; }

        public ComponentDetail(string iDCom, string iDGara, decimal wage, decimal curPrice, bool statusDetail)
        {
            this.IDCom = iDCom;
            this.IDGara = iDGara;
            this.Wage = wage;
            this.CurPrice = curPrice;
            this.StatusDetail = statusDetail;
        }

        public ComponentDetail(DataRow row)
        {
            this.IDCom = row["ID_COM"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.Wage = Convert.ToDecimal(row["WAGE"].ToString());
            this.CurPrice = Convert.ToDecimal(row["CUR_PRICE"].ToString());
            this.StatusDetail = Convert.ToBoolean(row["STATUS_DETAILS"]);
        }
    }
}
