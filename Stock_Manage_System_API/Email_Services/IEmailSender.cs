using System.Net.Mail;

namespace Stock_Manage_System_API.Email_Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithAttachmentAsync(string email, string subject, string body, Attachment attachment);
    }
}
