using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SalesController : Controller
    {

        public readonly Sales_BALBase sales_BALBase = new Sales_BALBase();

        #region INSERT

       
        [HttpPost]
        public IActionResult Insert_Sale(Sale_Customer_Combied_Model model)
        {
            // Insert operation
            bool is_Success = sales_BALBase.Insert_Sale_With_Customer(model);

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


        [HttpPut]
        public IActionResult Update_Sale(Sale_Customer_Combied_Model model)
        {
            // Insert operation
            bool is_Success = sales_BALBase.Update_Sale(model);

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


        [HttpGet]

        public IActionResult Sales()
        {
            List<Show_Sale> sales = sales_BALBase.Show_All_Sales();

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

        [HttpGet("{Sale_ID}")]

        public IActionResult Get_Sale_By_ID(int Sale_ID)
        {
            Show_Sale sale = sales_BALBase.Show_Sale_By_ID(Sale_ID);

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


        [HttpGet("{Sale_ID}&{Customer_ID}")]

        public IActionResult Fetch_Sale_And_Customer_Details(int Sale_ID ,int Customer_ID)
        {
            Sale_Customer_Combied_Model sale = sales_BALBase.Fetch_Sale_And_Customer_Details(Sale_ID,Customer_ID);

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

    }
}
