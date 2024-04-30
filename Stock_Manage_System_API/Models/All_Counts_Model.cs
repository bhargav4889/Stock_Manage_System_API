    namespace Stock_Manage_System_API.Models
    {
        public class All_Counts_Model
        {
            public int TotalCustomers { get; set; }
            public int TotalBags { get; set; }
            public decimal TotalWeight { get; set; }
            public decimal AmountOfPurchasedStock { get; set; }
            public int TotalPurchaseInvoice { get; set; }
            public decimal AmountOfPurchaseInvoice { get; set; }
            public int TotalSalesInvoice { get; set; }
            public decimal AmountOfSalesInvoice { get; set; }

            public int TotalPayments { get; set; }

            public int PaidPayments { get; set; }

            public int RemainPayments { get; set; }
            public int PendingPayments { get; set; }
            public decimal AmountOfPaidPayments { get; set; }

            public decimal AmountOfRemainingPayments { get; set; }
            public decimal AmountOfPendingPayments { get; set; }
        }
    }
