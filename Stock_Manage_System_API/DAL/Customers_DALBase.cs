﻿using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Input;
using DbCommand = System.Data.Common.DbCommand;

namespace Stock_Manage_System_API.DAL
{
    public class Customers_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Customers_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }


        #region DISPLAY ALL
        public List<Customer_Model> SHOW_ALL_CUSTOMERS()
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


        #region ACCOUNT DETAILS BY CUSTOMER ID

        public CustomerDetails_With_Purchased_Stock_Model Account_Details(int Customer_ID, string Customer_Type)
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
                            Sale_Info.IsFullPaymentReceive = isFullPaymentReceive;  // Corrected boolean logic

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

        #region INSERT

        [HttpPost]
        public bool CREATE_CUSTOMER(Customer_Model customers)
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

        #region DELETE 


        public bool Delete_Customer(int Customer_ID, string Customer_Type)
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


        #region UPDATE

        public bool Update_Customer(Customer_Model customers)
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


        public List<Customer_Model> BUYER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
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


        public List<Customer_Model> SELLER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
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



        public Customer_Model Customer_Info_By_PK(int Customer_ID, string Customer_Type)
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
    }
}
