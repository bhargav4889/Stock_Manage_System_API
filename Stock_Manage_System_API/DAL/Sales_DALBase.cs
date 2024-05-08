using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Sales_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Sales_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }


        #region Method : Insert Sale

        public bool Insert_Sale_With_Customer(Sale_Customer_Combied_Model sale_Customer_Combined_Model)
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
                sqlDatabase.AddInParameter(dbCommand, "@DISCOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Discount);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Payment_Method);
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Receive_Information_Id);
                sqlDatabase.AddInParameter(dbCommand, "@IS_PAYMENT_AMOUNT_RECEIVE", SqlDbType.Bit, sale_Customer_Combined_Model.sale.IsFullPaymentReceive);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Remain_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Remain_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Remain_Payment_Method);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Remain_Infromation_ID);
                
                sqlDatabase.AddInParameter(dbCommand, "@DEDUCT_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Deducted_Amount);
              

               
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


        #region Method : Update Sale

        public bool Update_Sale(Sale_Customer_Combied_Model sale_Customer_Combined_Model)
        {
            try
            {
                // Create the database command for the stored procedure
                DbCommand dbCommand = Command_Name("API_SALE_UPDATE");


                sqlDatabase.AddInParameter(dbCommand, "@SALE_STOCK_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.SaleId);

                // Add parameters to the command from the model
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
               

                if(sale_Customer_Combined_Model.sale.Payment_Method == "CASH")
                {
                    sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Receive_Information_Id);
                }

              
                sqlDatabase.AddInParameter(dbCommand, "@IS_PAYMENT_AMOUNT_RECEIVE", SqlDbType.Bit, sale_Customer_Combined_Model.sale.IsFullPaymentReceive);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Remain_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Remain_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Remain_Payment_Method);

                if(sale_Customer_Combined_Model.sale.Remain_Payment_Method == "CASH")
                {
                    sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Remain_Infromation_ID);

                }
              

                sqlDatabase.AddInParameter(dbCommand, "@DEDUCT_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Deducted_Amount);



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

        public List<Show_Sale> Show_All_Sales()
        {
            List<Show_Sale> List_of_Sales_Info = new List<Show_Sale>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_SALES");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Show_Sale Sale_Info = new Show_Sale();  // Move instantiation inside the loop

                    Sale_Info.saleId = Convert.ToInt32(reader["SALE_STOCK_ID"]);
                    Sale_Info.Create_Sales = Convert.ToDateTime(reader["SALE_STOCK_DATE"]);
                    Sale_Info.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
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
                    Sale_Info.Payment_Method = reader["RECEIVE_PAYMENT_METHOD"].ToString();
                    Sale_Info.Discount = reader["DISCOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DISCOUNT"]);
                    bool isFullPaymentReceive = reader.IsDBNull(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                    Sale_Info.IsFullPaymentReceive = !isFullPaymentReceive;  // Reversing the boolean logic

                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_DATE")))
                    {
                        Sale_Info.Remain_Payment_Date = Convert.ToDateTime(reader["REMAIN_PAYMENT_DATE"]);
                    }
                    else
                    {
                        Sale_Info.Remain_Payment_Date = null;  // It's safe to assign null
                    }

                    Sale_Info.Remain_Payment_Method = reader["REMAIN_PAYMENT_METHOD"].ToString();

                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_AMOUNT")))
                    {
                        Sale_Info.Receive_Remain_Amount = Convert.ToDecimal(reader["REMAIN_AMOUNT"]);
                    }
                    else
                    {
                        Sale_Info.Receive_Remain_Amount = null;  // It's safe to assign null 
                    }

                    Sale_Info.Deducted_Amount = Convert.ToDecimal(reader["DEDUCT_AMOUNT"]);

                    List_of_Sales_Info.Add(Sale_Info);  // Add each new object to the list
                }
            }
            return List_of_Sales_Info;
        }


        public Show_Sale Show_Sale_By_ID(int Sale_ID)
        {
            Show_Sale Sale_Info = new Show_Sale();  

            DbCommand dbCommand = Command_Name("API_SALE_BY_PK");

            sqlDatabase.AddInParameter(dbCommand,"@SALE_ID",SqlDbType.Int, Sale_ID);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    Sale_Info.saleId = Convert.ToInt32(reader["SALE_STOCK_ID"]);
                    
                    Sale_Info.Create_Sales = Convert.ToDateTime(reader["SALE_STOCK_DATE"]);
                    Sale_Info.Receive_Payment_Date = Convert.ToDateTime(reader["RECEIVE_PAYMENT_DATE"].ToString());
                    Sale_Info.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
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
                    // Handle the boolean conversion with reversed logic directly
                    bool isFullPaymentReceive = reader.IsDBNull(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                    Sale_Info.IsFullPaymentReceive = !isFullPaymentReceive;  // Reversing the boolean logic

                    Sale_Info.Payment_Method = reader["RECEIVE_PAYMENT_METHOD"].ToString();
                    Sale_Info.Deducted_Amount = reader["DEDUCT_AMOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DEDUCT_AMOUNT"].ToString());

                    Sale_Info.Receive_Information_ID = Convert.ToInt32(reader["INFORMATION_ID"].ToString());
                    Sale_Info.Remain_Receive_Information_ID = Convert.ToInt32(reader["REMAIN_INFORMATION_ID"].ToString());

                    Sale_Info.Receive_Account_No = reader["BANK_ACCOUNT_NO"].ToString();
                    Sale_Info.Bank_Icon = reader["BANK_ICON_ACTUAL"].ToString();
                    Sale_Info.Account_Holder_Name = reader["BANK_ACCOUNT_HOLDER_NAME"].ToString();


                    // Assuming Remain_Payment_Date is a nullable DateTime property (DateTime?)
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_DATE")))
                    {
                        Sale_Info.Remain_Payment_Date = Convert.ToDateTime(reader["REMAIN_PAYMENT_DATE"]);
                    }
                    else
                    {
                        Sale_Info.Remain_Payment_Date = null;  // It's safe to assign null
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_METHOD")))
                    {
                        Sale_Info.Remain_Payment_Method = reader["REMAIN_PAYMENT_METHOD"].ToString();
                    }
                    else
                    {
                        Sale_Info.Remain_Payment_Method = null;  // It's safe to assign null 
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_AMOUNT")))
                    {
                        Sale_Info.Receive_Remain_Amount = Convert.ToDecimal(reader["REMAIN_AMOUNT"]);
                    }
                    else
                    {
                        Sale_Info.Receive_Remain_Amount = null;  // It's safe to assign null 
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_BANK_ACCOUNT_NO")))
                    {
                        Sale_Info.Receive_Remain_Account_No = (reader["REMAIN_BANK_ACCOUNT_NO"].ToString());
                    }
                    else
                    {
                        Sale_Info.Receive_Remain_Account_No = null;  // It's safe to assign null 
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_BANK_ACCOUNT_HOLDER_NAME")))
                    {
                        Sale_Info.Remain_Account_Holder_Name = (reader["REMAIN_BANK_ACCOUNT_HOLDER_NAME"].ToString());
                    }
                    else
                    {
                        Sale_Info.Receive_Remain_Account_No = null;  // It's safe to assign null
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("BANK_ICON_REMAIN")))
                    {
                        Sale_Info.Remain_Bank_Icon = (reader["BANK_ICON_REMAIN"].ToString());
                    }
                    else
                    {
                        Sale_Info.Remain_Bank_Icon = null;  // It's safe to assign null
                    }


                  




                }
            }


            return Sale_Info;



        }



        public Sale_Customer_Combied_Model Fetch_Sale_And_Customer_Details(int Sale_ID, int Customer_ID)
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


        // Delete a Sale 
        public bool DELETE_SALE(int Sale_ID)
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



    }
}
