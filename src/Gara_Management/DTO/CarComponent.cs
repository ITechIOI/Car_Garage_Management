using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class CarComponent
    {
        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string nameCom;
        public string NameCom { get => nameCom; set => nameCom = value; }

        private bool statusCom;
        public bool StatusCom { get => statusCom; set => statusCom = value; }
        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        private decimal wage;//tiền công
        public decimal Wage { get => wage; set => wage = value; }

        private decimal curPrice;
        public decimal CurPrice { get => curPrice; set => curPrice = value; }

        private bool statusDetail;
        public bool StatusDetail { get => statusDetail; set => statusDetail = value; }

        public CarComponent(string iDCom, string nameCom, bool statusCom, int comQuantity, decimal wage, decimal curPrice, bool statusDetail)
        {
            this.IDCom = iDCom;
            this.NameCom = nameCom;
            this.StatusCom = statusCom;
            this.ComQuantity = comQuantity;
            this.Wage = wage;
            this.CurPrice = curPrice;
            this.StatusDetail = statusDetail;
        }

        public CarComponent(DataRow row)
        {
            this.IDCom = row["ID_COM"].ToString();
            this.NameCom = row["NAME_COM"].ToString();
            this.StatusCom = Convert.ToBoolean(row["STATUS_COM"].ToString());
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
            this.Wage = Convert.ToDecimal(row["WAGE"].ToString());
            this.CurPrice = Convert.ToDecimal(row["CUR_PRICE"].ToString());
            this.StatusDetail = Convert.ToBoolean(row["STATUS_DETAILS"]);
        }
    }
}
