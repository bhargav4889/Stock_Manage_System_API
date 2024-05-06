using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Features_BALBase
    {
        #region

        Features_DALBase Dashbaord_Features_DALBase = new Features_DALBase();

        #endregion


        public List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_List()
        {
            List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_Lists = Dashbaord_Features_DALBase.pending_Customers_Payment_Sort_List();

            return pending_Customers_Payment_Sort_Lists;
        }



    }
}
