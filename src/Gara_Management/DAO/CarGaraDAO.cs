using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class CarGaraDAO
    {
        private CarGaraDAO() { }
        private static CarGaraDAO instance;

        public static CarGaraDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CarGaraDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<CarGara> LoadCarGaraList()
        {
            List<CarGara> carGaraList = new List<CarGara>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM CAR_GARA");
            foreach (DataRow item in data.Rows)
            {
                CarGara carGara = new CarGara(item);
                carGaraList.Add(carGara);
            }
            return carGaraList;
        }

    }
}
