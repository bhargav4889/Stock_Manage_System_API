using Microsoft.Extensions.Logging.Abstractions;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Stock_BALBase
    {
        private readonly Stock_DALBase _stockDalBase = new Stock_DALBase();

        #region Section: Insert Purchase Stock With Customer Details

        /// <summary>
        /// Inserts a new purchase stock entry into the database.
        /// </summary>
        /// <param name="purchase_Stock_With_Customer_Model">The purchase stock with customer model to insert.</param>
        /// <returns>true if the insertion is successful; otherwise, false.</returns>

        public bool InsertPurchaseStock(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {

                if (_stockDalBase.InsertPurchaseStock(purchase_Stock_With_Customer_Model))
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

        #region Section: Update Purchase Stock Details

        /// <summary>
        /// Updates an existing purchase stock entry in the database.
        /// </summary>
        /// <param name="purchase_Stock_With_Customer_Model">The purchase stock model to update.</param>
        /// <returns>true if the update is successful; otherwise, false.</returns>

        public bool UpdatePurchaseStock(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {
                if (_stockDalBase.UpdatePurchaseStock(purchase_Stock_With_Customer_Model))
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

        #region Section: Fetch Stock And Customer Information By Those IDs

        /// <summary>
        /// Fetches the stock and customer details for specific stock and customer IDs.
        /// </summary>
        /// <param name="Stock_ID">The stock identifier.</param>
        /// <param name="Customer_ID">The customer identifier.</param>
        /// <returns>A model containing the stock and customer details.</returns>

        public Purchase_Stock_With_Customer_Model Fetch_Stock_And_Customer_Details(int Stock_ID, int Customer_ID)
        {
            Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model = _stockDalBase.Fetch_Stock_And_Customer_Details(Stock_ID, Customer_ID);

            return purchase_Stock_With_Customer_Model;
        }

        #endregion

        #region Section: Delete Purchase Stock

        /// <summary>
        /// Deletes a purchase stock entry based on the transaction ID.
        /// </summary>
        /// <param name="TN_ID">The transaction ID of the purchase stock to be deleted.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>

        public bool DeletePurchaseStock(int TN_ID)
        {
            try
            {

                if (_stockDalBase.DeletePurchaseStock(TN_ID))
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

        #region Section: Display All Purchase Stocks

        /// <summary>
        /// Retrieves all purchase stocks from the database.
        /// </summary>
        /// <returns>A list of all purchase stocks, or null if an error occurs.</returns>

        public List<Purchase_Stock>? DisplayAllPurchaseStock()
        {
            return _stockDalBase.DisplayAllPurchaseStock();
        }

        #endregion

        #region Section: Purchase Stock By Stock ID

        /// <summary>
        /// Retrieves a specific purchase stock entry by its ID.
        /// </summary>
        /// <param name="Stock_ID">The ID of the purchase stock to retrieve.</param>
        /// <returns>The purchase stock if found; otherwise, null.</returns>

        public Purchase_Stock? PurchaseStockByID(int Stock_ID)
        {
            return _stockDalBase.PurchaseStockByID(Stock_ID);
        }

        #endregion

    }
}