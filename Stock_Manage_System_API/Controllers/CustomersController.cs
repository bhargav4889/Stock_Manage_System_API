using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;
using System.Data;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class CustomersController : Controller
    {
        private readonly Customers_BALBase customers_BALBase = new Customers_BALBase();


        #region DISPLAY ALL

        [HttpGet]
       
        public IActionResult Customers_List()
        {


            List<Customer_Model> customers = customers_BALBase.SHOW_ALL_CUSTOMERS();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (customers.Count > 0 && customers != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", customers);

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

        #endregion


        #region ACCOUNT_DETAILS BY CUSTOMER ID 


        [HttpGet("{Customer_ID}&{Customer_Type}")]

        public IActionResult Account_Details(int Customer_ID, string Customer_Type)
        {


            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock = customers_BALBase.Account_Details(Customer_ID, Customer_Type);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (customerDetails_With_Purchased_Stock!= null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", customerDetails_With_Purchased_Stock);




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

        #endregion



        #region INSERT

        [HttpPost]

        public IActionResult Insert_Customer(Customer_Model customers)
        {
            Customers_BALBase customers_BALBase = new Customers_BALBase();

                bool IsSuccess = customers_BALBase.CREATE_CUSTOMER(customers);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {

                res.Add("status", true);
                res.Add("message", "Data Insert Successfully!");
                res.Add("data", customers);
                return Ok(res);
            }
            else
            {

                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }
        }


        #endregion

        #region DELETE

        [HttpDelete]
        public IActionResult Delete_Customer(int Customer_ID,string Customer_Type)
        {
            // Delete operation
            bool is_Success = customers_BALBase.Delete_Customer(Customer_ID, Customer_Type);

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


        #region UPDATE

        [HttpPut]

        public IActionResult Update_Customer(Customer_Model customers)
        {
            // Update operation
            bool is_Success = customers_BALBase.Update_Customer(customers);

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



        [HttpGet("{Customer_ID}&{Customer_Type}")]

        public IActionResult Get_Customer(int Customer_ID,string Customer_Type)
        {
            Customer_Model customers = customers_BALBase.Customer_Info_By_PK(Customer_ID, Customer_Type);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (Customer_ID != 0 && customers != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", customers);

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




        [HttpGet("{Customer_Name}")]

        public IActionResult BUYER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
        {

            List<Customer_Model> List_Of_Exist_Customers = customers_BALBase.BUYER_CUSTOMER_EXIST_IN_SYSTEM(Customer_Name);



            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (List_Of_Exist_Customers.Count > 0 && List_Of_Exist_Customers != null)
            {
                res.Add("status", true);

                res.Add("message", "Already Customers Exists.");

                res.Add("data", List_Of_Exist_Customers);

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


        [HttpGet("{Customer_Name}")]

        public IActionResult SELLER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
        {

            List<Customer_Model> List_Of_Exist_Customers = customers_BALBase.SELLER_CUSTOMER_EXIST_IN_SYSTEM(Customer_Name);



            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (List_Of_Exist_Customers.Count > 0 && List_Of_Exist_Customers != null)
            {
                res.Add("status", true);

                res.Add("message", "Already Customers Exists.");

                res.Add("data", List_Of_Exist_Customers);

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






    }
}
