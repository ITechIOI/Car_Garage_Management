using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class GaraQuantityRuleDAO
    {
        private GaraQuantityRuleDAO() { }
        private static GaraQuantityRuleDAO instance;

        public static GaraQuantityRuleDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GaraQuantityRuleDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<GaraQuantityRule> LoadGaraQuantityRuleList()
        {
            List<GaraQuantityRule> garaQuantityRuleList = new List<GaraQuantityRule>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM GARA_QUANTITY_RULES");
            foreach (DataRow item in data.Rows)
            {
                GaraQuantityRule garaQuantityRule = new GaraQuantityRule(item);
                garaQuantityRuleList.Add(garaQuantityRule);
            }
            return garaQuantityRuleList;
        }

    }
}
