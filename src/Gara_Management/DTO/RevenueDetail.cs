using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class RevenueDetail
    {
        private int rDOrdinalNum;
        public int RDOrdinalNum { get => rDOrdinalNum; set => rDOrdinalNum = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private string iDBrand;
        public string IDBrand { get => iDBrand; set => iDBrand = value; }

        private DateTime renderTime;
        public DateTime RenderTime { get => renderTime; set => renderTime = value; }

        private int numberOfRepair;
        public int NumberOfRepair { get => numberOfRepair; set => numberOfRepair = value; }

        private float rate;
        public float Rate { get => rate; set => rate = value; }

        private decimal totalMoney;
        public decimal TotalMoney { get => totalMoney; set => totalMoney = value; }

        private bool statusRD;
        public bool StatusRD { get => statusRD; set => statusRD = value; }

        public RevenueDetail(int rDOrdinalNum, string iDGara, string iDBrand, DateTime renderTime, int numberOfRepair, float rate, decimal totalMoney, bool statusRD)
        {
            this.RDOrdinalNum = rDOrdinalNum;
            this.IDGara = iDGara;
            this.IDBrand = iDBrand;
            this.RenderTime = renderTime;
            this.NumberOfRepair = numberOfRepair;
            this.Rate = rate;
            this.TotalMoney = totalMoney;
            this.StatusRD = statusRD;
        }

        public RevenueDetail(DataRow row)
        {
            this.RDOrdinalNum = int.Parse(row["RD_ORDINAL_NUM"].ToString());
            this.IDGara = row["ID_GARA"].ToString();
            this.IDBrand = row["ID_BRAND"].ToString();
            this.RenderTime = Convert.ToDateTime(row["RENDER_TIME"].ToString());
            this.NumberOfRepair = int.Parse(row["NUMBER_OF_REPAIRS"].ToString());
            this.Rate = float.Parse(row["RATE"].ToString());
            this.TotalMoney = Convert.ToDecimal(row["TOTAL_MONEY"].ToString());
            this.StatusRD = Convert.ToBoolean(row["STATUS_RD"].ToString());
        }
    }
}
