using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// Controller for managing information.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InformationController : Controller
    {
        /// <summary>
        /// The base class for managing information.
        /// </summary>
        private readonly Information_BALBase information_BALBase = new Information_BALBase();

        /// <summary>
        /// Inserts a bank information.
        /// </summary>
        /// <param name="information_Model">The bank information to insert.</param>
        /// <returns>A response indicating whether the insert was successful.</returns>
        [HttpPost]
        public IActionResult AddBankInformation(Information_Model information_Model)
        {
            bool IsSuccess = information_BALBase.InsertBankInformation(information_Model);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Insert Successfully!");
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }
        }


        /// <summary>
        /// Update a bank information.
        /// </summary>
        /// <param name="information_Model">The bank information to Update.</param>
        /// <returns>A response indicating whether the Update was successful.</returns>
        [HttpPut]
        public IActionResult UpdateBankInformation(Information_Model information_Model)
        {
            bool IsSuccess = information_BALBase.UpdateBankInformation(information_Model);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Update Successfully!");
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }
        }

        /// <summary>
        /// Retrieves all saved information.
        /// </summary>
        /// <returns>A list of saved information.</returns>
        [HttpGet]
        public IActionResult GetAllSaveInformation()
        {
            List<Information_Model> information_Models = information_BALBase.GetAllSaveInformation();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (information_Models.Count > 0 && information_Models != null)
            {
                res.Add("status", true);
                res.Add("message", "Data Found!");
                res.Add("data", information_Models);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                return Ok(res);
            }

        }

        /// <summary>
        /// Retrieves information by ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the information to retrieve.</param>
        /// <returns>The information with the specified ID.</returns>
        [HttpGet("{Information_ID}")]
        public IActionResult InformationByID(int Information_ID)
        {
            Information_Model information_Model = information_BALBase.InformationByID(Information_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (information_Model != null && information_Model.InformationID != 0)
            {
                res.Add("status", true);
                res.Add("message", "Data Insert Successfully!");
                res.Add("data", information_Model);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }
        }

        /// <summary>
        /// Deletes saved information by ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the information to delete.</param>
        /// <returns>A response indicating whether the delete was successful.</returns>
        [HttpDelete]
        public IActionResult DeleteInformation(int Information_ID)
        {
            bool IsSuccess = information_BALBase.DeleteInformation(Information_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Delete Successfully!");
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }
        }

    }
}