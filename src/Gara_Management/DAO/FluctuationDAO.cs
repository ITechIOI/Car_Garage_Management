using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    internal class FluctuationDAO
    {
        private FluctuationDAO() { }
        private static FluctuationDAO instance;
        public static FluctuationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FluctuationDAO();
                }    
                return instance;
            }
            private set { instance = value; }
        }
        public List<Fluctuation> LoadListFluctuationOfCus(string idCus, string gara)
        {
            List<Fluctuation> list = new List<Fluctuation>();
            string query = "SELECT * FROM (SELECT ID_CUS CUS, N'Phiếu thu tiền' CONTENT, ID_RECEIPT ID, " +
                "RECEIPT_DATE DDATE, -1*PROCEEDS MONEY  FROM RECEIPT WHERE ID_CUS = '" + idCus + "' AND " +
                "ID_GARA = '" + gara +"' UNION SELECT ID_CUS CUS, N'Phiếu sửa chữa' CONTENT, REPAIR_PAYMENT_BILL.ID_REC ID, " +
                "COMPLETION_DATE DDATE, TOTAL_PAYMENT MONEY FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS " +
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_CUS = '" + idCus + "' AND ID_GARA = '" + 
                gara + "' AND STATUS_BILL = 0 AND STATUS_REC = 0) A " +
                "ORDER BY DDATE DESC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Fluctuation fluctuation = new Fluctuation(row);
                list.Add(fluctuation);
            }
            return list;
        }
    }
}
