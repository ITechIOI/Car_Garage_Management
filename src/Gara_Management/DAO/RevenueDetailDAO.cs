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

        //lấy thông tin doanh thu theo ngày bắt đầu, kết thúc
        public DataTable LoadRevenueDetailListByPeriod(string gara, string startDate, string endDate)
        {
            string query = "SELECT CB.NAME_BRAND AS [Tên hãng], RD.RENDER_TIME AS [Thời gian kết xuất], RD.NUMBER_OF_REPAIRS AS [Số lượt sửa], RATE AS [Tỉ lệ], TOTAL_MONEY AS [Doanh thu] "
                + "FROM REVENUE_DETAILS RD, CAR_BRANDS CB, CAR_GARA CG "
                + "WHERE RD.ID_GARA = CG.ID_GARA AND RD.ID_BRAND = CB.ID_BRAND AND RD.ID_GARA = '" + gara + "' "
                + "AND RD.RENDER_TIME >= '" + startDate + "' "
                + "AND RD.RENDER_TIME <= '" + endDate + "'";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            return dataTable;
        }

        //lấy thông tin chi tiêu theo ngày bắt đầu, kết thúc
        public DataTable LoadSpendDetailListByPeriod(string gara, string startDate, string endDate)
        {
            string query = "EXEC USP_GARA_SPEND_DETAILS @ID_GARA , @START_DATE , @END_DATE";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query, new object[] { gara, startDate, endDate });
            return dataTable;
        }
    }
}
