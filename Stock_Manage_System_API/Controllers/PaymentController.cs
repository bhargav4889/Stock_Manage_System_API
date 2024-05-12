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

        #region Section: Get Payment Info By Stock & Customer ID

        /// <summary>
        /// Gets payment information by stock and customer IDs.
        /// </summary>
        /// <param name="Customer_ID">The customer ID.</param>
        /// <param name="Stock_ID">The stock ID.</param>
        /// <returns>Payment information as a JSON object.</returns>
        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult GetPaymentInfoByStockCustomerID(int Customer_ID, int Stock_ID)
        {
            Payment_Model payment_Info = payment_BALBase.GetPaymentInfoByStockCustomerId(Stock_ID, Customer_ID);

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

        #endregion


        #region Section: Get Remain Payment Info By Stock & Customer ID

        /// <summary>
        /// Gets the remaining payment information for a specific customer, stock, and payment ID.
        /// </summary>
        /// <param name="Customer_ID">The customer ID.</param>
        /// <param name="Stock_ID">The stock ID.</param>
        /// <returns>Remaining payment information as a JSON object.</returns>
        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult RemainGetPaymentInfoByCustomerFKAndStockIdAndPaymentID(int Customer_ID, int Stock_ID)
        {
            Remain_Payment_Model payment_Info = payment_BALBase.RemainGetPaymentInfoByCustomerFkAndStockIdAndPaymentId(Customer_ID, Stock_ID);

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

        #endregion

        #region Section : Full Payment Infromation

        /// <summary>
        /// Retrieves full payment information for a specific customer and stock.
        /// </summary>
        /// <param name="Customer_ID">The customer ID.</param>
        /// <param name="Stock_ID">The stock ID.</param>
        /// <returns>Full payment information as a JSON object.</returns>
        [HttpGet("{Customer_ID}&{Stock_ID}")]
        public IActionResult GetFullPaymentInfo(int Customer_ID, int Stock_ID)
        {
            Show_Payment_Info show_Payment_Info = payment_BALBase.GetFullPaymentInfo(Customer_ID, Stock_ID);

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
        #endregion


        #region Section: Display All Pending Customers Payments

        /// <summary>
        /// Displays all pending customers payments.
        /// </summary>
        /// <returns>A list of pending customers payments as a JSON object.</returns>
        [HttpGet]
        public IActionResult GetPendingCustomersPayments()
        {
            List<Pending_Customers_Payments> pending_Customers_Payments = payment_BALBase.GetPendingCustomersPayments();

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

        #endregion

        #region Section: Display All Remaining Customers Payments

        /// <summary>
        /// Displays all remaining customers payments.
        /// </summary>
        /// <returns>A list of remaining customers payments as a JSON object.</returns>
        [HttpGet]
        public IActionResult GetRemainingCustomersPayments()
        {
            List<Remain_Payment_Model> remain_Payment_Models = payment_BALBase.GetRemainingCustomersPayments();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (remain_Payment_Models != null && remain_Payment_Models.Count > 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", remain_Payment_Models);


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


        #region Section: Display All Pending Customers Payments

        /// <summary>
        /// Displays all paid customers payments.
        /// </summary>
        /// <returns>A list of paid customers payments as a JSON object.</returns>
        [HttpGet]
        public IActionResult GetPaidCustomersPayments()
            {
            List<Show_Payment_Info> show_Payment_Infos = payment_BALBase.GetPaidCustomersPayments();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (show_Payment_Infos != null && show_Payment_Infos.Count > 0)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", show_Payment_Infos);


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


        #region Section: Insert Payment

        /// <summary>
        /// Inserts a new payment record into the database.
        /// </summary>
        /// <param name="payment_Model">The payment model object.</param>
        /// <returns>A dictionary object with status, message, and data properties.</returns>
        [HttpPost]
        public IActionResult AddPayment(Payment_Model payment_Model)
        {
            bool IsSuccess = payment_BALBase.InsertPayment(payment_Model);

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

        #endregion


        #region Section : Insert Remain Payment

        /// <summary>
        /// Inserts a new remaining payment record into the database.
        /// </summary>
        /// <param name="remain_Payment_Model">The remain payment model object.</param>
        /// <returns>A dictionary object with status, message, and data properties.</returns>
        [HttpPost]
        public IActionResult AddRemainPayment(Remain_Payment_Model remain_Payment_Model)
        {
            bool IsSuccess = payment_BALBase.InsertRemainPayment(remain_Payment_Model);

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

        #endregion

        #region Section : Delete Payment When Status is Pending 

        /// <summary>
        /// Deletes a pending payment and maintains the associated stock status. This method is called when a payment initially marked as 'pending' needs to be removed, often due to errors or changes before finalizing.
        /// </summary>
        /// <param name="Payment_ID">The identifier of the payment to delete.</param>
        /// <param name="Stock_ID">The identifier of the stock associated with the payment.</param>
        /// <returns>Returns a success status and message if the operation is successful, otherwise returns an error message.</returns>
        [HttpDelete]
        public IActionResult DeletePendingStatusPayment(int Stock_ID)
        {
            bool IsSuccess = payment_BALBase.DeletePendingStatusPayment(Stock_ID);

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

        #endregion

        #region Section : Delete Payment When Status is Remain 

        /// <summary>
        /// Deletes a partially paid (remaining) payment. Useful for removing payments that were mistakenly or incorrectly entered and are yet to be fully paid.
        /// </summary>
        /// <param name="Payment_ID">The identifier of the payment to delete.</param>
        /// <param name="Stock_ID">The identifier of the stock associated with the payment.</param>
        /// <returns>Returns a success status and message if the operation is successful, otherwise returns an error message.</returns>
        [HttpDelete]
        public IActionResult DeleteRemainStatusPayment(int Payment_ID, int Stock_ID)
        {
            bool IsSuccess = payment_BALBase.DeleteRemainStatusPayment(Payment_ID, Stock_ID);

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

        #endregion
        #region Section : Delete Payment When Status is Paid 

        /// <summary>
        /// Deletes a fully paid payment entry and reverts the associated stock status to 'pending'. This action is typically used to correct errors after a payment has been fully processed.
        /// </summary>
        /// <param name="Payment_ID">The identifier of the payment to delete.</param>
        /// <param name="Stock_ID">The identifier of the stock associated with the payment.</param>
        /// <returns>Returns a success status and message if the operation is successful, otherwise returns an error message.</returns>
        [HttpDelete]
        public IActionResult DeletePaidStatusPayment(int Payment_ID, int Stock_ID)
        {
            bool IsSuccess = payment_BALBase.DeletePaidStatusPayment(Payment_ID, Stock_ID);

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

        #endregion

    }
}