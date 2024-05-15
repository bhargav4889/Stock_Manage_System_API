using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    /// <summary>
    /// IAuthBAL interface for handling user authentication in the business logic layer.
    /// </summary>
    public interface IAuthBAL
    {
        /// <summary>
        /// Authenticates a user based on the provided username and password.
        /// </summary>
        /// <param name="authInfo">The Auth_Model object containing the username and password.</param>
        /// <returns>A User_Model object containing user information if authentication is successful, otherwise null.</returns>
        User_Model AuthLoginDetails(Auth_Model authInfo);

        User_Model GetAuthUserByEmail(string email);
        bool SavePasswordResetToken(string email, string token);
        bool ValidatePasswordResetToken(string email, string token);
        bool ChangePassword(string email, string newPassword);

        bool ValidateAndDeleteToken(string email, string token);

    }
}