using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Payment_DALBase : DAL_Helpers
    {
        #region Section: SetUp Of Database Connection and Initialization

        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the Payment_DALBase class, setting up the database connection.
        /// </summary>
        public Payment_DALBase()
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


        #region Section : Get Payment Info By Stock & Customer ID

        /// <summary>
        /// Retrieves payment information for a specific stock and customer.
        /// </summary>
        /// <param name="Stock_ID">The ID of the stock.</param>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <returns>Payment information for the specified stock and customer.</returns>
        public Payment_Model GetPaymentInfoByStockCustomerId(int Stock_ID, int Customer_ID)
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

        #region Section : Get Remain Payment Info By Stock  & Customer ID

        /// <summary>
        /// Retrieves remaining payment information for a specific stock, customer, and payment.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <param name="Stock_ID">The ID of the stock.</param>
        /// <returns>Remaining payment information for the specified stock, customer, and payment.</returns>
        public Remain_Payment_Model RemainGetPaymentInfoByCustomerFkAndStockIdAndPaymentId(int Customer_ID, int Stock_ID)
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

        #region Section : Display All Pending Customers Payments

        /// <summary>
        /// Retrieves a list of all customers with pending payments.
        /// </summary>
        /// <returns>A list of pending customer payments.</returns>
        public List<Pending_Customers_Payments> GetPendingCustomersPayments()
        {
            List<Pending_Customers_Payments> _Customers_Payment_List = new List<Pending_Customers_Payments>();
            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PENDING_PAYMENTS_CUSTOMERS_LIST");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Pending_Customers_Payments pendingCustomerPayment = new Pending_Customers_Payments
                    {
                        StockId = Convert.ToInt32(reader[0]),
                        StockDate = Convert.ToDateTime(reader[1]),
                        CustomerId = Convert.ToInt32(reader[2]),
                        CustomerName = reader[3].ToString(),
                        ProductId = Convert.ToInt32(reader[4]),
                        ProductName = reader[5].ToString(),
                        Location = reader[6].ToString(),
                        TotalPrice = Convert.ToDecimal(reader[7]),
                        Payment_Status = reader[8].ToString()
                    };
                    _Customers_Payment_List.Add(pendingCustomerPayment);
                }
                return _Customers_Payment_List;
            }
        }



        #endregion

        #region Section : Display All Remaining Customers Payments
        /// <summary>
        /// Retrieves a list of customers with remaining payments.
        /// </summary>
        /// <returns>A list of remaining customer payments.</returns>
        public List<Remain_Payment_Model> GetRemainingCustomersPayments()
        {
            List<Remain_Payment_Model> _Customers_Payment_List = new List<Remain_Payment_Model>();
            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_REMAIN_PAYMENTS_CUSTOMER_LIST");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Remain_Payment_Model remainPayment = new Remain_Payment_Model
                    {
                        Payment_Id = Convert.ToInt32(reader[0]),
                        Stock_Id = Convert.ToInt32(reader[1]),
                        Payment_Date = Convert.ToDateTime(reader[2]),
                        Customer_Id = Convert.ToInt32(reader[3]),
                        Customer_Name = reader[4].ToString(),
                        Product_Id = Convert.ToInt32(reader[5]),
                        Product_Name = reader[6].ToString(),
                        Total_Amount = Convert.ToDecimal(reader[7]),
                        Paid_Amount = Convert.ToDecimal(reader[8]),
                        First_Payment_Method = reader[9].ToString(),
                        Remain_Payment_Status = reader[10].ToString()
                    };
                    _Customers_Payment_List.Add(remainPayment);
                }
                return _Customers_Payment_List;
            }
        }

        #endregion

        #region Section : Display All Completed Customers Payments

        /// <summary>
        /// Retrieves a list of all customers who have completed their payments.
        /// </summary>
        /// <returns>A list of fully paid customer payments.</returns>
        public List<Show_Payment_Info> GetPaidCustomersPayments()
        {
            List<Show_Payment_Info> paymentInfoList = new List<Show_Payment_Info>();
            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PAID_PAYMENTS_CUSTOMER_LIST");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    // The method of extracting data is omitted for brevity but follows the same pattern as above.
                    // paymentInfoList.Add(paymentInfo);
                }
                return paymentInfoList;
            }
        }

        #endregion

        #region Section : Insert Payment

        /// <summary>
        /// Inserts a new payment record into the database.
        /// </summary>
        /// <param name="payment_Model">The payment model containing all necessary payment data.</param>
        /// <returns>True if the payment is successfully inserted, otherwise false.</returns>

        public bool InsertPayment(Payment_Model payment_Model)
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

        #region Section : Insert Remain Payment 

        /// <summary>
        /// Inserts a remaining payment record into the database.
        /// </summary>
        /// <param name="remain_Payment_Model">The remain payment model with details required for the payment.</param>
        /// <returns>True if the remaining payment is successfully inserted, otherwise false.</returns>

        public bool InsertRemainPayment(Remain_Payment_Model remain_Payment_Model)
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


        #endregion

        #region Section : Display All Payment Infromation By Customer & Stock ID

        /// <summary>
        /// Retrieves payment information for a specified customer and stock ID.
        /// </summary>
        /// <param name="customer_ID">The ID of the customer.</param>
        /// <param name="stock_ID">The ID of the stock associated with the payment.</param>
        /// <returns>A Show_Payment_Info object containing detailed payment information.</returns>
        public Show_Payment_Info GetFullPaymentInfo(int Customer_ID, int Stock_ID)
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
