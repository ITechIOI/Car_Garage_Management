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

        public Account CheckForLogin(string username, string password)
        {
            DataTable data =  DataProvider.Instance.ExecuteQuery("SELECT * FROM ACCOUNTS " +
                "WHERE USERNAME = '" + username + "' AND PASSWORD = '" + password + "' AND STATUS_ACCOUNT=0");
            if (data.Rows.Count>0)
                return new Account(data.Rows[0]);
            return null;
        }

        public bool InsertAccount(string username, string password, string idStaff, bool accAuthor)
        {
            int author;
            if (accAuthor)
            {
                author = 1;
            }
            else
            {
                author = 0;
            }
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_INSERTACCOUNT '" + 
                username +"', '"+password+"', '"+idStaff+"', "+author) > 0);
        }

        public string GetIDAccountByIDStaff(string idStaff)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM ACCOUNTS WHERE ID_STAFF = '" + idStaff + "'");
            return new Account(data.Rows[0]).IDStaff;
        }


        public bool CheckExistedUsername(string username)
        {
            return (DataProvider.Instance.ExecuteQuery("SELECT * FROM ACCOUNTS " +
                "WHERE USERNAME='" + username + "'").Rows.Count > 0);
        }

        public bool ResetPassword(string idAcc)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_RESETPASSWORD '" + idAcc + "'") > 0);
        }

        public bool ChangePassword(string idAcc, string password)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_CHANGEPASSWORD '" + idAcc + "', '" + password + "'") > 0);
        }

        public bool ChangeAccAuthorization(string idAcc, bool accAuthor)
        {
            int author;
            if (accAuthor)
            {
                author = 1;
            }
            else
            {
                author = 0;
            }
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_UPDATEAUTHOR '" + idAcc + "', " + author) > 0);
        }

        public bool DeleteAccount(string idAcc)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_DELETEACCOUNTS '" + idAcc + "'") > 0);
        }
    }
}
