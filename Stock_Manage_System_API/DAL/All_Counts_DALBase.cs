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
                    int count = reader["ActualCount"] != DBNull.Value ? Convert.ToInt32(reader["ActualCount"]) : 0;
                    decimal totalPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPrice"]) : 0;

                    switch (itemName)
                    {
                        case "PURCHASE_INVOICE":
                            allCountsModel.TotalPurchaseInvoice = count;
                            allCountsModel.AmountOfPurchaseInvoice = totalPrice;
                            break;
                        case "SALE_INVOICE":
                            allCountsModel.TotalSalesInvoice = count;
                            allCountsModel.AmountOfSalesInvoice = totalPrice;
                            break;
                        case "CUSTOMERS":
                            allCountsModel.TotalCustomers = count;
                            break;
                        case "TOTAL_PAYMENTS":
                            allCountsModel.TotalPayments = count;
                            allCountsModel.AmountOfPaidPayments = totalPrice;  // Assuming total payments equals total paid amount.
                            break;
                        case "PAID_PAYMENTS":
                            allCountsModel.PaidPayments = count;
                            allCountsModel.AmountOfPaidPayments = totalPrice;
                            break;
                        case "REMAIN_PAYMENTS":
                            allCountsModel.RemainPayments = count;
                            allCountsModel.AmountOfRemainingPayments = totalPrice;
                            break;
                        case "PENDING_PAYMENTS":
                            allCountsModel.PendingPayments = count;
                            allCountsModel.AmountOfPendingPayments = totalPrice;
                            break;
                        case "PURCHASE_STOCK":
                            allCountsModel.AmountOfPurchasedStock = totalPrice;
                            break;
                        case "TOTAL_BAGS":
                            allCountsModel.TotalBags = count;
                            break;
                        case "TOTAL_WEIGHT":
                            allCountsModel.TotalWeight = count;
                            break;
                    }
                }

                return allCountsModel;
            }
        }





}
}
