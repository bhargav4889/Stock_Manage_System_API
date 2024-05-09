using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;

namespace Stock_Manage_System_API.DAL
{
    public class Counts_DALBase : DAL_Helpers
    {

        #region Method : Configurations 


        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Counts_DALBase"/> class.
        /// Ensures that the Database_Connection is initialized or throws an informative exception.
        /// </summary>
        public Counts_DALBase()
        {
            // Ensure Database_Connection is initialized or throw an informative exception.
            if (Database_Connection == null)
                throw new InvalidOperationException("Database connection Error.");

            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Configures and returns a stored procedure command object with the provided name.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A DbCommand object configured with the provided stored procedure name.</returns>

        private DbCommand Command_Name(string storedProcedureName)
        {
            // Retrieve a stored procedure command object configured with the provided name.
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #endregion

        #region Method : All Counts 

        /// <summary>
        /// Retrieves the total counts for all relevant entities.
        /// </summary>
        /// <returns>An AllCountsModel object containing the total counts and amounts for all relevant entities.</returns>

        public AllCountsModel GetTotalCounts()
        {
            AllCountsModel allCountsModel = new AllCountsModel();
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


        #endregion


    }
}
