using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class CarComponent
    {
        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string nameCom;
        public string NameCom { get => nameCom; set => nameCom = value; }

        private bool statusCom;
        public bool StatusCom { get => statusCom; set => statusCom = value; }

        public CarComponent(string iDCom, string nameCom, bool statusCom)
        {
            this.IDCom = iDCom;
            this.NameCom = nameCom;
            this.StatusCom = statusCom;
        }

        public CarComponent(DataRow row)
        {
            this.IDCom = row["ID_COM"].ToString();
            this.NameCom = row["NAME_COM"].ToString();
            this.StatusCom = Convert.ToBoolean(row["STATUS_COM"].ToString());
        }
    }
}
