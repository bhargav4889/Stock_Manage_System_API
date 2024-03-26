using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using static Stock_Manage_System_API.Models.InvoicesModel;
using Document = iTextSharp.text.Document;

namespace Stock_Manage_System_API.DAL
{
    public class Download_DALBase : DAL_Helpers
    {


        private readonly Invoices_DALBase invoices_DALBase = new Invoices_DALBase();

        private readonly Customers_DALBase customers_DALBase = new Customers_DALBase();

        private readonly Stock_DALBase stock_DALBase = new Stock_DALBase();


        #region Method : All List Models Convert Datatable 


        #region Method : Purchase Invoice Statement DataTable 

        public DataTable Convert_List_To_DataTable_For_Purchase_Invoice_Statement(List<Purchase_Invoice_Model> purchase_Invoices)
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
                row["Weight"] = invoice.TotalWeight;
                row["Total-Price"] = invoice.TotalPrice;
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

        public DataTable Convert_List_To_DataTable_For_Sale_Invoice_Statement(List<Sales_Invoice_Model> salesInvoices)
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
                row["Total-Price"] = invoice.TotalPrice;
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

        public DataTable Convert_List_To_DataTable_For_Customers_Account(List<Customer_Model> customer_Models)
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

        public (DataTable, DataTable) Convert_Model_To_DataTable_For_Customers_Account_Details_Statement(CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_)
        {


            DataTable Customers_Info = new DataTable();

            DataTable Statements = new DataTable();


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

            return (Customers_Info, Statements);
        }


        #endregion

        #region Method : Stock Statement DataTable

        public DataTable Convert_List_To_DataTable_For_Stock_Statement(List<Show_Purchase_Stock> purchase_Stocks)
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
                row["Bags"] = stock.Bags.HasValue ? stock.Bags.ToString() : "--";
                row["Bag-Per-Kg"] = stock.BagPerKg.HasValue ? stock.BagPerKg.ToString() : "--";
                row["Weight"] = stock.TotalWeight;
                row["Total-Price"] = stock.TotalPrice;
                row["Vehicle-Name"] = stock.VehicleName;
                row["Vehicle-No"] = stock.VehicleNo;
                row["Tolat"] = stock.TolatName;
                row["Driver-Name"] = stock.DriverName;
                row["Payment-Status"] = string.IsNullOrEmpty(stock.PaymentStatus) ? "PENDING" : stock.PaymentStatus;





                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        #endregion

        #endregion

        

        #region Method : Download PDf All Invoices Statements

