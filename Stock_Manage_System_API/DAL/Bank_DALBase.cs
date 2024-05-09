using Azure.Core;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// Base class for the Bank Data Access Layer.
    /// </summary>
    public class Bank_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bank_DALBase"/> class.
        /// </summary>
        public Bank_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Creates a new DbCommand instance for the specified stored procedure name.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A new DbCommand instance for the specified stored procedure name.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        /// <summary>
        /// Retrieves a list of bank names from the database.
        /// </summary>
        /// <returns>A list of Bank_Model objects containing bank names.</returns>
        public List<Bank_Model> GetBanksList()
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

        /// <summary>
        /// Retrieves a list of our banks dropdown data from the database.
        /// </summary>
        /// <returns>A list of Our_Banks_Dropdown objects containing our banks dropdown data.</returns>
        public List<Our_Banks_Dropdown> GetOurBanksSelectList()
        {
            DbCommand dbCommand = Command_Name("API_DROPDOWN_FOR_OUR_BANKS");

            using (IDataReader row = sqlDatabase.ExecuteReader(dbCommand))
            {
                List<Our_Banks_Dropdown> List_Of_Our_Bank_Names = new List<Our_Banks_Dropdown>();

                while (row.Read())
                {
                    Our_Banks_Dropdown our_Banks_Dropdown = new Our_Banks_Dropdown();

                    our_Banks_Dropdown.InformationId = Convert.ToInt32(row[0].ToString());

                    our_Banks_Dropdown.BankId = Convert.ToInt32(row[1].ToString());

                    our_Banks_Dropdown.BankIcon = (row[2].ToString());
                    our_Banks_Dropdown.AccountNo = (row[3].ToString());

                    List_Of_Our_Bank_Names.Add(our_Banks_Dropdown);
                }

                return List_Of_Our_Bank_Names;
            }
        }
    }
}