using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manage_System_API.Email_Services;

namespace Stock_Manage_System_API
{
    public class DailyEmailBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DailyEmailBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate the next run time, assuming you want to send the email at a specific hour every day
                var now = DateTime.Now;
                var nextRunTime = now.Date.AddDays(1).AddHours(9); // Example: send email at 9 AM every day

                if (nextRunTime <= now)
                {
                    nextRunTime = nextRunTime.AddDays(1);
                }

                var delay = nextRunTime - now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dailyMailService = scope.ServiceProvider.GetRequiredService<DailyEmailService>();

                    // Assuming SendDailyActionsEmailAsync checks for data availability and skips if no data
                    await dailyMailService.SendDailyActionsEmailAsync("bhargavkachhela1@gmail.com");
                    await dailyMailService.SendDailyActionsEmailAsync("ashishkachhela51@gmail.com");
                }

                // Wait for 24 hours before sending the next email
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

    }
}
