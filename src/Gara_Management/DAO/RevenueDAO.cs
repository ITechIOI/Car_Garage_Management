using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    internal class RevenueDAO
    {
        private RevenueDAO() { }
        private static RevenueDAO instance;

        public static RevenueDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RevenueDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Revenue> LoadRevenueList()
        {
            List<Revenue> revenueList = new List<Revenue>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM REVENUE");
            foreach (DataRow item in data.Rows)
            {
                Revenue revenue = new Revenue(item);
                revenueList.Add(revenue);
            }
            return revenueList;
        }
    }
}
