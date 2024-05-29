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
        public itRepairCardDetail()
        {
            InitializeComponent();
        }

        public bool isValid()
        {
            return true;
        }

        public void SetValue(int id, DataRow row)
        {
            tbl_stt.Text = id.ToString();
            tbx_description.Text = row["REPAIR_DESCRIPTION"].ToString().Trim();
            tbx_name.Text = row["NAME_COM"].ToString().Trim();
            tbx_price.Text = (float.Parse(row["CUR_PRICE"].ToString())).ToString("N");
            tbx_quantity.Text = row["COM_QUANTITY"].ToString().Trim();
            tbx_wage.Text = (float.Parse(row["WAGE"].ToString())).ToString("N");
            tbx_total.Text = (float.Parse(row["TOTAL_PRICE"].ToString())).ToString("N");
        }

        public void CreateNewRepairCardDetail(int id)
        {
            tbl_stt.Text = id.ToString();
        }
    }
}
