using Gara_Management.DAO;
using Gara_Management.DTO;
using Gara_Management.GUI.Card;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gara_Management.GUI.Item
{
    /// <summary>
    /// Interaction logic for itRepairCardDetail.xaml
    /// </summary>
    public partial class itRepairCardDetail : UserControl
    {
        public string numberPlate;
        public string recDate;
        public string idGara;
        public string idBill;
        public bool deleteThis = false;

        public itRepairCardDetail()
        {
            InitializeComponent();
        }

        public void CreateNewRepairCardDetail(int id, string idRec)
        {
            ReceptionForm receptionForm = ReceptionFormDAO.Instance.LoadReceptionFormByID(idRec);
            numberPlate = receptionForm.NumberPlate;
            recDate = receptionForm.ReceptionDate.ToString();
            idGara = receptionForm.IDGara;
            RepairPaymentBill repairPaymentBill = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillByIDRec(idRec);
            if (repairPaymentBill != null)
            {
                idBill = repairPaymentBill.IDBill;
            }
            else
            {
                RepairPaymentBillDAO.Instance.CreateNewRepairPaymentBill(idRec);
                repairPaymentBill = RepairPaymentBillDAO.Instance.LoadRepairPaymentBillByIDRec(idRec);
                idBill = repairPaymentBill.IDBill;
            }
            tbl_stt.Text = id.ToString();
        }

        public void DisableEditing()
        {
            tbl_stt.IsEnabled = false;
            tbx_description.IsReadOnly = true;
            tbx_name.IsReadOnly = true;
            tbx_price.IsReadOnly = true;
            tbx_quantity.IsReadOnly = true;
            tbx_wage.IsReadOnly = true;
            tbx_total.IsReadOnly = true;
        }

        public void EnableEditing()
        {
            tbl_stt.IsEnabled = true;
            tbx_description.IsReadOnly = false;
            tbx_name.IsReadOnly = false;
            tbx_price.IsReadOnly = false;
            tbx_quantity.IsReadOnly = false;
            tbx_wage.IsReadOnly = false;
        }

        //Tạo itRepairdCardDetail
        public void SetValue(int id, DataRow row)
        {
            numberPlate = row["NUMBER_PLATES"].ToString();
            recDate = row["RECEPTION_DATE"].ToString();
            idGara = row["ID_GARA"].ToString();
            idBill = row["ID_BILL"].ToString();
            tbl_stt.Text = id.ToString();
            tbx_description.Text = row["REPAIR_DESCRIPTION"].ToString().Trim();
            tbx_name.Text = row["NAME_COM"].ToString().Trim();
            tbx_price.Text = ((int)float.Parse(row["CUR_PRICE"].ToString())).ToString("N");
            tbx_quantity.Text = row["COM_QUANTITY"].ToString().Trim();
            tbx_wage.Text = ((int)float.Parse(row["WAGE"].ToString())).ToString("N");
            tbx_total.Text = ((int)float.Parse(row["TOTAL_PRICE"].ToString())).ToString("N");
        }

        //Lấy RPDOrdinalNum
        public string GetRPDOrdinalNum()
        {
            string idCom = CarComponentDAO.Instance.GetComponentIDByInfo(this.idGara, this.tbx_name.Text, this.tbx_wage.Text, this.tbx_price.Text);
            string ordinalNum = RepairPaymentDetailDAO.GetRPDOrdinalNum(idBill, idCom);
            return ordinalNum;
        }

        //Kiểm tra thông tin có hợp lệ không (vật tư có tồn tại không)
        public bool isValid()
        {
            string idCom = CarComponentDAO.Instance.GetComponentIDByInfo(this.idGara, this.tbx_name.Text, this.tbx_wage.Text, this.tbx_price.Text);
            if (idCom != "")
            {
                return true;
            }
            return false;
        }

        //Kiểm tra chi tiết sửa chữa đã tồn tại chưa
        public bool isExist()
        {
            string ordinalNum = GetRPDOrdinalNum();
            if (ordinalNum != "")
            {
                return true;
            }
            return false;
        }

        private void tbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (deleteThis == false) 
            {
                deleteThis = true;
                tbl_stt.FontWeight = FontWeights.Bold;
                tbx_description.FontWeight = FontWeights.Bold;
                tbx_name.FontWeight = FontWeights.Bold;
                tbx_price.FontWeight = FontWeights.Bold;
                tbx_quantity.FontWeight = FontWeights.Bold;
                tbx_wage.FontWeight = FontWeights.Bold;
                tbx_total.FontWeight = FontWeights.Bold;
            }
            else
            {
                deleteThis = false;
                tbl_stt.FontWeight = FontWeights.Normal;
                tbx_description.FontWeight = FontWeights.Normal;
                tbx_name.FontWeight = FontWeights.Normal;
                tbx_price.FontWeight = FontWeights.Normal;
                tbx_quantity.FontWeight = FontWeights.Normal;
                tbx_wage.FontWeight = FontWeights.Normal;
                tbx_total.FontWeight= FontWeights.Normal;
            }
        }
    }
}
