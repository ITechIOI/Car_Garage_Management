using Gara_Management.DAO;
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

        public itRepairCardDetail()
        {
            InitializeComponent();
        }

        public void CreateNewRepairCardDetail(int id)
        {
            tbl_stt.Text = id.ToString();
        }

        public void DisableEditing()
        {
            tbx_description.IsReadOnly = true;
            tbx_name.IsReadOnly = true;
            tbx_price.IsReadOnly = true;
            tbx_quantity.IsReadOnly = true;
            tbx_wage.IsReadOnly = true;
            tbx_total.IsReadOnly = true;
        }

        public void EnableEditing()
        {
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
            tbl_stt.Text = id.ToString();
            tbx_description.Text = row["REPAIR_DESCRIPTION"].ToString().Trim();
            tbx_name.Text = row["NAME_COM"].ToString().Trim();
            tbx_price.Text = (float.Parse(row["CUR_PRICE"].ToString())).ToString("N");
            tbx_quantity.Text = row["COM_QUANTITY"].ToString().Trim();
            tbx_wage.Text = (float.Parse(row["WAGE"].ToString())).ToString("N");
            tbx_total.Text = (float.Parse(row["TOTAL_PRICE"].ToString())).ToString("N");
        }

        //Kiểm tra thông tin có hợp lệ không (vật tư có tồn tại không)
        public bool isValid()
        {
            string idCom = ComponentDetailDAO.GetComponentIDByInfo(this.idGara, this.tbx_name.Text, this.tbx_wage.Text, this.tbx_price.Text);
            if (idCom != "")
            {
                return true;
            }
            return false;
        }
    }
}
