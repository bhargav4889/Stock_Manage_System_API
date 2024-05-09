/// <summary>
/// Represents the base class for managing recent actions in the data access layer.
/// </summary>
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Recent_Actions_DALBase : DAL_Helpers
    {
        /// <summary>
        /// Retrieves the recent actions.
        /// </summary>
        /// <returns>A list of <see cref="Recent_Action_Model"/> objects containing the recent actions.</returns>
        /// 
        #region Method : Get Recent Actions
        public List<Recent_Action_Model> GetRecentActions()
        {
            List<Recent_Action_Model> recent_Actions = new List<Recent_Action_Model>();

            SqlDatabase database = new SqlDatabase(Database_Connection);

            DbCommand dbCommand = database.GetStoredProcCommand("API_DISPLAY_RECENT_ACTIONS");

            using (IDataReader reader = database.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Recent_Action_Model recent_Action_ = new Recent_Action_Model();

                    recent_Action_.Rec_Act_Id = Convert.ToInt32(reader[0].ToString());

                    recent_Action_.Rec_Act_Table_Name = (reader[1].ToString());

                    recent_Action_.Rec_Act_Info = (reader[2].ToString());

                    recent_Action_.Rec_Act_Create = Convert.ToDateTime(reader[3].ToString());

                    recent_Actions.Add(recent_Action_);

                }

                return recent_Actions;
            }
            #endregion
        }
    }
}