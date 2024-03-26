using Azure.Core;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class Bank_DALBase: DAL_Helpers
    {

        private SqlDatabase sqlDatabase;

        public Bank_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        public List<Bank_Model> Get_Bank_Names()
        {

            
            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_BANKS");

            using (IDataReader row = sqlDatabase.ExecuteReader(dbCommand))
            {
                List<Bank_Model> List_Of_Bank_Names = new List<Bank_Model>();


                while (row.Read())
                {
                    Bank_Model bank_Model = new Bank_Model();

                    bank_Model.BankId = Convert.ToInt32(row[0].ToString());

                    bank_Model.BankName = row[1].ToString();

                    bank_Model.BankIcon = row[2].ToString();

                    List_Of_Bank_Names.Add(bank_Model);

                    

                }

                return List_Of_Bank_Names;
            }

            
        }
    }
}
