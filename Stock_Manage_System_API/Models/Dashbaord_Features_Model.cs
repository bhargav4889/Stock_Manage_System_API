using System.Text.Json.Serialization;

namespace Stock_Manage_System_API.Models
{
    public class Dashbaord_Features_Model
    {
        public class Pending_Customers_Payment_Sort_List
        {
            [JsonPropertyName("stockId")]
            public int StockId { get; set; }

            [JsonPropertyName("stockDate")]
            public DateTime StockDate { get; set; }

            [JsonPropertyName("customerId")]
            public int CustomerId { get; set; }

            [JsonPropertyName("customerName")]
            public string? CustomerName { get; set; }

            [JsonPropertyName("customerType")]
            public string? CustomerType { get; set; }

            [JsonPropertyName("productId")]
            public int ProductId { get; set; }

            [JsonPropertyName("productName")]
            public string? ProductName { get; set; }

            [JsonPropertyName("totalPrice")]
            public decimal TotalPrice { get; set; }
        }


        public class Upcoming_Reminders_Model
        {
            public int ReminderId { get; set; }

            public DateTime ReminderDateTime { get; set; }

            public string? ReminderType { get; set; }

            public string? ReminderCustomType { get; set; }

            public string? ReminderDescription { get; set; }

            public string? SentEmailAddress { get; set; }

            public string? SentPhoneNo { get; set; }
        }

    }
}

