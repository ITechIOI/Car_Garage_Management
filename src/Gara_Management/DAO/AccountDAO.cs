using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class AccountDAO
    {
        private AccountDAO() { }
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Account> LoadAccountList()
        {
            List<Account> accountList = new List<Account>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM ACCOUNTS");
            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                accountList.Add(account);
            }
            return accountList;
        }
    }
}
