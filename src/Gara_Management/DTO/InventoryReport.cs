using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class InventoryReport
    {
        
        private string com;
        public string Com { get => com; set => com = value; }

        private int biComQuantity;
        public int BIComQuantity { get => biComQuantity; set => biComQuantity = value; }

        private int icComQuantity;
        public int ICComQuantity { get => icComQuantity; set => icComQuantity = value; }
        private int eiComQuantity;
        public int EIComQuantity { get => eiComQuantity; set => eiComQuantity = value; }

        public InventoryReport(string Com, int BIcomQuantity, int ICComQuantity, int EIComQuantity)
        {
            this.Com = Com;
            this.BIComQuantity = BIcomQuantity;
            this.ICComQuantity = ICComQuantity;
            this.EIComQuantity = EIComQuantity;
        }

        public InventoryReport(DataRow row)
        {
            this.Com = row["NAME_COM"].ToString();
            int biQuantity, icQuantity, eiQuantity;
            if (!int.TryParse(row["BI"].ToString(), out biQuantity))
            {
                biQuantity = 0;
            }
            if (!int.TryParse(row["IC"].ToString(), out icQuantity))
            {
                icQuantity = 0;
            }
            if (!int.TryParse(row["EI"].ToString(), out eiQuantity))
            {
                eiQuantity = 0;
            }
            this.BIComQuantity = biQuantity;
            this.ICComQuantity = icQuantity;
            this.EIComQuantity = eiQuantity;
            
        }
    }
}
