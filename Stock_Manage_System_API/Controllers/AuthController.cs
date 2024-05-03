using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Login_Service;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBAL _authBAL;
        private readonly JWT_Service _jwtService;

        public AuthController(IAuthBAL authBAL, JWT_Service jwtService)
        {
            _authBAL = authBAL;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public IActionResult Login(Auth_Model authInfo)
        {
            var user = _authBAL.Auth_Login_Details(authInfo);
            if (user != null && user.Userid > 0)
            {
                 user.Token = _jwtService.GenerateJWTToken(user);
                return Ok(new { success = true, user = user });
            }
            return Unauthorized(new { success = false, message = "Invalid credentials" });
        }




    }
}
