using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// AuthDALBase class for handling database operations related to user authentication.
    /// </summary>
    public class Auth_DALBase : DAL_Helpers, IAuthDAL
    {
        #region Section: SetUp Of Database Connection and Initialization

        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the Payment_DALBase class, setting up the database connection.
        /// </summary>
        public Auth_DALBase()
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

        #region Section : Auth User Check

        /// <summary>
        /// Authenticates a user based on the provided username and password.
        /// </summary>
        /// <param name="auth_details">The Auth_Model object containing the username and password.</param>
        /// <returns>A User_Model object containing user information if authentication is successful, otherwise null.</returns>
        public User_Model AuthUser(Auth_Model auth_details)
        {
            DbCommand dbCommand = Command_Name("API_CHECK_AUTH_DETAILS");
            sqlDatabase.AddInParameter(dbCommand, "@Auth_Username", SqlDbType.VarChar, auth_details.Username);
            sqlDatabase.AddInParameter(dbCommand, "@Auth_Password", SqlDbType.VarChar, auth_details.Password);

            User_Model user_info = new User_Model();

            using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    user_info.Userid = Convert.ToInt32(dataReader["AUTH_ID"].ToString());
                    user_info.Username = dataReader["AUTH_NAME"].ToString();
                    user_info.Emailaddress = dataReader["AUTH_EMAIL"].ToString();
                    user_info.Password = dataReader["AUTH_PASSWORD"].ToString();
                    user_info.Phoneno = dataReader["AUTH_PHONE_NO"].ToString();
                }
            }

            return user_info;
        }


        public User_Model GetAuthUserByEmail(string email)
        {
            DbCommand dbCommand = Command_Name("API_GET_AUTH_BY_EMAIL");
            sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, email);

            User_Model user_info = null;

            using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    user_info = new User_Model
                    {
                        Userid = Convert.ToInt32(dataReader["AUTH_ID"]),
                        Username = dataReader["AUTH_NAME"].ToString(),
                        Emailaddress = dataReader["AUTH_EMAIL"].ToString()
                    };
                }
            }

            return user_info;
        }




        #endregion



        public bool SavePasswordResetToken(string email, string token)
        {
            DbCommand dbCommand = Command_Name("API_SAVE_PASSWORD_RESET_TOKEN");
            sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, email);
            sqlDatabase.AddInParameter(dbCommand, "@Token", SqlDbType.VarChar, token);

            // Convert current UTC time to Indian Standard Time (UTC+5:30)
            TimeZoneInfo indianZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime currentTimeUtc = DateTime.UtcNow;
            DateTime currentTimeIst = TimeZoneInfo.ConvertTimeFromUtc(currentTimeUtc, indianZone);

            // Set expiration time to 10 minutes from current IST time
            DateTime expirationTimeIst = currentTimeIst.AddMinutes(10);
            sqlDatabase.AddInParameter(dbCommand, "@Expiration", SqlDbType.DateTime, expirationTimeIst);

            int result = sqlDatabase.ExecuteNonQuery(dbCommand);
            return result > 0;
        }




        public bool ValidatePasswordResetToken(string email, string token)
        {
            using (DbCommand dbCommand = Command_Name("API_VALIDATE_PASSWORD_RESET_TOKEN"))
            {
                sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, email);
                sqlDatabase.AddInParameter(dbCommand, "@Token", SqlDbType.VarChar, token);

                using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        DateTime expiration = dataReader.GetDateTime(dataReader.GetOrdinal("EXPIRATION"));
                       
                        return expiration > DateTime.UtcNow;
                    }
                }
            }
            return false;
        }
    


        public bool ChangePassword(string email, string newPassword)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_AUTH_CHANGE_PASSWORD");
                sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, email);
                sqlDatabase.AddInParameter(dbCommand, "@NewPassword", SqlDbType.VarChar, newPassword);

                int rowsAffected = sqlDatabase.ExecuteNonQuery(dbCommand);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                throw new Exception("Error changing password", ex);
            }
        }


        public bool DeleteToken(string email, string token)
        {
            using (DbCommand dbCommand = sqlDatabase.GetStoredProcCommand("API_DELETE_TOKEN"))
            {
                sqlDatabase.AddInParameter(dbCommand, "@Email", SqlDbType.VarChar, email);
                sqlDatabase.AddInParameter(dbCommand, "@Token", SqlDbType.VarChar, token);

                int result = sqlDatabase.ExecuteNonQuery(dbCommand);
                return result > 0;
            }
        }

       
    }
}