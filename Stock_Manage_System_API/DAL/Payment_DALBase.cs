using Microsoft.AspNetCore.Mvc;
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



                    }
                }

                return payment_Model;

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

    }
}
