using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class StaffPositionDAO
    {
        private StaffPositionDAO() { }
        private static StaffPositionDAO instance;

        public static StaffPositionDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new StaffPositionDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<StaffPosition> LoadStaffPositionList()
        {
            List<StaffPosition> staffPositionList = new List<StaffPosition>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM STAFF_POSITION");
            foreach (DataRow item in data.Rows)
            {
                StaffPosition staffPosition = new StaffPosition(item);
                staffPositionList.Add(staffPosition);
            }
            return staffPositionList;
        }
    }
}
