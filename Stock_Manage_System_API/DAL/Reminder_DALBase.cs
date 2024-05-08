using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Reminder_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Reminder_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }


        public bool Insert_Reminder(Reminder_Model reminder_info)
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


        public bool Update_Reminder(Reminder_Model reminder_info)
        {

            try
            {
                DbCommand dbCommand = Command_Name("API_REMINDER_UPDATE");

                sqlDatabase.AddInParameter(dbCommand, "@REMINDER_ID", SqlDbType.Int, reminder_info.ReminderId);

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



        public List<Reminder_Model> Reminders()
        {
            List<Reminder_Model> reminders = new List<Reminder_Model>(); 

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_REMINDER");

            Reminder_Model reminder = new Reminder_Model();

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {

                    reminder.ReminderId = Convert.ToInt32(reader["REMINDER_ID"].ToString());

                    reminder.ReminderDateTime = Convert.ToDateTime(reader["REMINDER_DATE_TIME"].ToString());

                    reminder.ReminderType = reader["REMINDER_TYPE"].ToString();

                    reminder.ReminderCustomType = reader["REMINDER_CUSTOM_TYPE"].ToString();

                    reminder.ReminderDescription = reader["REMINDER_DESCRIPTION"].ToString();

                    reminder.SentEmailAddress = reader["EMAIL_ADDRESS"].ToString();

                    reminder.SentPhoneNo = reader["PHONE_NO"].ToString();

                    reminders.Add(reminder);    

                }
            }

            return reminders;
        }


        public bool Delete_Reminder(int Reminder_ID)
        {

            try
            {
                DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_REMINDER");
                sqlDatabase.AddInParameter(dbCommand,"@REMINDER_ID",SqlDbType.Int,Reminder_ID);

                if(Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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


        public Reminder_Model Get_Reminder_By_ID(int Reminder_ID)
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




    }
}
