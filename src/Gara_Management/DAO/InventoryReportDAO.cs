﻿using Gara_Management.DTO;
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

        public bool SummarizeBeginingInventory(string gara, string com, int quantity)
        {
            string query = "EXEC SUMMARIZEBI '" + DateTime.Now.ToString("dd/MM/yyyy") +"', '" + com + "', '" + gara + "', " + quantity;
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool SummarizeEndingInventory(string gara, string com, int quantity)
        {
            string query = "EXEC SUMMARIZEEI '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + com + "', '" + gara + "', " + quantity;
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool CheckExistBeginingInventory(string gara)
        {
            string query = "SELECT * FROM BEGINING_INVENTORY WHERE ID_GARA = '" + gara + 
                "' AND MONTH(RENDERING_TIME_BI) = " + DateTime.Now.Month + " AND YEAR(RENDERING_TIME_BI) =" + DateTime.Now.Year;
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool CheckExistEndingInventory(string gara)
        {
            string query = "SELECT * FROM ENDING_INVENTORY WHERE ID_GARA = '" + gara +
               "' AND MONTH(RENDERING_TIME_EI) = " + DateTime.Now.Month + " AND YEAR(RENDERING_TIME_EI) =" + DateTime.Now.Year; 
            return DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0;
        }
    }
}
