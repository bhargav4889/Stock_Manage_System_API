using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReminderController : Controller
    {
        private readonly Reminder_BALBase reminder_BALBase = new Reminder_BALBase();


        [HttpPost]
        public IActionResult Create_Reminder(Reminder_Model reminder)
        {
            bool isSucess =  reminder_BALBase.Insert_Reminder(reminder);

            Dictionary<string,dynamic> res = new Dictionary<string,dynamic>();

            if(isSucess)
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


        #region UPDATE

        // Update purchased stock
        [HttpPut]
        public IActionResult Update_Reminder(Reminder_Model reminder)
        {
            // Update operation
            bool is_Success = reminder_BALBase.Update_Reminder(reminder);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check update success
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

        #region Display All

        // Fetch all purchased stocks
        [HttpGet]
        public IActionResult Reminders()
        {
            // Retrieve stocks
            List<Reminder_Model> reminders = reminder_BALBase.Get_Reminders();

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check stock existence
            if (reminders.Count > 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!.");
                response.Add("data", reminders);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!.");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion


        #region Display All

        // Fetch all purchased stocks
        [HttpGet("{Reminder_ID}")]
        public IActionResult Get_Reminder_By_ID(int Reminder_ID)
        {
            
            Reminder_Model reminder = reminder_BALBase.Get_Reminder_By_ID(Reminder_ID);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check stock existence
            if (reminder != null && reminder.ReminderId != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found!.");
                response.Add("data", reminder);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data Not Found!.");
                response.Add("data", "Not Found Data !");
                return NotFound(response);
            }
        }

        #endregion

        #region DELETE 

        // Delete a purchased stock

        [HttpDelete]
        public IActionResult Delete_Reminder(int Reminder_ID)
        {
            // Delete operation
            bool is_Success = reminder_BALBase.Delete_Reminder(Reminder_ID);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check delete success
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
