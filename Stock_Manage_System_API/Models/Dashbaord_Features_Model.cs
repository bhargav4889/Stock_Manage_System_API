namespace Stock_Manage_System_API.Models
{
    public class Dashbaord_Features_Model
    {
        public class Pending_Customers_Payment_Sort_List

        {

            public int StockId { get; set; }

            public DateTime StockDate { get; set; }

            public int CustomerId { get; set; }
           
            public string? CustomerName { get; set; }

            public int ProductId { get; set; }

            public string? ProductName { get; set; }
            public decimal TotalPrice { get; set; }
        }
       

    }
}

