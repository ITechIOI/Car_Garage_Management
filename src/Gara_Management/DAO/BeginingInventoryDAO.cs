using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class BeginingInventoryDAO
    {
        private BeginingInventoryDAO() { }
        private static BeginingInventoryDAO instance;

        public static BeginingInventoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BeginingInventoryDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<BeginingInventory> LoadBeginingInventoryList()
        {
            List<BeginingInventory> beginingInventoryList = new List<BeginingInventory>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM BEGINING_INVENTORY");
            foreach (DataRow item in data.Rows)
            {
                BeginingInventory beginingInventory = new BeginingInventory(item);
                beginingInventoryList.Add(beginingInventory);
            }
            return beginingInventoryList;
        }
    }
}
