using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Email_Services;

namespace Stock_Manage_System_API.ResetPassword_Service
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IAuthBAL _authBAL;
        private readonly IEmailSender _emailSender;

        public PasswordResetService(IAuthBAL authBAL, IEmailSender emailSender)
        {
            _authBAL = authBAL;
            _emailSender = emailSender;
        }

        public async Task<bool> RequestPasswordResetAsync(string email)
        {
            var user = _authBAL.GetAuthUserByEmail(email);
            if (user == null)
            {
                return false; // User not found
            }

            var token = Guid.NewGuid().ToString(); // Generate a unique token
            _authBAL.SavePasswordResetToken(email, token);
            var resetLink = $"https://shree-ganesh-agro-management.somee.com/Auth/ChangePassword?token={token}&email={email}";

            var message = $"<p>Hello,{user.Username}<br/>Please reset your password by clicking <a href='{resetLink}'>here</a>.</p>";
            await _emailSender.SendEmailAsync(email, "Password Reset", message);
            return true; // User found and email sent
        }



        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            if (!_authBAL.ValidatePasswordResetToken(email, token))
            {
                return false;
            }

            var result = _authBAL.ChangePassword(email, newPassword);
            return result;
        }
    }
}
