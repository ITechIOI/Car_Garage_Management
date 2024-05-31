using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class CarComponentDAO
    {
        private CarComponentDAO() { }
        private static CarComponentDAO instance;

        public static CarComponentDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CarComponentDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<CarComponent> LoadCarComponentList(string gara)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CAR_COMPONENTS.ID_COM, NAME_COM, STATUS_COM," +
                " WAGE, CUR_PRICE,STATUS_DETAILS, COM_QUANTITY FROM CAR_COMPONENTS JOIN COMPONENT_DETAILS " +
                "ON CAR_COMPONENTS.ID_COM = COMPONENT_DETAILS.ID_COM JOIN INVENTORY_MANAGEMENT " +
                "ON CAR_COMPONENTS.ID_COM = INVENTORY_MANAGEMENT.ID_COM AND COMPONENT_DETAILS.ID_GARA = INVENTORY_MANAGEMENT.ID_GARA" +
                " WHERE COMPONENT_DETAILS.ID_GARA = '" + gara + "' AND STATUS_COM = 0 AND STATUS_DETAILS = 0");
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }
        public string GetComponentIDByInfo(string idGara, string name, string wage, string price)
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
