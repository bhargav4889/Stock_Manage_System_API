namespace Stock_Manage_System_API.Models
{
    public class Reminder_Model
    {
        public int ReminderId { get; set; }

        public DateTime ReminderDateTime { get; set; }

        public string? ReminderType { get; set; }

        public string? ReminderCustomType { get; set; }

        public string? ReminderDescription { get; set; }

        public string?  SentEmailAddress { get; set; }

        public string? SentPhoneNo { get; set; }


    }
}
