using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountsController : Controller
    {
        #region BAL Class Instance

        private readonly Counts_BALBase allCountsBALBase = new Counts_BALBase();

        #endregion


        #region Method : Get All Counts

        [HttpGet]
        
        public IActionResult Get_Counts()
        {

            AllCountsModel allCountsModel = allCountsBALBase.ALL_COUNTS();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (allCountsModel != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", allCountsModel);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                return NotFound(res);
            }


        }


        #endregion

    }
}


