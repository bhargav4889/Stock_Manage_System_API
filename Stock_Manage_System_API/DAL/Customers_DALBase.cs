using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Input;
using DbCommand = System.Data.Common.DbCommand;

namespace Stock_Manage_System_API.DAL
{
    public class Customers_DALBase : DAL_Helpers
    {
        #region Section: SetUp Of Database Connection and Initialization

        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the Customers_DALBase class, setting up the database connection.
        /// </summary>
        public Customers_DALBase()
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

        #region Section: Display All Customers
        /// <summary>
        /// Retrieves a list of all customers from the database.
        /// </summary>
        /// <returns>A list of <see cref="Customer_Model"/> instances representing all customers. Returns null if an error occurs.</returns>
        public List<Customer_Model> GetAllCustomers()
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_CUSTOMER_LIST");

                List<Customer_Model> List_Of_Customers = new List<Customer_Model>();

                using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {
                        Customer_Model customers = new Customer_Model();

                        customers.CustomerId = Convert.ToInt32(reader[0]);
                        customers.CustomerName = reader[1].ToString();
                        customers.CustomerType = reader[2].ToString();
                        customers.CustomerContact = reader[3].ToString();
                        customers.CustomerAddress = reader[4].ToString();

                        List_Of_Customers.Add(customers);

                    }
                }

                return List_Of_Customers;

            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Section: Account Details by Customer ID and Type

        /// <summary>
        /// Retrieves account details and transactions based on customer ID and type.
        /// </summary>
        /// <param name="customerID">The ID of the customer.</param>
        /// <param name="customerType">The type of the customer (e.g., "BUYER" or "SELLER").</param>
        /// <returns>A model containing customer details and either purchased stock or sales information based on customer type.</returns>

