using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Email_Services;
using Stock_Manage_System_API.Login_Service;
using Stock_Manage_System_API.Models;
using Stock_Manage_System_API.ResetPassword_Service;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// The AuthController class for handling user authentication.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthBAL _authBAL;
        private readonly JWT_Service _jwtService;
        private readonly IPasswordResetService _passwordResetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authBAL">The instance of the IAuthBAL interface.</param>
        /// <param name="jwtService">The instance of the JWT_Service class.</param>
        public AuthController(IAuthBAL authBAL, JWT_Service jwtService, IPasswordResetService passwordResetService)
        {
            _authBAL = authBAL;
            _jwtService = jwtService;
            _passwordResetService = passwordResetService;
        }

        /// <summary>
        /// Authenticates a user based on the provided username and password.
        /// </summary>
        /// <param name="authInfo">The Auth_Model object containing the username and password.</param>
        /// <returns>An IActionResult object with the authentication result.</returns>
        [HttpPost("Login")]
        public IActionResult Login(Auth_Model authInfo)
        {
            var user = _authBAL.AuthLoginDetails(authInfo);
            if (user != null && user.Userid > 0)
            {
                user.Token = _jwtService.GenerateJWTToken(user);
                return Ok(new { success = true, user = user });
            }
            return Unauthorized(new { success = false, message = "Invalid credentials" });
        }

        [HttpGet("request-reset")]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            var result = await _passwordResetService.RequestPasswordResetAsync(email);
            if (result)
            {
                return Ok(new { message = "A password reset link will be sent if the account with that email exists." });
            }
            else
            {
                return NotFound(new { message = "Email Address or Account Not Registered !." });
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(Reset_PasswordModel request)
        {
            // Validate the token
            bool isValidToken = _authBAL.ValidatePasswordResetToken(request.Email, request.Token);
            if (!isValidToken)
            {
                return BadRequest(new { success = false, message = "Invalid or expired token." });
            }

            // Attempt to change the password
            bool passwordChangeResult = _authBAL.ChangePassword(request.Email, request.NewPassword);
            if (passwordChangeResult)
            {
                // Delete the token after successful password change
                bool tokenDeletionResult = _authBAL.ValidateAndDeleteToken(request.Email, request.Token);
                if (tokenDeletionResult)
                {
                    return Ok(new { success = true, message = "Password has been reset successfully." });
                }
                else
                {
                    // Handle the scenario where the token couldn't be deleted (unlikely case)
                    return BadRequest(new { success = false, message = "Failed to invalidate the reset token." });
                }
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to reset password." });
            }
        }

        [HttpGet("validate-token")]
        public IActionResult ValidateToken(string email, string token)
        {
            bool isValidToken = _authBAL.ValidatePasswordResetToken(email, token);
            if (isValidToken)
            {
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest(new { success = false, message = "Invalid or expired token." });
            }
        }



    }
}