using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class All_CountsController : Controller
    {
        private readonly All_Counts_BALBase all_Counts_BALBase = new All_Counts_BALBase();

        [HttpGet]

        public IActionResult Get_All_Counts()
        {

            All_Counts_Model all_Counts_Model = all_Counts_BALBase.ALL_COUNTS();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (all_Counts_Model != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", all_Counts_Model);

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


