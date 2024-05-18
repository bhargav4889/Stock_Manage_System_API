using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using static Stock_Manage_System_API.Models.InvoicesModel;
using Document = iTextSharp.text.Document;
using Font = iTextSharp.text.Font;

namespace Stock_Manage_System_API.DAL
{
    public class Download_DALBase : DAL_Helpers
    {


        private readonly Invoices_DALBase invoices_DALBase = new Invoices_DALBase();

        private readonly Customers_DALBase customers_DALBase = new Customers_DALBase();

        private readonly Stock_DALBase stock_DALBase = new Stock_DALBase();

        private readonly Payment_DALBase payment_DALBase = new Payment_DALBase();

        private readonly Sales_DALBase sales_DALBase = new Sales_DALBase();


        #region Method : All List Models Convert Datatable 


        #region Section : Purchase Invoice Statement DataTable 

        public DataTable ListToDataTableConverterForPurchaseInvoiceStatement(List<Purchase_Invoice_Model> purchase_Invoices)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Invoice-Date", typeof(string));
            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Product-Grade", typeof(string));
            dataTable.Columns.Add("Bags", typeof(string));
            dataTable.Columns.Add("Bag-Per-Kg", typeof(string));
            dataTable.Columns.Add("Weight", typeof(string));
            dataTable.Columns.Add("Total-Price", typeof(string));
            dataTable.Columns.Add("Vehicle-Name", typeof(string));
            dataTable.Columns.Add("Vehicle-No", typeof(string));
            dataTable.Columns.Add("Tolat", typeof(string));
            dataTable.Columns.Add("Driver-Name", typeof(string));






