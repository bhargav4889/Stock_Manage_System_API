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
            All_Counts_Model allCountsModel = new All_Counts_Model();
            DbCommand dbCommand = Command_Name("API_DASHBOARD_ALL_COUNTS");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    string itemName = reader["ItemName"].ToString();

                    switch (itemName)
                    {
                        case "PURCHASE_INVOICE":
                            allCountsModel.TotalPurchaseInvoice = Convert.ToInt32(reader["ActualCount"]);
                            allCountsModel.AmountOfPurchaseInvoice = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "SALE_INVOICE":
                            allCountsModel.TotalSalesInvoice = Convert.ToInt32(reader["ActualCount"]);
                            allCountsModel.AmountOfSalesInvoice = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "CUSTOMERS":
                            allCountsModel.TotalCustomers = Convert.ToInt32(reader["ActualCount"]);
                            break;
                        case "PAID PAYMENTS":
                            allCountsModel.TotalPayments = Convert.ToInt32(reader["ActualCount"]);
                            allCountsModel.AmountOfPaidPayments = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "COMPLETE PAYMENTS":
                            allCountsModel.PaidPayments = Convert.ToInt32(reader["ActualCount"]);
                            allCountsModel.AmountOfPaidPayments = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "REMAIN PAYMENTS":
                            allCountsModel.RemainPayments = Convert.ToInt32(reader["ActualCount"]);
                            break;
                        case "PENDING PAYMENTS":
                            allCountsModel.PendingPayments = Convert.ToInt32(reader["ActualCount"]);
                            allCountsModel.AmountOfPendingPayments = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "PURCHASE_STOCK":
                            allCountsModel.AmountOfPurchasedStock = Convert.ToDecimal(reader["TotalPrice"]);
                            break;
                        case "TOTAL_BAGS":
                            allCountsModel.TotalBags = Convert.ToInt32(reader["ActualCount"]);
                            break;
                        case "TOTAL_WEIGHT":
                            allCountsModel.TotalWeight = Convert.ToDecimal(reader["ActualCount"]);
                            break;
                    }
                }

                return allCountsModel;
            }
        }




    }
}
