using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class Supplier
    {
        private string iDSupplier;
        public string IDSupplier { get => iDSupplier; set => iDSupplier = value; }

        private string nameSupplier;
        public string NameSupplier { get => nameSupplier; set => nameSupplier = value; }

        private string phoneNumberSup;
        public string PhoneNumberSup { get => phoneNumberSup; set => phoneNumberSup = value; }

        private string addressSup;
        public string AddressSup { get => addressSup; set => addressSup = value; }

        private bool statusSupplier;
        public bool StatusSupplier { get => statusSupplier; set => statusSupplier = value; }

        public Supplier(string iDSupplier, string nameSupplier, string phoneNumberSup, string addressSup, bool statusSupplier)
        {
            this.IDSupplier = iDSupplier;
            this.NameSupplier = nameSupplier;
            this.PhoneNumberSup = phoneNumberSup;
            this.AddressSup = addressSup;
            this.StatusSupplier = statusSupplier;
        }

        public Supplier(DataRow row)
        {
            this.IDSupplier = row["ID_SUPPLIER"].ToString();
            this.NameSupplier = row["NAME_SUPPLIER"].ToString();
            this.PhoneNumberSup = row["PHONE_NUMBER_SUP"].ToString();
            this.AddressSup = row["ADDRESS_SUP"].ToString();
            this.StatusSupplier = Convert.ToBoolean(row["STATUS_SUPPLIER"].ToString());
        }
    }
}
