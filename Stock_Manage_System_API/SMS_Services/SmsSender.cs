using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Stock_Manage_System_API.SMS_Services
{
    /// <summary>
    /// An implementation of the <see cref="ISmsSender"/> interface using Twilio.
    /// </summary>
    public class SmsSender : ISmsSender
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _fromNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsSender"/> class.
        /// </summary>
        /// <param name="accountSid">The Twilio account SID.</param>
        /// <param name="authToken">The Twilio auth token.</param>
        /// <param name="fromNumber">The Twilio phone number to send SMS messages from.</param>
        public SmsSender(string accountSid, string authToken, string fromNumber)
        {
            _accountSid = accountSid;
            _authToken = authToken;
            _fromNumber = fromNumber;
            TwilioClient.Init(_accountSid, _authToken);
        }

        /// <summary>
        /// Sends an SMS message to the specified phone number using Twilio.
        /// </summary>
        /// <param name="phoneNumber">The recipient's phone number.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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