        public CustomerDetails_With_Purchased_Stock_Model RetrieveAccountDetails(int Customer_ID, string Customer_Type)
        {

            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock = new CustomerDetails_With_Purchased_Stock_Model();

            SqlDatabase database = new SqlDatabase(Database_Connection);

            using (DbCommand dbCommand = Command_Name("API_CUSTOMER_BY_PK"))
            {
                database.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                database.AddInParameter(dbCommand, "@CUSTOMER_TYPE", SqlDbType.VarChar, Customer_Type);

                using (IDataReader reader = database.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {
                        Customer_Model customers = new Customer_Model();

                        customers.CustomerId = Convert.ToInt32(reader[0]);
                        customers.CustomerName = reader[1].ToString();
                        customers.CustomerType = reader[2].ToString();
                        customers.CustomerContact = reader[3].ToString();
                        customers.CustomerAddress = reader[4].ToString();

                        customerDetails_With_Purchased_Stock.Customers = customers;

                    }
                }

            }
            if (Customer_Type == "BUYER")
            {
                using (DbCommand dbCommand1 = Command_Name("API_PURCHASED_STOCK_BY_CUSTOMER_ID"))
                {
                    database.AddInParameter(dbCommand1, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                    List<Purchased_Stock_Model> List_Of_Purchased_Stock = new List<Purchased_Stock_Model>();

                    using (IDataReader reader = database.ExecuteReader(dbCommand1))
                    {
                        while (reader.Read())
                        {
                            Purchased_Stock_Model purchased_Stock = new Purchased_Stock_Model();
                            purchased_Stock.StockId = Convert.ToInt32(reader["STOCK_ID"]);
                            purchased_Stock.ProductId = Convert.ToInt32(reader["PRODUCT_ID"]);
                            purchased_Stock.StockDate = Convert.ToDateTime(reader["PUR_STOCK_DATE"]);
                            purchased_Stock.ProductName = reader["PRODUCT_NAME"].ToString();
                            purchased_Stock.PurchaseStockLocation = reader["LOCATION"].ToString();
                            purchased_Stock.Bags = Convert.ToDecimal(reader["BAGS"]);
                            purchased_Stock.BagPerKg = Convert.ToDecimal(reader["BAG_PER_KG"]);
                            purchased_Stock.TotalWeight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                            purchased_Stock.ProductPrice = Convert.ToDecimal(reader["PRODUCT_PRICE"]);
                            purchased_Stock.TotalPrice = Convert.ToDecimal(reader["TOTAL_PRICE"]);
                            purchased_Stock.VehicleName = reader["VEHICLE_NAME"].ToString();
                            purchased_Stock.VehicleNo = reader["VEHICLE_NO"].ToString();
                            purchased_Stock.DriverName = reader["DRIVER_NAME"].ToString();
                            purchased_Stock.TolatName = reader["TOLAT_NAME"].ToString();
                            purchased_Stock.PaymentStatus = reader["PAYMENT- STATUS"].ToString();

                            List_Of_Purchased_Stock.Add(purchased_Stock);
                        }

                        customerDetails_With_Purchased_Stock.Purchased_Stocks = List_Of_Purchased_Stock;
                    }

                }
            }
            else if (Customer_Type == "SELLER")
            {
                using (DbCommand dbCommand2 = Command_Name("API_SALE_BY_CUSTOMER_ID"))
                {
                    database.AddInParameter(dbCommand2, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                    List<Show_Sale> List_Sale_Info = new List<Show_Sale>();

                    using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand2))
                    {
                        int saleStockIdOrdinal = reader.GetOrdinal("SALE_STOCK_ID");
                        int saleStockDateOrdinal = reader.GetOrdinal("SALE_STOCK_DATE");
                        int customerIdOrdinal = reader.GetOrdinal("CUSTOMER_ID");
                        // Add other necessary columns in the same way...

                        while (reader.Read())
                        {
                            Show_Sale Sale_Info = new Show_Sale();

                            Sale_Info.saleId = Convert.ToInt32(reader[saleStockIdOrdinal]);

                            Sale_Info.Create_Sales = Convert.ToDateTime(reader[saleStockDateOrdinal]);
                            Sale_Info.Receive_Payment_Date = Convert.ToDateTime(reader["RECEIVE_PAYMENT_DATE"].ToString());
                            Sale_Info.CustomerId = Convert.ToInt32(reader[customerIdOrdinal]);
                            Sale_Info.CustomerName = reader["CUSTOMER_NAME"].ToString();
                            Sale_Info.CustomerType = reader["CUSTOMER_TYPE"].ToString();
                            Sale_Info.Product_Id = Convert.ToInt32(reader["PRODUCT_ID"]);
                            Sale_Info.Product_Name = reader["PRODUCT_NAME_IN_ENGLISH"].ToString();
                            Sale_Info.Brand_Name = reader["PRODUCT_BRAND_NAME"].ToString();
                            Sale_Info.Bags = reader["BAGS"] is DBNull ? null : Convert.ToDecimal(reader["BAGS"]);
                            Sale_Info.BagPerKg = reader["BAG_PER_KG"] is DBNull ? null : Convert.ToDecimal(reader["BAG_PER_KG"]);
                            Sale_Info.Rate = Convert.ToDecimal(reader["RATE"]);
                            Sale_Info.Total_Weight = Convert.ToDecimal(reader["TOTAL_WEIGHT"]);
                            Sale_Info.Total_Price = Convert.ToDecimal(reader["TOTAL_AMOUNT"]);
                            Sale_Info.Receive_Amount = Convert.ToDecimal(reader["RECEIVE_AMOUNT"]);
                            Sale_Info.Discount = reader["DISCOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DISCOUNT"]);
                            bool isFullPaymentReceive = !reader.IsDBNull(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE")) && reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                            Sale_Info.IsFullPaymentReceive = isFullPaymentReceive; // Corrected boolean logic

                            Sale_Info.Payment_Method = reader["RECEIVE_PAYMENT_METHOD"].ToString();
                            Sale_Info.Deducted_Amount = reader["DEDUCT_AMOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DEDUCT_AMOUNT"].ToString());

                            Sale_Info.Receive_Information_ID = Convert.ToInt32(reader["INFORMATION_ID"].ToString());
                            Sale_Info.Remain_Receive_Information_ID = Convert.ToInt32(reader["REMAIN_INFORMATION_ID"].ToString());

                            Sale_Info.Receive_Account_No = reader["BANK_ACCOUNT_NO"].ToString();
                            Sale_Info.Bank_Icon = reader["BANK_ICON_ACTUAL"].ToString();
                            Sale_Info.Account_Holder_Name = reader["BANK_ACCOUNT_HOLDER_NAME"].ToString();

                            // Continue handling other fields as already defined...

                            List_Sale_Info.Add(Sale_Info);
                        }
                        customerDetails_With_Purchased_Stock.Show_Sales = List_Sale_Info;
                    }
                }
            }

