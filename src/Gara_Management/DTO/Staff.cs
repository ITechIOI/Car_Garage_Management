using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class Staff
    {
        private string iDStaff;
        public string IDStaff { get => iDStaff; set => iDStaff = value; }

        private string nameStaff;
        public string NameStaff { get => nameStaff; set => nameStaff = value; }

        private DateTime birthdayStaff;
        public DateTime BirthdayStaff { get => birthdayStaff; set => birthdayStaff = value; }

        private string addressStaff;
        public string AddressStaff { get => addressStaff; set => addressStaff = value; }

        private string emailStaff;
        public string EmailStaff { get => emailStaff; set => emailStaff = value; }

        private string phoneNumberStaff;
        public string PhoneNumberStaff { get => phoneNumberStaff; set => phoneNumberStaff = value; }

        private decimal salary;
        public decimal Salary { get => salary; set => salary = value; }

        private string iDPosition;
        public string IDPosition { get => iDPosition; set => iDPosition = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private bool statusStaff;
        public bool StatusStaff { get => statusStaff; set => statusStaff = value; }

        public Staff(string iDStaff, string nameStaff, DateTime birthdayStaff, string addressStaff, string emailStaff, string phoneNumberStaff, decimal salary, string iDPosition, string iDGara, bool statusStaff)
        {
            this.IDStaff = iDStaff;
            this.NameStaff = nameStaff;
            this.BirthdayStaff = birthdayStaff;
            this.AddressStaff = addressStaff;
            this.EmailStaff = emailStaff;
            this.PhoneNumberStaff = phoneNumberStaff;
            this.Salary = salary;
            this.IDPosition = iDPosition;
            this.IDGara = iDGara;
            this.StatusStaff = statusStaff;
        }

        public Staff(DataRow row)
        {
            this.IDStaff = row["ID_STAFF"].ToString();
            this.NameStaff = row["NAME_STAFF"].ToString();
            this.BirthdayStaff = Convert.ToDateTime(row["BIRTHDAY_STAFF"]);
            this.AddressStaff = row["ADDRESS_STAFF"].ToString();
            this.EmailStaff = row["EMAIL_STAFF"].ToString();
            this.PhoneNumberStaff = row["PHONE_NUMBER_STAFF"].ToString();
            this.Salary = Convert.ToDecimal(row["SALARY"].ToString());
            this.IDPosition = row["ID_POSITION"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.StatusStaff = Convert.ToBoolean(row["STATUS_STAFF"]);
        }
    }
}
