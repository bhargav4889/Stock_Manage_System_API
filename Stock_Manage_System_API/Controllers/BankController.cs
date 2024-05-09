using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;
using System.Net;

namespace Stock_Manage_System_API.Controllers
{
    /// <summary>
    /// BankController class for handling bank-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class BankController : Controller
    {
        /// <summary>
        /// Retrieves a list of banks from the database.
        /// </summary>
        /// <returns>A dictionary object containing the status, message, and data.</returns>
        [HttpGet]
        public IActionResult GetBanksList()
        {
            Bank_BALBase bank_BALBase = new Bank_BALBase();

            List<Bank_Model> bank_Models = bank_BALBase.GetBanksList();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (bank_Models.Count > 0)
            {

                res.Add("status", true);
                res.Add("message", "Data Found Successfully!");
                res.Add("data", bank_Models);
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
        /// Retrieves a list of our banks for dropdown selection.
        /// </summary>
        /// <returns>A dictionary object containing the status, message, and data.</returns>
        [HttpGet]
        public IActionResult GetOurBanksSelectList()
        {
            Bank_BALBase bank_BALBase = new Bank_BALBase();

            List<Our_Banks_Dropdown> our_Banks_Dropdowns = bank_BALBase.GetOurBanksSelectList();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (our_Banks_Dropdowns.Count > 0)
            {

                res.Add("status", true);
                res.Add("message", "Data Found Successfully!");
                res.Add("data", our_Banks_Dropdowns);
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