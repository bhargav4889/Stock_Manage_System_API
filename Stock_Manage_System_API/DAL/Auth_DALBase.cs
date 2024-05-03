using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Auth_DALBase : DAL_Helpers ,IAuthDAL
    {
        private SqlDatabase sqlDatabase;

        public Auth_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        public User_Model Auth_User(Auth_Model auth_details)
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


    }
}
