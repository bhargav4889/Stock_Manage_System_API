using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.DAL
{
    public interface IAuthDAL
    {
        User_Model Auth_User(Auth_Model auth_info);
    }
}
