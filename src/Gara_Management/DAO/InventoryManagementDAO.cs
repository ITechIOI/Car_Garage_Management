using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class InventoryManagementDAO
    {
        private InventoryManagementDAO() { }
        private static InventoryManagementDAO instance;

        public static InventoryManagementDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryManagementDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<InventoryManagement> LoadInventoryManagementList()
        {
            List<InventoryManagement> inventoryManagementList = new List<InventoryManagement>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM INVENTORY_MANAGEMENT");
            foreach (DataRow item in data.Rows)
            {
                InventoryManagement inventoryManagement = new InventoryManagement(item);
                inventoryManagementList.Add(inventoryManagement);
            }
            return inventoryManagementList;
        }
    }
}
