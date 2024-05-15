namespace Stock_Manage_System_API.ResetPassword_Service
{
    public interface IPasswordResetService
    {
        Task<bool> RequestPasswordResetAsync(string email);

        Task<bool> ResetPasswordAsync(string email, string token, string newPassowrd);
    }
}
