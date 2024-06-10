using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class InventoryReportDAO
    {
        private InventoryReportDAO() { }
        private static InventoryReportDAO instance;

        public static InventoryReportDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new InventoryReportDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<InventoryReport> LoadEndingInventoryListByDate(string gara, DateTime date)
        {
            List<InventoryReport> InventoryList = new List<InventoryReport>();
            DataTable data = DataProvider.Instance.ExecuteQuery("EXEC USP_GETDATAFORINVENTORYREPORT '" + gara + "', '"
                + date.ToString("dd/MM/yyyy") + "'");
            foreach (DataRow item in data.Rows)
            {
                InventoryReport Inventory = new InventoryReport(item);
                InventoryList.Add(Inventory);
            }
            return InventoryList;
        }
    }
}
