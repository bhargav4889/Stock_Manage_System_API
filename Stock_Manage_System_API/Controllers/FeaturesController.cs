using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]   
    public class FeaturesController : Controller
    {
        [HttpGet]
        public IActionResult Pending_Customers_Payment_Sort_List()
        {
            Features_BALBase dashboard_Features_BALBase = new Features_BALBase();

            List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_Lists = dashboard_Features_BALBase.pending_Customers_Payment_Sort_List();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (pending_Customers_Payment_Sort_Lists.Count > 0 && pending_Customers_Payment_Sort_Lists != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", pending_Customers_Payment_Sort_Lists);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);


                return Ok(res);
            }



        }
    }
}
