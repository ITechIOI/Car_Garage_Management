using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class CustomerDAO
    {
        private CustomerDAO() { }
        private static CustomerDAO instance;
        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomerDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Customer> LoadCustomerList()
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM CUSTOMERS");
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }
    }
}