            foreach (var invoice in purchase_Invoices)
            {
                DataRow row = dataTable.NewRow();

                DateTime date = invoice.PurchaseInvoiceDate; // Your original date
                string formattedDate = date.ToString("dd/MM/yyyy"); // Formatting the date

                row["Invoice-Date"] = formattedDate;
                row["Customer-Name"] = invoice.CustomerName;
                row["Product"] = invoice.ProductName;
                row["Product-Grade"] = invoice.ProductGrade;
                row["Bags"] = invoice.Bags.HasValue ? invoice.Bags.ToString() : "--";
                row["Bag-Per-Kg"] = invoice.BagPerKg.HasValue ? invoice.BagPerKg.ToString() : "--";
                row["Weight"] = invoice.TotalWeight.ToString("C");
                row["Total-Price"] = invoice.TotalPrice.ToString("C");
                row["Vehicle-Name"] = invoice.VehicleName;
                row["Vehicle-No"] = invoice.VehicleNo;
                row["Tolat"] = invoice.TolatName;
                row["Driver-Name"] = invoice.DriverName;





                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


        #endregion

        #region Method : Sales Invoice Statement DataTable

        public DataTable ListToDataTableConverterForSaleInvoiceStatement(List<Sales_Invoice_Model> salesInvoices)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Invoice-Date", typeof(string));
            dataTable.Columns.Add("Invoice-Type", typeof(string));
            dataTable.Columns.Add("Broker-Name", typeof(string));
            dataTable.Columns.Add("Party-Name", typeof(string));
            dataTable.Columns.Add("Party-GSTNO", typeof(string));
            dataTable.Columns.Add("Party-Address", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Brand", typeof(string));
            dataTable.Columns.Add("Bags", typeof(string));
            dataTable.Columns.Add("Bag-Per-Kg", typeof(string));
            dataTable.Columns.Add("Weight", typeof(string));
            dataTable.Columns.Add("SGST", typeof(string));
            dataTable.Columns.Add("CGST", typeof(string));
            dataTable.Columns.Add("Total-Price", typeof(string));
            dataTable.Columns.Add("Vehicle-Name", typeof(string));
            dataTable.Columns.Add("Vehicle-No", typeof(string));
            dataTable.Columns.Add("Driver-Name", typeof(string));
            dataTable.Columns.Add("Container-No", typeof(string));





            foreach (var invoice in salesInvoices)
            {
                DataRow row = dataTable.NewRow();

                DateTime date = invoice.SalesInvoiceDate; // Your original date
                string formattedDate = date.ToString("dd/MM/yyyy"); // Formatting the date

                row["Invoice-Date"] = formattedDate; // Assigning the formatted



                row["Invoice-Type"] = invoice.InvoiceType;
                row["Broker-Name"] = invoice.BrokerName;
                row["Party-Name"] = invoice.PartyName;
                row["Party-GSTNO"] = invoice.PartyGstNo;
                row["Party-Address"] = invoice.PartyAddress;
                row["Product"] = invoice.ProductName;
                row["Brand"] = invoice.ProductBrandName;
                row["Bags"] = invoice.Bags.HasValue ? invoice.Bags.ToString() : "--";
                row["Bag-Per-Kg"] = invoice.BagPerKg.HasValue ? invoice.BagPerKg.ToString() : "--";
                row["Weight"] = invoice.TotalWeight;
                row["SGST"] = invoice.SGST.HasValue ? invoice.SGST.ToString() : "--";
                row["CGST"] = invoice.CGST.HasValue ? invoice.CGST.ToString() : "--";
                row["Total-Price"] = invoice.TotalPrice.ToString("C");
                row["Vehicle-Name"] = invoice.VehicleName;
                row["Vehicle-No"] = invoice.VehicleNo;
                row["Driver-Name"] = invoice.DriverName;
                row["Container-No"] = !string.IsNullOrEmpty(invoice.ContainerNo) ? invoice.ContainerNo : "--";




                dataTable.Rows.Add(row);
            }

            return dataTable;
        }


        #endregion

        #region Method : Customers Account List Statements DataTable

        public DataTable ListToDataTableConverterForCustomersAccount(List<Customer_Model> customer_Models)
        {
            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Customer-Type", typeof(string));
            dataTable.Columns.Add("Customer-Contact", typeof(string));
            dataTable.Columns.Add("Customer-Address", typeof(string));

            foreach (var customer in customer_Models)
            {
                DataRow row = dataTable.NewRow();

                row["Customer-Name"] = customer.CustomerName;
                row["Customer-Type"] = customer.CustomerType;
                row["Customer-Contact"] = customer.CustomerContact;
                row["Customer-Address"] = customer.CustomerAddress;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #region Method : Customer Account Details Statement DataTable 

        public (DataTable Customer_info, DataTable Statements_Info, DataTable Sale_Info) ListToDataTableConverterForCustomersAccountDetails(CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_)
        {


            DataTable Customers_Info = new DataTable();

            DataTable Statements = new DataTable();

            DataTable Sale_Info = new DataTable();


            Customers_Info.Columns.Add("Customer-ID", typeof(int));

            Customers_Info.Columns.Add("Customer-Name", typeof(string));

            Customers_Info.Columns.Add("Customer-Type", typeof(string));

            Customers_Info.Columns.Add("Customer-Contact", typeof(string));

            Customers_Info.Columns.Add("Customer-Address", typeof(string));


            DataRow customerRow = Customers_Info.NewRow();
            customerRow["Customer-ID"] = customerDetails_With_Purchased_Stock_.Customers.CustomerId; // Assuming you have CustomerID
            customerRow["Customer-Name"] = customerDetails_With_Purchased_Stock_.Customers.CustomerName;
            customerRow["Customer-Type"] = customerDetails_With_Purchased_Stock_.Customers.CustomerType;
            customerRow["Customer-Contact"] = customerDetails_With_Purchased_Stock_.Customers.CustomerContact;
            customerRow["Customer-Address"] = customerDetails_With_Purchased_Stock_.Customers.CustomerAddress;
            Customers_Info.Rows.Add(customerRow);


            if (customerDetails_With_Purchased_Stock_.Customers.CustomerType == "BUYER")
            {
                Statements.Columns.Add("Stock-Date", typeof(string));

                Statements.Columns.Add("Product", typeof(string));

                Statements.Columns.Add("Location", typeof(string));
                Statements.Columns.Add("Bags", typeof(string));
                Statements.Columns.Add("Bag-Per-Kg", typeof(string));
                Statements.Columns.Add("Weight", typeof(string));
                Statements.Columns.Add("Total-Price", typeof(string));
                Statements.Columns.Add("Vehicle-Name", typeof(string));
                Statements.Columns.Add("Vehicle-No", typeof(string));
                Statements.Columns.Add("Tolat", typeof(string));
                Statements.Columns.Add("Driver-Name", typeof(string));
                Statements.Columns.Add("Payment-Status", typeof(string));






                foreach (var statement in customerDetails_With_Purchased_Stock_.Purchased_Stocks)
                {
                    DataRow statementRow = Statements.NewRow();

                    DateTime date = statement.StockDate; // Your original date

                    string formattedDate = date.ToString("dd/MM/yyyy"); // Formatting the date

                    statementRow["Stock-Date"] = formattedDate;
                    statementRow["Product"] = statement.ProductName;
                    statementRow["Location"] = statement.PurchaseStockLocation;
                    statementRow["Bags"] = statement.Bags.HasValue ? statement.Bags.Value.ToString() : "--";
                    statementRow["Bag-Per-Kg"] = statement.BagPerKg.HasValue ? statement.BagPerKg.Value.ToString() : "--";
                    statementRow["Weight"] = statement.TotalWeight;
                    statementRow["Total-Price"] = statement.TotalPrice;
                    statementRow["Vehicle-Name"] = statement.VehicleName;
                    statementRow["Vehicle-No"] = statement.VehicleNo;
                    statementRow["Tolat"] = statement.TolatName;
                    statementRow["Driver-Name"] = statement.DriverName;
                    statementRow["Payment-Status"] = string.IsNullOrEmpty(statement.PaymentStatus) ? "--" : statement.PaymentStatus;


                    Statements.Rows.Add(statementRow);

                }
            }

            else if (customerDetails_With_Purchased_Stock_.Customers.CustomerType == "SELLER")
            {
                // Sales 

                Sale_Info.Columns.Add("Sale-Date", typeof(string));
                Sale_Info.Columns.Add("Payment-Receive-Date", typeof(string));
                Sale_Info.Columns.Add("Product", typeof(string));
                Sale_Info.Columns.Add("Brand", typeof(string));
                Sale_Info.Columns.Add("Bags", typeof(string));
                Sale_Info.Columns.Add("Bag-Per-Kg", typeof(string));
                Sale_Info.Columns.Add("Weight", typeof(string));
                Sale_Info.Columns.Add("Rate", typeof(string));
                Sale_Info.Columns.Add("Total-Amount", typeof(string));
                Sale_Info.Columns.Add("Received-Amount", typeof(string));
                Sale_Info.Columns.Add("Discount", typeof(string));
                Sale_Info.Columns.Add("Remain-Payment-Date", typeof(string));
                Sale_Info.Columns.Add("Remain-Amount", typeof(string));
                Sale_Info.Columns.Add("Deducted-Amount", typeof(string));

                foreach (var sale in customerDetails_With_Purchased_Stock_.Show_Sales)
                {
                    DataRow salerow = Sale_Info.NewRow();

                    salerow["Sale-Date"] = sale.Create_Sales.ToString("dd/MM/yyyy");
                    salerow["Payment-Receive-Date"] = sale.Receive_Payment_Date.ToString("dd/MM/yyyy");
                    salerow["Product"] = sale.Product_Name;
                    salerow["Brand"] = string.IsNullOrEmpty(sale.Brand_Name) ? "--" : sale.Brand_Name;
                    salerow["Bags"] = sale.Bags.HasValue ? sale.Bags.Value.ToString() : "--";
                    salerow["Bag-Per-Kg"] = sale.BagPerKg.HasValue ? sale.BagPerKg.Value.ToString() : "--";
                    salerow["Weight"] = sale.Total_Weight;
                    salerow["Rate"] = sale.Rate;
                    salerow["Total-Amount"] = sale.Total_Price;
                    salerow["Received-Amount"] = sale.Receive_Amount;
                    salerow["Discount"] = string.IsNullOrEmpty(sale.Discount.ToString()) ? "--" : sale.Discount;
                    salerow["Remain-Payment-Date"] = string.IsNullOrEmpty(sale.Remain_Payment_Date.ToString()) ? "--" : sale.Remain_Payment_Date;
                    salerow["Remain-Amount"] = string.IsNullOrEmpty(sale.Receive_Remain_Amount.ToString()) ? "--" : sale.Receive_Remain_Amount; ;
                    salerow["Deducted-Amount"] = string.IsNullOrEmpty(sale.Deducted_Amount.ToString()) ? "--" : sale.Deducted_Amount; ;

                    Sale_Info.Rows.Add(salerow);


                }
            }





            return (Customers_Info, Statements, Sale_Info);
        }


        #endregion

        #region Method : Stock Statement DataTable

        public DataTable ListToDataTableConverterForPurchaseStockStatement(List<Purchase_Stock> purchase_Stocks)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Stock-Date", typeof(string));
            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Product-Grade", typeof(string));
            dataTable.Columns.Add("Location", typeof(string));
            dataTable.Columns.Add("Bags", typeof(string));
            dataTable.Columns.Add("Bag-Per-Kg", typeof(string));
            dataTable.Columns.Add("Weight", typeof(string));
            dataTable.Columns.Add("Total-Price", typeof(string));
            dataTable.Columns.Add("Vehicle-Name", typeof(string));
            dataTable.Columns.Add("Vehicle-No", typeof(string));
            dataTable.Columns.Add("Tolat", typeof(string));
            dataTable.Columns.Add("Driver-Name", typeof(string));
            dataTable.Columns.Add("Payment-Status", typeof(string));






            foreach (var stock in purchase_Stocks)
            {
                DataRow row = dataTable.NewRow();

                DateTime date = stock.PurchaseStockDate; // Your original date
                string formattedDate = date.ToString("dd/MM/yyyy"); // Formatting the date

                row["Stock-Date"] = formattedDate;
                row["Customer-Name"] = stock.CustomerName;
                row["Product"] = stock.ProductName;
                row["Product-Grade"] = stock.ProductGrade;
                row["Location"] = stock.PurchaseStockLocation;
                row["Bags"] = stock.Bags.HasValue && stock.Bags != 0 ? stock.Bags.ToString() : "--";
                row["Bag-Per-Kg"] = stock.BagPerKg.HasValue && stock.BagPerKg != 0 ? stock.BagPerKg.ToString() : "--";
                row["Weight"] = stock.TotalWeight;
                row["Total-Price"] = stock.TotalPrice;
                row["Vehicle-Name"] = stock.VehicleName;
                row["Vehicle-No"] = stock.VehicleNo;
                row["Tolat"] = string.IsNullOrEmpty(stock.TolatName) ? "--" : stock.TolatName;
                row["Driver-Name"] = stock.DriverName;
                row["Payment-Status"] = string.IsNullOrEmpty(stock.PaymentStatus) ? "PENDING" : stock.PaymentStatus;





                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #region Method : Sale Satement DataTable

        public DataTable ListToDataTableConverterForSalesStatement(List<Show_Sale> sales)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Sale-Date", typeof(string));
            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Brand-Name", typeof(string));
            dataTable.Columns.Add("Bags", typeof(string));
            dataTable.Columns.Add("Bag-Per-Kg", typeof(string));
            dataTable.Columns.Add("Weight", typeof(string));
            dataTable.Columns.Add("Rate", typeof(string));
            dataTable.Columns.Add("Total-Amount", typeof(string));
            dataTable.Columns.Add("Receive-Amount", typeof(string));
            dataTable.Columns.Add("Receive-Payment-Method", typeof(string));
            dataTable.Columns.Add("Discount", typeof(string));
            dataTable.Columns.Add("Is-FullPayment-Receive", typeof(string));
            dataTable.Columns.Add("Remain-Payment-Date",typeof(string));    
            dataTable.Columns.Add("Remain-Amount", typeof(string));
            dataTable.Columns.Add("Remain-Payment-Method", typeof(string));
            dataTable.Columns.Add("Deducted-Amount", typeof(string));


            foreach (var sale in sales)
            {
                DataRow row = dataTable.NewRow();

                row["Sale-Date"] = sale.Create_Sales.ToString("dd/MM/yyyy");
                row["Customer-Name"] = sale.CustomerName;
                row["Product"] = sale.Product_Name;
                row["Brand-Name"] = string.IsNullOrEmpty(sale.Brand_Name) ? "--" : sale.Brand_Name;
                row["Bags"] = sale.Bags.HasValue ? sale.Bags.ToString() : "--";
                row["Bag-Per-Kg"] = sale.BagPerKg.HasValue ? sale.BagPerKg.ToString() : "--";
                row["Rate"] = sale.Rate;
                row["Weight"] = sale.Total_Weight.Value.ToString();
                row["Total-Amount"] = sale.Total_Price.ToString("C");
                row["Receive-Amount"] = sale.Receive_Amount.ToString("C");
                row["Receive-Payment-Method"] = string.IsNullOrEmpty(sale.Payment_Method) ? "--" : sale.Payment_Method;
                row["Discount"] = sale.Discount != 0 ? sale.Discount?.ToString("C") : "--";
                row["Is-FullPayment-Receive"] = sale.IsFullPaymentReceive.ToString();
                // Assuming `sale.Remain_Payment_Date` is of type `DateTime?` (nullable DateTime)
                row["Remain-Payment-Date"] = sale.Remain_Payment_Date.HasValue ? sale.Remain_Payment_Date.Value.ToString("dd/MM/yyyy") : "--";  

                row["Remain-Amount"] = sale.Receive_Remain_Amount.HasValue ? sale.Receive_Remain_Amount.Value.ToString("C") : "--";
                row["Remain-Payment-Method"] = string.IsNullOrEmpty(sale.Remain_Payment_Method) ? "--" : sale.Remain_Payment_Method;
                row["Deducted-Amount"] = sale.Deducted_Amount.HasValue ? sale.Deducted_Amount.Value.ToString("C") : "--";

                dataTable.Rows.Add(row);
            }





            return dataTable;
        }


        #endregion

        #region Method : Pending Payments List DataTable

        public DataTable ListToDataTableConverterForPendingPayments(List<Pending_Customers_Payments> pending_payments)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Stock-Date", typeof(string));
            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Total-Amount", typeof(string));
            dataTable.Columns.Add("Payment-Status", typeof(string));

            foreach (var payments in pending_payments)
            {
                DataRow row = dataTable.NewRow();

                row["Stock-Date"] = payments.StockDate.ToString("dd/MM/yyyy");
                row["Customer-Name"] = payments.CustomerName;
                row["Product"] = payments.ProductName;
                row["Total-Amount"] = payments.TotalPrice.ToString("C");
                row["Payment-Status"] = payments.Payment_Status;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #region Method : Remain Payments List DataTable

        public DataTable ListToDataTableConverterForRemainPayments(List<Remain_Payment_Model> remain_payments)
        {
            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Payment-Date", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Total-Amount", typeof(string));
            dataTable.Columns.Add("Paid-Amount", typeof(string));
            dataTable.Columns.Add("Remain-Amount", typeof(string));
            dataTable.Columns.Add("Payment-Method", typeof(string));
            dataTable.Columns.Add("Payment-Status", typeof(string));

            foreach (var payments in remain_payments)
            {
                DataRow row = dataTable.NewRow();

                row["Customer-Name"] = payments.Customer_Name;
                row["Payment-Date"] = payments.Payment_Date.ToString("dd/MM/yyyy");
                row["Product"] = payments.Product_Name;
                row["Total-Amount"] = payments.Total_Amount.ToString("C");
                row["Paid-Amount"] = payments.Paid_Amount.ToString("C");
                row["Remain-Amount"] = payments.Remain_Amount.ToString("C");
                row["Payment-Method"] = payments.First_Payment_Method;
                row["Payment-Status"] = payments.Remain_Payment_Status;

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #region Method : Paid Payments List DataTable

        public DataTable ListToDataTableConverterForPaidPayments(List<Show_Payment_Info> paid_payments)
        {
            DataTable dataTable = new DataTable();


            dataTable.Columns.Add("Customer-Name", typeof(string));
            dataTable.Columns.Add("Payment-Date", typeof(string));
            dataTable.Columns.Add("Product", typeof(string));
            dataTable.Columns.Add("Total-Amount", typeof(string));
            dataTable.Columns.Add("Paid-Amount", typeof(string));
            dataTable.Columns.Add("First-Payment-Method", typeof(string));
            dataTable.Columns.Add("Remain-Payment-Date", typeof(string));
            dataTable.Columns.Add("Remain-Amount", typeof(string));
            dataTable.Columns.Add("Remain-Payment-Method", typeof(string));
            dataTable.Columns.Add("Payment-Status", typeof(string));

            foreach (var payments in paid_payments)
            {
                DataRow row = dataTable.NewRow();

                row["Customer-Name"] = payments.CustomerName;
                row["Payment-Date"] = payments.PaymentDate.ToString("dd/MM/yyyy");
                row["Product"] = payments.ProductName;
                row["Total-Amount"] = payments.TotalPrice.ToString("C");
                row["Paid-Amount"] = payments.AmountPaid.ToString("C");

                row["First-Payment-Method"] = payments.PaymentMethod;

                // Assuming payments is the object containing the data
                if (payments.RemainPaymentDate != null)
                {
                    row["Remain-Payment-Date"] = payments.RemainPaymentDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    row["Remain-Payment-Date"] = "--";
                }

                // Use the null-coalescing operator to simplify the null check
                row["Remain-Payment-Method"] = payments.RemainPaymentMethod?.ToString() ?? "--";

                row["Remain-Amount"] = payments.RemainPaymentAmount.HasValue ? payments.RemainPaymentAmount.ToString() : "--";

                row["Payment-Status"] = payments.Payment_Status;


                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion





        #endregion



        #region Method : Download PDf All Invoices Statements

        public byte[] PurchaseInvoiceStatementPDF()
        {
            List<InvoicesModel.Purchase_Invoice_Model> Purchase_Invoices = invoices_DALBase.DisplayAllPurchaseInvoices();

            DataTable dataTable = ListToDataTableConverterForPurchaseInvoiceStatement(Purchase_Invoices);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);
                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Define fonts
                    BaseFont boldBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");
                    backimage.ScaleToFit(500, 500);
                    backimage.SetAbsolutePosition(900, 400);
                    document.Add(backimage);

                    // Table setup
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count)
                    {
                        WidthPercentage = 100,
                        DefaultCell = { Padding = 10 }
                    };

                    // Headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Font headerFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 10
                        };
                        pdfTable.AddCell(headerCell);
                    }

                    // Data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];
                            Font itemFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;

                            PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), itemFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 10
                            };
                            pdfTable.AddCell(dataCell);
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();
                }



                return memoryStream.ToArray();
            }


        }

