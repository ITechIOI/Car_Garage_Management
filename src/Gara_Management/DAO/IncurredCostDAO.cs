using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class IncurredCostDAO
    {
        private IncurredCostDAO() { }
        private static IncurredCostDAO instance;

        public static IncurredCostDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new IncurredCostDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<IncurredCost> LoadIncurredCostList()
        {
            List<IncurredCost> incurredCostList = new List<IncurredCost>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM INCURRED_COSTS");
            foreach (DataRow item in data.Rows)
            {
                IncurredCost incurredCost = new IncurredCost(item);
                incurredCostList.Add(incurredCost);
            }
            return incurredCostList;
        }
    }
}
