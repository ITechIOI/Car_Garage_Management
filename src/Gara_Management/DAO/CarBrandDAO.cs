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

        public List<CarBrand> LoadCarBrandList()
        {
            List<CarBrand> carBrandList = new List<CarBrand>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM CAR_BRANDS");
            foreach (DataRow item in data.Rows)
            {
                CarBrand carBrand = new CarBrand(item);
                carBrandList.Add(carBrand);
            }
            return carBrandList;
        }

        //lấy thông tin hãng theo id
        public static CarBrand LoadCarBrandByID(string id)
        {
            string query = "SELECT * FROM CAR_BRANDS WHERE ID_BRAND = '" + id + "'";
            CarBrand carBrand = new CarBrand(DataProvider.Instance.ExecuteQuery(query).Rows[0]);
            return carBrand;
        }

        //lấy id hãng theo tên hãng
        public static string GetIDBrandByName(string name)
        {
            string id;
            string query = "SELECT ID_BRAND FROM CAR_BRANDS WHERE NAME_BRAND = '" + name + "'";
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
    }
}
