using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Stock_Manage_System_API.Email_Services;
using Stock_Manage_System_API.SMS_Services;

namespace Stock_Manage_System_API.Reminder_Service
{
    /// <summary>
    /// A service for handling reminders.
    /// </summary>
    public class ReminderService
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderService"/> class.
        /// </summary>
        /// <param name="emailSender">The email sender to use for sending email reminders.</param>
        /// <param name="smsSender">The SMS sender to use for sending SMS reminders.</param>
        public ReminderService(IEmailSender emailSender, ISmsSender smsSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
            _connectionString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
              .Build()
                .GetConnectionString("MyConnection");
        }

        /// <summary>
        /// Gets the reminders from the database as a DataTable.
        /// </summary>
        /// <returns>A DataTable containing the reminders.</returns>
        public async Task<DataTable> GetRemindersAsync()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[dbo].[API_DISPLAY_REMINDER_DETAILS]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                            Console.WriteLine($"Fetched {dataTable.Rows.Count} records from the database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching reminders from database: {ex.Message}");
            }
            return dataTable;
        }

        /// <summary>
        /// Sends the reminders to the appropriate recipients.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SendRemindersAsync()
        {
            DataTable reminders = await GetRemindersAsync();
            DateTime now = DateTime.UtcNow;  // Ensure UTC is used everywhere or align with your server time settings.

            foreach (DataRow row in reminders.Rows)
            {
                string email = row["EMAIL_ADDRESS"].ToString();
                string subject = $"Reminder! - {row["REMINDER_TYPE"].ToString()}";
                string addtionaltypeinfo = row["REMINDER_CUSTOM_TYPE"].ToString();

                // Construct the HTML body
                StringBuilder bodyBuilder = new StringBuilder();
                bodyBuilder.AppendLine("<html><body>");
                bodyBuilder.AppendLine($"<h2>Reminder: {row["REMINDER_DESCRIPTION"].ToString()}</h2>");
                bodyBuilder.AppendLine("<p>Dear User,</p>");
                bodyBuilder.AppendLine("<p>This is a your reminder for the following:</p>");
                bodyBuilder.AppendLine($"<p><strong>Reminder Type:</strong> {row["REMINDER_TYPE"].ToString()}</p>");
                bodyBuilder.AppendLine($"<p><strong>Description:</strong> {row["REMINDER_DESCRIPTION"].ToString()}</p>");

                // Add "Additional Type Information" only if it is not null or empty
                if (!string.IsNullOrEmpty(addtionaltypeinfo))
                {
                    bodyBuilder.AppendLine($"<p><strong>Additional Type Information:</strong> {addtionaltypeinfo}</p>");
                }

                bodyBuilder.AppendLine("<p>Thank you,<br/> Stock Manage System</p>");
                bodyBuilder.AppendLine("</body></html>");

                string body = bodyBuilder.ToString();

                await _emailSender.SendEmailAsync(email, subject, body);
            }

        }
    }
}