using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Login_Service;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Auth_BALBase : IAuthBAL
    {
        private readonly IAuthDAL _authDAL;

        public Auth_BALBase(IAuthDAL authDAL)
        {
            _authDAL = authDAL;
        }

        public User_Model Auth_Login_Details(Auth_Model authInfo)
        {
            return _authDAL.Auth_User(authInfo);
        }
    }

}
