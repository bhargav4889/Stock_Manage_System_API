using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class All_DropDownsController : Controller
    {
        private readonly All_DropDowns_BALBase all_DropDowns_BALBase;

        public All_DropDownsController()
        {
            all_DropDowns_BALBase = new All_DropDowns_BALBase();
        }

        // GET: api/AllDropDowns
        [HttpGet]
        public IActionResult GetAllDropDowns()
        {
            try
            {
                var allDropDowns = all_DropDowns_BALBase.GET_ALL_DROPDOWNS();
                if (allDropDowns != null)
                {
                    return Ok(allDropDowns);
                }
                else
                {
                    return NotFound("Dropdown data not found.");
                }
            }
            catch (System.Exception ex)
            {
                // Log the exception details here as per your logging strategy
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

       



    }
}
