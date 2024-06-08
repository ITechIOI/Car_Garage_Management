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

        public List<CarComponent> LoadCarComponentListByName(string gara, string name)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CAR_COMPONENTS.ID_COM, NAME_COM, STATUS_COM," +
                " WAGE, CUR_PRICE,STATUS_DETAILS, COM_QUANTITY FROM CAR_COMPONENTS JOIN COMPONENT_DETAILS " +
                "ON CAR_COMPONENTS.ID_COM = COMPONENT_DETAILS.ID_COM JOIN INVENTORY_MANAGEMENT " +
                "ON CAR_COMPONENTS.ID_COM = INVENTORY_MANAGEMENT.ID_COM AND COMPONENT_DETAILS.ID_GARA = INVENTORY_MANAGEMENT.ID_GARA" +
                " WHERE COMPONENT_DETAILS.ID_GARA = '" + gara + "' AND STATUS_COM = 0 AND STATUS_DETAILS = 0 AND " +
                "DBO.[non_unicode_convert](NAME_COM) LIKE DBO.[non_unicode_convert](N'%" + name + "%')");
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByQuantity(string gara, int minQuan, int maxQuan)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (IM.COM_QUANTITY BETWEEN " + minQuan + " AND " + maxQuan + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByPrice(string gara, int minPrice, int maxPrice)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (CD.CUR_PRICE BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByQuantityAndPrice(string gara, int minQuan, int maxQuan, int minPrice, int maxPrice)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (IM.COM_QUANTITY BETWEEN " + minQuan + " AND " + maxQuan + ") " +
                "AND (CD.CUR_PRICE BETWEEN " + minPrice + " AND " + maxPrice + ")";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByNameAndQuantity(string gara, string name, int minQuan, int maxQuan)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (IM.COM_QUANTITY BETWEEN " + minQuan + " AND " + maxQuan + ") " +
                "AND DBO.[non_unicode_convert](CC.NAME_COM) LIKE DBO.[non_unicode_convert](N'%" + name + "%')";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByNameAndPrice(string gara, string name, int minPrice, int maxPrice)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (CD.CUR_PRICE BETWEEN " + minPrice + " AND " + maxPrice + ") " +
                "AND DBO.[non_unicode_convert](CC.NAME_COM) LIKE DBO.[non_unicode_convert](N'%" + name + "%')";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

        public List<CarComponent> LoadCarComponentListByNamePriceAndQuantity(string gara, string name, int minPrice, int maxPrice, int minQuan, int maxQuan)
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            string query = "SELECT CC.ID_COM, CC.NAME_COM, CC.STATUS_COM, IM.COM_QUANTITY, CD.WAGE, CD.CUR_PRICE, CD.STATUS_DETAILS " +
                "FROM INVENTORY_MANAGEMENT IM, CAR_COMPONENTS CC, COMPONENT_DETAILS CD " +
                "WHERE IM.ID_GARA = CD.ID_GARA AND CD.ID_COM = CC.ID_COM AND CC.ID_COM = IM.ID_COM AND CC.STATUS_COM = 0 AND CD.STATUS_DETAILS = 0 AND " +
                "CD.ID_GARA = '" + gara + "' AND (IM.COM_QUANTITY BETWEEN " + minQuan + " AND " + maxQuan + ") " +
                "AND (CD.CUR_PRICE BETWEEN " + minPrice + " AND " + maxPrice + ") " +
                "AND DBO.[non_unicode_convert](CC.NAME_COM) LIKE DBO.[non_unicode_convert](N'%" + name + "%')";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
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

        public CarComponent GetCarComponentByID(string idCom, string gara)
        {
            string query = "SELECT CAR_COMPONENTS.ID_COM, NAME_COM, STATUS_COM, WAGE, CUR_PRICE,STATUS_DETAILS, COM_QUANTITY " +
                "FROM CAR_COMPONENTS JOIN COMPONENT_DETAILS ON CAR_COMPONENTS.ID_COM = COMPONENT_DETAILS.ID_COM " +
                "JOIN INVENTORY_MANAGEMENT ON CAR_COMPONENTS.ID_COM = INVENTORY_MANAGEMENT.ID_COM " +
                "AND COMPONENT_DETAILS.ID_GARA = INVENTORY_MANAGEMENT.ID_GARA WHERE COMPONENT_DETAILS.ID_GARA = '" + gara + "' " +
                "AND STATUS_COM = 0 AND STATUS_DETAILS = 0 AND CAR_COMPONENTS.ID_COM LIKE '%" + idCom + "%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0)
            {
                return null;
            }
            return new CarComponent(data.Rows[0]);
        }

        public string GetComponentIDByName(string gara, string name)
        {
            string query = "SELECT CAR_COMPONENTS.ID_COM, NAME_COM, STATUS_COM, WAGE, CUR_PRICE,STATUS_DETAILS, COM_QUANTITY " +
               "FROM CAR_COMPONENTS JOIN COMPONENT_DETAILS ON CAR_COMPONENTS.ID_COM = COMPONENT_DETAILS.ID_COM " +
               "JOIN INVENTORY_MANAGEMENT ON CAR_COMPONENTS.ID_COM = INVENTORY_MANAGEMENT.ID_COM " +
               "AND COMPONENT_DETAILS.ID_GARA = INVENTORY_MANAGEMENT.ID_GARA WHERE COMPONENT_DETAILS.ID_GARA = '" + gara + "' " +
               "AND STATUS_COM = 0 AND STATUS_DETAILS = 0 AND NAME_COM LIKE N'%" + name + "%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0)
            {
                return null;
            }
            return new CarComponent(data.Rows[0]).IDCom;
        }

        public bool InsertCarComponent(string name, string gara, decimal wage)
        {
            return DataProvider.Instance.ExecuteNonQuery("EXEC INSERT_CARCOMPONENT N'" + name + "', '" + gara + "', " + wage) > 0;
        }

        public bool UpdateCarComponent(string id, string name, string gara, decimal wage, decimal curPrice)
        {
            string query = "EXEC UPDATE_CARCOMPONENT '" + id + "', N'" + name + "', '" + gara + "', " + wage + ", " + curPrice;
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
        public bool DeleteCarComponent(string id, string gara)
        {
            return DataProvider.Instance.ExecuteNonQuery("EXEC DELETE_CARCOMPONENT '" + id + "', '" + gara + "'") > 0;
        }

    }
}
