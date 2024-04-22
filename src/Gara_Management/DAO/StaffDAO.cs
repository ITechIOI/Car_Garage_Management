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

        public List<Staff> LoadStaffList()
        {
            List<Staff> staffList = new List<Staff>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM STAFFS");
            foreach (DataRow item in data.Rows)
            {
                Staff staff = new Staff(item);
                staffList.Add(staff);
            }
            return staffList;
        }
    }
}
