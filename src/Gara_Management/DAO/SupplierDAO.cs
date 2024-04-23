using Gara_Management.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management.DAO
{
    public class SupplierDAO
    {
        private SupplierDAO() { }
        private static SupplierDAO instance;

        public static SupplierDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new SupplierDAO();
                return instance;
            }
            private set { instance = value; }
        }

        public List<Supplier> LoadSupplierList()
        {
            List<Supplier> supplierList = new List<Supplier>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM SUPPLIERS");
            foreach (DataRow item in data.Rows)
            {
                Supplier supplier = new Supplier(item);
                supplierList.Add(supplier);
            }
            return supplierList;
        }

    }
}
