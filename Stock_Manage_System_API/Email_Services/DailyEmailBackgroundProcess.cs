using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manage_System_API.Email_Services;

namespace Stock_Manage_System_API
{
    /// <summary>
    /// A background service for sending a daily email with a report attached as a PDF.
    /// </summary>
    public class DailyEmailBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyEmailBackgroundService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use for resolving dependencies.</param>
        public DailyEmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// The main execution method for the background service.
        /// </summary>
        /// <param name="stoppingToken">A token that can be used to stop the background service.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Wait until the system time is exactly 2 minutes from now
            while (!stoppingToken.IsCancellationRequested)
            {
                var nextRunTime = DateTime.Now.AddDays(1).AddHours(8);
                var delay = nextRunTime - DateTime.Now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dailyMailService = scope.ServiceProvider.GetRequiredService<DailyEmailService>();
                    await dailyMailService.SendDailyActionsEmailAsync("bhargavkachhela1@gmail.com"); // Sending the email
                }
            }
        }
    }
}