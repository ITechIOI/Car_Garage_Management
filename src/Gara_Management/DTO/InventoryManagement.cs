using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class InventoryManagement
    {
        private int iMOrdinalNum;
        public int IMOrdinalNum { get => iMOrdinalNum; set => iMOrdinalNum = value; }

        private string iDCom;
        public string IDCom { get => iDCom; set => iDCom = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private int comQuantity;
        public int ComQuantity { get => comQuantity; set => comQuantity = value; }

        public InventoryManagement(int iMOrdinalNum, string iDCom, string iDGara, int comQuantity)
        {
            this.IMOrdinalNum = iMOrdinalNum;
            this.IDCom = iDCom;
            this.IDGara = iDGara;
            this.ComQuantity = comQuantity;
        }

        public InventoryManagement(DataRow row)
        {
            this.IMOrdinalNum = int.Parse(row["IM_ORDINAL_NUM"].ToString());
            this.IDCom = row["ID_COM"].ToString();
            this.IDGara = row["ID_GARA"].ToString();
            this.ComQuantity = int.Parse(row["COM_QUANTITY"].ToString());
        }
    }
}
