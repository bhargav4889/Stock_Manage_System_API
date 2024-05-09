using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    /// <summary>
    /// Base class for the Bank Business Access Layer.
    /// </summary>
    public class Bank_BALBase
    {
        private readonly Bank_DALBase _bank_DAL = new Bank_DALBase();

        /// <summary>
        /// Retrieves a list of bank names from the database.
        /// </summary>
        /// <returns>A list of Bank_Model objects containing bank names.</returns>
        public List<Bank_Model> GetBanksList()
        {
            List<Bank_Model> bank_Models = _bank_DAL.GetBanksList();

            return bank_Models;
        }

        /// <summary>
        /// Retrieves a list of our banks dropdown data from the database.
        /// </summary>
        /// <returns>A list of Our_Banks_Dropdown objects containing our banks dropdown data.</returns>
        public List<Our_Banks_Dropdown> GetOurBanksSelectList()
        {
            List<Our_Banks_Dropdown> our_Banks_Dropdowns = _bank_DAL.GetOurBanksSelectList();

            return our_Banks_Dropdowns;
        }
    }
}