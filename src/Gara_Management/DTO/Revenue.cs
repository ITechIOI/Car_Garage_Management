using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class Revenue
    {
        private string iDRevenueReport;
        public string IDRevenueReport { get => iDRevenueReport; set => iDRevenueReport = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime renderingTime;
        public DateTime RenderingTime { get => renderingTime; set => renderingTime = value; }

        private decimal totalRevenue;
        public decimal TotalRevenue { get => totalRevenue; set => totalRevenue = value; }

        public Revenue(string iDRevenueReport, string iDGara, DateTime renderingTime, decimal totalRevenue)
        {
            this.IDRevenueReport = iDRevenueReport;
            this.IDGara = iDGara;
            this.RenderingTime = renderingTime;
            this.TotalRevenue = totalRevenue;
        }

        public Revenue(DataRow row)
        {
            this.IDRevenueReport = row["ID_REVENUE_REPORT"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.RenderingTime = Convert.ToDateTime(row["RENDERING_TIME"].ToString());
            this.TotalRevenue = Convert.ToDecimal(row["TOTAL_REVENUE"].ToString());
        }
    }
}
