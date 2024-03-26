using System.ComponentModel.DataAnnotations;

namespace Stock_Manage_System_API.Models
{
    public class InvoicesModel
    {
        #region Purchase_Invoice_Model

        public class Purchase_Invoice_Model
        {
            public int PurchaseInvoiceId { get; set; }
            public DateTime PurchaseInvoiceDate { get; set; }

            public string? CustomerName { get; set; }
            public int ProductId { get; set; }
            public string? ProductName { get; set; }
            public int? ProductGradeId { get; set; }
            public string? ProductGrade { get; set; }

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


        }

        #endregion


        #region Sell_Invoice_Model

        public class Sales_Invoice_Model
        {
            public int SalesInvoiceId { get; set; }
            public DateTime SalesInvoiceDate { get; set; }

            public string? BrokerName { get; set; }

            public string? PartyName { get; set; }

            public string? PartyGstNo { get; set; }

            public string? PartyAddress { get; set; }

            public string? OtherInvoiceType { get; set; }

            public string? InvoiceType { get; set; }


            public int ProductId { get; set; }
            public string? ProductName { get; set; }

            public string? ProductBrandName { get; set; }

            public decimal? Bags { get; set; }
            public decimal? BagPerKg { get; set; }
            public decimal TotalWeight { get; set; }

            public decimal ProductPrice { get; set; }

            public decimal? CGST { get; set; }

            public decimal? SGST { get; set; }

            public decimal? TotalCGSTPrice { get; set; }

            public decimal? TotalSGSTPrice { get; set; }

            public decimal? WithoutGSTPrice { get; set; }


            public decimal TotalPrice { get; set; }
            public int VehicleId { get; set; }
            public string? VehicleName { get; set; }
            public string? VehicleNo { get; set; }

            public string? ContainerNo { get; set; }

            public string? DriverName { get; set; }

        }


        #endregion




    }
}
