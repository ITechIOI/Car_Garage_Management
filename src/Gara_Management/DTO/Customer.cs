using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class Customer
    {
        private string iDCus;
        public string IDCus { get => iDCus; set => iDCus = value; }

        private string nameCus;
        public string NameCus { get => nameCus; set => nameCus = value; }

        private string phoneNumberCus;
        public string PhoneNumberCus { get => phoneNumberCus; set => phoneNumberCus = value; }

        private string addressCus;
        public string AddressCus { get => addressCus; set => addressCus = value; }

        private decimal debt;
        public decimal Debt { get => debt; set => debt = value; }

        private bool statusCus;
        public bool StatusCus { get => statusCus; set => statusCus = value; }

        public Customer(string iDCus, string nameCus, string phoneNumberCus, string addressCus, decimal debt, bool statusCus)
        {
            this.IDCus = iDCus;
            this.NameCus = nameCus;
            this.PhoneNumberCus = phoneNumberCus;
            this.AddressCus = addressCus;
            this.Debt = debt;
            this.StatusCus = statusCus;
        }

        public Customer(DataRow row)
        {
            this.IDCus = row["ID_CUS"].ToString();
            this.NameCus = row["NAME_CUS"].ToString();
            this.PhoneNumberCus = row["PHONE_NUMBER_CUS"].ToString();
            this.AddressCus = row["ADDRESS_CUS"].ToString();
            this.Debt = Convert.ToDecimal(row["DEBT"].ToString());
            this.StatusCus = Convert.ToBoolean(row["STATUS_CUS"].ToString());
        }
    }
}
