using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Login_Service;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// The AuthController class for handling user authentication.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBAL _authBAL;
        private readonly JWT_Service _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authBAL">The instance of the IAuthBAL interface.</param>
        /// <param name="jwtService">The instance of the JWT_Service class.</param>
        public AuthController(IAuthBAL authBAL, JWT_Service jwtService)
        {
            _authBAL = authBAL;
            _jwtService = jwtService;
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
    }
}