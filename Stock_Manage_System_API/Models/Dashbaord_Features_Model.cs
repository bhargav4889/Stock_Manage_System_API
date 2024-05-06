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


    }
}

