using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class RepairPaymentDetailDAO
    {
        private RepairPaymentDetailDAO() { }
        private static RepairPaymentDetailDAO instance;

        public static RepairPaymentDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new RepairPaymentDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<RepairPaymentDetail> LoadRepairPaymentDetailList()
        {
            List<RepairPaymentDetail> repairPaymentDetailList = new List<RepairPaymentDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM REPAIR_PAYMENT_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentDetail repairPaymentDetail = new RepairPaymentDetail(item);
                repairPaymentDetailList.Add(repairPaymentDetail);
            }
            return repairPaymentDetailList;
        }
    }
}
