using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class StaffDAO
    {
        private StaffDAO() { }
        private static StaffDAO instance;

        public static StaffDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StaffDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Staff> LoadStaffList(string gara)
        {
            List<Staff> staffList = new List<Staff>();
            string query = "SELECT ID_STAFF, NAME_STAFF, BIRTHDAY_STAFF, ADDRESS_STAFF, EMAIL_STAFF, " +
                "PHONE_NUMBER_STAFF, SALARY, NAME_POS, ID_GARA FROM STAFFS JOIN STAFF_POSITION " +
                "ON STAFF_POSITION.ID_POS=STAFFS.ID_POSITION WHERE ID_GARA = '" + gara + "' AND STATUS_STAFF=0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                staffList.Add(staff);
            }
            return staffList;
        }

        public Staff GetStaffById(int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT ID_STAFF, NAME_STAFF, BIRTHDAY_STAFF, ADDRESS_STAFF, EMAIL_STAFF, " +
                "PHONE_NUMBER_STAFF, SALARY, NAME_POS, ID_GARA FROM STAFFS JOIN STAFF_POSITION " +
                "ON STAFF_POSITION.ID_POS=STAFFS.ID_POSITION WHERE ID_STAFF = '" + id + "'");
            if (data.Rows.Count == 0)
                return null;
            return new Staff(data.Rows[0]);
        }

        public bool InsertStaff(string name, string birthday, string address, string email, string phone, int salary,
            string position, string gara) 
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_INSERTSTAFF @name=N'" + name + "', @birthday='"
                + birthday + "' , @address=N'" + address + "', @email='" + email + "', @phone = '"
                + phone + "', @salary=" + salary.ToString() + ", @position=N'" + position + "', @gara='"
                + gara + "'") > 0);
        }

        public bool UpdateStaff (string id, string name, string birthday, string address, string email, string phone, int salary,
            string position)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC UPDATESTAFF @id='" + id + "', @name=N'" + name + "', @birthday='"
                + birthday + "' , @address=N'" + address + "', @email='" + email + "', @phone = '"
                + phone + "', @salary=" + salary.ToString() + ", @position=N'" + position + "'") > 0);
        }

        public bool DeleteStaff (string id)
        {
            return (DataProvider.Instance.ExecuteNonQuery("EXEC USP_DELETESTAFF @id='" + id + "'") > 0);
        }

    }
}
