using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class BrandDetail
    {
        private string iDBrand;
        public string IDBrand { get => iDBrand; set => iDBrand = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private bool statusDetail;
        public bool StatusDetail { get => statusDetail; set => statusDetail = value; }

        public BrandDetail(string iDBrand, string iDGara, bool statusDetail)
        {
            this.IDBrand = iDBrand;
            this.IDGara = iDGara;
            this.StatusDetail = statusDetail;
        }

        public BrandDetail(DataRow row)
        {
            this.IDBrand = row["ID_BRAND"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.StatusDetail = Convert.ToBoolean(row["STATUS_DETAILS"]);
        }
    }
}
