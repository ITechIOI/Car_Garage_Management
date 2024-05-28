using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class CarBrand
    {
        private string iDBrand;
        public string IDBrand { get => iDBrand; set => iDBrand = value; }

        private string nameBrand;
        public string NameBrand { get => nameBrand; set => nameBrand = value; }

        private bool statusBrand;
        public bool StatusBrand { get => statusBrand; set => statusBrand = value; }

        public CarBrand(string iDBrand, string nameBrand, bool statusBrand)
        {
            this.IDBrand = iDBrand;
            this.NameBrand = nameBrand;
            this.StatusBrand = statusBrand;
        }

        public CarBrand(DataRow row)
        {
            this.IDBrand = row["ID_BRAND"].ToString().Trim();
            this.NameBrand = row["NAME_BRAND"].ToString().Trim();
            this.StatusBrand = Convert.ToBoolean(row["STATUS_BRAND"].ToString().Trim());
        }
    }
}
