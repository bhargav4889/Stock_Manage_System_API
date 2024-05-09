using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.BAL
{
    /// <summary>
    /// Provides business logic functions for customer operations, bridging between DAL and API layers.
    /// </summary>
    public class Customers_BALBase
    {
        private readonly Customers_DALBase _customers_DAL = new Customers_DALBase();

        #region Section: Insert Customer

        /// <summary>
        /// Inserts a new customer into the system.
        /// </summary>
        /// <param name="customers">The customer model to insert.</param>
        /// <returns>True if the operation is successful, False otherwise.</returns>
        public bool InsertCustomer(Customer_Model customers)
        {
            try
            {
                return _customers_DAL.InsertCustomer(customers);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Section: Display All Customers



        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>A list of all customers in the system.</returns>
        public List<Customer_Model> GetAllCustomers()
        {
            return _customers_DAL.GetAllCustomers();
        }






        #endregion

        #region Section: Account Details by Customer ID and Type

        /// <summary>
        /// Retrieves account details for a specific customer based on their ID and Type.
        /// </summary>
        /// <param name="Customer_ID">The customer's ID.</param>
        /// <param name="Customer_Type">The customer's type.</param>
        /// <returns>Detailed customer and their purchased stock info.</returns>
        public CustomerDetails_With_Purchased_Stock_Model RetrieveAccountDetails(int Customer_ID, string Customer_Type)
        {
            return _customers_DAL.RetrieveAccountDetails(Customer_ID, Customer_Type);
        }

        #endregion

        #region Section: Customer By ID and Type

        /// <summary>
        /// Retrieves a customer by ID and Type.
        /// </summary>
        /// <param name="Customer_ID">The customer's ID.</param>
        /// <param name="Customer_Type">The customer's type.</param>
        /// <returns>The customer model if found; otherwise, null.</returns>
        public Customer_Model CustomerByIDAndType(int Customer_ID, string Customer_Type)
        {
            return _customers_DAL.CustomerByIDAndType(Customer_ID, Customer_Type);
        }



        #endregion

        #region Section : Delete Customer

        /// <summary>
        /// Deletes a customer based on ID and Type.
        /// </summary>
        /// <param name="Customer_ID">The ID of the customer to delete.</param>
        /// <param name="Customer_Type">The type of the customer.</param>
        /// <returns>True if the operation is successful, False otherwise.</returns>
        public bool DeleteCustomer(int Customer_ID, string Customer_Type)
        {
            try
            {
                return _customers_DAL.DeleteCustomer(Customer_ID, Customer_Type);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Section : Update Customer

        /// <summary>
        /// Updates a customer's information.
        /// </summary>
        /// <param name="customers">The customer model to update.</param>
        /// <returns>True if the operation is successful, False otherwise.</returns>
        public bool UpdateCustomer(Customer_Model customers)
        {
            try
            {
                return _customers_DAL.UpdateCustomer(customers);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Section : Customer Does Exist

        #region Area: Buyer Customer Exist

        /// <summary>
        /// Checks if a buyer customer exists by name.
        /// </summary>
        /// <param name="Customer_Name">The name of the buyer customer to check.</param>
        /// <returns>A list of customer models.</returns>
        public List<Customer_Model> DoesBuyerCustomerExist(string Customer_Name)
        {
            return _customers_DAL.DoesBuyerCustomerExist(Customer_Name);
        }

        #endregion

        #region Area: Seller Customer Exist

        /// <summary>
        /// Checks if a seller customer exists by name.
        /// </summary>
        /// <param name="Customer_Name">The name of the seller customer to check.</param>
        /// <returns>A list of customer models.</returns>
        public List<Customer_Model> DoesSellerCustomerExist(string Customer_Name)
        {
            return _customers_DAL.DoesSellerCustomerExist(Customer_Name);
        }

        #endregion


        #endregion
    }
}
