using Gara_Management.DTO;
using Gara_Management.GUI.Item;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

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

        //Lấy RPDOrdinalNum theo IDBill và IDCom
        public static string GetRPDOrdinalNum(string idBill, string idCom)
        {
            string query = "SELECT dbo.GET_RPD_ORDINALNUM('" + idBill + "', '" + idCom + "')";
            object ordinalNum = DataProvider.Instance.ExecuteScalar(query);
            if (ordinalNum != null)
            {
                return ordinalNum.ToString();
            }
            return "";
        }

        //Lấy thông tin từ database theo IDREC vào itRepairCardDetail
        public List<itRepairCardDetail> LoadItRepairCardDetail(string id)
        {
            int stt = 1;
            List<itRepairCardDetail> list = new List<itRepairCardDetail>();
            string query = "EXEC USP_LOAD_REPAIR_CARD_DETAILS @ID_REC";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            foreach (DataRow row in dataTable.Rows)
            {
                itRepairCardDetail item = new itRepairCardDetail();
                item.SetValue(stt, row);
                item.DisableEditing();
                list.Add(item);
                stt++;
            }
            return list;
        }
        public List<RepairPaymentDetail> LoadListRepairDetail(string idRec)
        {
            int stt = 1;
            List<RepairPaymentDetail> list = new List<RepairPaymentDetail>();
            string query = "SELECT * FROM REPAIR_PAYMENT_DETAILS WHERE ID_BILL = '" + idRec +"' AND STATUS_RPD = 0";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                RepairPaymentDetail detail = new RepairPaymentDetail(row);
                list.Add(detail);
            }
            return list;
        }

        //Thêm dữ liệu vào REPAIR PAYMENT DETAILS
        public bool InsertRepairCardDetail(string idRec, itRepairCardDetail item)
        {
            string query = "EXEC USP_INSERT_REPAIR_CARD_DETAILS @ID_REC , @REPAIR_DESCRIPTION , @NAME_COM ,  @CUR_PRICE , @COM_QUANTITY , @WAGE";
            return (DataProvider.Instance.ExecuteNonQuery(query, new object[] { idRec, item.tbx_description.Text, item.tbx_name.Text, item.tbx_price.Text, item.tbx_quantity.Text, item.tbx_wage.Text }) > 0);
        }

        //Cập nhật dữ liệu REPAIR PAYMENT DETAILS
        public bool UpdateRepairCardDetail(string ordinalNum, itRepairCardDetail item)
        {
            string query = "EXEC USP_UPDATE_REPAIR_CAR_DETAILS @RPD_ORDINAL_NUM , @REPAIR_DESCRIPTION , @COM_QUANTITY";
            return (DataProvider.Instance.ExecuteNonQuery(query, new object[] { ordinalNum, item.tbx_description.Text, item.tbx_quantity.Text }) > 0);
        }

        //Xóa dữ liệu REPAIR PAYMENT DETAILS
        public bool DeleteRepairCardDetail(string ordinalNum) 
        {
            string query = "EXEC USP_DELETE_REPAIR_CAR_DETAILS @RPD_ORDINAL_NUM";
            return (DataProvider.Instance.ExecuteNonQuery(query, new object[] { ordinalNum }) > 0);
        }
    }
}
