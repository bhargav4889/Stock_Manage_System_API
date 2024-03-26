using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;
using System.Net;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class BankController : Controller
    {
        [HttpGet]

        public IActionResult Get_Bank_Names()
        {
            Bank_BALBase images_BALBase = new Bank_BALBase();

            List<Bank_Model> bank_Models  = images_BALBase.Get_Bank_Names();

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



    }
}
