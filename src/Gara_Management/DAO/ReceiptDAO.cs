using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    internal class ReceiptDAO
    {
        private ReceiptDAO() { }
        private static ReceiptDAO instance;
        public static ReceiptDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReceiptDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Receipt> LoadListReceipt(string gara, string cus)
        {
            List<Receipt> list = new List<Receipt>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM RECEIPT " +
                "WHERE ID_GARA = '" + gara + "' AND ID_CUS = '" + cus + "'");
            foreach (DataRow item in data.Rows)
            {
                Receipt receipt = new Receipt(item);
                list.Add(receipt);
            }
            return list;
        }

    }
}
