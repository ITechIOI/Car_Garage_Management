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

        public List<CarComponent> LoadCarComponentList()
        {
            List<CarComponent> carComponentList = new List<CarComponent>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM CAR_COMPONENTS");
            foreach (DataRow item in data.Rows)
            {
                CarComponent carComponent = new CarComponent(item);
                carComponentList.Add(carComponent);
            }
            return carComponentList;
        }

    }
}
