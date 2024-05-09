using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Stock_Manage_System_API.Email_Services
{
    /// <summary>
    /// Class for sending emails asynchronously using SMTP
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSSL;
        private readonly string _userName;
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class with the specified SMTP server host, port, SSL enable flag, and credentials
        /// </summary>
        /// <param name="host">The SMTP server host</param>
        /// <param name="port">The SMTP server port</param>
        /// <param name="enableSSL">A flag indicating whether to use SSL</param>
        /// <param name="userName">The SMTP server user name</param>
        /// <param name="password">The SMTP server password</param>
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSSL;
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// Sends an email asynchronously with the specified email, subject, and message
        /// </summary>
        /// <param name="email">The recipient's email address</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="message">The body of the email</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_host)
            {
                Port = _port,
                Credentials = new NetworkCredential(_userName, _password),
                EnableSsl = _enableSSL,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_userName, "Stock Manage System"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

        /// <summary>
        /// Sends an email asynchronously with the specified email, subject, body, and attachment
        /// </summary>
        /// <param name="email">The recipient's email address</param>
        /// <param name="subject">The subject of the email</param>
        /// <param name="body">The body of the email</param>
        /// <param name="attachment">The attachment to include with the email</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        public async Task SendEmailWithAttachmentAsync(string email, string subject, string body, Attachment attachment)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_host)
                {
                    Port = _port,
                    Credentials = new NetworkCredential(_userName, _password),
                    EnableSsl = _enableSSL,
                })
                using (var mailMessage = new MailMessage
                {
                    From = new MailAddress(_userName, "Stock Manage System"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                })
                {
                    mailMessage.To.Add(email);
                    mailMessage.Attachments.Add(attachment);

                    Console.WriteLine("Attempting to send an email...");
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                // It's better to log the full exception to preserve the stack trace in your logging mechanism
                Console.WriteLine($"An error occurred whilesending email: {ex}");
                throw; // Consider the appropriate error handling here (logging, retrying, etc.)
            }
        }
    }
}