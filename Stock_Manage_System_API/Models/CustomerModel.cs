﻿using System.ComponentModel.DataAnnotations;

namespace Stock_Manage_System_API.Models
{

    public class Customer_Model
    {
        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerType { get; set; }

        public string? CustomerContact { get; set; }

        public string? CustomerAddress { get; set; }



    }

    public class Purchased_Stock_Model
    {

        public int StockId { get; set; }
        public DateTime StockDate { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductGradeId { get; set; }
        public string? ProductGrade { get; set; }
        public string? PurchaseStockLocation { get; set; }
        public decimal? Bags { get; set; }
        public decimal? BagPerKg { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public string? VehicleNo { get; set; }
        public string? DriverName { get; set; }
        public string? TolatName { get; set; }
        public string? PaymentStatus { get; set; }




    }


    // ----------- Account Details.cshtml   ------------------------//

    public class CustomerDetails_With_Purchased_Stock_Model
    {
        public Customer_Model Customers { get; set; }

        public List<Purchased_Stock_Model> Purchased_Stocks { get; set; }

        public List<Show_Sale> Show_Sales { get; set; }

    }




}
