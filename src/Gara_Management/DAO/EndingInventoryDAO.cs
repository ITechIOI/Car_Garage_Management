using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class EndingInventoryDAO
    {
        private EndingInventoryDAO() { }
        private static EndingInventoryDAO instance;

        public static EndingInventoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new EndingInventoryDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<EndingInventory> LoadEndingInventoryList()
        {
            List<EndingInventory> endingInventoryList = new List<EndingInventory>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM ENDING_INVENTORY");
            foreach (DataRow item in data.Rows)
            {
                EndingInventory endingInventory = new EndingInventory(item);
                endingInventoryList.Add(endingInventory);
            }
            return endingInventoryList;
        }
    }
}