        public byte[] SalesInvoiceStatementPDF()
        {
            List<Sales_Invoice_Model> salesInvoices = invoices_DALBase.DisplayAllSaleInvoices();

            // Convert to DataTable
            DataTable dataTable = ListToDataTableConverterForSaleInvoiceStatement(salesInvoices);





            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);
                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Define fonts
                    BaseFont boldBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");
                    backimage.ScaleToFit(500, 500);
                    backimage.SetAbsolutePosition(900, 400);
                    document.Add(backimage);

                    // Table setup
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count)
                    {
                        WidthPercentage = 100,
                        DefaultCell = { Padding = 10 }
                    };

                    // Headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Font headerFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 10
                        };
                        pdfTable.AddCell(headerCell);
                    }

                    // Data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];
                            Font itemFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;

                            PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), itemFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 10
                            };
                            pdfTable.AddCell(dataCell);
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();
                }
                return memoryStream.ToArray();
            }
        }



        #endregion

        #region Method : Download Excel All Invoices Statements 

        public byte[] PurchaseInvoiceStatementEXCEL()
        {
            List<InvoicesModel.Purchase_Invoice_Model> Purchase_Invoices = invoices_DALBase.DisplayAllPurchaseInvoices();

            DataTable dataTable = ListToDataTableConverterForPurchaseInvoiceStatement(Purchase_Invoices);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Purchase Invoices Statements");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return stream.ToArray();
                }
            }
        }

        public byte[] SalesInvoiceStatementEXCEL()
        {
            List<Sales_Invoice_Model> salesInvoices = invoices_DALBase.DisplayAllSaleInvoices();

            // Convert to DataTable
            DataTable dataTable = ListToDataTableConverterForSaleInvoiceStatement(salesInvoices);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Sales Invoices Statements");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row

                    foreach (var cell in row.CellsUsed())
                    {
                        // Ensure all cells are center-aligned
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        // Check for null or empty cell values and replace them with "--"
                        if (string.IsNullOrEmpty(cell.Value.ToString()))
                        {
                            cell.Value = "--";
                        }
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);

                    return stream.ToArray();
                }

            }

        }



        #endregion

        #region Method : Download PDF For Customers Account & Account Details Statements

        public byte[] CustomersStatementPDF()
        {
            List<Customer_Model> customer_Models = customers_DALBase.GetAllCustomers();

            DataTable dataTable = ListToDataTableConverterForCustomersAccount(customer_Models);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 100f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



                    // Add title
                    Paragraph title = new Paragraph("Customers List", new iTextSharp.text.Font(boldfont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);


                    document.Add(new Chunk("\n"));

                    // Add a line break


                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

                    pdfTable.WidthPercentage = 100;

                    pdfTable.DefaultCell.Padding = 10;






                    // Set the same width for all columns
                    pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");



                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF






                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.Padding = 10;
                        pdfTable.AddCell(headerCell);

                        /*  if (column.ColumnName == "GRAIN_NAME")
                          {
                              PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                              productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                              productCell.Padding = 10;
                              pdfTable.AddCell(productCell);
                          }*/
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];

                            if (item is DateTime dateTimeValue)
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                                // If the column is a DateTime, format it to display only the date
                            }
                            else if (column.ColumnName == "GRAIN_NAME" && item is string productName)
                            {
                                PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 12)));
                                productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                productTypeCell.Padding = 10;
                                pdfTable.AddCell(productTypeCell);
                            }
                            else
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                            }
                        }
                    }

                    // Add data rows


                    document.Add(pdfTable);
                }

                // Get the current date for the file name






                return memoryStream.ToArray();
            }
        }

        public byte[] CustomerAccountStatementPDF(int Customer_ID, string Customer_Type)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_Model = customers_DALBase.RetrieveAccountDetails(Customer_ID, Customer_Type);

                (DataTable Customer_info, DataTable Statements_Info, DataTable Sale_Info) = ListToDataTableConverterForCustomersAccountDetails(customerDetails_With_Purchased_Stock_Model);


                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 200f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



                    // Add title
                    Paragraph title = new Paragraph("Account Details", new iTextSharp.text.Font(boldfont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);


                    document.Add(new Chunk("\n"));



                    Paragraph customerid = new Paragraph($"Customer ID:- {customerDetails_With_Purchased_Stock_Model.Customers.CustomerId}", new iTextSharp.text.Font(boldfont, 19));
                    customerid.Alignment = Element.ALIGN_LEFT;
                    document.Add(customerid);
                    document.Add(new Chunk("\n"));

                    Paragraph customername = new Paragraph($"Name:- {customerDetails_With_Purchased_Stock_Model.Customers.CustomerName}", new iTextSharp.text.Font(boldfont, 19));
                    customername.Alignment = Element.ALIGN_LEFT;
                    document.Add(customername);
                    document.Add(new Chunk("\n"));

                    Paragraph customer_type = new Paragraph($"Type:- {customerDetails_With_Purchased_Stock_Model.Customers.CustomerType}", new iTextSharp.text.Font(boldfont, 19));
                    customer_type.Alignment = Element.ALIGN_LEFT;
                    document.Add(customer_type);
                    document.Add(new Chunk("\n"));

                    Paragraph customer_phone = new Paragraph($"Contact:- {customerDetails_With_Purchased_Stock_Model.Customers.CustomerContact}", new iTextSharp.text.Font(boldfont, 19));
                    customer_phone.Alignment = Element.ALIGN_LEFT;
                    document.Add(customer_phone);
                    document.Add(new Chunk("\n"));

                    Paragraph customer_address = new Paragraph($"Address:- {customerDetails_With_Purchased_Stock_Model.Customers.CustomerAddress}", new iTextSharp.text.Font(boldfont, 19));
                    customer_type.Alignment = Element.ALIGN_LEFT;
                    document.Add(customer_address);
                    document.Add(new Chunk("\n"));


                    // Set Back Image

                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");



                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF



                    // Check Customer Type 

                    if (Customer_Type == "BUYER")
                    {


                        PdfPTable pdfTable = new PdfPTable(Statements_Info.Columns.Count);

                        pdfTable.WidthPercentage = 100;

                        pdfTable.DefaultCell.Padding = 10;

                        // Set the same width for all columns
                        pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, Statements_Info.Columns.Count).ToArray());

                        foreach (DataColumn column in Statements_Info.Columns)
                        {
                            PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 15)));
                            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCell.Padding = 10;
                            pdfTable.AddCell(headerCell);

                            /*  if (column.ColumnName == "GRAIN_NAME")
                              {
                                  PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                                  productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                  productCell.Padding = 10;
                                  pdfTable.AddCell(productCell);
                              }*/
                        }

                        foreach (DataRow row in Statements_Info.Rows)
                        {
                            foreach (DataColumn column in Statements_Info.Columns)
                            {
                                var item = row[column];

                                if (item is DateTime dateTimeValue)
                                {
                                    PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 15)));
                                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    dataCell.Padding = 10;
                                    pdfTable.AddCell(dataCell);
                                    // If the column is a DateTime, format it to display only the date
                                }
                                else if (column.ColumnName == "Product" && item is string productName)
                                {
                                    PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 15)));
                                    productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    productTypeCell.Padding = 10;
                                    pdfTable.AddCell(productTypeCell);
                                }
                                else
                                {
                                    PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 15)));
                                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    dataCell.Padding = 10;
                                    pdfTable.AddCell(dataCell);
                                }
                            }
                        }


                        document.Add(pdfTable);

                    }
                    else if (Customer_Type == "SELLER")
                    {

                        PdfPTable pdfTable = new PdfPTable(Sale_Info.Columns.Count);

                        pdfTable.WidthPercentage = 100;

                        pdfTable.DefaultCell.Padding = 10;


                        foreach (DataColumn column in Sale_Info.Columns)
                        {
                            PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 15)));
                            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            headerCell.Padding = 10;
                            pdfTable.AddCell(headerCell);

                            /*  if (column.ColumnName == "GRAIN_NAME")
                              {
                                  PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                                  productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                  productCell.Padding = 10;
                                  pdfTable.AddCell(productCell);
                              }*/
                        }

                        foreach (DataRow row in Sale_Info.Rows)
                        {
                            foreach (DataColumn column in Sale_Info.Columns)
                            {
                                var item = row[column];

                                if (item is DateTime dateTimeValue)
                                {
                                    PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 15)));
                                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    dataCell.Padding = 10;
                                    pdfTable.AddCell(dataCell);
                                    // If the column is a DateTime, format it to display only the date
                                }
                                else if (column.ColumnName == "Product" && item is string productName)
                                {
                                    PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 15)));
                                    productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    productTypeCell.Padding = 10;
                                    pdfTable.AddCell(productTypeCell);
                                }
                                else
                                {
                                    PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 15)));
                                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    dataCell.Padding = 10;
                                    pdfTable.AddCell(dataCell);
                                }
                            }
                        }

                        document.Add(pdfTable);
                    }



















                }



                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Method : Download Excel For Customers Account & Account Details Statements

        public byte[] CustomersStatementEXCEL()
        {
            List<Customer_Model> customer_Models = customers_DALBase.GetAllCustomers();

            DataTable dataTable = ListToDataTableConverterForCustomersAccount(customer_Models);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Customers");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }
        }

        public byte[] CustomerAccountStatementEXCEL(int Customer_ID, string Customer_Type)
        {
            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_Model = customers_DALBase.RetrieveAccountDetails(Customer_ID, Customer_Type);
            (DataTable Customer_info, DataTable Statements_Info, DataTable Sale_Info) = ListToDataTableConverterForCustomersAccountDetails(customerDetails_With_Purchased_Stock_Model);

            using (XLWorkbook wb = new XLWorkbook())
            {
                string customer_name = customerDetails_With_Purchased_Stock_Model.Customers.CustomerName;
                string customer_type = customerDetails_With_Purchased_Stock_Model.Customers.CustomerType;

                IXLWorksheet ws = wb.Worksheets.Add($"{customer_name}_{customer_type}_Account");

                // Adding the Customer Account Table to the worksheet
                var customerTableRange = ws.Cell(1, 1).InsertTable(Customer_info, "CustomerDetails", true).AsRange();
                FormatTable(customerTableRange);

                // Adding a blank row after the first table
                int nextRow = customerTableRange.LastRow().RowNumber() + 2;

                // Adding the Transactions Table to the worksheet
                IXLRange transactionsTableRange;
                if (Customer_Type == "BUYER")
                {
                    transactionsTableRange = ws.Cell(nextRow, 1).InsertTable(Statements_Info, "Transactions", true).AsRange();
                    FormatTable(transactionsTableRange);

                }
                else if (Customer_Type == "SELLER")
                {
                    transactionsTableRange = ws.Cell(nextRow, 1).InsertTable(Sale_Info, "Sales", true).AsRange();
                    FormatTable(transactionsTableRange);

                }

                // Adjust column widths to fit contents for the entire worksheet
                ws.Columns().AdjustToContents();

                void FormatTable(IXLRange tableRange)
                {
                    // Apply styling to header row
                    var headerRow = tableRange.FirstRow();
                    headerRow.Style.Font.Bold = true;
                    headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRow.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    headerRow.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Apply style to all rows including headers
                    foreach (var row in tableRange.RowsUsed())
                    {
                        row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        row.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        ws.Row(row.RowNumber()).Height = 20; // Set the height for the row

                        foreach (var cell in row.CellsUsed())
                        {
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }


        #endregion

        #region Method : Download PDF For Purchase Stocks Statement

        public byte[] PurchaseStocksStatementPDF()
        {
            List<Purchase_Stock> purchase_Stocks = stock_DALBase.DisplayAllPurchaseStock();

            DataTable dataTable = ListToDataTableConverterForPurchaseStockStatement(purchase_Stocks);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Define fonts
                    BaseFont boldBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");
                    backimage.ScaleToFit(500, 500);
                    backimage.SetAbsolutePosition(900, 400);
                    document.Add(backimage);

                    // Table setup
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count)
                    {
                        WidthPercentage = 100,
                        DefaultCell = { Padding = 10 }
                    };

                    // Headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Font headerFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 10
                        };
                        pdfTable.AddCell(headerCell);
                    }

                    // Data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];
                            Font itemFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;

                            PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), itemFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 10
                            };
                            pdfTable.AddCell(dataCell);
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();
                }

                // Get the current date for the file name






                return memoryStream.ToArray();
            }

        }

        #endregion

        #region Method : Download Excel Purchase Stocks Statements 

        public byte[] PurchaseStocksStatementEXCEL()
        {
            List<Purchase_Stock> purchase_Stocks = stock_DALBase.DisplayAllPurchaseStock();

            DataTable dataTable = ListToDataTableConverterForPurchaseStockStatement(purchase_Stocks);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Stock Statements");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }

        }

        #endregion


        #region Method : Download PDF for Sale Statements

        public byte[] SalesStatementPDF()
        {
            List<Show_Sale> show_Sales = sales_DALBase.GetAllSales();

            DataTable dataTable = ListToDataTableConverterForSalesStatement(show_Sales);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    // Define fonts
                    BaseFont boldBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Sales Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");
                    backimage.ScaleToFit(500, 500);
                    backimage.SetAbsolutePosition(900, 400);
                    document.Add(backimage);

                    // Table setup
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count)
                    {
                        WidthPercentage = 100,
                        DefaultCell = { Padding = 10 }
                    };

                    // Headers
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Font headerFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 10
                        };
                        pdfTable.AddCell(headerCell);
                    }

                    // Data rows
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];
                            Font itemFont = column.ColumnName.Equals("Product", StringComparison.InvariantCultureIgnoreCase) ? gujaratiFont : boldFont;

                            PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), itemFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER,
                                Padding = 10
                            };
                            pdfTable.AddCell(dataCell);
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();
                }

              






                return memoryStream.ToArray();
            }

        }

        #endregion

        #region Method : Download Excel for Sale Statements

        public byte[] SalesStatementEXCEL()
        {
            List<Show_Sale> show_Sales = sales_DALBase.GetAllSales();

            DataTable dataTable = ListToDataTableConverterForSalesStatement(show_Sales);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Sales Statement");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }
        }

        #endregion

        #region Method : Download Purchase Invoice 

        public byte[] Purchase_InvoiceCreate_PDf(Purchase_Invoice_Model invoiceModel)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Create the document
                using (Document document = new Document(iTextSharp.text.PageSize.A4, 0, 0, 0, 0))
                {
                    // Create PdfWriter instance
                    using (PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream))
                    {
                        // Open the document
                        document.Open();

                        // Get PdfContentByte
                        PdfContentByte contentByte = pdfWriter.DirectContent;



                        BaseFont defaultfont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        iTextSharp.text.Font dfont = new iTextSharp.text.Font(defaultfont, 18);

                        BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                        iTextSharp.text.Font bfont = new iTextSharp.text.Font(boldfont, 18);


                        BaseFont inrfont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/Rupee.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
                        iTextSharp.text.Font ifont = new iTextSharp.text.Font(inrfont, 18);


                        BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED, true);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(gujaratifont, 18);
                        font.Color = new BaseColor(System.Drawing.Color.Red);




                        contentByte.BeginText();

                        contentByte.SetFontAndSize(gujaratifont, 14);
                        contentByte.SetTextMatrix(250, 820); // X, Y position
                        contentByte.ShowText("|| ગણેશાય નમઃ ||"); //header

                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(460, 810); // X, Y position
                        contentByte.ShowText("Mo: +91 98254 22091");

                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(460, 790); // X, Y position
                        contentByte.ShowText("Mo: +91 94277 23092");

                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Logo.png");

                        iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");


                        image.ScaleToFit(60, 60); // Adjust width and height

                        image.SetAbsolutePosition(275, 765); // Position Of Image

                        document.Add(image); // Add the image to the PDF


                        backimage.ScaleToFit(300, 300); // Adjust width and height 

                        backimage.SetAbsolutePosition(145, 230); // Position Of Image



                        document.Add(backimage); // Add the image to the PDF




                        contentByte.SetFontAndSize(boldfont, 20);
                        contentByte.SetTextMatrix(155, 725); // X, Y position
                        contentByte.ShowText("Shree Ganesh Agro Industries"); // name

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(165, 690); // X, Y position
                        contentByte.ShowText("Address:- GIDC Plot No.36,Porbandar Road,"); // address

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(260, 670); // X, Y position
                        contentByte.ShowText("Upleta(Dist.Rajkot)360-490."); // address of city

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(275, 640); // X, Y position
                        contentByte.ShowText("Invoice"); // invoice


                        DateTime? invoiceDate = invoiceModel.PurchaseInvoiceDate;
                        DateTime? onlyDate = invoiceDate?.Date;

                        string? formattedDate = onlyDate?.ToString("dd-MM-yyyy");

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(450, 600); // X, Y position
                        contentByte.ShowText("Date: " + formattedDate); // date


                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(25, 565); // X, Y position
                        contentByte.ShowText("Farmer Name: -"); // farmer


                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(150, 568); // X, Y position
                        contentByte.ShowText(invoiceModel.CustomerName); // farmer name


                        // -- Table Content -- //

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(25, 515); // X, Y position
                        contentByte.ShowText("No.");  // No.



                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(65, 515); // X, Y position
                        contentByte.ShowText("Product Name"); // product name



                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(190, 515); // X, Y position
                        contentByte.ShowText("Bags"); // bags 


                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(245, 525); // X, Y position
                        contentByte.ShowText("Bags"); // pbags +

                        //+//

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(245, 505); // X, Y position
                        contentByte.ShowText("Per Kg"); // per kg


                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(310, 515); // X, Y position
                        contentByte.ShowText("Weight"); // bags 

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(385, 525); // X, Y position
                        contentByte.ShowText("Product"); // product +

                        //+//

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(385, 508); // X, Y position
                        contentByte.ShowText("Price(  )"); // pr price

                        //+//
                        contentByte.SetFontAndSize(inrfont, 15);
                        contentByte.SetTextMatrix(427, 506); // X, Y position
                        contentByte.ShowText("K"); // ₹ symbol added

                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(470, 515); // X, Y position
                        contentByte.ShowText("Total Price(  )"); // total price 

                        //+//
                        contentByte.SetFontAndSize(inrfont, 15);
                        contentByte.SetTextMatrix(552, 515); // X, Y position
                        contentByte.ShowText("K"); // ₹ symbol added


                        // ---- Details Invoice ---= //


                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(27, 475); // X, Y position
                        contentByte.ShowText("1."); // No.of items +

                        contentByte.SetFontAndSize(gujaratifont, 14);
                        contentByte.SetTextMatrix(90, 475); // X, Y position
                        contentByte.ShowText(Convert.ToString(invoiceModel.ProductName)); //  Product Type

                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(200, 475); // X, Y position


                        contentByte.ShowText(!string.IsNullOrWhiteSpace(Convert.ToString(invoiceModel.Bags)) ? Convert.ToString(invoiceModel.Bags) : "--");






                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(260, 475); // X, Y position
                        contentByte.ShowText(!string.IsNullOrWhiteSpace(Convert.ToString(invoiceModel.BagPerKg)) ? Convert.ToString(invoiceModel.BagPerKg) : "--");



                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(316, 475); // X, Y position
                        contentByte.ShowText(invoiceModel.TotalWeight.ToString()); // weight


                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(400, 475); // X, Y position
                        contentByte.ShowText(invoiceModel.ProductPrice.ToString()); // product price

                        contentByte.SetFontAndSize(boldfont, 13);
                        contentByte.SetTextMatrix(490, 475); // X, Y position
                        contentByte.ShowText(invoiceModel.TotalPrice.ToString()); // total price


                        // ------xxxxxxxxxxx------ //


                        //  -- footer table content -- //



                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(490, 105); // X, Y position
                        contentByte.ShowText(invoiceModel.TotalPrice.ToString()); // final total


                        contentByte.SetFontAndSize(boldfont, 15);
                        contentByte.SetTextMatrix(355, 105); // X, Y position
                        contentByte.ShowText("Final Total(  )"); // final total title


                        contentByte.SetFontAndSize(inrfont, 15);
                        contentByte.SetTextMatrix(435, 105); // X, Y position
                        contentByte.ShowText("K"); // ₹ symbol added



                        // --- main footwer -- //


                        contentByte.SetFontAndSize(boldfont, 14);
                        contentByte.SetTextMatrix(490, 60); // X, Y position
                        contentByte.ShowText("(Signature)."); // sign.


                        contentByte.SetFontAndSize(boldfont, 14);
                        contentByte.SetTextMatrix(445, 40); // X, Y position
                        contentByte.ShowText("Bhavesh S. Kachhela"); // sign name

                        contentByte.SetFontAndSize(boldfont, 14);
                        contentByte.SetTextMatrix(225, 15); // X, Y position
                        contentByte.ShowText("Thanks For Selling Us!."); // thanks you label 





                        contentByte.EndText();  // ---- Text End ---- //

                        // ---- Design Format Of Invoice ---- //

                        //-- Name Above line --//
                        contentByte.MoveTo(0, 750); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(600, 750); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        //-- Name Below line --//
                        contentByte.MoveTo(0, 710); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(600, 710); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        //-- Invoice Above line --//
                        contentByte.MoveTo(0, 660); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(600, 660); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        //-- Invoice Below line --//
                        contentByte.MoveTo(0, 630); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(600, 630); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        //-- Farmer Name Right line --//
                        contentByte.MoveTo(145, 562); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(450, 562); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        // -- Table Design -- //


                        // ------ //

                        contentByte.MoveTo(20, 540); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(575, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        /*    *//*
                             * |
                             * | |||| 1
                             * |
                             * |
                             *//**/

                        contentByte.MoveTo(575, 95); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(575, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();
                        /*
                                                *//*
                                                * |
                                                * ||||| 2
                                                * |
                                                * |
                                                *//**/

                        contentByte.MoveTo(20, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(20, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        // ------ // - 2 

                        contentByte.MoveTo(20, 500); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(575, 500); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        /*         *//*
                                 * |
                                 * ||||| 3
                                 * |
                                 * |
                                 *//**/

                        contentByte.MoveTo(55, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(55, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        /*  *//*
                           * |
                           * ||||| 4
                           * |
                           * |
                           *//**/

                        contentByte.MoveTo(180, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(180, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        /**//*
                         * |
                         * ||||| 5
                         * |
                         * |
                         *//**/

                        contentByte.MoveTo(240, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(240, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        /*  *//*
                           * |
                           * ||||| 6
                           * |
                           * |
                           *//**/

                        contentByte.MoveTo(300, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(300, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        /*  *//*
                          * |
                          * ||||| 7
                          * |
                          * |
                          *//**/

                        contentByte.MoveTo(375, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(375, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();

                        /**//*
                      * |
                      * ||||| 8
                      * |
                      * |
                      *//**/

                        contentByte.MoveTo(460, 95); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(460, 540); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        // ------ // - 3

                        contentByte.MoveTo(20, 125); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(575, 125); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        // ------ // - 4

                        contentByte.MoveTo(460, 95); // Starting point (x, y) x--> starting line start  
                        contentByte.LineTo(575, 95); // Ending point (x, y) x--> straight line 
                        contentByte.Stroke();


                        // Close the document
                        document.Close();
                    }
                }



                return memoryStream.ToArray();


            }
        }

        #endregion


        #region Method : Download PDF of Pending Payments

        public byte[] PendingPaymentsPDF()
        {
            List<Pending_Customers_Payments> pending_payments = payment_DALBase.GetPendingCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForPendingPayments(pending_payments);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 100f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



                    // Add title
                    Paragraph title = new Paragraph("Pending Payments List", new iTextSharp.text.Font(boldfont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);


                    document.Add(new Chunk("\n"));

                    // Add a line break


                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

                    pdfTable.WidthPercentage = 100;

                    pdfTable.DefaultCell.Padding = 10;






                    // Set the same width for all columns
                    pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");


                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF






                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.Padding = 10;
                        pdfTable.AddCell(headerCell);

                        /*  if (column.ColumnName == "GRAIN_NAME")
                          {
                              PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                              productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                              productCell.Padding = 10;
                              pdfTable.AddCell(productCell);
                          }*/
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];

                            if (item is DateTime dateTimeValue)
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                                // If the column is a DateTime, format it to display only the date
                            }
                            else if (column.ColumnName == "Product" && item is string productName)
                            {
                                PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 12)));
                                productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                productTypeCell.Padding = 10;
                                pdfTable.AddCell(productTypeCell);
                            }
                            else
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                            }
                        }
                    }

                    // Add data rows


                    document.Add(pdfTable);
                }

                // Get the current date for the file name






                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Method : Download Excel of Pending Payments

        public byte[] PendingPaymentsEXCEL()
        {
            List<Pending_Customers_Payments> pending_payments = payment_DALBase.GetPendingCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForPendingPayments(pending_payments);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Pending Payments");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }
        }

        #endregion

        #region Method : Download PDF of Remain Payments

        public byte[] RemainPaymentsPDF()
        {
            List<Remain_Payment_Model> remain_payments = payment_DALBase.GetRemainingCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForRemainPayments(remain_payments);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 100f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



                    // Add title
                    Paragraph title = new Paragraph("Pending Payments List", new iTextSharp.text.Font(boldfont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);


                    document.Add(new Chunk("\n"));

                    // Add a line break


                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

                    pdfTable.WidthPercentage = 100;

                    pdfTable.DefaultCell.Padding = 10;






                    // Set the same width for all columns
                    pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");



                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF






                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.Padding = 10;
                        pdfTable.AddCell(headerCell);

                        /*  if (column.ColumnName == "GRAIN_NAME")
                          {
                              PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                              productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                              productCell.Padding = 10;
                              pdfTable.AddCell(productCell);
                          }*/
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];

                            if (item is DateTime dateTimeValue)
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                                // If the column is a DateTime, format it to display only the date
                            }
                            else if (column.ColumnName == "Product" && item is string productName)
                            {
                                PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 12)));
                                productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                productTypeCell.Padding = 10;
                                pdfTable.AddCell(productTypeCell);
                            }
                            else
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                            }
                        }
                    }

                    // Add data rows


                    document.Add(pdfTable);
                }

                // Get the current date for the file name






                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Method : Download Excel of Remain Payments

        public byte[] RemainPaymentsEXCEL()
        {
            List<Remain_Payment_Model> remain_payments = payment_DALBase.GetRemainingCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForRemainPayments(remain_payments);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Remain Payments");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }
        }

        #endregion

        #region Method : Download PDF of Paid Payments

        public byte[] PaidPaymentsPDF()
        {
            List<Show_Payment_Info> paid_payments = payment_DALBase.GetPaidCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForPaidPayments(paid_payments);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 100f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



                    // Add title
                    Paragraph title = new Paragraph("Paid Payments List", new iTextSharp.text.Font(boldfont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);


                    document.Add(new Chunk("\n"));

                    // Add a line break


                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

                    pdfTable.WidthPercentage = 100;

                    pdfTable.DefaultCell.Padding = 10;






                    // Set the same width for all columns
                    pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://stock-manage-api-shree-ganesh-agro-ind.somee.com/Images/Backimg.png");



                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF






                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        headerCell.Padding = 10;
                        pdfTable.AddCell(headerCell);

                        /*  if (column.ColumnName == "GRAIN_NAME")
                          {
                              PdfPCell productCell = new PdfPCell(new Phrase("GRAIN_NAME", new iTextSharp.text.Font(gujaratifont, 12)));
                              productCell.HorizontalAlignment = Element.ALIGN_CENTER;
                              productCell.Padding = 10;
                              pdfTable.AddCell(productCell);
                          }*/
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            var item = row[column];

                            if (item is DateTime dateTimeValue)
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                                // If the column is a DateTime, format it to display only the date
                            }
                            else if (column.ColumnName == "Product" && item is string productName)
                            {
                                PdfPCell productTypeCell = new PdfPCell(new Phrase(productName, new iTextSharp.text.Font(gujaratifont, 12)));
                                productTypeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                productTypeCell.Padding = 10;
                                pdfTable.AddCell(productTypeCell);
                            }
                            else
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 12)));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                dataCell.Padding = 10;
                                pdfTable.AddCell(dataCell);
                            }
                        }
                    }

                    // Add data rows


                    document.Add(pdfTable);
                }

                // Get the current date for the file name






                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Method : Download Excel of Paid Payments

        public byte[] PaidPaymentsEXCEL()
        {
            List<Show_Payment_Info> paid_payments = payment_DALBase.GetPaidCustomersPayments();

            DataTable dataTable = ListToDataTableConverterForPaidPayments(paid_payments);

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet ws = wb.Worksheets.Add("Paid Payments");

                // Adding the DataTable data to the worksheet starting from cell A1
                var tableRange = ws.Cell(1, 1).InsertTable(dataTable, true).AsRange();

                // Adjust column widths to fit contents
                ws.Columns().AdjustToContents();

                // Apply styling to header row
                var headerRow = tableRange.FirstRow();
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Set the row height and cell alignment for data rows
                foreach (var row in tableRange.RowsUsed().Skip(1))
                {
                    ws.Row(row.RowNumber()).Height = 20; // Set the height for the row
                    foreach (var cell in row.CellsUsed())
                    {
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    }
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);


                    return stream.ToArray();
                }
            }
        }

        #endregion

    }
}
