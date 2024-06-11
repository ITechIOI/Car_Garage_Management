using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Themes;

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
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_GARA = '" + gara + "' " +
                "AND STATUS_BILL = 0 AND STATUS_REC = 0";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }
        public int LoadNotFinishedRepairPaymentBillList(string gara)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, " +
                "TOTAL_PAYMENT, PAID, STATUS_BILL FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS " +
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_GARA = '" + gara + "' " +
                "AND STATUS_BILL = 0 AND STATUS_REC = 0 AND COMPLETION_DATE = '1/1/1900'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data.Rows.Count;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByNumberPlate(string gara, string numberPlate)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, " +
                "TOTAL_PAYMENT, PAID, STATUS_BILL FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS " +
                "ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC WHERE ID_GARA = '" + gara + "' " +
                "AND STATUS_BILL = 0 AND STATUS_REC = 0 AND NUMBER_PLATES LIKE '%" + numberPlate + "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDate(string gara, string date)
        {
            string query = "EXEC USP_GETLISTBILLBYDATE '" + gara + "', '" + date + "'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }
        
        public List<RepairPaymentBill> LoadRepairPaymentBillListByDateAndNumberPlate(string gara, string date, string num)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATEANDNUMBERPLATE '" + gara + "', '" + date + "', '%" + num+ "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }
        public List<RepairPaymentBill> LoadRepairPaymentBillListByMoney(string gara, int minMoney, int maxMoney)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, TOTAL_PAYMENT, PAID, STATUS_BILL " +
                "FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BILL = 0 AND STATUS_REC = 0 " +
                "AND TOTAL_PAYMENT BETWEEN " + minMoney + " AND " + maxMoney;
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByMoneyAndNumberPlate(string gara, int minMoney, int maxMoney, string num)
        {
            string query = "SELECT ID_BILL, REPAIR_PAYMENT_BILL.ID_REC, COMPLETION_DATE, TOTAL_PAYMENT, PAID, STATUS_BILL " +
                "FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BILL = 0 AND STATUS_REC = 0 " +
                "AND TOTAL_PAYMENT BETWEEN " + minMoney + " AND " + maxMoney + " AND NUMBER_PLATES LIKE '%" + num + "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDate2(string gara, string date, string endDate)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATE2 '" + gara + "', '" + date + "', '" + endDate + "'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDate2AndNumberPlate(string gara, string date, string endDate, string num)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATE2ANDNUMBERPLATE '" + gara + "', '" + date + "', '" + endDate + "', '%" + num + "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDateAndMoney(string gara, string date, int minMoney, int maxMoney)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATEANDMONEY '" + gara + "', '" + date + "', " + minMoney + ", " + maxMoney;
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDateMoneyAndNumberPlate(string gara, string date, int minMoney,
            int maxMoney, string num)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATEMONEYANDNUMBERPLATE '" + gara + "', '" + date + "', " +
                minMoney + ", "+ maxMoney + ", '%" + num + "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDate2AndMoney(string gara, string date, string endDate, int minMoney, int maxMoney)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATE2ANDMONEY '" + gara + "', '" + date + "', '" + 
                endDate + "', "+ minMoney+ ", " + maxMoney;
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public List<RepairPaymentBill> LoadRepairPaymentBillListByDate2MoneyAndNumberPlate(string gara, string date, string endDate, 
            int minMoney, int maxMoney, string num)
        {
            string query = "EXEC DBO.USP_GETLISTBILLBYDATE2MONEYANDNUMBERPLATE '" + gara + "', '" + date + "', '" + 
                endDate + "', " + minMoney + " ," + maxMoney + ", '%" + num + "%'";
            List<RepairPaymentBill> repairPaymentBillList = new List<RepairPaymentBill>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(item);
                repairPaymentBillList.Add(repairPaymentBill);
            }
            return repairPaymentBillList;
        }

        public int GetMaxTotalPayment(string gara)
        {
            string query = "SELECT MAX(TOTAL_PAYMENT) TOTAL_PAYMENT " +
                "FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BILL = 0 AND STATUS_REC = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            string s = data.Rows[0]["TOTAL_PAYMENT"].ToString();
            decimal d;
            if (Decimal.TryParse(s, out d))
            {
                return (int)d;
            }
            return 0;
        }

        public int GetMinTotalPayment(string gara)
        {
            string query = "SELECT MIN(TOTAL_PAYMENT) TOTAL_PAYMENT " +
                "FROM REPAIR_PAYMENT_BILL JOIN RECEPTION_FORMS ON REPAIR_PAYMENT_BILL.ID_REC = RECEPTION_FORMS.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BILL = 0 AND STATUS_REC = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            string s = data.Rows[0]["TOTAL_PAYMENT"].ToString();
            decimal d;
            if (Decimal.TryParse(s, out d))
            {
                return (int)d;
            }
            return 0;
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


        //lấy thông tin RepairPaymentBill theo idRec
        public RepairPaymentBill LoadRepairPaymentBillByIDRec(string idRec)
        {
            string query = "SELECT * FROM REPAIR_PAYMENT_BILL WHERE ID_REC = '" + idRec + "'";
            if (DataProvider.Instance.ExecuteScalar(query) != null)
            {
                RepairPaymentBill repairPaymentBill = new RepairPaymentBill(DataProvider.Instance.ExecuteQuery(query).Rows[0]);
                return repairPaymentBill;
            }
            return null;
        }

        //tạo RepairPaymentBill cho ReceptionForm chưa có
        public void CreateNewRepairPaymentBill(string idRec)
        {
            string query = "EXEC USP_CREATE_REPAIRPAYMENTBILL @ID_REC = '" + idRec + "'";
            DataProvider.Instance.ExecuteQuery(query);
        }
        public bool SetFinishBill(string id)
        {
            string query = "UPDATE REPAIR_PAYMENT_BILL SET COMPLETION_DATE = GETDATE() WHERE ID_REC = '" + id +"'";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool DeleteRepairPaymentBill(string idBill)
        {
            string query = "EXEC DELETEBILL '" + idBill + "'";
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public string GetIDBillByIDRec(string idRec)
        {
            string query = "SELECT ID_BILL FROM REPAIR_PAYMENT_BILL WHERE ID_REC = '" + idRec + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0)
            {
                return "";
            }
            return data.Rows[0]["ID_BILL"].ToString();
        }
    }
}
