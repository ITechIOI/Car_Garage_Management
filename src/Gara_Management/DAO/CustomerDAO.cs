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

        public List<Customer> LoadCustomerList(string gara)
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0");
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }

        //lấy thông tin khách hàng theo id
        public static Customer LoadCustomerByID(string id)
        {
            string query = "SELECT * FROM CUSTOMERS WHERE ID_CUS = '" + id + "'";
            Customer customer = new Customer(DataProvider.Instance.ExecuteQuery(query).Rows[0]);
            return customer;
        }

        //lấy id khách hàng theo tên khách hàng và số điện thoại
        public static string GetIDCusByNameAndPhone(string name, string phone)
        {
            string id;
            string query = "EXEC USP_GET_IDCUSTOMER @NAME_CUS , @PHONE_NUMBER_CUS";
            if (DataProvider.Instance.ExecuteScalar(query, new object[] { name, phone }) != null)
            {
                id = DataProvider.Instance.ExecuteScalar(query, new object[] { name, phone }).ToString().Trim();
            }
            else
            {
                id = null;
            }
            return id;
        }

        public bool InSertCustomer(string gara, string name, string phone, string address)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_INSERTCUSTOMER " +
                "@gara = '" + gara + "', @name = N'" + name
                + "', @phone = '" + phone + "', @address = N'" + address + "'") > 0);
        }

        public bool UpdateCustomer(string id, string name, string phone, string address)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_UPDATECUSTOMER @id = '" + "', @name = N'" + name
                + "', @phone = '" + phone + "', @address = N'" + address + "'") > 0);
        }

        public bool DeleteCustomer(string gara, string id)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_DELETECUSTOMER" +
                " @gara = '" + gara + "', @id = '" + id + "'") > 0);
        }

        public bool UpdateDebtOfCustomer(string gara, string id, decimal updateMoney)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_UPDATEDEBTOFCUSTOMER @gara = '" + gara + "', " +
                "@id = '" + id + "', @updateMoney = " + updateMoney.ToString()) > 0);
        }

    }
}
