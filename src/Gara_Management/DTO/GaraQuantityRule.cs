using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DTO
{
    public class GaraQuantityRule
    {
        private int iDRule;
        public int IDRule { get => iDRule; set => iDRule = value; }

        private string iDGara;
        public string IDGara { get => iDGara; set => iDGara = value; }

        private DateTime ruleDate;
        public DateTime RuleDate { get => ruleDate; set => ruleDate = value; }

        private int maxQuantity;
        public int MaxQuantity { get => maxQuantity; set => maxQuantity = value; }

        public GaraQuantityRule(int iDRule, string iDGara, DateTime ruleDate, int maxQuantity)
        {
            this.IDRule = iDRule;
            this.IDGara = iDGara;
            this.RuleDate = ruleDate;
            this.MaxQuantity = maxQuantity;
        }

        public GaraQuantityRule(DataRow row)
        {
            this.IDRule = int.Parse(row["ID_RULE"].ToString());
            this.IDGara = row["ID_GARA"].ToString();
            this.RuleDate = Convert.ToDateTime(row["RULE_DATE"].ToString());
            this.MaxQuantity = int.Parse(row["MAX_QUANTITY"].ToString());
        }
    }
}
