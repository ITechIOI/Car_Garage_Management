using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Themes;

namespace Gara_Management.DAO
{
    public class ReceptionFormDAO
    {
        private ReceptionFormDAO() { }
        private static ReceptionFormDAO instance;

        public static ReceptionFormDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ReceptionFormDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<ReceptionForm> LoadReceptionFormtList(string gara)
        {
            string query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
                "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
                "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900'" +
                " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
                "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL) AND ID_GARA = '" + gara + "'";
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

        public List<ReceptionForm> LoadReceptionFormtListToday(string gara)
        {
            string query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
                "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
                "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE = '1/1/1900' AND RECEPTION_DATE = GETDATE()" +
                " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
                "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL) AND ID_GARA = '" + gara + "' AND RECEPTION_DATE = GETDATE()";
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

        public int GetMaxDebt(string gara)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT DBO.GET_MAX_TOTALPAYMENT('" + gara + "') TOTALPAYMENT");
            return (int)Convert.ToDecimal(data.Rows[0]["TOTALPAYMENT"].ToString());
        }
        public int GetMinDebt(string gara)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT DBO.GET_MIN_TOTALPAYMENT('" + gara + "') TOTALPAYMENT");
            return (int)Convert.ToDecimal(data.Rows[0]["TOTALPAYMENT"].ToString());
        }


        public List<ReceptionForm> LoadReceptionFormtListByNameCus(string gara, string name)
        {
            string query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
                "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
                "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_CUS IN " +
                "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
                "LIKE [DBO].[non_unicode_convert]('%" + name + "%') AND STATUS_CUS = 0)" +
                " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
                "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL) AND ID_GARA = '" + gara +"' AND ID_CUS IN (SELECT ID_CUS FROM CUSTOMERS " +
                "WHERE [DBO].[non_unicode_convert](NAME_CUS) LIKE [DBO].[non_unicode_convert]('%" + name + "%')" +
                " AND STATUS_CUS = 0)";
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }


        public List<ReceptionForm> LoadReceptionFormtListByCarBrand(string gara, string brand)
        {
            string query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_BRAND = '" + brand + "'" +
               " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
               "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL) AND ID_BRAND = '" + brand + "' AND ID_GARA = '" + gara + "'";
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }
        public List<ReceptionForm> LoadReceptionFormtListByDebt(string gara, int minDebt, int maxDebt)
        {
            string query;
            if (minDebt == 0)
            {
                query = "SELECT RECEPTION_FORMS.ID_REC,  ID_CUS, ID_BRAND, ID_GARA, NUMBER_PLATES,RECEPTION_DATE, STATUS_REC " +
                "FROM RECEPTION_FORMS JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND TOTAL_PAYMENT " +
                "BETWEEN " + minDebt + " AND "+ maxDebt +" UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN(SELECT ID_REC " +
                "FROM REPAIR_PAYMENT_BILL WHERE TOTAL_PAYMENT NOT BETWEEN " + minDebt + " AND " + maxDebt + ") AND ID_GARA = '" + gara + "'";
            }
            else
            {
                query = "SELECT RECEPTION_FORMS.ID_REC,  ID_CUS, ID_BRAND, ID_GARA, NUMBER_PLATES,RECEPTION_DATE, STATUS_REC " +
                "FROM RECEPTION_FORMS JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND " +
                "TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt;
            }    
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }
        public List<ReceptionForm> LoadReceptionFormtListByCarBrandAndDebt(string gara, string brand, int minDebt, int maxDebt)
        {
            string query;
            if (minDebt == 0)
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_BRAND = '" + brand +
               "' AND TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt +
               " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
               "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL WHERE TOTAL_PAYMENT NOT BETWEEN " + minDebt + " AND " + maxDebt +
               ") AND ID_BRAND = '" + brand + "' AND ID_GARA = '" + gara + "'";
            }
            else
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_BRAND = '" + brand +
               "' AND TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt;
            }    
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

