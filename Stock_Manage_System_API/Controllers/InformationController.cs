using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InformationController : Controller
    {
        public readonly Information_BALBase information_BALBase = new Information_BALBase();

        [HttpPost]
        public IActionResult Insert_Bank_Information(Information_Model information_Model)
        {
            bool IsSuccess = information_BALBase.Insert_Bank_Infromation(information_Model);


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

        [HttpGet]

        public IActionResult Show_All_Save_Informations()
        {
            List<Information_Model> information_Models = information_BALBase.Show_All_Save_Informations();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if(information_Models.Count > 0 && information_Models != null) 
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

        [HttpGet("{Information_ID}")]

        public IActionResult Information_By_ID(int Information_ID)
        {
            Information_Model information_Model = information_BALBase.Information_Model(Information_ID);

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

        [HttpDelete("{Information_ID}")]
        public IActionResult Delete_Save_Infromation_By_ID(int Information_ID)
        {
            bool IsSuccess = information_BALBase.Delete_Save_Infromation(Information_ID);


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

    }
}
