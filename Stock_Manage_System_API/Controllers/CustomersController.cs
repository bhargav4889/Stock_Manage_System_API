using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;
using System.Collections.Generic;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomersController : Controller
    {
        private readonly Customers_BALBase _customers_BALBase;

        /// <summary>
        /// Initializes a new instance of the CustomersController.
        /// </summary>
        public CustomersController()
        {
            _customers_BALBase = new Customers_BALBase();
        }

        #region Section: Display All Customers

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>A list of all customers or a message indicating no data was found.</returns>
      /*  [Authorize]*/
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customers_BALBase.GetAllCustomers();
            var res = new Dictionary<string,
              dynamic>();

            if (customers.Count > 0)
            {
                res.Add("status", true);
                res.Add("message", "Data Found!");
                res.Add("data", customers);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                res.Add("data", "No Data!");
                return Ok(res);
            }
        }

        #endregion

        #region Section: Account Details by Customer ID and Type

        /// <summary>
        /// Retrieves detailed account information for a customer based on their ID and type.
        /// </summary>
        /// <param name="Customer_ID">The customer's ID.</param>
        /// <param name="Customer_Type">The type of the customer.</param>
        /// <returns>Account details including purchased stock information or a message if no data is found.</returns>
        [HttpGet("{Customer_ID}&{Customer_Type}")]
        public IActionResult GetAccountDetails(int Customer_ID, string Customer_Type)
        {
            var customerDetails = _customers_BALBase.RetrieveAccountDetails(Customer_ID, Customer_Type);
            var res = new Dictionary<string,
              dynamic>();

            if (customerDetails != null)
            {
                res.Add("status", true);
                res.Add("message", "Data Found!");
                res.Add("data", customerDetails);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                res.Add("data", "No Data!");
                return Ok(res);
            }
        }

        #endregion

        #region Section: Insert Customer

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customers">The customer data model to be inserted.</param>
        /// <returns>Status and message indicating whether the insertion was successful.</returns>
        [HttpPost]
        public IActionResult AddCustomer(Customer_Model customers)
        {
            bool isSuccess = _customers_BALBase.InsertCustomer(customers);
            var res = new Dictionary<string,
              dynamic>();

            if (isSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Inserted Successfully!");
                res.Add("data", customers);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occurred!");
                return Ok(res);
            }
        }

        #endregion

        #region Section: Delete Customer

        /// <summary>
        /// Deletes a customer based on their ID and type.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer to delete.</param>
        /// <param name="Customer_Type">The type of the customer to delete.</param>
        /// <returns>Status and message indicating whether the deletion was successful.</returns>
        [HttpDelete]
        public IActionResult DeleteCustomer(int Customer_ID, string Customer_Type)
        {
            bool isSuccess = _customers_BALBase.DeleteCustomer(Customer_ID, Customer_Type);
            var response = new Dictionary<string,
              dynamic>();

            if (isSuccess)
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

        #region Section: Update Customer

        /// <summary>
        /// Updates an existing customer's information.
        /// </summary>
        /// <param name="customers">The customer data model to be updated.</param>
        /// <returns>Status and message indicating whether the update was successful.</returns>
        [HttpPut]
        public IActionResult UpdateCustomer(Customer_Model customers)
        {
            bool isSuccess = _customers_BALBase.UpdateCustomer(customers);
            var response = new Dictionary<string,
              dynamic>();

            if (isSuccess)
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

        #region Section: Customer By ID and Type

        /// <summary>
        /// Retrieves customer data based on customer ID and type.
        /// </summary>
        /// <param name="Customer_ID">The customer's ID.</param>
        /// <param name="Customer_Type">The type of the customer.</param>
        /// <returns>Customer information if found or a message if not found.</returns>
        [HttpGet("{Customer_ID}&{Customer_Type}")]
        public IActionResult GetCustomerByIDAndType(int Customer_ID, string Customer_Type)
        {
            var customers = _customers_BALBase.CustomerByIDAndType(Customer_ID, Customer_Type);
            var res = new Dictionary<string,
              dynamic>();

            if (customers != null)
            {
                res.Add("status", true);
                res.Add("message", "Data Found!");
                res.Add("data", customers);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                res.Add("data", null);
                return NotFound(res);
            }
        }

        #endregion

        #region Section: Customer Does Exist

        #region Area: Buyer Customer Exist

        /// <summary>
        /// Checks if a buyer customer with a specific name exists.
        /// </summary>
        /// <param name="Customer_Name">The name of the buyer customer to check.</param>
        /// <returns>List of customers if found, or a message if no customer is found.</returns>
        [HttpGet("{Customer_Name}")]
        public IActionResult IsBuyerCustomerExist(string Customer_Name)
        {
            var existingCustomers = _customers_BALBase.DoesBuyerCustomerExist(Customer_Name);
            var res = new Dictionary<string,
              dynamic>();

            if (existingCustomers.Count > 0)
            {
                res.Add("status", true);
                res.Add("message", "Already Customers Exists.");
                res.Add("data", existingCustomers);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                res.Add("data", "No Data!");
                return NotFound(res);
            }
        }

        #endregion

        #region Area: Seller Customer Exist

        /// <summary>
        /// Checks if a seller customer with a specific name exists.
        /// </summary>
        /// <param name="Customer_Name">The name of the seller customer to check.</param>
        /// <returns>List of customers if found, or a message if no customer is found.</returns>
        [HttpGet("{Customer_Name}")]
        public IActionResult IsSellerCustomerExist(string Customer_Name)
        {
            var existingCustomers = _customers_BALBase.DoesSellerCustomerExist(Customer_Name);
            var res = new Dictionary<string,
              dynamic>();

            if (existingCustomers.Count > 0)
            {
                res.Add("status", true);
                res.Add("message", "Already Customers Exists.");
                res.Add("data", existingCustomers);
                return Ok(res);
            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Data Not Found!");
                res.Add("data", "No Data!");
                return Ok(res);
            }
        }

        #endregion

        #endregion
    }
}
