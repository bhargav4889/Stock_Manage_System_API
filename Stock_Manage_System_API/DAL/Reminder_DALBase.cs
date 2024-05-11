/// <summary>
/// Represents the base class for managing reminders in the data access layer.
/// </summary>
/// <remarks>
/// This class uses the Microsoft Enterprise Library Data Access Application Block to execute SQL commands and stored procedures.
/// </remarks>
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Reminder_DALBase : DAL_Helpers
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder_DALBase"/> class.
        /// </summary>
        /// <remarks>
        /// The <see cref="sqlDatabase"/> field is initialized to a new instance of <see cref="SqlDatabase"/> using the <see cref="Database_Connection"/> string.
        /// </remarks>
        public Reminder_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Gets or sets the <see cref="SqlDatabase"/> object used to execute SQL commands and stored procedures.
        /// </summary>
        private SqlDatabase sqlDatabase { get; set; }

        /// <summary>
        /// Creates a new <see cref="DbCommand"/> object for the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A new <see cref="DbCommand"/> object for the specified stored procedure.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #region Method : Insert Reminder

        /// <summary>
        /// Inserts a new reminder into the database.
        /// </summary>
        /// <param name="reminder_info">The reminder information to insert.</param>
        /// <returns><c>true</c> if the reminder was inserted successfully, otherwise <c>false</c>.</returns>

        public bool InsertReminder(Reminder_Model reminder_info)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_REMINDER_INSERT");

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_DATE_TIME", SqlDbType.DateTime, reminder_info.ReminderDateTime);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_TYPE", SqlDbType.VarChar, reminder_info.ReminderType);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_CUSTOM_TYPE", SqlDbType.VarChar, reminder_info.ReminderCustomType);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_DESCRIPTION", SqlDbType.VarChar, reminder_info.ReminderDescription);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_SET_EMAIL_ADDRESS", SqlDbType.VarChar, reminder_info.SentEmailAddress);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_SET_MOBILE_NO", SqlDbType.VarChar, reminder_info.SentPhoneNo);

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

        #region Method : Update Reminder

        /// <summary>
        /// Updates an existing reminder in the database.
        /// </summary>
        /// <param name="reminder_info">The reminder information to update.</param>
        /// <returns><c>true</c> if the reminder was updated successfully, otherwise <c>false</c>.</returns>
        public bool UpdateReminder(Reminder_Model reminder_info)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_REMINDER_UPDATE");

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_ID", SqlDbType.Int, reminder_info.ReminderId);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_DATE_TIME", SqlDbType.DateTime, reminder_info.ReminderDateTime);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_TYPE", SqlDbType.VarChar, reminder_info.ReminderType);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_CUSTOM_TYPE", SqlDbType.VarChar, reminder_info.ReminderCustomType);

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_DESCRIPTION", SqlDbType.VarChar, reminder_info.ReminderDescription);

    

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


        #region Method : Get All Reminders

        /// <summary>
        /// Retrieves a list of all reminders in the database.
        /// </summary>
        /// <returns>A list of <see cref="Reminder_Model"/> objects containing all reminders in the database.</returns>
        public List<Reminder_Model> GetReminders()
        {
            List<Reminder_Model> reminders = new List<Reminder_Model>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_REMINDER");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    // Move the instantiation inside the loop
                    Reminder_Model reminder = new Reminder_Model
                    {
                        ReminderId = Convert.ToInt32(reader["REMINDER_ID"]),
                        ReminderDateTime = Convert.ToDateTime(reader["REMINDER_DATE_TIME"]),
                        ReminderType = reader["REMINDER_TYPE"].ToString(),
                        ReminderCustomType = reader["REMINDER_CUSTOM_TYPE"].ToString(),
                        ReminderDescription = reader["REMINDER_DESCRIPTION"].ToString(),
                        SentEmailAddress = reader["EMAIL_ADDRESS"].ToString(),
                        SentPhoneNo = reader["PHONE_NO"].ToString()
                    };

                    reminders.Add(reminder);
                }
            }

            return reminders;
        }


        #endregion

        #region Method : Delete Reminder

        /// <summary>
        /// Deletes a reminder from the database.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to delete.</param>
        /// <returns><c>true</c> if the reminder was deleted successfully, otherwise <c>false</c>.</returns>
        public bool DeleteReminder(int Reminder_ID)
        {

            try
            {
                DbCommand dbCommand = Command_Name("API_REMINDER_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_ID", SqlDbType.Int, Reminder_ID);

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

        #region Method : Get Reminder By ID 

        /// <summary>
        /// Retrieves a single reminder from the database by its ID.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to retrieve.</param>
        /// <returns>A <see cref="Reminder_Model"/> object containing the reminder information.</returns>
        public Reminder_Model GetReminderByID(int Reminder_ID)
        {

            Reminder_Model reminder = new Reminder_Model();

            DbCommand dbCommand = Command_Name("API_REMINDER_BY_PK");

            sqlDatabase.AddInParameter(dbCommand, "@REMINDER_ID", SqlDbType.Int, Reminder_ID);


            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {

                    reminder.ReminderId = Convert.ToInt32(reader["REMINDER_ID"].ToString());

                    reminder.ReminderDateTime = Convert.ToDateTime(reader["REMINDER_DATE_TIME"].ToString());

                    reminder.ReminderType = reader["REMINDER_TYPE"].ToString();

                    reminder.ReminderCustomType = reader["REMINDER_CUSTOM_TYPE"].ToString();

                    reminder.ReminderDescription = reader["REMINDER_DESCRIPTION"].ToString();

                    reminder.SentEmailAddress = reader["EMAIL_ADDRESS"].ToString();

                    reminder.SentPhoneNo = reader["PHONE_NO"].ToString();

                }
            }

            return reminder;

        }
        #endregion
    }
}