using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Recent_ActionsController : Controller
    {
        [HttpGet]

       
        public IActionResult Recent_Actions_List()
        {
            Recent_Actions_BALBase recent_Actions_BALBase = new Recent_Actions_BALBase();

            List<Recent_Action_Model> recent_Actions = recent_Actions_BALBase.Recent_Actions();
            

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (recent_Actions != null && recent_Actions.Count > 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", recent_Actions);

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

    }
}
