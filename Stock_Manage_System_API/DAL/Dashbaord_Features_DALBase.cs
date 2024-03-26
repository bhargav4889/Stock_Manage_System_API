using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Dashbaord_Features_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Dashbaord_Features_DALBase()
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
                    customers_Payment_Sort_List.StockId = Convert.ToInt32(reader[0].ToString());
                    customers_Payment_Sort_List.StockDate = Convert.ToDateTime(reader[1].ToString());
                    customers_Payment_Sort_List.CustomerId = Convert.ToInt32(reader[2].ToString());
                    customers_Payment_Sort_List.CustomerName = reader[3].ToString();
                    customers_Payment_Sort_List.ProductId = Convert.ToInt32(reader[4].ToString());
                    customers_Payment_Sort_List.ProductName = reader[5].ToString();
                    customers_Payment_Sort_List.TotalPrice = Convert.ToDecimal(reader[6].ToString());

                    _Customers_Payment_Sort_List.Add(customers_Payment_Sort_List);


                }

                return _Customers_Payment_Sort_List;
            }



        }
    }
}
