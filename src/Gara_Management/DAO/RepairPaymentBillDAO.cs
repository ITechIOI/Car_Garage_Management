using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class RepairPaymentBillDAO
    {
        private RepairPaymentBillDAO() { }
        private static RepairPaymentBillDAO instance;

        public static RepairPaymentBillDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RepairPaymentBillDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillList(string gara)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, " +
                "TOTAL_PAYMENT, PAID, STATUS_BILL FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS " +
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_GARA = '" + gara + "'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public RepairPaymentBill GetRepairPaymentBillByIDRec(string gara, string idRec)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, " +
                "TOTAL_PAYMENT, PAID, STATUS_BILL FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS " +
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_GARA = '" + gara +
                "' AND REPAIR_PAYMENT_BILL.ID_REC = '" + idRec + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0)
                return null;
            return new RepairPaymentBill(data.Rows[0]);
        }

    }
}