            return customerDetails_With_Purchased_Stock;
        }

        #endregion

        #region Section: Insert Customer

        /// <summary>
        /// Inserts a new customer into the database.
        /// </summary>
        /// <param name="customers">The customer model containing the data to insert.</param>
        /// <returns>True if the customer was successfully inserted, otherwise false.</returns>

        [HttpPost]
        public bool InsertCustomer(Customer_Model customers)
        {
            int generatedCustomerId = 0;

            DbCommand dbCommand = Command_Name("API_CUSTOMER_INSERT");

            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, customers.CustomerName);
            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_TYPE", SqlDbType.NVarChar, customers.CustomerType);
            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ADDRESS", SqlDbType.NVarChar, customers.CustomerAddress);
            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_CONTACT", SqlDbType.NVarChar, customers.CustomerContact);

            var cidParameter = new System.Data.SqlClient.SqlParameter("@CUSTOMER_ID", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            dbCommand.Parameters.Add(cidParameter);

            // Execute the command
            if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
            {
                // Retrieve the generated CID
                generatedCustomerId = Convert.ToInt32(cidParameter.Value);
                customers.CustomerId = generatedCustomerId; // Set the generated ID in the customer model
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Section: Delete Customer

        /// <summary>
        /// Deletes a customer from the database based on the provided customer ID and type.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer to be deleted.</param>
        /// <param name="Customer_Type">The type of the customer to be deleted.</param>
        /// <returns>True if the customer was successfully deleted, otherwise false.</returns>

        public bool DeleteCustomer(int Customer_ID, string Customer_Type)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_CUSTOMER_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_TYPE", SqlDbType.VarChar, Customer_Type);
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

        #region Section: Update Customer

        /// <summary>
        /// Updates the details of an existing customer in the database.
        /// </summary>
        /// <param name="customers">The customer model containing updated information about the customer.</param>
        /// <returns>True if the customer was successfully updated, otherwise false.</returns>

        public bool UpdateCustomer(Customer_Model customers)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_CUSTOMER_UPDATE");

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, customers.CustomerId);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, customers.CustomerName);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_TYPE", SqlDbType.NVarChar, customers.CustomerType);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ADDRESS", SqlDbType.NVarChar, customers.CustomerAddress);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_CONTACT", SqlDbType.NVarChar, customers.CustomerContact);

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

        #region Section: Customer By ID and Type

        /// <summary>
        /// Retrieves a customer's details based on their ID and type.
        /// </summary>
        /// <param name="Customer_ID">The unique identifier for the customer.</param>
        /// <param name="Customer_Type">The type of the customer, e.g., "BUYER" or "SELLER".</param>
        /// <returns>A <see cref="Customer_Model"/> object containing the customer's details if found; otherwise, an empty <see cref="Customer_Model"/> object.</returns>

        public Customer_Model CustomerByIDAndType(int Customer_ID, string Customer_Type)
        {
            Customer_Model Customer_Info_By_PK = new Customer_Model();

            using (DbCommand dbCommand = Command_Name("API_CUSTOMER_BY_PK"))
            {
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_TYPE", SqlDbType.VarChar, Customer_Type);

                using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    if (reader.Read())
                    {

                        Customer_Info_By_PK.CustomerId = Convert.ToInt32(reader[0]);
                        Customer_Info_By_PK.CustomerName = reader[1].ToString();
                        Customer_Info_By_PK.CustomerType = reader[2].ToString();
                        Customer_Info_By_PK.CustomerContact = reader[3].ToString();
                        Customer_Info_By_PK.CustomerAddress = reader[4].ToString();

                    }
                }

                return Customer_Info_By_PK;

            }

        }

        #endregion

        #region Section: Customer Does Exist

        #region Area: Buyer Customer Exist

        /// <summary>
        /// Checks if a buyer-type customer with a specific name exists in the system and returns a list of matching customer records.
        /// </summary>
        /// <param name="Customer_Name">The name of the buyer-type customer to check for existence.</param>
        /// <returns>A list of Customer_Model instances matching the specified customer name; can be empty if no matches found.</returns>

        public List<Customer_Model> DoesBuyerCustomerExist(string Customer_Name)
        {

            List<Customer_Model> List_Of_Customers = new List<Customer_Model>();

            DbCommand dbCommand = Command_Name("API_CUSTOMER_EXIST_IN_SYSTEM_FOR_BUYER");

            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, Customer_Name);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Customer_Model customers = new Customer_Model();

                    customers.CustomerId = Convert.ToInt32(reader[0]);
                    customers.CustomerName = reader[1].ToString();
                    customers.CustomerType = reader[2].ToString();
                    customers.CustomerContact = reader[3].ToString();
                    customers.CustomerAddress = reader[4].ToString();

                    List_Of_Customers.Add(customers);

                }

                return List_Of_Customers;

            }

        }

        #endregion

        #region Area: Seller Customer Exist

        /// <summary>
        /// Checks if a seller-type customer with a specific name exists in the system and returns a list of matching customer records.
        /// </summary>
        /// <param name="Customer_Name">The name of the seller-type customer to check for existence.</param>
        /// <returns>A list of Customer_Model instances matching the specified customer name; can be empty if no matches found.</returns>
        public List<Customer_Model> DoesSellerCustomerExist(string Customer_Name)
        {

            List<Customer_Model> List_Of_Customers = new List<Customer_Model>();

            System.Data.Common.DbCommand dbCommand = Command_Name("API_CUSTOMER_EXIST_IN_SYSTEM_FOR_SELLER");

            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, Customer_Name);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Customer_Model customers = new Customer_Model();

                    customers.CustomerId = Convert.ToInt32(reader[0]);
                    customers.CustomerName = reader[1].ToString();
                    customers.CustomerType = reader[2].ToString();
                    customers.CustomerContact = reader[3].ToString();
                    customers.CustomerAddress = reader[4].ToString();

                    List_Of_Customers.Add(customers);

                }

                return List_Of_Customers;

            }

        }

        #endregion

        #endregion

    }
}