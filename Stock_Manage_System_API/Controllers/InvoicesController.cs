using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class InvoicesController : Controller
    {

        private readonly Invoices_BALBase _invoice_BAL;

        /// <summary>
        /// Initializes a new instance of the StockController.
        /// </summary>
        public InvoicesController()
        {
            _invoice_BAL = new Invoices_BALBase();
        }

        #region Area: Sale Invoice

        #region Section: Display All Sale Invoices

        /// <summary>
        /// Retrieves all sale invoice entries.
        /// </summary>
        /// <returns>An IActionResult containing either the list of sale invoice or a not found error.</returns>
        [HttpGet]
        public IActionResult GetAllSaleInvoices()
        {
            List<InvoicesModel.Sales_Invoice_Model>? sales_Invoices = _invoice_BAL.DisplayAllSaleInvoices();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (sales_Invoices?.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", sales_Invoices);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion

        #region Section: Insert Sale Invoice Details

        /// <summary>
        /// Inserts a new Sale Invoice entry.
        /// </summary>
        /// <param name="sales_Invoice">The sale invoice model to insert.</param>
        /// <returns>An IActionResult indicating whether the insertion was successful.</returns>

        [HttpPost]

        public IActionResult AddSaleInvoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {

            bool is_Success = _invoice_BAL.InsertSaleInvoice(sales_Invoice);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Inserted Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #region Section: Delete Sale Invoice

        /// <summary>
        /// Deletes a Sale invoice entry based on the Invoice ID.
        /// </summary>
        /// <param name="Sales_Invoice_ID">Sale Invoice ID of the Sale Invoice to delete.</param>
        /// <returns>An IActionResult indicating whether the deletion was successful.</returns>

        [HttpDelete]

        public IActionResult DeleteSaleInvoice(int Sale_Invoice_ID)
        {

            bool is_Success = _invoice_BAL.DeleteSaleInvoice(Sale_Invoice_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Delete Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #region Section: Sale Invoice By Invoice ID

        /// <summary>
        /// Retrieves a Sale invoice Information by its ID.
        /// </summary>
        /// <param name="Sale_Invoice_ID">The ID of the sale invoice infromation to retrieve.</param>
        /// <returns>An IActionResult containing the sale invoice data or a not found error.</returns>

        [HttpGet("{Sale_Invoice_ID}")]

        public IActionResult GetSaleInvoiceByID(int Sale_Invoice_ID)
        {

            InvoicesModel.Sales_Invoice_Model sale_Invoice_Model = _invoice_BAL.SaleInvoiceByID(Sale_Invoice_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (sale_Invoice_Model.SalesInvoiceId != 0 && sale_Invoice_Model != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", sale_Invoice_Model);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);
            }

        }

        #endregion

        #region Section: Update Sale Invoice Details

        /// <summary>
        /// Updates an existing sale invoice information.
        /// </summary>
        /// <param name="sale_Invoice">The sale invoice model to update.</param>
        /// <returns>An IActionResult indicating whether the update was successful.</returns>

        [HttpPut]
        public IActionResult UpdateSaleInvoice(InvoicesModel.Sales_Invoice_Model sale_Invoice)
        {
            bool is_Success = _invoice_BAL.UpdateSaleInvoice(sale_Invoice);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #endregion

        #region Area: Purchase Invoice

        #region Section: Display All Purchase Invoices

        /// <summary>
        /// Retrieves all purchase invoice entries.
        /// </summary>
        /// <returns>An IActionResult containing either the list of purchase invoice or a not found error.</returns>
        [HttpGet]
        public IActionResult GetAllPurchaseInvoices()
        {
            List<InvoicesModel.Purchase_Invoice_Model>? purchase_Invoices = _invoice_BAL.DisplayAllPurchaseInvoices();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (purchase_Invoices?.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", purchase_Invoices);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion

        #region Section: Insert Purchase Stock With Customer Details

        /// <summary>
        /// Inserts a new Purchase Invoice entry.
        /// </summary>
        /// <param name="purchase_Invoice">The Purchase invoice model to insert.</param>
        /// <returns>An IActionResult indicating whether the insertion was successful.</returns>

        [HttpPost]

        public IActionResult AddPurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {

            bool is_Success = _invoice_BAL.InsertPurchaseInvoice(purchase_Invoice);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Inserted Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #region Section: Delete Purchase Invoice

        /// <summary>
        /// Deletes a Purchase invoice entry based on the Invoice ID.
        /// </summary>
        /// <param name="Purchase_Invoice_ID">Purchase Invoice ID of the Purchase Invoice to delete.</param>
        /// <returns>An IActionResult indicating whether the deletion was successful.</returns>

        [HttpDelete]

        public IActionResult DeletePurchaseInvoice(int Purchase_Invoice_ID)
        {

            bool is_Success = _invoice_BAL.DeletePurchaseInvoice(Purchase_Invoice_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Delete Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #region Section: Purchase Invoice By Invoice ID

        /// <summary>
        /// Retrieves a Purchase invoice Information by its ID.
        /// </summary>
        /// <param name="Purchase_Invoice_ID">The ID of the Purchase invoice infromation to retrieve.</param>
        /// <returns>An IActionResult containing the Purchase invoice data or a not found error.</returns>

        [HttpGet("{Purchase_Invoice_ID}")]

        public IActionResult GetPurchaseInvoiceByID(int Purchase_Invoice_ID)
        {

            InvoicesModel.Purchase_Invoice_Model purchase_Invoice_Model = _invoice_BAL.PurchasenInvoiceByID(Purchase_Invoice_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (purchase_Invoice_Model.PurchaseInvoiceId != 0 && purchase_Invoice_Model != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", purchase_Invoice_Model);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);
            }

        }

        #endregion

        #region Section: Update Purchase Invoice Details

        /// <summary>
        /// Updates an existing Purchase invoice information.
        /// </summary>
        /// <param name="purchase_Invoice">The Purchase invoice model to update.</param>
        /// <returns>An IActionResult indicating whether the update was successful.</returns>

        [HttpPut]
        public IActionResult UpdatePurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            bool is_Success = _invoice_BAL.UpdatePurchaseInvoice(purchase_Invoice);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }

        #endregion

        #endregion

    }
}