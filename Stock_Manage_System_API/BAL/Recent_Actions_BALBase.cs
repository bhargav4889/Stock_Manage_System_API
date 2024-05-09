/// <summary>
/// Represents the base class for managing recent actions in the business access layer.
/// </summary>
/// <remarks>
/// This class contains a single method <see cref="Recent_Actions()"/> which returns a list of
/// <see cref="Recent_Action_Model"/> objects representing the recent actions.
/// </remarks>
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Recent_Actions_BALBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recent_Actions_BALBase"/> class.
        /// </summary>
        /// <remarks>
        /// The <see cref="Recent_Actions_DAL"/> property is initialized to a new instance of
        /// <see cref="Recent_Actions_DALBase"/>.
        /// </remarks>
        public Recent_Actions_BALBase()
        {
            Recent_Actions_DAL = new Recent_Actions_DALBase();
        }

        /// <summary>
        /// Gets or sets the <see cref="Recent_Actions_DALBase"/> object used to retrieve recent actions
        /// from the data access layer.
        /// </summary>
        private Recent_Actions_DALBase Recent_Actions_DAL = new Recent_Actions_DALBase();

        /// <summary>
        /// Retrieves the recent actions.
        /// </summary>
        /// <returns>A list of <see cref="Recent_Action_Model"/> objects containing the recent actions.</returns>
        /// <remarks>
        /// This method uses the <see cref="Recent_Actions_DAL"/> property to retrieve the recent actions
        /// from the data access layer.
        /// </remarks>
        /// 
        #region Method : Get Recent Actions
        public List<Recent_Action_Model> GetRecentActions()
        {
            List<Recent_Action_Model> recent_Actions = Recent_Actions_DAL.GetRecentActions();

            return recent_Actions;
        }

        #endregion
    }
}