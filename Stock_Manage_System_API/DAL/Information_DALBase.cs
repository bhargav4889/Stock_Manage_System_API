using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Information_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public Information_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }


        #region Method : Bank Information Insert

        public bool Insert_Bank_Infromation(Information_Model information_Model)
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


        public List<Information_Model> Show_All_Save_Informations()
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

        public Information_Model Information_Model(int Information_ID)
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


        public bool Delete_Save_Information(int Information_ID)
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

    }
}
