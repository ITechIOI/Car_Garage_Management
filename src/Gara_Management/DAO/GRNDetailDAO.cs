using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class GRNDetailDAO
    {
        private GRNDetailDAO() { }
        private static GRNDetailDAO instance;

        public static GRNDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new GRNDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<GRNDetail> LoadGRNDetailList()
        {
            List<GRNDetail> gRNDetailList = new List<GRNDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM GRN_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                GRNDetail gRNDetail = new GRNDetail(item);
                gRNDetailList.Add(gRNDetail);
            }
            return gRNDetailList;
        }

        public List<GRNDetail> LoadGRNDetailListByLotNumber(string lot)
        {
            List<GRNDetail> gRNDetailList = new List<GRNDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM GRN_DETAILS WHERE LOTNUMBER = '" + lot + "' AND STATUS_GRN = 0");
            foreach (DataRow item in data.Rows)
            {
                GRNDetail gRNDetail = new GRNDetail(item);
                gRNDetailList.Add(gRNDetail);
            }
            return gRNDetailList;
        }

        public bool InsertGRNDetail(string lotNumber, string com, decimal price, int quantity)
        {
            string query = "EXEC USP_INSERTGRNDETAIL '" + lotNumber + "', '" + com + "', " + price + ", "+ quantity;
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }

        public bool UpdateGRNDetail(string lotNumber, string com, decimal price, int quantity)
        {
            string query = "EXEC USP_UPDATEGRNDETAIL '" + lotNumber + "', '" + com + "', " + price + ", " + quantity;
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }

        public bool DeleteGRNDetail(string lotNumber, string com)
        {
            string query = "EXEC USP_DELETEGRNDETAIL '" + lotNumber + "', '" + com + "'";
            return (DataProvider.Instance.ExecuteNonQuery(query) > 0);
        }

        public bool CheckExistedGRNDetail(string lotNumber, string com)
        {
            string query = "SELECT * FROM GRN_DETAILS WHERE LOTNUMBER = '" + lotNumber + "' AND ID_COM = '" + com + "'";
            return (DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0);
        }
    }
}
