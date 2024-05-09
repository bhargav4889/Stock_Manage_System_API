/// <summary>
/// Represents the base class for managing information in the data access layer.
/// </summary>
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Information_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Information_DALBase"/> class.
        /// </summary>
        public Information_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Creates a database command for the specified stored procedure name.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A <see cref="DbCommand"/> object for the specified stored procedure.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #region Method : Bank Information Insert

        /// <summary>
        /// Inserts a new bank information.
        /// </summary>
        /// <param name="information_Model">The <see cref="Information_Model"/> object containing the bank information to insert.</param>
        /// <returns><c>true</c> if the bank information was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertBankInformation(Information_Model information_Model)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_SAVE_INFORMATION_INSERT");

                sqlDatabase.AddInParameter(dbCommand, "@BANK_ID", SqlDbType.Int, information_Model.BankId);

                sqlDatabase.AddInParameter(dbCommand, "@Account_No", SqlDbType.NVarChar, information_Model.AccountNo);

                sqlDatabase.AddInParameter(dbCommand, "@IFSC_Code", SqlDbType.NVarChar, information_Model.ifsc_code);

                sqlDatabase.AddInParameter(dbCommand, "@Account_Holder_Name", SqlDbType.NVarChar, information_Model.AccountHolderName);

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

        #region Method : Show All Informations 

        /// <summary>
        /// Retrieves all saved information.
        /// </summary>
        /// <returns>A list of <see cref="Information_Model"/> objects containing all saved information.</returns>
        public List<Information_Model> GetAllSaveInformation()
        {
            List<Information_Model> List_Of_Save_Informations = new List<Information_Model>();

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_SAVE_INFORMATION");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    Information_Model information_Model = new Information_Model();  // Move instantiation inside the loop
                    information_Model.InformationID = Convert.ToInt32(reader[0]);
                    information_Model.AccountHolderName = reader[1].ToString();
                    information_Model.BankName = reader[2].ToString();
                    information_Model.BankIcon = reader[3].ToString();

                    List_Of_Save_Informations.Add(information_Model);
                }
            }

            return List_Of_Save_Informations;
        }

        #endregion


        #region Method : Information Show By ID 

        /// <summary>
        /// Retrieves the saved information with the specified information ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the saved information to retrieve.</param>
        /// <returns>A <see cref="Information_Model"/> object containing the saved information with the specified information ID.</returns>
        public Information_Model InformationByID(int Information_ID)
        {
            Information_Model information_Model = new Information_Model();

            DbCommand dbCommand = Command_Name("API_DISPLAY_SAVE_INFORMATION_BY_ID");

            sqlDatabase.AddInParameter(dbCommand, "@Information_ID", SqlDbType.Int, Information_ID);

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                if (reader.Read())
                {
                    information_Model.InformationID = Convert.ToInt32(reader[0]);
                    information_Model.AccountNo = reader[1].ToString();
                    information_Model.ifsc_code = reader[2].ToString();
                    information_Model.AccountHolderName = reader[3].ToString();
                    information_Model.BankId = Convert.ToInt32(reader[4].ToString());
                    information_Model.BankName = reader[5].ToString();
                    information_Model.BankIcon = reader[6].ToString();
                }
            }


            return information_Model;
        }

        #endregion



        #region Method : Delete Information
        /// <summary>
        /// Deletes the saved information with the specified information ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the saved information to delete.</param>
        /// <returns><c>true</c> if the saved information was deleted successfully; otherwise, <c>false</c>.</returns>
        public bool DeleteInformation(int Information_ID)
        {
            DbCommand dbCommand = Command_Name("API_DISPLAY_SAVE_INFORMATION_BY_ID");

            sqlDatabase.AddInParameter(dbCommand, "@Information_ID", SqlDbType.Int, Information_ID);

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