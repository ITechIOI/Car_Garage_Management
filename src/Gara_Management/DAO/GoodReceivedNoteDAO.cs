using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class GoodReceivedNoteDAO
    {
        private GoodReceivedNoteDAO() { }
        private static GoodReceivedNoteDAO instance;

        public static GoodReceivedNoteDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GoodReceivedNoteDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteList(string gara)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, " +
                "DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN FROM GOOD_RECEIVED_NOTES " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_GRN = 0");
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplier(string gara, string supplier)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, " +
                "DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN FROM GOOD_RECEIVED_NOTES " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_GRN = 0 " +
                "AND [DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%')");
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByStartDate(string gara, string startDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND IMPORT_TIME >= '" + startDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByEndDate(string gara, string endDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND IMPORT_TIME <= '" + endDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByPrice(string gara, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND (TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierAndStartDate(string gara, string supplier, string startDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME >= '" + startDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierAndEndDate(string gara, string supplier, string endDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME <= '" + endDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierAndPrice(string gara, string supplier, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByStartAndEndDate(string gara, string startDate, string endDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "IMPORT_TIME >= '" + startDate + "' AND " +
                "IMPORT_TIME <= '" + endDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByStartDateAndPrice(string gara, string startDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "IMPORT_TIME >= '" + startDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByEndDateAndPrice(string gara, string endDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "IMPORT_TIME <= '" + endDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierStartAndEndDate(string gara, string supplier, string startDate, string endDate)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME >= '" + startDate + "' AND IMPORT_TIME <= '" + endDate + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierStartDateAndPrice(string gara, string supplier, string startDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME >= '" + startDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierEndDateAndPrice(string gara, string supplier, string endDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME <= '" + endDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListByStartEndDateAndPrice(string gara, string startDate, string endDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "IMPORT_TIME >= '" + startDate + "' AND  " +
                "IMPORT_TIME <= '" + endDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public List<GoodReceivedNote> LoadGoodReceivedNoteListBySupplierStartEndDateAndPrice(string gara, string supplier, string startDate, string endDate, int minPrice, int maxPrice)
        {
            List<GoodReceivedNote> goodReceivedNoteList = new List<GoodReceivedNote>();
            string query = "SELECT LOTNUMBER, SUPPLIER, IMPORT_TIME, DATA_ENTRY_STAFF, TOTAL_PAYMENT_GRN, STATUS_GRN " +
                "FROM GOOD_RECEIVED_NOTES WHERE STATUS_GRN = 0 AND " +
                "ID_GARA = '" + gara + "' AND " +
                "[DBO].[non_unicode_convert](SUPPLIER) LIKE [DBO].[non_unicode_convert]('%" + supplier + "%') AND " +
                "IMPORT_TIME >= '" + startDate + "' AND " +
                "IMPORT_TIME <= '" + endDate + "' AND " +
                "(TOTAL_PAYMENT_GRN BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                GoodReceivedNote goodReceivedNote = new GoodReceivedNote(item);
                goodReceivedNoteList.Add(goodReceivedNote);
            }
            return goodReceivedNoteList;
        }

        public bool InsertGoodReceivedNote(string lotNumber, string supplier, string gara, string importTime, string staff)
        {
            string query = "EXEC USP_INSERTGRN '" + lotNumber + "', N'" + supplier + "', '" + gara + "', '" +
                importTime + "', '" + staff + "'";
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }
        public bool UpdateGoodReceivedNote(string lotNumber, string supplier, string gara, string importTime, string staff)
        {
            string query = "EXEC USP_UPDATEGRN '" + lotNumber + "', N'" + supplier + "', '" + gara + "', '" +
                importTime + "', '" + staff + "'";
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }

        public bool DeleteGoodReceivedNote(string lotNumber)
        {
            string query = "EXEC USP_DELETEGRN '" + lotNumber + "'";
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }

        public int GetMaxLotNumber()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT LOTNUMBER = dbo.GET_MAX_LOTNUMBER()");
            return int.Parse(data.Rows[0]["LOTNUMBER"].ToString());
        }
    }
}
