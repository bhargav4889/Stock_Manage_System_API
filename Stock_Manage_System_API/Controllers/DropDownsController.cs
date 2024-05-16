using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// Controller responsible for handling requests related to dropdown data.
    /// </summary>

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DropDownsController : Controller
    {
        private readonly DropDowns_DALBase _allDropDownsBALBase;

        /// <summary>
        /// Initializes a new instance of the DropDowns_BALBase to handle business logic.
        /// </summary>
        public DropDownsController()
        {
            _allDropDownsBALBase = new DropDowns_DALBase();
        }

        /// <summary>
        /// Retrieves all dropdown data from the Business Access Layer.
        /// </summary>
        /// <returns>An IActionResult that contains all dropdown options or an error message.</returns>

        [HttpGet]
        public IActionResult GetAllDropDowns()
        {
            try
            {
                var allDropDowns = _allDropDownsBALBase.GetAllDropdownsAsync();
                if (allDropDowns != null)
                {
                    return Ok(allDropDowns); // Successfully found and returned the dropdown data.
                }
                else
                {
                    return NotFound("Dropdown data not found."); // No dropdown data was found.
                }
            }
            catch (System.Exception ex)
            {
                // Log the exception here. Consider using logging frameworks like Serilog or NLog.
                return StatusCode(500, $"{ex} this error occurred while processing your request."); // An internal server error occurred.
            }
        }
    }
}