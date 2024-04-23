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

        public List<RepairPaymentBill> LoadRepairPaymentBillList()
        {
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM REPAIR_PAYMENT_BILL");
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }
    }
}
