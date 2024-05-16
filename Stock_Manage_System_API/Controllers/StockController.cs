using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// Controller for managing stock-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class StockController : Controller
    {
        private readonly Stock_BALBase _stock_BAL;

        /// <summary>
        /// Initializes a new instance of the StockController.
        /// </summary>
        public StockController()
        {
            _stock_BAL = new Stock_BALBase();
        }

        #region Section: Display All Purchase Stocks

        /// <summary>
        /// Retrieves all purchase stock entries.
        /// </summary>
        /// <returns>An IActionResult containing either the list of stocks or a not found error.</returns>
        [HttpGet]
        [Authorize]
        public IActionResult GetAllPurchaseStocks()
        {
            List<Purchase_Stock>? stocks_List = _stock_BAL.DisplayAllPurchaseStock();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (stocks_List?.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", stocks_List);
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

        #region Section: Delete Purchase Stock

        /// <summary>
        /// Deletes a purchase stock entry based on the transaction ID.
        /// </summary>
        /// <param name="TN_ID">Transaction ID of the purchase stock to delete.</param>
        /// <returns>An IActionResult indicating whether the deletion was successful.</returns>
        [HttpDelete]
        public IActionResult RemovePurchaseStock(int TN_ID)
        {
            bool is_Success = _stock_BAL.DeletePurchaseStock(TN_ID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Deleted Successfully");
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

        #region Section: Insert Purchase Stock With Customer Details

        /// <summary>
        /// Inserts a new purchase stock entry.
        /// </summary>
        /// <param name="model">The purchase stock model to insert.</param>
        /// <returns>An IActionResult indicating whether the insertion was successful.</returns>
        [HttpPost]
        public IActionResult AddPurchaseStockWithCustomerDetails(Purchase_Stock_With_Customer_Model model)
        {
            bool is_Success = _stock_BAL.InsertPurchaseStock(model);
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

        #region Section: Update Purchase Stock Details

        /// <summary>
        /// Updates an existing purchase stock entry.
        /// </summary>
        /// <param name="stock">The purchase stock model to update.</param>
        /// <returns>An IActionResult indicating whether the update was successful.</returns>
        [HttpPut]
        public IActionResult UpdatePurchaseStock(Purchase_Stock_With_Customer_Model stock)
        {
            bool is_Success = _stock_BAL.UpdatePurchaseStock(stock);
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

        #region Section: Purchase Stock By Stock ID

        /// <summary>
        /// Retrieves a purchase stock entry by its ID.
        /// </summary>
        /// <param name="Stock_ID">The ID of the stock to retrieve.</param>
        /// <returns>An IActionResult containing the stock data or a not found error.</returns>
        [HttpGet("{Stock_ID}")]
        public IActionResult GetPurchaseStockByID(int Stock_ID)
        {
            Purchase_Stock? show_Purchase_Stock = _stock_BAL.PurchaseStockByID(Stock_ID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (show_Purchase_Stock != null && show_Purchase_Stock.PurchaseStockId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", show_Purchase_Stock);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "No Data");
                return NotFound(response);
            }
        }

        #endregion

        #region Section: Fetch Stock And Customer Information By Those IDs

        /// <summary>
        /// Fetches stock and customer details by their IDs.
        /// </summary>
        /// <param name="Stock_ID">The stock ID to search for.</param>
        /// <param name="Customer_ID">The customer ID to search for.</param>
        /// <returns>An IActionResult containing the combined details of stock and customer or a not found error.</returns>

        [HttpGet("{Stock_ID}&{Customer_ID}")]
        public IActionResult GetPurchaseStockAndCustomerDetails(int Stock_ID, int Customer_ID)
        {
            Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model = _stock_BAL.Fetch_Stock_And_Customer_Details(Stock_ID, Customer_ID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (purchase_Stock_With_Customer_Model != null && purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockId != 0 && purchase_Stock_With_Customer_Model.Customers_Model.CustomerId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", purchase_Stock_With_Customer_Model);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "No Data");
                return NotFound(response);
            }
        }
        #endregion
    }
}