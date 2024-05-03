using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stock_Manage_System_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Stock_Manage_System_API.Login_Service
{
   
    public class JWT_Service
    {
        private readonly IConfiguration _configuration;

        public JWT_Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWTToken(User_Model user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Emailaddress),
            new Claim("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
