using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class EndingInventory
    {
        private int eIOrdinalNum;
        public int EIOrdinalNum { get => eIOrdinalNum; set => eIOrdinalNum = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime renderingTimeEI;
        public DateTime RenderingTimeEI { get => renderingTimeEI; set => renderingTimeEI = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        public EndingInventory(int eIOrdinalNum, string iDCom, string iDGara, DateTime renderingTimeEI, int comQuantity)
        {
            this.EIOrdinalNum = eIOrdinalNum;
            this.IDCom = iDCom;
            this.IDGara = iDGara;
            this.RenderingTimeEI = renderingTimeEI;
            this.ComQuantity = comQuantity;
        }

        public EndingInventory(DataRow row)
        {
            this.EIOrdinalNum = int.Parse(row["EI_ORDINAL_NUM"].ToString());
            this.IDCom = row["ID_COM"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.RenderingTimeEI = Convert.ToDateTime(row["RENDERING_TIME_EI"].ToString());
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
        }
    }
}
