using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// IAuthDAL interface for handling user authentication in the data access layer.
    /// </summary>
    public interface IAuthDAL
    {
        /// <summary>
        /// Authenticates a user based on the provided username and password.
        /// </summary>
        /// <param name="auth_info">The Auth_Model object containing the username and password.</param>
        /// <returns>A User_Model object containing user information if authentication is successful, otherwise null.</returns>
        User_Model AuthUser(Auth_Model auth_info);
    }
}