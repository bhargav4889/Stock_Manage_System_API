using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Stock_Manage_System_API.Email_Services
{
    /// <summary>
    /// Interface for sending emails asynchronously
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously with the specified email, subject, and message
        /// </summary>
        /// <param name="email">The recipient's email address</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The body of the email</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// Sends an email asynchronously with the specified email, subject, body, and attachment
        /// </summary>
        /// <param name="email">The recipient's email address</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="body">The body of the email</param>
        /// <param name="attachment">The attachment to include with the email</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        Task SendEmailWithAttachmentAsync(string email, string subject, string body, Attachment attachment);
    }
}