namespace Stock_Manage_System_API.Reminder_Service
{
    public class Reminder_Model
    {
        public DateTime ReminderDateTime { get; set; }
        public string ReminderType { get; set; }
        public string CustomType { get; set; }
        public string Description { get; set; }
        public string EmailAddress { get; set; }
    }
}
