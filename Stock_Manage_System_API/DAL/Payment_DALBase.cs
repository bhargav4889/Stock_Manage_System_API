﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Payment_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Payment_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }


        #region Method : Get Payment Info By Stock Id And Customer Id 

        public Payment_Model Get_Payment_Info_By_Stock_Customer_PK(int Stock_ID, int Customer_ID)
        {
            Payment_Model payment_Model = new Payment_Model();

            using (DbCommand dbCommand = Command_Name("API_SHOW_PAYMENT_INFO_BY_CUSTOMER_PK_AND_STOCK_ID"))
            {

                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, Stock_ID);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);


                using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {


                        payment_Model.Customer_Id = Convert.ToInt32(reader[0]);
                        payment_Model.Customer_Name = reader[1].ToString();
                        payment_Model.Customer_Type = reader[2].ToString();
                        payment_Model.Stock_Id = Convert.ToInt32(reader[3].ToString());
                        payment_Model.Stock_Added_Date = Convert.ToDateTime(reader[4].ToString());
                        payment_Model.Stock_Loaction = reader[5].ToString();
                        payment_Model.Product_Id = Convert.ToInt32(reader[6].ToString());
                        payment_Model.Product_Name = reader[7].ToString();
                        payment_Model.Total_Price = Convert.ToDecimal(reader[8].ToString());
                        payment_Model.Payment_Status = reader[9].ToString();



                    }
                }

                return payment_Model;

            }


           
        }

        #endregion

        #region Method : Get Remain Payment Info By Stock Id and Customer Id 

        public Remain_Payment_Model Remain_Get_Payment_Info_By_Customer_FK_And_Stock_Id_And_Payment_Id(int Customer_ID, int Stock_ID)
        {
            Remain_Payment_Model remain_Payment_Model = new Remain_Payment_Model();

            using (DbCommand dbCommand = Command_Name("API_SHOW_REMAIN_PAYMENT_INFO_BY_CUSTOMER_FK_AND_STOCK_ID_AND_PAYMENT_ID"))
            {

                sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, Stock_ID);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

               


                using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (reader.Read())
                    {

                        remain_Payment_Model.Stock_Id = Convert.ToInt32(reader[0]);

                        remain_Payment_Model.Payment_Id = Convert.ToInt32(reader[1]);

                        remain_Payment_Model.Payment_Date = Convert.ToDateTime(reader[2].ToString());

                        remain_Payment_Model.Customer_Id = Convert.ToInt32(reader[3]);

                        remain_Payment_Model.Customer_Name = reader[4].ToString();

                        remain_Payment_Model.Product_Id = Convert.ToInt32(reader[5]);

                        remain_Payment_Model.Product_Name = reader[6].ToString();

                        remain_Payment_Model.Total_Amount = Convert.ToDecimal(reader[7].ToString());

                        remain_Payment_Model.Paid_Amount = Convert.ToDecimal(reader[8].ToString());

                        remain_Payment_Model.Remain_Amount = Convert.ToDecimal(reader[9].ToString());

                        remain_Payment_Model.First_Payment_Method = (reader[10]).ToString();

                        remain_Payment_Model.Bank_Id = Convert.ToInt32(reader[11].ToString());
                      
                        remain_Payment_Model.Bank_Name = reader[12].ToString();
                      
                        remain_Payment_Model.Bank_Icon = reader[13].ToString();
                        
                        remain_Payment_Model.Bank_Ac_No = reader[14].ToString();


                        
                    }


                }

                return remain_Payment_Model;
            }

        }

        #endregion

        #region Method : List All Customers Pending Payment


        public List<Pending_Customers_Payments> Pending_Customers_Payments()
        {
            List<Pending_Customers_Payments> _Customers_Payment_List = new List<Pending_Customers_Payments>();



            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PENDING_PAYMENTS_CUSTOMERS_LIST");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Pending_Customers_Payments Pending_Customers_Payments = new Pending_Customers_Payments();
                    Pending_Customers_Payments.StockId = Convert.ToInt32(reader[0].ToString());
                    Pending_Customers_Payments.StockDate = Convert.ToDateTime(reader[1].ToString());
                    Pending_Customers_Payments.CustomerId = Convert.ToInt32(reader[2].ToString());
                    Pending_Customers_Payments.CustomerName = reader[3].ToString();
                    Pending_Customers_Payments.ProductId = Convert.ToInt32(reader[4].ToString());
                    Pending_Customers_Payments.ProductName = reader[5].ToString();
                    Pending_Customers_Payments.Location = reader[6].ToString();
                    Pending_Customers_Payments.TotalPrice = Convert.ToDecimal(reader[7].ToString());
                    Pending_Customers_Payments.Payment_Status = reader[8].ToString();

                    _Customers_Payment_List.Add(Pending_Customers_Payments);


                }

                return _Customers_Payment_List;
            }
        }


        #endregion

        #region Method : Create Payment

        public bool Create_Payment(Payment_Model payment_Model)
        {
            DbCommand dbCommand = Command_Name("API_INSERT_PAYMENT");

            sqlDatabase.AddInParameter(dbCommand, "@PaymentDate", SqlDbType.DateTime, payment_Model.Payment_Date);

            sqlDatabase.AddInParameter(dbCommand, "@CustomerID", SqlDbType.Int, payment_Model.Customer_Id);
            sqlDatabase.AddInParameter(dbCommand, "@ProductID", SqlDbType.Int, payment_Model.Product_Id);
            sqlDatabase.AddInParameter(dbCommand, "@StockID", SqlDbType.Int, payment_Model.Stock_Id);
            sqlDatabase.AddInParameter(dbCommand, "@PaidAmount", SqlDbType.Decimal, payment_Model.Paid_Amount);
            sqlDatabase.AddInParameter(dbCommand, "@RemainAmount", SqlDbType.Decimal, payment_Model.Remain_Amount);

            sqlDatabase.AddInParameter(dbCommand, "@PaymentMethod", SqlDbType.VarChar, payment_Model.Payment_Method);
            sqlDatabase.AddInParameter(dbCommand, "@BankID", SqlDbType.Int, payment_Model.Bank_Id);
            sqlDatabase.AddInParameter(dbCommand, "@Bank_AC_No", SqlDbType.VarChar, payment_Model.Bank_Ac_No);
            sqlDatabase.AddInParameter(dbCommand, "@CHEQ_No", SqlDbType.VarChar, payment_Model.CHEQ_No);
            sqlDatabase.AddInParameter(dbCommand, "@RTGS_No", SqlDbType.VarChar, payment_Model.RTGS_No);

            if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        #endregion

        #region Method : Create Remain Payment 

        public bool Create_Remain_Payment(Remain_Payment_Model remain_Payment_Model)
        {
            DbCommand dbCommand = Command_Name("API_INSERT_REMAIN_PAYMENT");

            sqlDatabase.AddInParameter(dbCommand, "@PaymentID", SqlDbType.Int, remain_Payment_Model.Payment_Id);
            sqlDatabase.AddInParameter(dbCommand, "@StockID", SqlDbType.Int, remain_Payment_Model.Stock_Id);

            sqlDatabase.AddInParameter(dbCommand, "@CustomerID", SqlDbType.Int, remain_Payment_Model.Customer_Id);
            sqlDatabase.AddInParameter(dbCommand, "@Remain_PaymentDate", SqlDbType.DateTime, remain_Payment_Model.Remain_Payment_Date);
           
            sqlDatabase.AddInParameter(dbCommand, "@Remain_PayingAmount", SqlDbType.Decimal, remain_Payment_Model.Pay_Amount);
           

            sqlDatabase.AddInParameter(dbCommand, "@Remain_PaymentMethod", SqlDbType.VarChar, remain_Payment_Model.Remain_Payment_Method);
            sqlDatabase.AddInParameter(dbCommand, "@Remain_BankID", SqlDbType.Int, remain_Payment_Model.Remain_Bank_Id);
            sqlDatabase.AddInParameter(dbCommand, "@Remain_Bank_AC_No", SqlDbType.VarChar, remain_Payment_Model.Remain_Bank_Ac_No);
            sqlDatabase.AddInParameter(dbCommand, "@Remain_CHEQ_No", SqlDbType.VarChar, remain_Payment_Model.Remain_CHEQ_NO);
            sqlDatabase.AddInParameter(dbCommand, "@Remain_RTGS_No", SqlDbType.VarChar, remain_Payment_Model.Remain_RTGS_No);

            if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public Show_Payment_Info Show_All_Payment_Info(int Customer_ID, int Stock_ID)
        {
            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PAYMENT_INFO");

            sqlDatabase.AddInParameter(dbCommand, "@STOCK_ID", SqlDbType.Int, Stock_ID);
            sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

            Show_Payment_Info show_Payment_Info = new Show_Payment_Info();

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    show_Payment_Info.PaymentID = Convert.ToInt32(reader["PAYMENT_ID"]);
                    show_Payment_Info.PaymentDate = Convert.ToDateTime(reader["PAYMENT_DATE"]);
                    show_Payment_Info.CustomerID = Convert.ToInt32(reader["CUSTOMER_ID"]);
                    show_Payment_Info.CustomerName = reader["CUSTOMER_NAME"].ToString();
                    show_Payment_Info.ProductID = Convert.ToInt32(reader["PRODUCT_ID"]);
                    show_Payment_Info.ProductName = reader["PRODUCT_NAME_IN_GUJARATI"].ToString();
                    show_Payment_Info.StockID = Convert.ToInt32(reader["PUR_STOCK_ID"]);
                    show_Payment_Info.TotalPrice = Convert.ToDecimal(reader["TOTAL_PRICE"]);
                    show_Payment_Info.AmountPaid = Convert.ToDecimal(reader["AMOUNT_PAID"]);
                    show_Payment_Info.PaymentMethod = reader["PAYMENT_METHOD"].ToString();
                    show_Payment_Info.BankID = Convert.ToInt32(reader["BANK_ID"]);
                    show_Payment_Info.BankName = reader["BANK_NAME"].ToString();
                    show_Payment_Info.BankIcon = reader["BANK_ICON"].ToString();
                    show_Payment_Info.BankAcNo = reader["BANK_AC_NO"].ToString();
                    show_Payment_Info.CheqNo = reader["CHEQ_NO"].ToString();
                    show_Payment_Info.RtgsNo = reader["RTGS_NO"].ToString();

                    // Check for DBNull before converting
                    if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_ID")))
                    {
                        show_Payment_Info.RemainPaymentID = Convert.ToInt32(reader["REMAIN_PAYMENT_ID"]);

                        // Handle nullable DateTime
                        if (!reader.IsDBNull(reader.GetOrdinal("REMAIN_PAYMENT_DATE")))
                        {
                            show_Payment_Info.RemainPaymentDate = Convert.ToDateTime(reader["REMAIN_PAYMENT_DATE"]);
                        }
                       

                        show_Payment_Info.RemainPaymentAmount = Convert.ToDecimal(reader["REMAIN_PAYMENT_AMOUNT"]);
                        show_Payment_Info.RemainPaymentMethod = reader["REMAIN_PAYMENT_METHOD"].ToString();
                        show_Payment_Info.RemainBankID = Convert.ToInt32(reader["REMAIN_BANK_ID"]);
                        show_Payment_Info.RemainBankName = reader["REMAIN_BANK_NAME"].ToString();
                        show_Payment_Info.RemainBankIcon = reader["REMAIN_BANK_ICON"].ToString();
                        show_Payment_Info.RemainBankAcNo = reader["REMAIN_BANK_AC_NO"].ToString();
                        show_Payment_Info.RemainCheqNo = reader["REMAIN_CHEQ_NO"].ToString();
                        show_Payment_Info.RemainRtgsNo = reader["REMAIN_RTGS_NO"].ToString();
                    }
                }
            }

            return show_Payment_Info;
        }



        #endregion

    }
}