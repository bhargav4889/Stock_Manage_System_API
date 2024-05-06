using Azure;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL; // Business Logic Layer
using Stock_Manage_System_API.Models; // Data Models

namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {
        // Stock business logic handler
        private readonly Stock_BALBase _stock_BAL = new Stock_BALBase();


        #region Display All

        // Fetch all purchased stocks
        [HttpGet]
        public IActionResult Purchase_Stocks()
        {
            // Retrieve stocks
            List<Purchase_Stock>? stocks_List = _stock_BAL.DISPLAY_ALL_PURCHASE_STOCK();

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check stock existence
            if (stocks_List?.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!.");
                response.Add("data", stocks_List);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!.");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion

        #region DELETE 

        // Delete a purchased stock

        [HttpDelete]
        public IActionResult Delete_Purchase_Stock(int TN_ID)
        {
            // Delete operation
            bool is_Success = _stock_BAL.PURCHASE_STOCK_DELETE(TN_ID);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check delete success
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


        #region INSERT

        // Add new purchased stock
        [HttpPost]
        public IActionResult Insert_Purchase_Stock(Purchase_Stock_With_Customer_Model model)
        {
            // Insert operation
            bool is_Success = _stock_BAL.PURCHASE_STOCK_INSERT(model);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check insert success
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

        #region UPDATE

        // Update purchased stock
        [HttpPut]
        public IActionResult Update_Purchase_Stock(Purchase_Stock_With_Customer_Model stock)
        {
            // Update operation
            bool is_Success = _stock_BAL.PURCHASE_STOCK_UPDATE(stock);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check update success
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


        #region DISPLAY BY ID

        // Fetch purchased stock by ID
        [HttpGet("{Stock_ID}")]
        public IActionResult Get_Purchase_Stock_By_Id(int Stock_ID)
        {
            // Retrieve stock
            Purchase_Stock? show_Purchase_Stock = _stock_BAL.PURCHASE_STOCKS_BY_PK(Stock_ID);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check stock existence
            if (show_Purchase_Stock != null && show_Purchase_Stock.PurchaseStockId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!.");
                response.Add("data", show_Purchase_Stock);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!.");
                response.Add("data", "No Data");
                return NotFound(response);
            }
        }


        [HttpGet("{Stock_ID}&{Customer_ID}")]

        public IActionResult Fetch_Stock_And_Customer_Details(int Stock_ID, int Customer_ID)
        {
            Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model = _stock_BAL.Fetch_Stock_And_Customer_Details(Stock_ID, Customer_ID);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check stock existence
            if (purchase_Stock_With_Customer_Model != null && purchase_Stock_With_Customer_Model.Purchase_Stock.PurchaseStockId != 0 && purchase_Stock_With_Customer_Model.Customers_Model.CustomerId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!.");
                response.Add("data", purchase_Stock_With_Customer_Model);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!.");
                response.Add("data", "No Data");
                return NotFound(response);
            }

        }

        #endregion



    }
}