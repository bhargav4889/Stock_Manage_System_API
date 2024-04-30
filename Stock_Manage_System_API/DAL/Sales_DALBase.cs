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
                sqlDatabase.AddInParameter(dbCommand, "@RECEIVE_AMOUNT_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Receive_Bank_Id);
                sqlDatabase.AddInParameter(dbCommand, "@IS_PAYMENT_AMOUNT_RECEIVE", SqlDbType.Bit, sale_Customer_Combined_Model.sale.IsFullPaymentReceive);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_DATE", SqlDbType.Date, sale_Customer_Combined_Model.sale.Remain_Payment_Date);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_AMOUNT", SqlDbType.Decimal, sale_Customer_Combined_Model.sale.Receive_Remain_Amount);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_PAYMENT_METHOD", SqlDbType.VarChar, sale_Customer_Combined_Model.sale.Remain_Payment_Method);
                sqlDatabase.AddInParameter(dbCommand, "@REMAIN_INFORMATION_ID", SqlDbType.Int, sale_Customer_Combined_Model.sale.Remain_Amount_Receive_Bank_Id);
                
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
                    Sale_Info.Discount = reader["DISCOUNT"] is DBNull ? null : Convert.ToDecimal(reader["DISCOUNT"]);
                    // Handle the boolean conversion with reversed logic directly
                    bool isFullPaymentReceive = reader.IsDBNull(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE")) ? false : reader.GetBoolean(reader.GetOrdinal("IS_FULL_AMOUNT_RECEIVE"));
                    Sale_Info.IsFullPaymentReceive = !isFullPaymentReceive;  // Reversing the boolean logic

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


    }
}
