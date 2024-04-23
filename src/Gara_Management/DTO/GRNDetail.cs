using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class GRNDetail
    {
        private int gRNOrdinalNum;
        public int GRNOrdinalNum { get => gRNOrdinalNum; set => gRNOrdinalNum = value; }

        private string lotNumber;
        public string LotNumber { get => lotNumber; set => lotNumber = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private decimal comPrice;
        public decimal ComPrice { get => comPrice; set => comPrice = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        private decimal gRNTotalPayment;
        public decimal GRNTotalPayment { get => gRNTotalPayment; set => gRNTotalPayment = value; }

        private bool statusGRN;
        public bool StatusGRN { get => statusGRN; set => statusGRN = value; }

        public GRNDetail(int gRNOrdinalNum, string lotNumber, string iDCom, decimal comPrice, int comQuantity, decimal gRNTotalPayment, bool statusGRN)
        {
            this.GRNOrdinalNum = gRNOrdinalNum;
            this.LotNumber = lotNumber;
            this.IDCom = iDCom;
            this.ComPrice = comPrice;
            this.ComQuantity = comQuantity;
            this.GRNTotalPayment = gRNTotalPayment;
            this.StatusGRN = statusGRN;
        }

        public GRNDetail(DataRow row)
        {
            this.GRNOrdinalNum = int.Parse(row["GRN_ORDINAL_NUM"].ToString());
            this.LotNumber = row["LOTNUMBER"].ToString();
            this.IDCom = row["ID_COM"].ToString();
            this.ComPrice = Convert.ToDecimal(row["COM_PRICE"].ToString());
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
            this.GRNTotalPayment = Convert.ToDecimal(row["GRN_TOTAL_PAYMENT"].ToString());
            this.StatusGRN = Convert.ToBoolean(row["STATUS_GRN"].ToString());
        }
    }
}
