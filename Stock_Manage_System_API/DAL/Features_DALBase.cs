using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Features_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Features_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        public List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_List()
        {
            List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> _Customers_Payment_Sort_List = new List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List>();



            DbCommand dbCommand = Command_Name("API_PENDING_PAYMENTS_CUSTOMERS_LIST_DASHBAORD");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List customers_Payment_Sort_List = new Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List();
                    customers_Payment_Sort_List.StockId = Convert.ToInt32(reader["STOCK_ID"].ToString());
                    customers_Payment_Sort_List.StockDate = Convert.ToDateTime(reader["STOCK_DATE"].ToString());
                    customers_Payment_Sort_List.CustomerId = Convert.ToInt32(reader["CUSTOMER_ID"].ToString());
                    customers_Payment_Sort_List.CustomerName = reader["CUSTOMER_NAME"].ToString();
                    customers_Payment_Sort_List.CustomerType = reader["CUSTOMER_TYPE"].ToString();
                    customers_Payment_Sort_List.ProductId = Convert.ToInt32(reader["PRODUCT_ID"].ToString());
                    customers_Payment_Sort_List.ProductName = reader["PRODUCT_NAME"].ToString();
                    customers_Payment_Sort_List.TotalPrice = Convert.ToDecimal(reader["TOTAL_PRICE"].ToString());



                    _Customers_Payment_Sort_List.Add(customers_Payment_Sort_List);


                }

                return _Customers_Payment_Sort_List;
            }



        }
    }
}
