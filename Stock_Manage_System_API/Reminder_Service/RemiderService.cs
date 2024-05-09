using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                string body = $"Your reminder for {row["REMINDER_DESCRIPTION"].ToString()}";

                body += $"\n\nAdditional Type Information: {row["REMINDER_CUSTOM_TYPE"].ToString()}";

                await _emailSender.SendEmailAsync(email, subject, body);
            }
        }
    }
}