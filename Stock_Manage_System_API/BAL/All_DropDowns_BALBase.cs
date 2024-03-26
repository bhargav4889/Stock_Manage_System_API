using Stock_Manage_System_API.Models;
using Stock_Manage_System_API.DAL;
namespace Stock_Manage_System_API.BAL
{
    public class All_DropDowns_BALBase
    {

        private readonly All_DropDowns_DALBase all_DropDowns_DALBase = new All_DropDowns_DALBase();

        public All_DropDowns_Model GET_ALL_DROPDOWNS()
        {
            All_DropDowns_Model all_DropDowns_Model = new All_DropDowns_Model();

            all_DropDowns_Model = all_DropDowns_DALBase.GET_ALL_DROPDOWNS();

            return all_DropDowns_Model;

        }

    }
}
