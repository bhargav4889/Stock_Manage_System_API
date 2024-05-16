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
                var nextRunTime = DateTime.Today.AddDays(1).AddHours(8); // Adjust time for daily email
                var delay = nextRunTime - DateTime.Now;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dailyMailService = scope.ServiceProvider.GetRequiredService<DailyEmailService>();
                    await dailyMailService.SendDailyActionsEmailAsync("bhargavkachhela1@gmail.com");
                }
            }
        }
    }
}
