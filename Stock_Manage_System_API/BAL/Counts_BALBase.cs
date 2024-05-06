using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Counts_BALBase
    {
        #region DAL Class Instance

        private readonly Counts_DALBase allCountsDALBase = new Counts_DALBase();

        #endregion


        #region Method : All Counts 
        public AllCountsModel ALL_COUNTS()
        {
            AllCountsModel allCountsModel = allCountsDALBase.ALL_COUNTS();


            return allCountsModel;
        }

        #endregion


    }
}
