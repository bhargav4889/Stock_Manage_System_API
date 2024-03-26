using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Data;

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
        public IActionResult Purchase_Invoice_Statement_PDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Purchase_Invoice_Statement_PDF();

                // Set the filename for the PDF
                string fileName = "Purchase-Invoice-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }


        [HttpGet]
        public IActionResult Purchase_Invoice_Statement_EXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Purchase_Invoice_Statement_EXCEL();

                // Set the filename for the PDF
                string fileName = "Purchase-Invoice-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #region Download PDF & Excel For Sales Invoice

        [HttpGet]
        public IActionResult Sales_Invoice_Statement_PDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Sales_Invoice_Statement_PDF();

                // Set the filename for the PDF
                string fileName = "Sales-Invoice-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult Sales_Invoice_Statement_EXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Sales_Invoice_Statement_EXCEL();

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
        public IActionResult Customers_Statement_PDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Customers_Statement_PDF();

                // Set the filename for the PDF
                string fileName = "Customers-Account-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet]
        public IActionResult Customers_Statement_EXCEL()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Customers_Statement_EXCEL();

                // Set the filename for the PDF
                string fileName = "Customers-Statements-List.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        #endregion

        #region Method : Download PDF & Excel Customer Account Statements

        [HttpGet("{Customer_ID}")]
        public IActionResult Customer_Account_Statement_PDF(int Customer_ID)
        {
            var customerDetails = customers_DALBase.Customer_Info_By_PK(Customer_ID);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Customer_Account_Statement_PDF(Customer_ID);

                // Set the filename for the PDF
                string fileName = $"{customerDetails.CustomerName}-{customerDetails.CustomerType}-Account-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }

        [HttpGet("{Customer_ID}")]
        public IActionResult Customer_Account_Statement_EXCEL(int Customer_ID)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                var customerDetails = customers_DALBase.Customer_Info_By_PK(Customer_ID);

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Customer_Account_Statement_EXCEL(Customer_ID);

                // Set the filename for the PDF
                string fileName = $"{customerDetails.CustomerName}-{customerDetails.CustomerType}-Account-Statement.xlsx";

                // Return the file
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }




        #endregion

        #endregion



        #region Section : Download Purchase Stock Statement PDF & Excel

        [HttpGet]
        public IActionResult Purchase_Stocks_Statement_PDF()
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Custom page size and PDF generation code remains the same...

                // After generating the PDF into memoryStream

                // Prepare the memoryStream for reading
                byte[] content = download_BALbase.Purchase_Stocks_Statement_PDF();

                // Set the filename for the PDF
                string fileName = "Purchase-Stocks-Statement.pdf";

                // Return the file
                return File(content, "application/pdf", fileName);
            }
        }


        #endregion















    }
}
