using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manage_System_API.Email_Services;
using Stock_Manage_System_API.SMS_Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Stock_Manage_System_API.Reminder_Service
{
    /// <summary>
    /// A background process for sending reminders.
    /// </summary>
    public class ReminderBackgroundProcess : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderBackgroundProcess"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use for getting the reminder service.</param>
        public ReminderBackgroundProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// The main method for executing the background process.
        /// </summary>
        /// <param name="stoppingToken">A cancellation token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reminderService = scope.ServiceProvider.GetRequiredService<ReminderService>();
                    await reminderService.SendRemindersAsync();
                }

                // Adjust to the frequency that matches your scheduling needs.
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}