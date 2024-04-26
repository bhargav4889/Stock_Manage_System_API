using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class PaymentController : Controller
    {
        private readonly Payment_BALBase payment_BALBase = new Payment_BALBase();

        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult Get_Payment_Info(int Customer_ID, int Stock_ID)
        {
            Payment_Model payment_Info = payment_BALBase.Get_Payment_Info_By_Stock_Customer_PK(Stock_ID, Customer_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (payment_Info != null && payment_Info.Customer_Id != 0 && payment_Info.Stock_Id != 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", payment_Info);


                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);


                return NotFound(res);
            }


        }


        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult Get_Remain_Payment_Info(int Customer_ID, int Stock_ID)
        {
            Remain_Payment_Model payment_Info = payment_BALBase.Remain_Get_Payment_Info_By_Customer_FK_And_Stock_Id_And_Payment_Id(Customer_ID, Stock_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (payment_Info != null && payment_Info.Payment_Id != 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", payment_Info);


                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);


                return NotFound(res);
            }


        }

        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult Payment_Info_By_Customer_ID_AND_Stock_ID(int Customer_ID, int Stock_ID)
        {
            Show_Payment_Info show_Payment_Info = payment_BALBase.Show_All_Payment_Info(Customer_ID, Stock_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (show_Payment_Info != null && show_Payment_Info.PaymentID != 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", show_Payment_Info);


                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);


                return NotFound(res);
            }


        }

        [HttpGet]
        public IActionResult Pending_Customers_Payments()
        {
            List<Pending_Customers_Payments> pending_Customers_Payments = payment_BALBase.Pending_Customers_Payments();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (pending_Customers_Payments != null && pending_Customers_Payments.Count > 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", pending_Customers_Payments);


                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);


                return NotFound(res);
            }
        }


        [HttpPost]

        public IActionResult Create_Payment(Payment_Model payment_Model)
        {
            bool IsSuccess = payment_BALBase.Create_Payment(payment_Model);


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


        [HttpPost]

        public IActionResult Create_Remain_Payment(Remain_Payment_Model remain_Payment_Model)
        {
            bool IsSuccess = payment_BALBase.Create_Remain_Payment(remain_Payment_Model);


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
