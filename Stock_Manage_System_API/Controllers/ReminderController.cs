using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// Controller for managing reminders.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ReminderController : Controller
    {
        /// <summary>
        /// The reminder business access layer base object.
        /// </summary>
        private readonly Reminder_BALBase reminder_BALBase = new Reminder_BALBase();

        #region Method : Insert Reminder

        /// <summary>
        /// Creates a new reminder.
        /// </summary>
        /// <param name="reminder">The reminder to create.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>

        [HttpPost]
        public IActionResult AddReminder(Reminder_Model reminder)
        {
            bool isSucess = reminder_BALBase.InsertReminder(reminder);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (isSucess)
            {
                res.Add("status", true);
                res.Add("message", "Insert Successfully");
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Error Occured !");
                return BadRequest(res);
            }
        }

        #endregion

        #region Method : Update Reminder

        /// <summary>
        /// Updates an existing reminder.
        /// </summary>
        /// <param name="reminder">The reminder to update.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        [HttpPut]
        public IActionResult UpdateReminder(Reminder_Model reminder)
        {
            bool is_Success = reminder_BALBase.UpdateReminder(reminder);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully!");
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

        #region Method : Get All Upcoming Reminder

        /// <summary>
        /// Retrieves all reminders.
        /// </summary>
        /// <returns>An HTTP response containing a list of reminders.</returns>
        [HttpGet]
        public IActionResult GetAllReminders()
        {
            List<Reminder_Model> reminders = reminder_BALBase.GetReminders();

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (reminders.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", reminders);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion

        #region Method : Get Reminder By ID

        /// <summary>
        /// Retrieves a reminder by its ID.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to retrieve.</param>
        /// <returns>An HTTP response containing the reminder with the specified ID.</returns>
        [HttpGet("{Reminder_ID}")]
        public IActionResult GetReminderByID(int Reminder_ID)
        {
            Reminder_Model reminder = reminder_BALBase.GetReminderByID(Reminder_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            if (reminder != null && reminder.ReminderId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!");
                response.Add("data", reminder);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }
        #endregion

        #region Method : Delete Reminder

        /// <summary>
        /// Deletes a reminder by its ID.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to delete.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        [HttpDelete]
        public IActionResult DeleteReminder(int Reminder_ID)
        {
            bool is_Success = reminder_BALBase.DeleteReminder(Reminder_ID);

            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

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