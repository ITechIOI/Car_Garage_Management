using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class CustomerDetail
    {
        private string iDCus;
        public string IDCus { get => iDCus; set => iDCus = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private decimal debt;
        public decimal Debt { get => debt; set => debt = value; }

        public CustomerDetail(string iDCus, string iDGara, decimal debt)
        {
            this.IDCus = iDCus;
            this.IDGara = iDGara;
            this.Debt = debt;
        }

        public CustomerDetail(DataRow row)
        {
            this.IDCus = row["ID_CUS"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.Debt = Convert.ToDecimal(row["DEBT"].ToString());
        }
    }
}
