namespace Stock_Manage_System_API.Models
{
    public class Reset_PasswordModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
