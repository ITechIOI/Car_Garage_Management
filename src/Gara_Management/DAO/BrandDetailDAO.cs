using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class BrandDetailDAO
    {
        private BrandDetailDAO() { }
        private static BrandDetailDAO instance;

        public static BrandDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BrandDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<BrandDetail> LoadBrandDetailList()
        {
            List<BrandDetail> brandDetaillist = new List<BrandDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM BRAND_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                BrandDetail brandDetail = new BrandDetail(item);
                brandDetaillist.Add(brandDetail);
            }
            return brandDetaillist;
        }

    }
}
