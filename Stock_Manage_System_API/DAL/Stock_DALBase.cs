﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data.Common;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Windows.Input;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// Base Data Access Layer class responsible for managing database operations related to stock.
    /// </summary>
    public class Stock_DALBase : DAL_Helpers
    {
        #region Section: SetUp Of Database Connection and Initialization

        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the Stock_DALBase class, setting up the database connection.
        /// </summary>
        public Stock_DALBase()
        {
            // Assuming 'Database_Connection' is a predefined string or obtained elsewhere in your application.

            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Retrieves a DbCommand object configured for executing the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure for which to get the DbCommand.</param>
        /// <returns>A DbCommand object configured to execute the named stored procedure.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #endregion

        #region Section: Display All Purchase Stocks

        /// <summary>
        /// Retrieves all purchase stock entries from the database.
        /// </summary>
        /// <returns>A list of Purchase_Stock objects if successful; otherwise, null.</returns>

        public List<Purchase_Stock>? DisplayAllPurchaseStock()
        {
            List<Purchase_Stock> List_of_Stock_Model = new List<Purchase_Stock>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PURCHASE_STOCK");

            try
            {
                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {

                        Purchase_Stock Stock_Information = new Purchase_Stock();

                        Stock_Information.PurchaseStockId = Convert.ToInt32(dataReader[0].ToString());

                        Stock_Information.PurchaseStockDate = Convert.ToDateTime(dataReader[1].ToString());

                        Stock_Information.CustomerId = Convert.ToInt32(dataReader[2].ToString());

                        Stock_Information.CustomerName = dataReader[3].ToString();

                        Stock_Information.CustomerType = dataReader[4].ToString();

                        Stock_Information.ProductId = Convert.ToInt32(dataReader[5].ToString());

                        Stock_Information.ProductName = dataReader[6].ToString();
                        // Assuming Stock_Information is an instance of a class representing your stock information
                        // and dataReader is your SqlDataReader or similar reader from your database

                        // Handling ProductGradeId - converting to Int32, setting to 0 if null or conversion fails
                        object productGradeIdValue = dataReader[7];
                        Stock_Information.ProductGradeId = productGradeIdValue != DBNull.Value && int.TryParse(productGradeIdValue.ToString(), out int productGradeId) ?
                          productGradeId :
                          0; // Default to 0 if null or conversion fails

                        // Handling ProductGrade - assigning directly, replacing null or empty string with "--"
                        object productGradeValue = dataReader[8];
                        Stock_Information.ProductGrade = productGradeValue != DBNull.Value && !string.IsNullOrWhiteSpace(productGradeValue.ToString()) ?
                          productGradeValue.ToString() :
                          "--"; // Replace null or empty string with "--"

                        Stock_Information.PurchaseStockLocation = dataReader[9].ToString();

                        string bagsStr = dataReader[10].ToString();
                        if (decimal.TryParse(bagsStr, out decimal bags))
                        {
                            Stock_Information.Bags = bags;
                        }
                        else
                        {
                            Stock_Information.Bags = null; // Or handle "--" specifically if needed
                        }

                        string bagPerKgStr = dataReader[11].ToString();
                        if (decimal.TryParse(bagPerKgStr, out decimal bagPerKg))
                        {
                            Stock_Information.BagPerKg = bagPerKg;
                        }
                        else
                        {
                            Stock_Information.BagPerKg = null; // Or handle "--" specifically if needed
                        }

                        Stock_Information.TotalWeight = Convert.ToDecimal(dataReader[12]);

                        Stock_Information.ProductPrice = Convert.ToDecimal(dataReader[13]);

                        Stock_Information.TotalPrice = Convert.ToDecimal(dataReader[14]);

                        Stock_Information.VehicleId = Convert.ToInt32(dataReader[15].ToString());

                        Stock_Information.VehicleName = dataReader[16].ToString();

                        Stock_Information.VehicleNo = dataReader[17].ToString();

                        Stock_Information.DriverName = dataReader[18].ToString();

                        Stock_Information.TolatName = dataReader[19].ToString();

                        Stock_Information.PaymentStatus = dataReader[20].ToString();

                        List_of_Stock_Model.Add(Stock_Information);

                    }
                    return List_of_Stock_Model;

                };
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Section: Insert Purchase Stock With Customer Details

        /// <summary>
        /// Inserts a new purchase stock entry with customer details into the database.
        /// </summary>
        /// <param name="purchase_Stock_With_Customer_Model">The model containing all necessary data for the purchase stock and customer details.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>

        public bool InsertPurchaseStock(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_PURCHASE_STOCK_INSERT");

                DateTime? invoiceDate = purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@PUR_STOCK_DATE", SqlDbType.Date, onlyDate);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Customers_Model.CustomerId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_GRADE_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductGradeId);
                sqlDatabase.AddInParameter(dbCommand, "@PUR_STOCK_LOCATION", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockLocation);
                sqlDatabase.AddInParameter(dbCommand, "@BAG", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.Bags);
                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.BagPerKg);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.TotalWeight);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductPrice);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.TotalPrice);
                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleId);
                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.VarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleNo);
                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.DriverName);
                sqlDatabase.AddInParameter(dbCommand, "@TOLAT_NAME", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.TolatName);
                sqlDatabase.AddInParameter(dbCommand, "@PAYMENT_STATUS", SqlDbType.VarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.PaymentStatus);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Section: Delete Purchase Stock

        /// <summary>
        /// Deletes a specific purchase stock entry based on its ID.
        /// </summary>
        /// <param name="TN_ID">The ID of the purchase stock to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>

        public bool DeletePurchaseStock(int TN_ID)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_PURCHASE_STOCK_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, TN_ID);
                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Section: Purchase Stock By Stock ID

        /// <summary>
        /// Fetches purchase stock information by primary key (stock ID).
        /// </summary>
        /// <param name="Stock_ID">The primary key ID of the stock record to retrieve.</param>
        /// <returns>A Purchase_Stock object if found; otherwise, null.</returns>

        public Purchase_Stock? PurchaseStockByID(int Stock_ID)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_PURCHASE_STOCK_BY_PK");

                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, Stock_ID);

                Purchase_Stock Stock_Information = new Purchase_Stock();

                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {

                        Stock_Information.PurchaseStockId = Convert.ToInt32(dataReader[0].ToString());

                        Stock_Information.PurchaseStockDate = Convert.ToDateTime(dataReader[1].ToString());

                        Stock_Information.CustomerId = Convert.ToInt32(dataReader[2].ToString());

                        Stock_Information.CustomerName = dataReader[3].ToString();

                        Stock_Information.CustomerType = dataReader[4].ToString();

                        Stock_Information.ProductId = Convert.ToInt32(dataReader[5].ToString());

                        Stock_Information.ProductName = dataReader[6].ToString();

                        Stock_Information.ProductGradeId = Convert.ToInt32(dataReader[7].ToString());

                        Stock_Information.ProductGrade = dataReader[8].ToString();

                        Stock_Information.PurchaseStockLocation = dataReader[9].ToString();

                        Stock_Information.Bags = Convert.ToDecimal(dataReader[10].ToString());

                        Stock_Information.BagPerKg = Convert.ToDecimal(dataReader[11].ToString());

                        Stock_Information.TotalWeight = Convert.ToDecimal(dataReader[12]);

                        Stock_Information.ProductPrice = Convert.ToDecimal(dataReader[13]);

                        Stock_Information.TotalPrice = Convert.ToDecimal(dataReader[14]);

                        Stock_Information.VehicleId = Convert.ToInt32(dataReader[15].ToString());

                        Stock_Information.VehicleName = dataReader[16].ToString();

                        Stock_Information.VehicleNo = dataReader[17].ToString();

                        Stock_Information.DriverName = dataReader[18].ToString();

                        Stock_Information.TolatName = dataReader[19].ToString();

                    }

                    return Stock_Information;
                };

            }
            catch
            {
                return null;
            }

        }

        #endregion

        #region Section: Fetch Stock And Customer Information By Those IDs

        /// <summary>
        /// Fetches both stock and customer information based on their IDs.
        /// </summary>
        /// <param name="Stock_ID">The stock ID to use in query.</param>
        /// <param name="Customer_ID">The customer ID to use in query.</param>
        /// <returns>A Purchase_Stock_With_Customer_Model object if successful; otherwise, null.</returns>

        public Purchase_Stock_With_Customer_Model Fetch_Stock_And_Customer_Details(int Stock_ID, int Customer_ID)
        {
            try
            {
                Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model = new Purchase_Stock_With_Customer_Model();

                DbCommand dbCommand = Command_Name("API_FETCH_PURCHASE_STOCK_AND_CUSTOMER_DETAILS_BY_STOCK_AND_CUSTOMER_ID");

                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, Stock_ID);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        purchase_Stock_With_Customer_Model.Purchase_Stock = new Insert_And_Update_Purchase_Stock(); // Assuming PurchaseStock is the correct class name
                        purchase_Stock_With_Customer_Model.Customers_Model = new Customer_Model(); // Assuming CustomerModel is the correct class name

                        purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockId = Convert.ToInt32(dataReader["STOCK_ID"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockDate = Convert.ToDateTime(dataReader["PUR_STOCK_DATE"]);
                        purchase_Stock_With_Customer_Model.Customers_Model.CustomerId = Convert.ToInt32(dataReader["CUSTOMER_ID"]);
                        purchase_Stock_With_Customer_Model.Customers_Model.CustomerName = dataReader["CUSTOMER_NAME"].ToString();
                        purchase_Stock_With_Customer_Model.Customers_Model.CustomerType = dataReader["CUSTOMER_TYPE"].ToString();
                        purchase_Stock_With_Customer_Model.Customers_Model.CustomerContact = dataReader["CUSTOMER_CONTACT"].ToString();
                        purchase_Stock_With_Customer_Model.Customers_Model.CustomerAddress = dataReader["CUSTOMER_ADDRESS"].ToString();
                        purchase_Stock_With_Customer_Model.Purchase_Stock.ProductId = Convert.ToInt32(dataReader["PRODUCT_ID"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.ProductGradeId = Convert.ToInt32(dataReader["PRODUCT_GRADE_ID"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockLocation = dataReader["LOCATION"].ToString();
                        purchase_Stock_With_Customer_Model.Purchase_Stock.Bags = Convert.ToDecimal(dataReader["BAGS"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.BagPerKg = Convert.ToDecimal(dataReader["BAG_PER_KG"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.TotalWeight = Convert.ToDecimal(dataReader["TOTAL_WEIGHT"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.ProductPrice = Convert.ToDecimal(dataReader["PRODUCT_PRICE"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.TotalPrice = Convert.ToDecimal(dataReader["TOTAL_PRICE"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleId = Convert.ToInt32(dataReader["VEHICLE_ID"]);
                        purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleNo = dataReader["VEHICLE_NO"].ToString();
                        purchase_Stock_With_Customer_Model.Purchase_Stock.DriverName = dataReader["DRIVER_NAME"].ToString();
                        purchase_Stock_With_Customer_Model.Purchase_Stock.TolatName = dataReader["TOLAT_NAME"].ToString();
                        purchase_Stock_With_Customer_Model.Purchase_Stock.PaymentStatus = dataReader["PAYMENT_STATUS"].ToString();
                    }
                }

                return purchase_Stock_With_Customer_Model;
            }
            catch (Exception ex)
            {
                // Optionally, log the error or handle it as needed
                return null;
            }
        }

        #endregion

        #region Section: Update Purchase Stock Details

        /// <summary>
        /// Updates a purchase stock entry with detailed information including customer and product details.
        /// </summary>
        /// <param name="purchase_Stock_With_Customer_Model">The model containing all data necessary for updating the purchase stock and associated customer details.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>

        public bool UpdatePurchaseStock(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_PURCHASE_STOCK_UPDATE");

                DateTime? invoiceDate = purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockId);
                sqlDatabase.AddInParameter(dbCommand, "@PUR_STOCK_DATE", SqlDbType.Date, onlyDate);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Customers_Model.CustomerId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_GRADE_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductGradeId);
                sqlDatabase.AddInParameter(dbCommand, "@PUR_STOCK_LOCATION", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockLocation);
                sqlDatabase.AddInParameter(dbCommand, "@BAG", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.Bags);
                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.BagPerKg);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.TotalWeight);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.ProductPrice);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, purchase_Stock_With_Customer_Model.Purchase_Stock.TotalPrice);
                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleId);
                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.VarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.VehicleNo);
                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.DriverName);
                sqlDatabase.AddInParameter(dbCommand, "@TOLAT_NAME", SqlDbType.NVarChar, purchase_Stock_With_Customer_Model.Purchase_Stock.TolatName);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        #endregion

    }
}