﻿namespace Stock_Manage_System_API.Models
{
    public class SaleModel
    {
        public int SaleId { get; set; }

        public DateTime Create_Sales { get; set; }
        public int Product_Id { get; set; }

        public DateTime Receive_Payment_Date { get; set; }

        public string? Product_Name { get; set; }

        public string? Brand_Name { get; set; }

        public string? Payment_Method { get; set; }

        public int Receive_Bank_Id { get; set; }

        public int? Receive_Information_Id { get; set; }


        public decimal? Bags { get; set; }

        public decimal? BagPerKg { get; set; }


        public decimal? Total_Weight { get; set; }

        public decimal Rate { get; set; }

        public decimal? CGST { get; set; }

        public decimal? SGST { get; set; }

        public decimal? TotalCGSTPrice { get; set; }

        public decimal? TotalSGSTPrice { get; set; }

        public decimal? WithoutGSTPrice { get; set; }
        public decimal Total_Price { get; set; }

        public decimal Receive_Amount { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Deducted_Amount { get; set; }

        public string? IsFullPaymentReceive { get; set; }



        public DateTime? Remain_Payment_Date { get; set; }

        public decimal? Receive_Remain_Amount { get; set; }

        public string? Remain_Payment_Method { get; set; }


        public int? Remain_Amount_Receive_Bank_Id { get; set; }

        public int? Remain_Infromation_ID { get; set; }




    }
    public class Sale_Customer_Combied_Model
    {
        public Customer_Model customer { get; set; }

        public SaleModel sale { get; set; }
    }

    public class Show_Sale
    {
        public int saleId { get; set; }
        public DateTime Create_Sales { get; set; }
        public int Product_Id { get; set; }

        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerType { get; set; }


        public DateTime Receive_Payment_Date { get; set; }

        public string? Product_Name { get; set; }

        public string? Brand_Name { get; set; }

        public string? Payment_Method { get; set; }

        public int? Receive_Information_ID { get; set; }

        public string? Receive_Account_No { get; set; }

        public string? Account_Holder_Name { get; set; }

        public string? Bank_Icon { get; set; }

        public decimal? Bags { get; set; }

        public decimal? BagPerKg { get; set; }


        public decimal? Total_Weight { get; set; }

        public decimal Rate { get; set; }

        public decimal? CGST { get; set; }

        public decimal? SGST { get; set; }

        public decimal? TotalCGSTPrice { get; set; }

        public decimal? TotalSGSTPrice { get; set; }

        public decimal? WithoutGSTPrice { get; set; }

        public decimal Total_Price { get; set; }

        public decimal Receive_Amount { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Deducted_Amount { get; set; }

        public string? IsFullPaymentReceive { get; set; }

        public DateTime? Remain_Payment_Date { get; set; }

        public decimal? Receive_Remain_Amount { get; set; }

        public string? Remain_Payment_Method { get; set; }

        public int? Remain_Receive_Information_ID { get; set; }

        public string? Receive_Remain_Account_No { get; set; }

        public string? Remain_Account_Holder_Name { get; set; }

        public string? Remain_Bank_Icon { get; set; }

    }
}
