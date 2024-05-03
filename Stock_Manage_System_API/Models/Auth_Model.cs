using System.ComponentModel.DataAnnotations;

namespace Stock_Manage_System_API.Models
{
    public class Auth_Model
    {
        
        public string? Username { get; set; }

        public string? Password { get; set; }

        


    }

    public class User_Model
    {
        public int Userid { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string? Emailaddress { get; set; }

        public string? Phoneno { get; set; }

        public string? Token { get; set; }
    }
}
