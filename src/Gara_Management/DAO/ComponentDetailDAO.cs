using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class ComponentDetailDAO
    {
        private ComponentDetailDAO() { }
        private static ComponentDetailDAO instance;

        public static ComponentDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ComponentDetailDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<ComponentDetail> LoadComponentDetailList()
        {
            List<ComponentDetail> componentDetailList = new List<ComponentDetail>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM COMPONENT_DETAILS");
            foreach (DataRow item in data.Rows)
            {
                ComponentDetail componentDetail = new ComponentDetail(item);
                componentDetailList.Add(componentDetail);
            }
            return componentDetailList;
        }

        //Lấy IDCom theo thông tin
        public static string GetComponentIDByInfo(string idGara, string name, string wage, string price)
        {
            string query = "EXEC USP_GET_IDCOMPONENT @ID_GARA = '" + idGara + "', @NAME_COM = N'" + name + "', @WAGE = '" + wage + "', @CUR_PRICE = '" + price + "'";
            object idCom = DataProvider.Instance.ExecuteScalar(query);
            if (idCom != null)
            {
                return idCom.ToString();
            }
            return "";
        }
    }
}
