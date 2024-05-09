using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Data;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DownloadController : Controller
    {
        private readonly Download_BALbase download_BALbase = new Download_BALbase();

        private readonly Customers_DALBase customers_DALBase = new Customers_DALBase();


        #region Section : Download Invoice PDF & Excel

        #region Download PDF & Excel For Purchase Invoice

        [HttpGet]
        public IActionResult PurchaseInvoiceStatementPDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PurchaseInvoiceStatementPDF();

                // Set the filename for the PDF
                string fileName = "Purchase-Invoice-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }


        [HttpGet]
        public IActionResult PurchaseInvoiceStatementEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PurchaseInvoiceStatementEXCEL();

                // Set the filename for the PDF
                string fileName = "Purchase-Invoice-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #region Download PDF & Excel For Sales Invoice

        [HttpGet]
        public IActionResult SalesInvoiceStatementPDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.SalesInvoiceStatementPDF();

                // Set the filename for the PDF
                string fileName = "Sales-Invoice-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult SalesInvoiceStatementEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.SalesInvoiceStatementEXCEL();

                // Set the filename for the PDF
                string fileName = "Sales-Invoice-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion



        #endregion


        #region  Section : Download Customers Statement & Customer Account Statements PDF & Excel

        #region Method : Download PDF & Excel Customers Statements

        [HttpGet]
        public IActionResult CustomersStatementPDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.CustomersStatementPDF();

                // Set the filename for the PDF
                string fileName = "Customers-Account-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult CustomersStatementEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.CustomersStatementEXCEL();

                // Set the filename for the PDF
                string fileName = "Customers-Statements-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #region Method : Download PDF & Excel Customer Account Statements

        [HttpGet("{Customer_ID}&{Customer_Type}")]
        public IActionResult CustomerAccountStatementPDF(int Customer_ID, string Customer_Type)
        {
            var customerDetails = customers_DALBase.CustomerByIDAndType(Customer_ID, Customer_Type);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.CustomerAccountStatementPDF(Customer_ID, Customer_Type);

                // Set the filename for the PDF
                string fileName = $"{customerDetails.CustomerName}-{Customer_Type}-Account-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet("{Customer_ID}&{Customer_Type}")]
        public IActionResult CustomerAccountStatementEXCEL(int Customer_ID, string Customer_Type)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                var customerDetails = customers_DALBase.CustomerByIDAndType(Customer_ID, Customer_Type);

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.CustomerAccountStatementEXCEL(Customer_ID,Customer_Type);

                // Set the filename for the PDF
                string fileName = $"{customerDetails.CustomerName}-{Customer_Type}-Account-Statement.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }




        #endregion

        #endregion



        #region Section : Download Purchase Stock Statement PDF & Excel

        [HttpGet]
        public IActionResult PurchaseStocksStatementPDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PurchaseStocksStatementPDF();

                // Set the filename for the PDF
                string fileName = "Purchase-Stocks-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }


        [HttpGet]
        public IActionResult PurchaseStocksStatementEXCEL()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PurchaseStocksStatementEXCEL();

                // Set the filename for the PDF
                string fileName = "Purchase-Stocks-Statement.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }


        #endregion


        #region Section : Download Sales Statement PDF & Excel 

        [HttpGet]
        public IActionResult SalesStatementPDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.SalesStatementPDF();

                // Set the filename for the PDF
                string fileName = "Sales-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }


        [HttpGet]
        public IActionResult SalesStatementEXCEL()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.SalesStatementEXCEL();

                // Set the filename for the PDF
                string fileName = "Sales-Statement.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion


        #region Section : Download Payments Statements 

        #region Method : Download Pending Payments  PDF & Excel


        [HttpGet]
        public IActionResult PendingPaymentsPDF()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PendingPaymentsPDF();

                // Set the filename for the PDF
                string fileName = "Pending-Payments-List.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult PendingPaymentsEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PendingPaymentsEXCEL();

                // Set the filename for the PDF
                string fileName = "Pending-Payments-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion


        #region Method : Download Remain Payments  PDF & Excel


        [HttpGet]
        public IActionResult RemainPaymentsPDF()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.RemainPaymentsPDF();

                // Set the filename for the PDF
                string fileName = "Remain-Payments-List.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult RemainPaymentsEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.RemainPaymentsEXCEL();

                // Set the filename for the PDF
                string fileName = "Remain-Payments-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #region Method : Download Paid Payments  PDF & Excel


        [HttpGet]
        public IActionResult PaidPaymentsPDF()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PaidPaymentsPDF();

                // Set the filename for the PDF
                string fileName = "Paid-Payments-List.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult PaidPaymentsEXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.PaidPaymentsEXCEL();

                // Set the filename for the PDF
                string fileName = "Paid-Payments-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #endregion











    }
}
