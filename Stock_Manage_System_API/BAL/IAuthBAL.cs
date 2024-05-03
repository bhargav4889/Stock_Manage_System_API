using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public interface IAuthBAL
    {
        User_Model Auth_Login_Details(Auth_Model authInfo);
    }
}
