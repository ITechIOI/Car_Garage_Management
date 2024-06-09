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
