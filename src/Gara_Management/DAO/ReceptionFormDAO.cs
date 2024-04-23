using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ReceptionForm> LoadReceptionFormtList()
        {
            List<ReceptionForm> receptionFormList = new List<ReceptionForm>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM RECEPTION_FORMS");
            foreach (DataRow item in data.Rows)
            {
                ReceptionForm receptionForm = new ReceptionForm(item);
                receptionFormList.Add(receptionForm);
            }
            return receptionFormList;
        }

    }
}
