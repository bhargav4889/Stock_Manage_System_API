using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;

using System.Data;
using System.Data.Common;

/// <summary>
/// Represents the base class for managing features in the data access layer.
/// </summary>
namespace Stock_Manage_System_API.DAL
{
    public class Features_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Features_DALBase"/> class.
        /// </summary>
        public Features_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        /// <summary>
        /// Retrieves a list of pending customers payment sort list.
        /// </summary>
        /// <returns>A list of <see cref="Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List"/> objects.</returns>


        #region Method : Sort List of Pending Customer Payments Recent 

        public List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> PendingCustomersPaymentSortList()
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

        #endregion

        #region Method : Upcoming Reminders Show

        public List<Dashbaord_Features_Model.Upcoming_Reminders_Model> UpcomingRemindersList()
        {
            List<Dashbaord_Features_Model.Upcoming_Reminders_Model> _Upcoming_ReminderList = new List<Dashbaord_Features_Model.Upcoming_Reminders_Model>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_UPCOMING_REMINDER");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Dashbaord_Features_Model.Upcoming_Reminders_Model _Upcoming_Reminder = new Dashbaord_Features_Model.Upcoming_Reminders_Model();

                    _Upcoming_Reminder.ReminderId = Convert.ToInt32(reader["REMINDER_ID"].ToString());
                    _Upcoming_Reminder.ReminderDateTime = Convert.ToDateTime(reader["REMINDER_DATETIME"].ToString());
                    _Upcoming_Reminder.ReminderType = reader["REMINDER_TYPE"].ToString();
                    _Upcoming_Reminder.ReminderCustomType = reader["REMINDER_CUSTOM_TYPE"].ToString();


                    _Upcoming_ReminderList.Add(_Upcoming_Reminder);
                }

                return _Upcoming_ReminderList;
            }
        }

        #endregion

    }
}