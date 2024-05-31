using Gara_Management.DTO;
using Gara_Management.GUI.Item;
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

        //Thêm dòng dữ liệu vào REPAIR PAYMENT DETAILS
        private void InsertRepairCardDetail(string id, itRepairCardDetail item)
        {
            string query = "EXEC USP_INSERT_REPAIR_CARD_DETAILS @ID_REC , @REPAIR_DESCRIPTION , @NAME_COM ,  @CUR_PRICE , @COM_QUANTITY , @WAGE";
            DataProvider.Instance.ExecuteQuery(query, new object[] { id, item.tbx_description.Text, item.tbx_name.Text, item.tbx_price.Text, item.tbx_quantity.Text, item.tbx_wage.Text });
        }

        //Cập nhật dữ liệu 

    }
}