        public List<ReceptionForm> LoadReceptionFormtListByCusCarBrandAndDebt(string gara,string cus, string brand, int minDebt, int maxDebt)
        {
            string query;
            if (minDebt == 0)
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND ID_CUS IN " + "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
               "LIKE [DBO].[non_unicode_convert]('%" + cus + "%')) AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_BRAND = '" + brand +
               "' AND TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt +
               " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
               "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL WHERE TOTAL_PAYMENT NOT BETWEEN " + minDebt + " AND " + maxDebt +
               ") AND ID_BRAND = '" + brand + "' AND ID_GARA = '" + gara + "' AND ID_CUS IN " +
               "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
               "LIKE [DBO].[non_unicode_convert]('%" + cus + "%'))";
            }
            else
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND ID_CUS IN (SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) "
               +"LIKE [DBO].[non_unicode_convert]('%" + cus + "%'))  AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' " +
               "AND ID_BRAND = '" + brand + "' ANDTOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt;
            }
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

        public List<ReceptionForm> LoadReceptionFormtListByCusAndDebt(string gara, string cus, int minDebt, int maxDebt)
        {
            string query;
            if (minDebt == 0)
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND ID_CUS IN " + "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
               "LIKE [DBO].[non_unicode_convert]('%" + cus + "%')) AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900'" +
               " AND TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt +
               " UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
               "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL WHERE TOTAL_PAYMENT NOT BETWEEN " + minDebt + " AND " + maxDebt +
               ") AND ID_GARA = '" + gara + "' AND ID_CUS IN " +
               "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
               "LIKE [DBO].[non_unicode_convert]('%" + cus + "%'))";
            }
            else
            {
                query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
               "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
               "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
               "WHERE ID_GARA = '" + gara + "' AND ID_CUS IN (SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) "
               + "LIKE [DBO].[non_unicode_convert]('%" + cus + "%'))  AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' " +
               "AND TOTAL_PAYMENT BETWEEN " + minDebt + " AND " + maxDebt;
            }

            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }
        public List<ReceptionForm> LoadReceptionFormtListByCusAndCarBrand(string gara, string cus, string brand)
        {
                string query = "SELECT RECEPTION_FORMS.ID_REC, ID_CUS, ID_BRAND, ID_GARA, " +
                   "NUMBER_PLATES,RECEPTION_DATE, STATUS_REC FROM RECEPTION_FORMS " +
                   "JOIN REPAIR_PAYMENT_BILL ON RECEPTION_FORMS.ID_REC = REPAIR_PAYMENT_BILL.ID_REC " +
                   "WHERE ID_GARA = '" + gara + "' AND ID_CUS IN " + "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
                   "LIKE [DBO].[non_unicode_convert]('%" + cus + "%')) AND STATUS_REC = 0 AND COMPLETION_DATE ='1/1/1900' AND ID_BRAND = '" + brand +
                   "' UNION SELECT * FROM RECEPTION_FORMS WHERE STATUS_REC = 0 AND ID_REC NOT IN " +
                   "(SELECT ID_REC FROM REPAIR_PAYMENT_BILL) AND ID_BRAND = '" + 
                   brand + "' AND ID_GARA = '" + gara + "' AND ID_CUS IN " +
                   "(SELECT ID_CUS FROM CUSTOMERS WHERE [DBO].[non_unicode_convert](NAME_CUS) " +
                   "LIKE [DBO].[non_unicode_convert]('%" + cus + "%'))";
            
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

        //lấy số id cao nhất từ RECEPTION_FORMS
        public static int GetMaxID()
        {
            string query = "SELECT dbo.GET_MAX_IDRECEPTION()";
            int maxID = int.Parse(DataProvider.Instance.ExecuteScalar(query).ToString());
            return maxID;
        }

        //lấy id Rec trong database theo thông tin có sẵn
        public static string GetReceptionFormIDByInfo(ReceptionForm receptionForm)
        {
            string query = "EXEC USP_GET_IDRECEPTION @ID_CUS , @ID_GARA , @NUMBER_PLATES , @ID_BRAND , @RECEPTION_DATE";
            string id = DataProvider.Instance.ExecuteScalar(query, new object[] { receptionForm.IDCus, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.IDBrand, receptionForm.ReceptionDate }).ToString().Trim();
            return id;
        }

        //thêm dữ liệu từ phiếu sửa chữa vào RECEPTION_FORMS
        public static int InsertReceptionForm(ReceptionForm receptionForm)
        {
            int result = 0;
            string query = "EXEC USP_INSERT_RECEPTIONFORM @ID_CUS , @ID_BRAND , @ID_GARA , @NUMBER_PLATES , @RECEPTION_DATE";
            result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { receptionForm.IDCus, receptionForm.IDBrand, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.ReceptionDate });
            return result;
        }

        //chỉnh sửa dữ liệu của RECEPTION_FORMS
        public static int UpdateReceptionForm(ReceptionForm receptionForm)
        {
            int result = 0;
            string query = "EXEC USP_UPDATE_RECEPTIONFORM @ID_REC , @ID_CUS , @ID_BRAND , @ID_GARA , @NUMBER_PLATES , @RECEPTION_DATE";
            result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { receptionForm.IDRec, receptionForm.IDCus, receptionForm.IDBrand, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.ReceptionDate });
            return result;
        }

        //kiểm tra xem thông tin có trùng lặp không 
        public static int IsExist(ReceptionForm receptionForm)
        {
            string IDCheck = "SELECT * FROM RECEPTION_FORMS WHERE ID_REC = '" + receptionForm.IDRec + "'";
            object i = DataProvider.Instance.ExecuteScalar(IDCheck);
            string InfoCheck = "EXEC USP_GET_IDRECEPTION @ID_CUS , @ID_GARA , @NUMBER_PLATES , @ID_BRAND , @RECEPTION_DATE";
            object j = DataProvider.Instance.ExecuteScalar(InfoCheck, new object[] { receptionForm.IDCus, receptionForm.IDGara, receptionForm.NumberPlate, receptionForm.IDBrand, receptionForm.ReceptionDate });
            if (j != null)
            {
                //thông tin phiếu trùng lặp
                return 0;
            }
            else if (i != null)
            {
                //id Rec trùng lặp
                return 1;
            }
            //không trùng lặp
            return -1;
        }

        //lấy thông tin Reception Form theo ID
        public ReceptionForm LoadReceptionFormByID(string idRec, string idGara)
        {
            string loadReceptionForm = "SELECT * FROM RECEPTION_FORMS WHERE ID_REC = '" + idRec + "' AND ID_GARA = '" + idGara + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(loadReceptionForm);
            if (data.Rows.Count == 0)
                return null;
            ReceptionForm receptionForm = new ReceptionForm(data.Rows[0]);
            return receptionForm;
        }

    }
}
