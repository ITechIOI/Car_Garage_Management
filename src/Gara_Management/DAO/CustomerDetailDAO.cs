using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class CustomerDetailDAO
    {
        private CustomerDetailDAO() { }
        private static CustomerDetailDAO instance;

        public static CustomerDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomerDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<CustomerDetail> LoadCustomerDetailList()
        {
            List<CustomerDetail> customerDetailList = new List<CustomerDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM CUSTOMER_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                CustomerDetail customerDetail = new CustomerDetail(item);
                customerDetailList.Add(customerDetail);
            }
            return customerDetailList;
        }
    }
}
