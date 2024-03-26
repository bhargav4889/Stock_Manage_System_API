using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Recent_Actions_BALBase
    {
        Recent_Actions_DALBase Recent_Actions_DAL = new Recent_Actions_DALBase();

        public List<Recent_Action_Model> Recent_Actions()
        {
            List<Recent_Action_Model> recent_Actions = Recent_Actions_DAL.Recent_Actions();

            return recent_Actions;
        }

    }
}
