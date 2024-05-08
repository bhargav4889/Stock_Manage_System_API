using System.Threading.Tasks;

namespace Stock_Manage_System_API.SMS_Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
