using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Collections.Generic;

namespace Stock_Manage_System_API.BAL
{
    /// <summary>
    /// Provides business logic layer functionalities for payment management.
    /// </summary>
    public class Payment_BALBase
    {
        private readonly Payment_DALBase _payment_DAL = new Payment_DALBase();

        #region Section: Get Payment Info By Stock & Customer ID

        /// <summary>
        /// Retrieves payment information based on stock ID and customer ID.
        /// </summary>
        /// <param name="Stock_ID">The ID of the stock.</param>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <returns>A Payment_Model instance containing the payment details.</returns>
        public Payment_Model GetPaymentInfoByStockCustomerId(int Stock_ID, int Customer_ID)
        {
            return _payment_DAL.GetPaymentInfoByStockCustomerId(Stock_ID, Customer_ID);
        }

        #endregion

        #region Section: Get Remain Payment Info By Stock & Customer ID

        /// <summary>
        /// Retrieves remaining payment information by customer, stock, and payment ID.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <param name="Stock_ID">The ID of the stock.</param>
        /// <returns>A Remain_Payment_Model instance containing the remaining payment details.</returns>
        public Remain_Payment_Model RemainGetPaymentInfoByCustomerFkAndStockIdAndPaymentId(int Customer_ID, int Stock_ID)
        {
            return _payment_DAL.RemainGetPaymentInfoByCustomerFkAndStockIdAndPaymentId(Customer_ID, Stock_ID);
        }

        #endregion

        #region Section: Display All Pending Customers Payments

        /// <summary>
        /// Retrieves a list of pending payments for all customers.
        /// </summary>
        /// <returns>A list of pending payments.</returns>
        public List<Pending_Customers_Payments> GetPendingCustomersPayments()
        {
            return _payment_DAL.GetPendingCustomersPayments();
        }

        #endregion

        #region Section: Display All Remaining Customers Payments

        /// <summary>
        /// Retrieves a list of remaining payments for all customers.
        /// </summary>
        /// <returns>A list of remaining payments.</returns>
        public List<Remain_Payment_Model> GetRemainingCustomersPayments()
        {
            return _payment_DAL.GetRemainingCustomersPayments();
        }

        #endregion

        #region Section: Display All Paid Customers Payments

        /// <summary>
        /// Retrieves a list of all paid payments for customers.
        /// </summary>
        /// <returns>A list of paid payment details.</returns>
        public List<Show_Payment_Info> GetPaidCustomersPayments()
        {
            return _payment_DAL.GetPaidCustomersPayments();
        }

        #endregion

        #region Section: Show Payment By Customer, Stock

        /// <summary>
        /// Displays all payment information for a specified customer and stock.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <param name="Stock_ID">The ID of the stock.</param>
        /// <returns>Show_Payment_Info detailing all relevant payment info.</returns>
        public Show_Payment_Info GetFullPaymentInfo(int Customer_ID, int Stock_ID)
        {
            return _payment_DAL.GetFullPaymentInfo(Customer_ID, Stock_ID);
        }

        #endregion

        #region Section: Insert Payment

        /// <summary>
        /// Creates a new payment record.
        /// </summary>
        /// <param name="payment_Model">The payment details to insert.</param>
        /// <returns>True if the payment was created successfully, otherwise false.</returns>
        public bool InsertPayment(Payment_Model payment_Model)
        {
            try
            {
                return _payment_DAL.InsertPayment(payment_Model);
            }
            catch
            {
                // Consider logging the exception
                return false;
            }
        }

        #endregion

        #region Section: Insert Remain Payment

        /// <summary>
        /// Creates a record for a remaining payment.
        /// </summary>
        /// <param name="remain_Payment_Model">The details of the remaining payment.</param>
        /// <returns>True if the remaining payment was created successfully, otherwise false.</returns>
        public bool InsertRemainPayment(Remain_Payment_Model remain_Payment_Model)
        {
            try
            {
                return _payment_DAL.InsertRemainPayment(remain_Payment_Model);
            }
            catch
            {

                return false;
            }
        }

        #endregion


        #region Section : Delete Payment When Status is Paid 

        /// <summary>
        /// Deletes a payment entry that has been fully paid and resets the associated stock status to 'pending'.
        /// This function is utilized when a full payment needs to be reversed due to incorrect details or other issues.
        /// </summary>
        /// <param name="Payment_ID">The identifier for the payment to be deleted.</param>
        /// <param name="Stock_ID">The stock ID associated with the payment, which will be reverted to 'pending' status.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool DeletePaidStatusPayment(int Payment_ID, int Stock_ID)
        {
            try
            {
                if (Convert.ToBoolean(_payment_DAL.DeletePaidStatusPayment(Payment_ID, Stock_ID)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region Section : Delete Payment When Status is Remain

        /// <summary>
        /// Deletes a payment entry that is partially paid and remains due to correction or cancellation requirements.
        /// Useful when initial payment entries are found to be erroneous and need to be removed completely.
        /// </summary>
        /// <param name="Payment_ID">The identifier for the payment to be deleted.</param>
        /// <param name="Stock_ID">The stock ID associated with the payment.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool DeleteRemainStatusPayment(int Payment_ID, int Stock_ID)
        {
            try
            {
                if (Convert.ToBoolean(_payment_DAL.DeleteRemainStatusPayment(Payment_ID, Stock_ID)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion


        #region Section : Delete Payment When Status is Pending

        /// <summary>
        /// Deletes a payment entry that is marked as pending. This function is used when a mistake is identified in the payment details before any part of it has been processed as 'paid'.
        /// </summary>
        
        /// <param name="Stock_ID">The stock ID associated with the payment, whose status will remain unchanged since the payment was not processed.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool DeletePendingStatusPayment(int Stock_ID)
        {
            try
            {
                if (Convert.ToBoolean(_payment_DAL.DeletePendingStatusPayment(Stock_ID)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion


    }
}