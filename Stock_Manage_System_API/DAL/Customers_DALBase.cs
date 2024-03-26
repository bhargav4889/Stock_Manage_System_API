using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Input;

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

        public CustomerDetails_With_Purchased_Stock_Model Account_Details(int Customer_ID)
        {

            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock = new CustomerDetails_With_Purchased_Stock_Model();

            SqlDatabase database = new SqlDatabase(Database_Connection);

            using (DbCommand dbCommand = Command_Name("API_CUSTOMER_BY_PK"))
            {
                database.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);



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

            using (DbCommand dbCommand1 = Command_Name("API_PURCHASED_STOCK_BY_CUSTOMER_ID"))
            {
                database.AddInParameter(dbCommand1, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);

                List<Purchased_Stock_Model> List_Of_Purchased_Stock = new List<Purchased_Stock_Model>();

                using (IDataReader reader = database.ExecuteReader(dbCommand1))
                {
                    while (reader.Read())
                    {
                        Purchased_Stock_Model purchased_Stock = new Purchased_Stock_Model();

                        purchased_Stock.StockId = Convert.ToInt32(reader[0]);

                        purchased_Stock.StockDate = Convert.ToDateTime(reader[1]);

                        purchased_Stock.ProductId = Convert.ToInt32(reader[2].ToString());

                        purchased_Stock.ProductName = reader[3].ToString();

                        purchased_Stock.ProductGradeId = Convert.ToInt32(reader[4].ToString());

                        purchased_Stock.ProductGrade = reader[5].ToString();

                        purchased_Stock.PurchaseStockLocation = reader[6].ToString();

                        purchased_Stock.Bags = Convert.ToDecimal(reader[7].ToString());

                        purchased_Stock.BagPerKg = Convert.ToDecimal(reader[8].ToString());

                        purchased_Stock.TotalWeight = Convert.ToDecimal(reader[9].ToString());

                        purchased_Stock.ProductPrice = Convert.ToDecimal(reader[10].ToString());

                        purchased_Stock.TotalPrice = Convert.ToDecimal(reader[11].ToString());

                        purchased_Stock.VehicleId = Convert.ToInt32(reader[12].ToString());

                        purchased_Stock.VehicleName = reader[13].ToString();

                        purchased_Stock.VehicleNo = reader[14].ToString();

                        purchased_Stock.DriverName = reader[15].ToString();

                        purchased_Stock.TolatName = reader[16].ToString();

                        purchased_Stock.PaymentStatus = reader[17].ToString();

                        List_Of_Purchased_Stock.Add(purchased_Stock);

                    }

                    customerDetails_With_Purchased_Stock.Purchased_Stocks = List_Of_Purchased_Stock;
                }




            }

            return customerDetails_With_Purchased_Stock;
        }


        #endregion

        #region INSERT

        [HttpPost]
        public bool CREATE_CUSTOMER(Customer_Model customers)
        {


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

        #region DELETE 


        public bool Delete_Customer(int Customer_ID)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_CUSTOMER_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);
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


        public List<Customer_Model> CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
        {

            List<Customer_Model> List_Of_Customers = new List<Customer_Model>();

            DbCommand dbCommand = Command_Name("API_CUSTOMER_EXIST_IN_SYSTEM");


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



        public Customer_Model Customer_Info_By_PK(int Customer_ID)
        {
            Customer_Model Customer_Info_By_PK = new Customer_Model();

            using (DbCommand dbCommand = Command_Name("API_CUSTOMER_BY_PK"))
            {
                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_ID", SqlDbType.Int, Customer_ID);



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
