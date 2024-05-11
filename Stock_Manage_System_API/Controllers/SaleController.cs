using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SaleController : Controller
    {

        public readonly Sales_BALBase sales_BALBase = new Sales_BALBase();

        #region Method : Insert Sale


        [HttpPost]
        public IActionResult AddSale(Sale_Customer_Combied_Model model)
        {
            // Insert operation
            bool is_Success = sales_BALBase.InsertSale(model);

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

        #region Method : Update Sale


        [HttpPut]
        public IActionResult UpdateSale(Sale_Customer_Combied_Model model)
        {
            // Insert operation
            bool is_Success = sales_BALBase.UpdateSale(model);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check insert success
            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Update Successfully!");
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

        #region Method : Get All Sales 

        [HttpGet]

        public IActionResult GetAllSales()
        {
            List<Show_Sale> sales = sales_BALBase.GetAllSales();

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if(sales.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "data found !");
                response.Add("data", sales);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "data not found");
                return Ok(response);
            }

        }

        #endregion

        #region Method : Sale Information By ID

        [HttpGet("{Sale_ID}")]

        public IActionResult GetSaleByID(int Sale_ID)
        {
            Show_Sale sale = sales_BALBase.GetSaleByID(Sale_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (sale != null && sale.saleId != 0)
            {
                response.Add("status", true);
                response.Add("message", "data found !");
                response.Add("data", sale);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "data not found");
                return Ok(response);
            }
        }
        #endregion


        #region Method :Sale With Customer Information Data

        [HttpGet("{Sale_ID}&{Customer_ID}")]

        public IActionResult GetSaleAndCustomerDetails(int Sale_ID ,int Customer_ID)
        {
            Sale_Customer_Combied_Model sale = sales_BALBase.GetSaleByCustomerAndSaleID(Sale_ID,Customer_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (sale != null && sale.sale.SaleId != 0)
            {
                response.Add("status", true);
                response.Add("message", "data found !");
                response.Add("data", sale);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "data not found");
                return Ok(response);
            }
        }

        #endregion

        #region Method : Delete Sale

        [HttpDelete]
        public IActionResult DeleteSale(int Sale_ID)
        {
            // Delete operation
            bool is_Success = sales_BALBase.DeleteSale(Sale_ID);

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

    }
}
