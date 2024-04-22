using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class Account
    {
        private string iDAcc;
        public string IDAcc { get => iDAcc; set => iDAcc = value; }

        private string userName;
        public string UserName { get => userName; set => userName = value; }

        private string password;
        public string Password { get => password; set => password = value; }

        private string iDStaff;
        public string IDStaff { get => iDStaff; set => iDStaff = value; }

        private bool accAuthorization;
        public bool AccAuthorization { get => accAuthorization; set => accAuthorization = value; }

        private bool statusAccount;
        public bool StatusAccount { get => statusAccount; set => statusAccount = value; }

        public Account(string iDAcc, string userName, string password, string iDStaff, bool accAuthorization, bool statusAccount)
        {
            this.IDAcc = iDAcc;
            this.UserName = userName;
            this.Password = password;
            this.IDStaff = iDStaff;
            this.AccAuthorization = accAuthorization;
            this.StatusAccount = statusAccount;
        }

        public Account(DataRow row)
        {
            this.IDAcc = row["ID_ACC"].ToString();
            this.UserName = row["USERNAME"].ToString();
            this.Password = row["PASSWORD"].ToString();
            this.IDStaff = row["ID_STAFF"].ToString();
            this.AccAuthorization = Convert.ToBoolean(row["ACC_AUTHORIZATION"]);
            this.StatusAccount = Convert.ToBoolean(row["STATUS_ACCOUNT"]);
        }

    }
}
