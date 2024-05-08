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
    public class ReminderBackgroundProcess : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReminderBackgroundProcess(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
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
