using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class CarBrandDAO
    {
        private CarBrandDAO() { }
        private static CarBrandDAO instance;

        public static CarBrandDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CarBrandDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<CarBrand> LoadCarBrandList(string gara)
        {
            List<CarBrand> carBrandList = new List<CarBrand>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT CAR_BRANDS.ID_BRAND, NAME_BRAND, STATUS_DETAILS " +
                "FROM CAR_BRANDS JOIN BRAND_DETAILS ON CAR_BRANDS.ID_BRAND = BRAND_DETAILS.ID_BRAND " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BRAND = 0 AND STATUS_DETAILS = 0");
            foreach (DataRow item in data.Rows)
            {
                CarBrand carBrand = new CarBrand(item);
                carBrandList.Add(carBrand);
            }
            return carBrandList;
        }

        //lấy thông tin hãng theo id
        public CarBrand LoadCarBrandByID(string id, string gara)
        {
            string query = "SELECT CAR_BRANDS.ID_BRAND, NAME_BRAND, STATUS_DETAILS " +
                "FROM CAR_BRANDS JOIN BRAND_DETAILS ON CAR_BRANDS.ID_BRAND = BRAND_DETAILS.ID_BRAND " +
                "WHERE ID_GARA = '" + gara + "' AND STATUS_BRAND = 0 AND STATUS_DETAILS = 0  ";
            CarBrand carBrand = new CarBrand(DataProvider.Instance.ExecuteQuery(query).Rows[0]);
            return carBrand;
        }

        //lấy id hãng theo tên hãng
        public string GetIDBrandByName(string name, string gara)
        {
            string id;
            string query = "SELECT DISTINCT CAR_BRANDS.ID_BRAND " +
                "FROM CAR_BRANDS JOIN BRAND_DETAILS ON CAR_BRANDS.ID_BRAND = BRAND_DETAILS.ID_BRAND " +
                "WHERE NAME_BRAND = '" + name + "' AND STATUS_DETAILS = 0 AND ID_GARA = '" + gara + "'";
            if (DataProvider.Instance.ExecuteScalar(query) != null)
            {
                id = DataProvider.Instance.ExecuteScalar(query).ToString().Trim();
            }
            else
            {
                id = null;
            }
            return id;
        }

        public bool InsertCarBrand(string name, string gara)
        {
            return DataProvider.Instance.ExecuteNonQuery("EXEC USP_INSERT_CARBRAND N'" + name + "', '" + gara + "'") > 0;
        }

        public bool DeleteCarBrand(string brand, string gara)
        {
            return DataProvider.Instance.ExecuteNonQuery("EXEC USP_DELETE_CARBRAND '" + brand + "', '" + gara + "'") > 0;
        }
    }
}
