/// <summary>
/// Represents the Features controller for managing features in the API.
/// </summary>
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class FeaturesController : Controller
    {
        /// <summary>
        /// Retrieves a list of pending customers payment sort list.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> object containing a dictionary with the status, message, and data.</returns>

        #region Method : Sort List of Pending Customer Payments Recent 

        [HttpGet]
        public IActionResult PendingCustomersPaymentSortList()
        {
            Features_BALBase dashboard_Features_BALBase = new Features_BALBase();

            List<Dashbaord_Features_Model.Pending_Customers_Payment_Sort_List> pending_Customers_Payment_Sort_Lists = dashboard_Features_BALBase.PendingCustomersPaymentSortList();

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
        #endregion


        #region Method : Upcoming Reminder

        [HttpGet]
        public IActionResult UpcomingRemindersList()
        {
            Features_BALBase dashboard_Features_BALBase = new Features_BALBase();

            List<Dashbaord_Features_Model.Upcoming_Reminders_Model> upcoming_Reminders = dashboard_Features_BALBase.UpcomingRemindersList();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (upcoming_Reminders.Count > 0 && upcoming_Reminders != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", upcoming_Reminders);

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
        #endregion
    }
}