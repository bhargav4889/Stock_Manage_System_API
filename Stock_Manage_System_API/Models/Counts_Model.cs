using Newtonsoft.Json;

namespace Stock_Manage_System_API.Models
{
    public class AllCountsModel
    {
        [JsonProperty("totalCustomers")]
        public int TotalCustomers { get; set; }

        [JsonProperty("totalBags")]
        public int TotalBags { get; set; }

        [JsonProperty("totalWeight")]
        public decimal TotalWeight { get; set; }

        [JsonProperty("amountOfPurchasedStock")]
        public decimal AmountOfPurchasedStock { get; set; }

        [JsonProperty("totalPurchaseInvoice")]
        public int TotalPurchaseInvoice { get; set; }

        [JsonProperty("amountOfPurchaseInvoice")]
        public decimal AmountOfPurchaseInvoice { get; set; }

        [JsonProperty("totalSalesInvoice")]
        public int TotalSalesInvoice { get; set; }

        [JsonProperty("amountOfSalesInvoice")]
        public decimal AmountOfSalesInvoice { get; set; }

        [JsonProperty("totalPayments")]
        public int TotalPayments { get; set; }

        [JsonProperty("paidPayments")]
        public int PaidPayments { get; set; }

        [JsonProperty("remainPayments")]
        public int RemainPayments { get; set; }

        [JsonProperty("pendingPayments")]
        public int PendingPayments { get; set; }

        [JsonProperty("amountOfPaidPayments")]
        public decimal AmountOfPaidPayments { get; set; }

        [JsonProperty("amountOfRemainingPayments")]
        public decimal AmountOfRemainingPayments { get; set; }

        [JsonProperty("amountOfPendingPayments")]
        public decimal AmountOfPendingPayments { get; set; }

        [JsonProperty("amountOfSaleStock")]
        public decimal AmountOfSaleStock { get; set; }
    }
}
