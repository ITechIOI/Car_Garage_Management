using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class CarGara
    {
        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private string addressGara;
        public string AddressGara { get => addressGara; set => addressGara = value; }

        private string phoneNumberGara;
        public string PhoneNumberGara { get => phoneNumberGara; set => phoneNumberGara = value; }

        private bool statusGara;
        public bool StatusGara { get => statusGara; set => statusGara = value; }

        public CarGara(string iDGara, string addressGara, string phoneNumberGara, bool statusGara)
        {
            this.IDGara = iDGara;
            this.AddressGara = addressGara;
            this.PhoneNumberGara = phoneNumberGara;
            this.StatusGara = statusGara;
        }

        public CarGara(DataRow row)
        {
            this.IDGara = row["ID_GARA"].ToString();
            this.AddressGara = row["ADDRESS_GARA"].ToString();
            this.PhoneNumberGara = row["PHONE_NUMBER_GARA"].ToString();
            this.StatusGara = Convert.ToBoolean(row["STATUS_GARA"]);
        }
    }
}
