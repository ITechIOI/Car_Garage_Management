using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class BeginingInventory
    {
        private int bIOrdinalNum;
        public int BIOrdinalNum { get => bIOrdinalNum; set => bIOrdinalNum = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime renderingTimeBI;
        public DateTime RenderingTimeBI { get => renderingTimeBI; set => renderingTimeBI = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        public BeginingInventory(int bIOrdinalNum, string iDCom, string iDGara, DateTime renderingTimeBI, int comQuantity)
        {
            this.BIOrdinalNum = bIOrdinalNum;
            this.IDCom = iDCom;
            this.IDGara = iDGara;
            this.RenderingTimeBI = renderingTimeBI;
            this.ComQuantity = comQuantity;
        }

        public BeginingInventory(DataRow row)
        {
            this.BIOrdinalNum = int.Parse(row["BI_ORDINAL_NUM"].ToString());
            this.IDCom = row["ID_COM"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.RenderingTimeBI = Convert.ToDateTime(row["RENDERING_TIME_BI"].ToString());
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
        }
    }
}