        public byte[] Purchase_Invoice_Statement_PDF()
        {
            List<InvoicesModel.Purchase_Invoice_Model> Purchase_Invoices = invoices_DALBase.DISPLAY_ALL_PURCHASE_INVOICE();

            DataTable dataTable = Convert_List_To_DataTable_For_Purchase_Invoice_Statement(Purchase_Invoices);

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
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("D:\\Font\\NotoSansGujarati-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("C:\\Users\\bharg\\OneDrive\\Desktop\\Icons\\Backimg.png");
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

        public byte[] Sales_Invoice_Statement_PDF()
        {
            List<Sales_Invoice_Model> salesInvoices = invoices_DALBase.SHOW_ALL_SALES_INVOICES();

            // Convert to DataTable
            DataTable dataTable = Convert_List_To_DataTable_For_Sale_Invoice_Statement(salesInvoices);





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
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("D:\\Font\\NotoSansGujarati-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("C:\\Users\\bharg\\OneDrive\\Desktop\\Icons\\Backimg.png");
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

        public byte[] Purchase_Invoice_Statement_EXCEL()
        {
            List<InvoicesModel.Purchase_Invoice_Model> Purchase_Invoices = invoices_DALBase.DISPLAY_ALL_PURCHASE_INVOICE();

            DataTable dataTable = Convert_List_To_DataTable_For_Purchase_Invoice_Statement(Purchase_Invoices);

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

        public byte[] Sales_Invoice_Statement_EXCEL()
        {
            List<Sales_Invoice_Model> salesInvoices = invoices_DALBase.SHOW_ALL_SALES_INVOICES();

            // Convert to DataTable
            DataTable dataTable = Convert_List_To_DataTable_For_Sale_Invoice_Statement(salesInvoices);

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

        public byte[] Customers_Statement_PDF()
        {
            List<Customer_Model> customer_Models = customers_DALBase.SHOW_ALL_CUSTOMERS();

            DataTable dataTable = Convert_List_To_DataTable_For_Customers_Account(customer_Models);

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

                    BaseFont gujaratifont = BaseFont.CreateFont("D:\\Font\\NotoSansGujarati-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



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



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("C:\\Users\\bharg\\OneDrive\\Desktop\\Icons\\Backimg.png");



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

        public byte[] Customer_Account_Statement_PDF(int Customer_ID)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_Model = customers_DALBase.Account_Details(Customer_ID);

                (DataTable Customer_info, DataTable Statements_Info) = Convert_Model_To_DataTable_For_Customers_Account_Details_Statement(customerDetails_With_Purchased_Stock_Model);


                // Set your custom page size (width x height) in points
                iTextSharp.text.Rectangle customPageSize = new iTextSharp.text.Rectangle(2300, 1200);

                using (Document document = new Document(customPageSize))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();

                    float columnWidth = 200f; // Adjust this value based on your design preference


                    BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    BaseFont gujaratifont = BaseFont.CreateFont("D:\\Font\\NotoSansGujarati-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);



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










                    PdfPTable pdfTable = new PdfPTable(Statements_Info.Columns.Count);

                    pdfTable.WidthPercentage = 100;

                    pdfTable.DefaultCell.Padding = 10;






                    // Set the same width for all columns
                    pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, Statements_Info.Columns.Count).ToArray());



                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("C:\\Users\\bharg\\OneDrive\\Desktop\\Icons\\Backimg.png");



                    backimage.ScaleToFit(500, 500); // Adjust width and height 

                    backimage.SetAbsolutePosition(900, 400); // Position Of Image


                    document.Add(backimage); // Add the image to the PDF








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

                    // Add data rows


                    document.Add(pdfTable);
                }



                return memoryStream.ToArray();
            }
        }

        #endregion

        #region Method : Download Excel For Customers Account & Account Details Statements

        public byte[] Customers_Statement_EXCEL()
        {
            List<Customer_Model> customer_Models = customers_DALBase.SHOW_ALL_CUSTOMERS();

            DataTable dataTable = Convert_List_To_DataTable_For_Customers_Account(customer_Models);

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

        public byte[] Customer_Account_Statement_EXCEL(int Customer_ID)
        {
            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock_Model = customers_DALBase.Account_Details(Customer_ID);

            (DataTable Customer_info, DataTable Statements_Info) = Convert_Model_To_DataTable_For_Customers_Account_Details_Statement(customerDetails_With_Purchased_Stock_Model);
            using (XLWorkbook wb = new XLWorkbook())
            {
               
                string? customer_name = customerDetails_With_Purchased_Stock_Model.Customers.CustomerName;

                string? customer_type = customerDetails_With_Purchased_Stock_Model.Customers.CustomerType;



                IXLWorksheet ws = wb.Worksheets.Add($"{customer_name}_{customer_type}_Account");

                // Adding the Customer Account Table to the worksheet
                var customerTableRange = ws.Cell(1, 1).InsertTable(Customer_info, "CustomerDetails", true).AsRange();
                FormatTable(customerTableRange);

                // Adding a blank row after the first table
                int nextRow = customerTableRange.LastRow().RowNumber() + 2;

                // Adding the Transactions Table to the worksheet
                var transactionsTableRange = ws.Cell(nextRow, 1).InsertTable(Statements_Info, "Transactions", true).AsRange();
                FormatTable(transactionsTableRange);

                // Adjust column widths to fit contents for the entire worksheet
                ws.Columns().AdjustToContents();

                void FormatTable(IXLRange tableRange)
                {
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


                }
                // Add "Created by Stock Manage System" text




                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                   
                    return stream.ToArray();
                }
            }
        }

        #endregion

        #region Method : Download PDF For Purchase Stocks Statement

        public byte[] Purchase_Stocks_Statement_PDF()
        {
            List<Show_Purchase_Stock> purchase_Stocks = stock_DALBase.DISPLAY_ALL_PURCHASE_STOCK();

            DataTable dataTable = Convert_List_To_DataTable_For_Stock_Statement(purchase_Stocks);

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
                    BaseFont gujaratiBaseFont = BaseFont.CreateFont("D:\\Font\\NotoSansGujarati-Bold.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);
                    Font boldFont = new Font(boldBaseFont, 12);
                    Font gujaratiFont = new Font(gujaratiBaseFont, 12);

                    // Title
                    Paragraph title = new Paragraph("Statement", new Font(boldBaseFont, 35));
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Chunk("\n"));

                    // Image
                    iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("C:\\Users\\bharg\\OneDrive\\Desktop\\Icons\\Backimg.png");
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

        public byte[] Purchase_Stocks_Statement_EXCEL()
        {
            List<Show_Purchase_Stock> purchase_Stocks = stock_DALBase.DISPLAY_ALL_PURCHASE_STOCK();

            DataTable dataTable = Convert_List_To_DataTable_For_Stock_Statement(purchase_Stocks);

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

    }
}
