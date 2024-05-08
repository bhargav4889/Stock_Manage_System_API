using Stock_Manage_System_API.Models;
using Stock_Manage_System_API.DAL;
namespace Stock_Manage_System_API.BAL
{
    public class DropDowns_BALBase
    {
        #region DAL Initialization

        /// <summary>
        /// Initializes an instance of DropDownsDALBase for data access operations.
        /// </summary>

        private readonly DropDowns_DALBase _dropDownsDALBase = new DropDowns_DALBase();

        #endregion

        #region Dropdown Retrieval

        /// <summary>
        /// Retrieves all dropdown options from the data layer asynchronously.
        /// </summary>
        /// <returns>
        /// A model containing lists of dropdown data for products, product grades, and vehicles.
        /// </returns>

        public DropDowns_Model GetAllDropdownsAsync()
        {
            DropDowns_Model dropDownsModel = _dropDownsDALBase.GetAllDropdownsAsync();

            return dropDownsModel;
        }

        #endregion

    }
}