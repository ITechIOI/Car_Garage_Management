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
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM GRN_DETAILS WHERE LOTNUMBER = '" + lot + "'");
            foreach (DataRow item in data.Rows)
            {
                GRNDetail gRNDetail = new GRNDetail(item);
                gRNDetailList.Add(gRNDetail);
            }
            return gRNDetailList;
        }

    }
}
