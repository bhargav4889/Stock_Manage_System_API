using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.Email_Services;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmailController : Controller
    {

        private IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet("{Email}&{Username}")]
        public async Task<IActionResult> Login_Email(string Email, string Username)
        {
            // Email Service - After successful Login Details Fetch, send a email
            string _email = Email;// Dynamically fetch the admin's email
            string _emailSubject = "Login Details!";
            string _emailBody = $@"
<html>
<head>
</head>
<body style='font-family: Arial, sans-serif; color: #333; background-color: #f4f4f4;'>
    <div style='max-width: 600px; margin: 0 auto; background-color: #FFFFFF; padding: 20px;'>
        <div style='text-align: center;'>           
            <h1 style='color: #000000;'>Welcome to Our Platform!</h1>
            <p>Hello {Username},</p>
            <p>Thank you for registering with us! We're excited to have you on board and look forward to supporting your journey with us.</p>
        </div>
        
        <div style='margin-top: 40px; color: #333;'>
            <p style='font-weight:bold;'>Thanks & Regards,</p>
            <p style='font-weight:bold;'>Team Stock Manage System</p>
        </div>
    </div>
</body>
</html>";
            await _emailSender.SendEmailAsync(_email, _emailSubject, _emailBody);
            return Ok("Email Sent Successfully !!");
        }
    }
}   
