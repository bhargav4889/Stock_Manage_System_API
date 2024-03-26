using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class All_Counts_BALBase
    {
        private readonly All_Counts_DALBase All_Counts_DALBase = new All_Counts_DALBase();

        public All_Counts_Model ALL_COUNTS()
        {
            All_Counts_Model all_Counts_Model = All_Counts_DALBase.ALL_COUNTS();


            return all_Counts_Model;
        }




    }
}
