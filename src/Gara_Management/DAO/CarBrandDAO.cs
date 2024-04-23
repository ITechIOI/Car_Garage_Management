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
    }
}
