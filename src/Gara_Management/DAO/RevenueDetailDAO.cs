using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class RevenueDetailDAO
    {
        private RevenueDetailDAO() { }
        private static RevenueDetailDAO instance;

        public static RevenueDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RevenueDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<RevenueDetail> LoadRevenueDetailList()
        {
            List<RevenueDetail> revenueDetailList = new List<RevenueDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM REVENUE_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                RevenueDetail revenueDetail = new RevenueDetail(item);
                revenueDetailList.Add(revenueDetail);
            }
            return revenueDetailList;
        }
    }
}
