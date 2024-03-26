using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Bank_BALBase
    {
        private readonly Bank_DALBase images_DALBase = new Bank_DALBase();

        public List<Bank_Model> Get_Bank_Names()
        {
            List<Bank_Model> bank_Models = images_DALBase.Get_Bank_Names();

            return bank_Models;
        }
    }
}
