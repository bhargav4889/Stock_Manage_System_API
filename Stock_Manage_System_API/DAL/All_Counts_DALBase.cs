using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;

namespace Stock_Manage_System_API.DAL
{
    public class All_Counts_DALBase : DAL_Helpers
    {
        

        private SqlDatabase sqlDatabase;

        public All_Counts_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        public All_Counts_Model ALL_COUNTS()
        {
            All_Counts_Model all_Counts_Model = new All_Counts_Model();

            DbCommand dbCommand = Command_Name("API_ALL_COUNTS");

            using(IDataReader  reader = sqlDatabase.ExecuteReader(dbCommand)) 
            {
                if (reader.Read())
                {
                    all_Counts_Model.TotalCustomers = Convert.ToInt32(reader["TOTAL_CUSTOMERS"]);
                    all_Counts_Model.TotalBags = Convert.ToInt32(reader["TOTAL_BAGS"]);
                    all_Counts_Model.TotalWeight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                    all_Counts_Model.AmountOfPurchasedStock = Convert.ToDecimal(reader["AMOUNT_OF_PURCHASED_STOCK"]);
                    all_Counts_Model.TotalPurchaseInvoice = Convert.ToInt32(reader["TOTAL_PURCHASE_INVOICE"]);
                    all_Counts_Model.AmountOfPurchaseInvoice = Convert.ToDecimal(reader["AMOUNT_OF_PURCHASE_INVOICE"]);
                    all_Counts_Model.TotalSalesInvoice = Convert.ToInt32(reader["TOTAL_SALES_INVOICE"]);
                    all_Counts_Model.AmountOfSalesInvoice = Convert.ToDecimal(reader["AMOUNT_OF_SALES_INVOICE"]);
                }

                return all_Counts_Model;
            }


           
        }




    }
}
