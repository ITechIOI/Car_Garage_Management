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
        public Customer LoadCustomerByID(string id, string gara)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                 " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                 "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                 "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0 AND CUSTOMERS.ID_CUS = '" + id + "'");
            if (data.Rows.Count == 0)
                return null;
            return new Customer(data.Rows[0]);
        }

        //lấy id khách hàng theo tên khách hàng và số điện thoại
        public string GetIDCusByNameAndPhone(string name, string phone)
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

        public List<Customer> GetCustomerByPhone(string phone, string gara)
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0 AND PHONE_NUMBER_CUS LIKE '%"
                + phone +"%'");
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }

        public bool InSertCustomer(string gara, string name, string phone, string address)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_INSERTCUSTOMER " +
                "@gara = '" + gara + "', @name = N'" + name
                + "', @phone = '" + phone + "', @address = N'" + address + "'") > 0);
        }

        public bool UpdateCustomer(string id, string name, string phone, string address)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_UPDATECUSTOMER '" + id + "', N'" + name
                + "', '" + phone + "', N'" + address + "'") > 0);
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

        public List<Customer> LoadCustomerListByName(string gara, string name)
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0 " +
                "AND DBO.[non_unicode_convert](NAME_CUS) LIKE DBO.[non_unicode_convert](N'%" + name + "%')");
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }
        public List<Customer> LoadCustomerListByDebt(string gara, int minDebt, int maxDebt)
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0 " +
                "AND DEBT BETWEEN " + minDebt + " AND " + maxDebt);
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }
        public List<Customer> LoadCustomerListByDebtAndName(string gara, string name, int minDebt, int maxDebt)
        {
            List<Customer> customerList = new List<Customer>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CUSTOMERS.ID_CUS, NAME_CUS, PHONE_NUMBER_CUS," +
                " ADDRESS_CUS, DEBT, STATUS_CUSD FROM CUSTOMERS JOIN CUSTOMER_DETAILS " +
                "ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS  " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0 " +
                "AND DBO.[non_unicode_convert](NAME_CUS) LIKE DBO.[non_unicode_convert](N'%" + name + "%') " +
                "AND DEBT BETWEEN " + minDebt + " AND " + maxDebt);
            foreach (DataRow item in data.Rows)
            {
                Customer customer = new Customer(item);
                customerList.Add(customer);
            }
            return customerList;
        }
        public int GetMaxDebt(string gara)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MAX( DEBT) DEBT " +
                "FROM CUSTOMERS JOIN CUSTOMER_DETAILS ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0");

            string s = data.Rows[0]["DEBT"].ToString();
            decimal d;
            if (Decimal.TryParse(s, out d))
            {
                return (int)d;
            }
            return 0;
        }
        public int GetMinDebt(string gara)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MIN( DEBT) DEBT " +
                "FROM CUSTOMERS JOIN CUSTOMER_DETAILS ON CUSTOMERS.ID_CUS = CUSTOMER_DETAILS.ID_CUS " +
                "WHERE STATUS_CUS = 0 AND ID_GARA = '" + gara + "' AND STATUS_CUSD = 0");
            string s = data.Rows[0]["DEBT"].ToString();
            decimal d;
            if (Decimal.TryParse(s, out d))
            {
                return (int)d;
            }
            return 0;
        }

    }
}
