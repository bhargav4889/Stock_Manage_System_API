using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Bank_BALBase
    {
        private readonly Bank_DALBase bank_DALBase = new Bank_DALBase();

        public List<Bank_Model> Get_Bank_Names()
        {
            List<Bank_Model> bank_Models = bank_DALBase.Get_Bank_Names();

            return bank_Models;
        }

        public List<Our_Banks_Dropdown> Our_Banks_Dropdowns()
        {
            List<Our_Banks_Dropdown> our_Banks_Dropdowns = bank_DALBase.Our_Banks_Dropdowns();

            return our_Banks_Dropdowns;
        }
    }
}
