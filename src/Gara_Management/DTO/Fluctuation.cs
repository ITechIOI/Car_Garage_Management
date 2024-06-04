using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Gara_Management.DTO
{
    internal class Fluctuation
    {
        private string idCus;
        public string IDCus { get => idCus; set => idCus = value; }
        private string content;
        public string Content { get => content; set  => content = value; }
        private string id;
        public string ID { get => id;  set => id = value; }
        private DateTime date;
        public DateTime Date { get => date; set => date = value; }
        private decimal money;
        public decimal Money { get => money;  set => money = value; }
        public Fluctuation(string idCus, string content, string id, DateTime date, decimal money) 
        {
            this.IDCus = idCus;
            this.Content = content;
            this.ID = id;
            this.Date = date;
            this.Money = money;
        }
        public Fluctuation(DataRow row)
        {
            this.IDCus = row["CUS"].ToString();
            this.Content = row["CONTENT"].ToString();
            this.ID = row["ID"].ToString();
            this.Date = Convert.ToDateTime(row["DDATE"]);
            this.Money = Convert.ToDecimal(row["MONEY"]);
        }
    }
}
