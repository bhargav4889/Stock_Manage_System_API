namespace Stock_Manage_System_API.SMS_Services
{
    using System;
    using System.Threading.Tasks;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;
    using Twilio.Types;

    public class SmsSender : ISmsSender
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        public SmsSender(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _fromNumber = fromNumber;
            TwilioClient.Init(_accountSid, _authToken);
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber))
                {
                    From = new PhoneNumber(_fromNumber),
                    Body = message
                };

                Console.WriteLine("Attempting to send an SMS...");
                var msg = await MessageResource.CreateAsync(messageOptions);
                Console.WriteLine("SMS sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending SMS: {ex}");
                throw; // Proper error handling should be considered here
            }
        }
    }
}
