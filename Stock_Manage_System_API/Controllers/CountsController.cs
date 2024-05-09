using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;

using Stock_Manage_System_API.Models;

/// <summary>
/// Represents the Counts controller for managing counts in the API.
/// </summary>
namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountsController : Controller
    {
        #region BAL Class Instance

        /// <summary>
        /// An instance of the <see cref="Counts_BALBase"/> class for managing counts in the business access layer.
        /// </summary>
        private readonly Counts_BALBase allCountsBALBase = new Counts_BALBase();

        #endregion


        #region Method : Get All Counts

        /// <summary>
        /// Retrieves the total counts for all relevant entities.
        /// </summary>
        /// <returns>An IActionResult object containing the total counts and amounts for all relevant entities.</returns>
        [HttpGet]
        public IActionResult GetTotalCounts()
        {
            AllCountsModel allCountsModel = allCountsBALBase.GetTotalCounts();

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