using System.ComponentModel.DataAnnotations;

namespace Stock_Manage_System_API.Models
{


    public class Show_Purchase_Stock
    {
        public int PurchaseStockId { get; set; }
        public DateTime PurchaseStockDate { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerType { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductGradeId { get; set; }
        public string? ProductGrade { get; set; }
        public string? PurchaseStockLocation { get; set; }
        public decimal? Bags { get; set; }
        public decimal? BagPerKg { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public string? VehicleNo { get; set; }
        public string? DriverName { get; set; }
        public string? TolatName { get; set; }
        public string? PaymentStatus {  get; set; }
    }

    public class Insert_And_Update_Purchase_Stock
    {
        public int PurchaseStockId { get; set; }
        public DateTime PurchaseStockDate { get; set; }
        public int ProductId { get; set; }
        public int? ProductGradeId { get; set; }
        public string? PurchaseStockLocation { get; set; }
        public decimal? Bags { get; set; }
        public decimal? BagPerKg { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public string? VehicleNo { get; set; }
        public string? DriverName { get; set; }
        public string? TolatName { get; set; }
        public string? PaymentStatus { get; set; }
    }


}
