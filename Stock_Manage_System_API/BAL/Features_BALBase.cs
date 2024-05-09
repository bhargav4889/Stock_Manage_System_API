using Stock_Manage_System_API.DAL;

using Stock_Manage_System_API.Models;

/// <summary>
/// Represents the base class for managing features in the business access layer.
/// </summary>
namespace Stock_Manage_System_API.BAL
{
    public class Features_BALBase
    {
       
        Features_DALBase Dashbaord_Features_DALBase = new Features_DALBase();


        /// <summary>
        /// Retrieves a list of pending customers payment sort list.
        /// </summary>
        /// <returns>A list of <see cref="Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List"/> objects.</returns>

        #region Method : Sort List of Pending Customer Payments Recent 
        public List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> PendingCustomersPaymentSortList()
        {
            List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_Lists = Dashbaord_Features_DALBase.PendingCustomersPaymentSortList();

            return pending_Customers_Payment_Sort_Lists;
        }

        #endregion
    }
}