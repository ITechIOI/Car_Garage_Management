using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class StaffPosition
    {
        private string iDPos;
        public string IDPos { get => iDPos; set => iDPos = value; }

        private string namePos;
        public string NamePos { get => namePos; set => namePos = value; }

        public StaffPosition(string iDPos, string namePos)
        {
            this.IDPos = iDPos;
            this.NamePos = namePos;
        }

        public StaffPosition(DataRow row)
        {
            this.IDPos = row["ID_POS"].ToString();
            this.NamePos = row["NAME_POS"].ToString();
        }
    }
}
