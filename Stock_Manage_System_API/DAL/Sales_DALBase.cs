using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// A base class for Sales Data Access Layer operations.
    /// </summary>
    public class Sales_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sales_DALBase"/> class.
        /// </summary>
        public Sales_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Creates a database command for the specified stored procedure name.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A <see cref="DbCommand"/> instance for the stored procedure.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #region Method : Insert Sale

        /// <summary>
        /// Inserts a sale with customer information into the database.
        /// </summary>
        /// <param name="sale_Customer_Combined_Model">The sale and customer information to be inserted.</param>
        /// <returns>True if the sale is inserted successfully, false otherwise.</returns>
        public bool InsertSale(Sale_Customer_Combied_Model sale_Customer_Combined_Model)
        {
            try
            {
                // Create the database command for the stored procedure
                DbCommand dbCommand = Command_Name("API_SALE_INSERT");

                // Add parameters to the command from the model
                sqlDatabase.AddInParameter(dbCommand, "@SALE_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Create_Sales);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Product_Id);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Receive_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, sale_Customer_Combined_Model.customer.CustomerId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_BRAND_NAME", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Brand_Name);
                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Bags);
                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.BagPerKg);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Total_Weight);
                sqlDatabase.AddInParameter(dbCommand, "@RATE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Rate);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Total_Price);

                if (sale_Customer_Combined_Model.sale.CGST.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@CGST", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.CGST.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@CGST", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }


                if (sale_Customer_Combined_Model.sale.SGST.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@SGST", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.SGST.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@SGST", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }


                if (sale_Customer_Combined_Model.sale.TotalCGSTPrice.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@TOTAL_CGST_PRICE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.TotalCGSTPrice.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@TOTAL_CGST_PRICE", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }


                if (sale_Customer_Combined_Model.sale.TotalSGSTPrice.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@TOTAL_SGST_PRICE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.TotalSGSTPrice.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@TOTAL_SGST_PRICE", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }


                if (sale_Customer_Combined_Model.sale.WithoutGSTPrice.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@WITHOUT_GST_PRICE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.WithoutGSTPrice.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@WITHOUT_GST_PRICE", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }

                if (sale_Customer_Combined_Model.sale.Discount.HasValue)
                {
                    sqlDatabase.AddInParameter(dbCommand, "@DISCOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Discount.Value);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@DISCOUNT", SqlDbType.Decimal, 0m); // Use 0m for decimal zero
                }


                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Payment_Method);

                if (sale_Customer_Combined_Model.sale.Payment_Method == "BANK")
                {
                    sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Receive_Information_Id);
                }



                sqlDatabase.AddInParameter(dbCommand, "@IS_PAYMENT_AMOUNT_RECEIVE", SqlDbType.Bit, sale_Customer_Combined_Model.sale.IsFullPaymentReceive);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Remain_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Remain_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Remain_Payment_Method);


                if (sale_Customer_Combined_Model.sale.Remain_Payment_Method == "BANK")
                {
                    sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Remain_Infromation_ID);
                }

                sqlDatabase.AddInParameter(dbCommand, "@DEDUCT_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Deducted_Amount);

                // Execute the command and return the result
                return Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Method : Update Sale

        /// <summary>
        /// Updates a sale in the database.
        /// </summary>
        /// <param name="sale_Customer_Combined_Model">The updated sale and customer information.</param>
        /// <returns>True if the sale is updated successfully, false otherwise.</returns>
        public bool UpdateSale(Sale_Customer_Combied_Model sale_Customer_Combined_Model)
        {
            try
            {
                // Create the database command for the stored procedure
                DbCommand dbCommand = Command_Name("API_SALE_UPDATE");

                // Add parameters to the command from the model
                sqlDatabase.AddInParameter(dbCommand, "@SALE_STOCK_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.SaleId);
                sqlDatabase.AddInParameter(dbCommand, "@SALE_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Create_Sales);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Product_Id);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Receive_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Payment_Method);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, sale_Customer_Combined_Model.customer.CustomerId);
                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_BRAND_NAME", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Brand_Name);
                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Bags);
                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.BagPerKg);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Total_Weight);
                sqlDatabase.AddInParameter(dbCommand, "@RATE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Rate);
                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Total_Price);
                sqlDatabase.AddInParameter(dbCommand, "@DISCOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Discount);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Receive_Information_Id);
                sqlDatabase.AddInParameter(dbCommand, "@IS_PAYMENT_AMOUNT_RECEIVE", SqlDbType.Bit, sale_Customer_Combined_Model.sale.IsFullPaymentReceive);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Remain_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Remain_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Remain_Payment_Method);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Remain_Infromation_ID);
                sqlDatabase.AddInParameter(dbCommand, "@DEDUCT_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Deducted_Amount);

                // Execute the command and return the result
                return Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand));
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Method : Sale Information By ID


        /// <summary>
        /// Retrieves a sale by its ID from the database.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale to retrieve.</param>
        /// <returns>A <see cref="Show_Sale"/> object representing the sale with the specified ID.</returns>
        public Show_Sale GetSaleByID(int Sale_ID)
        {
            Show_Sale Sale_Info = new Show_Sale();

            DbCommand dbCommand = Command_Name("API_SALE_BY_PK");

            sqlDatabase.AddInParameter(dbCommand, "@SALE_ID", SqlDbType.Int, Sale_ID);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read()) // Checks if there is at least one row
                {
                    Sale_Info.saleId = Convert.ToInt32(reader["SALE_STOCK_ID"]);
                    Sale_Info.Create_Sales = Convert.ToDateTime(reader["SALE_STOCK_DATE"]);
                    Sale_Info.Receive_Payment_Date = Convert.ToDateTime(reader["RECEIVE_PAYMENT_DATE"]);
                    Sale_Info.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
                    Sale_Info.CustomerName = reader["CUSTOMER_NAME"].ToString();
                    Sale_Info.CustomerType = reader["CUSTOMER_TYPE"].ToString();
                    Sale_Info.Product_Id = Convert.ToInt32(reader["PRODUCT_ID"]);
                    Sale_Info.Product_Name = reader["PRODUCT_NAME_IN_ENGLISH"].ToString();
                    Sale_Info.Brand_Name = reader["PRODUCT_BRAND_NAME"].ToString();
                    Sale_Info.Bags = Convert.ToDecimal(reader["BAGS"]);
                    Sale_Info.BagPerKg = Convert.ToDecimal(reader["BAG_PER_KG"]);
                    Sale_Info.Rate = Convert.ToDecimal(reader["RATE"]);
                    Sale_Info.CGST = Convert.ToDecimal(reader["CGST"]);
                    Sale_Info.SGST = Convert.ToDecimal(reader["SGST"]);
                    Sale_Info.TotalCGSTPrice = Convert.ToDecimal(reader["TOTAL_CGST_PRICE"]);
                    Sale_Info.TotalSGSTPrice = Convert.ToDecimal(reader["TOTAL_SGST_PRICE"]);
                    Sale_Info.WithoutGSTPrice = Convert.ToDecimal(reader["WITHOUT_GST_PRICE"]);

                    Sale_Info.Total_Weight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                    Sale_Info.Total_Price = Convert.ToDecimal(reader["TOTAL_AMOUNT"]);
                    Sale_Info.Receive_Amount = Convert.ToDecimal(reader["RECEIVE_AMOUNT"]);
                    Sale_Info.Discount = Convert.ToDecimal(reader["DISCOUNT"]);
                    Sale_Info.IsFullPaymentReceive = reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                    Sale_Info.Payment_Method = reader["RECEIVE_PAYMENT_METHOD"].ToString();
                    Sale_Info.Deducted_Amount = Convert.ToDecimal(reader["DEDUCT_AMOUNT"]);

                    Sale_Info.Receive_Information_ID = reader.IsDBNull(reader.GetOrdinal("INFORMATION_ID")) ? (int?)null : Convert.ToInt32(reader["INFORMATION_ID"]);
                    Sale_Info.Remain_Receive_Information_ID = reader.IsDBNull(reader.GetOrdinal("REMAIN_INFORMATION_ID")) ? (int?)null : Convert.ToInt32(reader["REMAIN_INFORMATION_ID"]);

                    Sale_Info.Receive_Account_No = reader["BANK_ACCOUNT_NO"].ToString();
                    Sale_Info.Bank_Icon = reader["BANK_ICON_ACTUAL"].ToString();
                    Sale_Info.Account_Holder_Name = reader["BANK_ACCOUNT_HOLDER_NAME"].ToString();

                    Sale_Info.Remain_Payment_Date = reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_DATE")) ? (DateTime?)null : Convert.ToDateTime(reader["REMAIN_PAYMENT_DATE"]);
                    Sale_Info.Remain_Payment_Method = reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_METHOD")) ? null : reader["REMAIN_PAYMENT_METHOD"].ToString();
                    Sale_Info.Receive_Remain_Amount = reader.IsDBNull(reader.GetOrdinal("REMAIN_AMOUNT")) ? (decimal?)null : Convert.ToDecimal(reader["REMAIN_AMOUNT"]);
                    Sale_Info.Receive_Remain_Account_No = reader.IsDBNull(reader.GetOrdinal("REMAIN_BANK_ACCOUNT_NO")) ? null : reader["REMAIN_BANK_ACCOUNT_NO"].ToString();
                    Sale_Info.Remain_Account_Holder_Name = reader.IsDBNull(reader.GetOrdinal("REMAIN_BANK_ACCOUNT_HOLDER_NAME")) ? null : reader["REMAIN_BANK_ACCOUNT_HOLDER_NAME"].ToString();
                    Sale_Info.Remain_Bank_Icon = reader.IsDBNull(reader.GetOrdinal("BANK_ICON_REMAIN")) ? null : reader["BANK_ICON_REMAIN"].ToString();
                }
            }

            return Sale_Info;
        }


        #endregion

        #region Method : Sale With Customer Information Data

        /// <summary>
        /// Retrieves the sale and customer details for a given sale and customer ID.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale.</param>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <returns>A <see cref="Sale_Customer_Combied_Model"/> object containing the sale and customer details.</returns>
        public Sale_Customer_Combied_Model GetSaleByCustomerAndSaleID(int Sale_ID, int Customer_ID)
        {
            Sale_Customer_Combied_Model Sale_Info = new Sale_Customer_Combied_Model();

            DbCommand dbCommand = Command_Name("API_FETCH_SALE_STOCK_CUSTOMER_DETAILS_BY_SALE_AND_CUSTOMER_ID");

            sqlDatabase.AddInParameter(dbCommand, "@SALE_ID", SqlDbType.Int, Sale_ID);

            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

            Sale_Info.sale = new SaleModel();
            Sale_Info.customer = new Customer_Model();

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {

                    Sale_Info.sale.SaleId = Convert.ToInt32(reader["SALE_STOCK_ID"]);
                    Sale_Info.sale.Create_Sales = Convert.ToDateTime(reader["SALE_STOCK_DATE"]);
                    Sale_Info.sale.Receive_Payment_Date = Convert.ToDateTime(reader["RECEIVE_PAYMENT_DATE"].ToString());
                    Sale_Info.customer.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
                    Sale_Info.customer.CustomerName = reader["CUSTOMER_NAME"].ToString();
                    Sale_Info.customer.CustomerType = reader["CUSTOMER_TYPE"].ToString();
                    Sale_Info.sale.Product_Id = Convert.ToInt32(reader["PRODUCT_ID"]);
                    Sale_Info.sale.Product_Name = reader["PRODUCT_NAME_IN_ENGLISH"].ToString();
                    Sale_Info.sale.Brand_Name = reader["PRODUCT_BRAND_NAME"].ToString();
                    Sale_Info.sale.Bags = reader["BAGS"] is DBNull ? null : Convert.ToDecimal(reader["BAGS"]);
                    Sale_Info.sale.BagPerKg = reader["BAG_PER_KG"] is DBNull ? null : Convert.ToDecimal(reader["BAG_PER_KG"]);
                    Sale_Info.sale.Rate = Convert.ToDecimal(reader["RATE"]);
                    Sale_Info.sale.Total_Weight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                    Sale_Info.sale.Total_Price = Convert.ToDecimal(reader["TOTAL_AMOUNT"]);
                    Sale_Info.sale.Receive_Amount = Convert.ToDecimal(reader["RECEIVE_AMOUNT"]);
                    Sale_Info.sale.Discount = reader["DISCOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DISCOUNT"]);
                    // Handle the boolean conversion with reversed logic directly
                    bool isFullPaymentReceive = reader.IsDBNull(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                    Sale_Info.sale.IsFullPaymentReceive = isFullPaymentReceive;  // Reversing the boolean logic

                    Sale_Info.sale.Payment_Method = reader["RECEIVE_PAYMENT_METHOD"].ToString();
                    Sale_Info.sale.Deducted_Amount = reader["DEDUCT_AMOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DEDUCT_AMOUNT"].ToString());

                    Sale_Info.sale.Receive_Information_Id = Convert.ToInt32(reader["INFORMATION_ID"].ToString());
                    Sale_Info.sale.Remain_Infromation_ID = Convert.ToInt32(reader["REMAIN_INFORMATION_ID"].ToString());

                    // Assuming Remain_Payment_Date is a nullable DateTime property (DateTime?)
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_DATE")))
                    {
                        Sale_Info.sale.Remain_Payment_Date = Convert.ToDateTime(reader["REMAIN_PAYMENT_DATE"]);
                    }
                    else
                    {
                        Sale_Info.sale.Remain_Payment_Date = null;  // It's safe to assign null
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_METHOD")))
                    {
                        Sale_Info.sale.Remain_Payment_Method = reader["REMAIN_PAYMENT_METHOD"].ToString();
                    }
                    else
                    {
                        Sale_Info.sale.Remain_Payment_Method = null;  // It's safe to assign null 
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_AMOUNT")))
                    {
                        Sale_Info.sale.Receive_Remain_Amount = Convert.ToDecimal(reader["REMAIN_AMOUNT"]);
                    }
                    else
                    {
                        Sale_Info.sale.Receive_Remain_Amount = null;  // It's safe to assign null 
                    }

                }
            }

            return Sale_Info;
        }

        #endregion

        #region Method : Delete Sale

        /// <summary>
        /// Deletes a sale from the database.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale to delete.</param>
        /// <returns>True if the sale is deleted successfully, false otherwise.</returns>
        public bool DeleteSale(int Sale_ID)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_SALE_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@SALE_ID", SqlDbType.Int, Sale_ID);
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

        #region Method : Show All Sales

        public List<Show_Sale> GetAllSales()
        {
            List<Show_Sale> List_of_Sales_Info = new List<Show_Sale>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_SALES");

            Show_Sale saleInfo = new Show_Sale();

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {


                    saleInfo.saleId = Convert.ToInt32(reader["SALE_STOCK_ID"]);
                    saleInfo.Create_Sales = Convert.ToDateTime(reader["SALE_STOCK_DATE"]);
                    saleInfo.Receive_Payment_Date = Convert.ToDateTime(reader["RECEIVE_PAYMENT_DATE"]);
                    saleInfo.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
                    saleInfo.CustomerName = reader["CUSTOMER_NAME"].ToString();
                    saleInfo.CustomerType = reader["CUSTOMER_TYPE"].ToString();
                    saleInfo.Product_Id = Convert.ToInt32(reader["PRODUCT_ID"]);
                    saleInfo.Product_Name = reader["PRODUCT_NAME_IN_ENGLISH"].ToString();
                    saleInfo.Brand_Name = reader["PRODUCT_BRAND_NAME"].ToString();
                    saleInfo.Bags = Convert.ToDecimal(reader["BAGS"]);
                    saleInfo.BagPerKg = Convert.ToDecimal(reader["BAG_PER_KG"]);
                    saleInfo.Rate = Convert.ToDecimal(reader["RATE"]);
                    saleInfo.Total_Weight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                    saleInfo.Total_Price = Convert.ToDecimal(reader["TOTAL_AMOUNT"]);
                    List_of_Sales_Info.Add(saleInfo);



                }
                return List_of_Sales_Info;
            }

            #endregion
        }
    }
}