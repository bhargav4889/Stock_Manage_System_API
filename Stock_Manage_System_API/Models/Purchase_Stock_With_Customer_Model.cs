using Newtonsoft.Json;

namespace Stock_Manage_System_API.Models
{
    public class Purchase_Stock_With_Customer_Model
    {
        [JsonProperty("purchase-stock")]
        public Insert_And_Update_Purchase_Stock Purchase_Stock { get; set; }
        [JsonProperty("customer-info")]
        public Customer_Model Customers_Model { get; set; }

    }
}
