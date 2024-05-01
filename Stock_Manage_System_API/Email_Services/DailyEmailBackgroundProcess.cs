using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manage_System_API.Email_Services;

public class DailyEmailBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DailyEmailBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait until the system time is exactly 2 minutes from now
        while (!stoppingToken.IsCancellationRequested)
        {
            var nextRunTime = DateTime.Now.AddMinutes(1); // Calculate next run time to be 2 minutes from now
            var delay = nextRunTime - DateTime.Now;
            if (delay.TotalMilliseconds > 0)
            {
                await Task.Delay(delay, stoppingToken); // Wait for 2 minutes
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var dailyMailService = scope.ServiceProvider.GetRequiredService<DailyEmailService>();
                await dailyMailService.SendDailyActionsEmailAsync("bhargavkachhela1@gmail.com"); // Sending the email
            }
        }
    }
}
