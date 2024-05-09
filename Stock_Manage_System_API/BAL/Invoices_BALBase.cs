using Microsoft.Extensions.Logging.Abstractions;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Invoices_BALBase
    {
        private readonly Invoices_DALBase _invoicesDALBase = new Invoices_DALBase();

        #region Area: Sale Invoice

        #region Section: Display All Sale Invoices

        /// <summary>
        /// Retrieves all sales invoices from the database.
        /// </summary>
        /// <returns>A list of all sales invoices.</returns>
        public List<InvoicesModel.Sales_Invoice_Model> DisplayAllSaleInvoices()
        {
            return _invoicesDALBase.DisplayAllSaleInvoices();
        }

        #endregion

        #region Section: Insert Sale Invoice Details

        /// <summary>
        /// Inserts a new sales invoice in the database.
        /// </summary>
        /// <param name="sales_Invoice">The sales invoice model to insert.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>
        public bool InsertSaleInvoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {
                if (_invoicesDALBase.InsertSaleInvoice(sales_Invoice))
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

        #region Section: Delete Sale Invoice

        /// <summary>
        /// Deletes a sales invoice from the database using its ID.
        /// </summary>
        /// <param name="Sales_Invoice_ID">The ID of the sales invoice to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        public bool DeleteSaleInvoice(int Sale_Invoice_ID)
        {
            try
            {
                if (_invoicesDALBase.DeleteSaleInvoice(Sale_Invoice_ID))
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

        #region Section: Sale Invoice By Invoice ID

        /// <summary>
        /// Retrieves details of a specific sales invoice by its ID.
        /// </summary>
        /// <param name="Sales_Invoice_ID">The ID of the sales invoice to retrieve.</param>
        /// <returns>The sales invoice model if found; otherwise, null.</returns>
        public InvoicesModel.Sales_Invoice_Model SaleInvoiceByID(int Sale_Invoice_ID)
        {
            return _invoicesDALBase.SaleInvoiceByID(Sale_Invoice_ID);
        }

        #endregion

        #region Section: Update Sale Invoice Details

        /// <summary>
        /// Updates an existing sales invoice in the database.
        /// </summary>
        /// <param name="sales_Invoice">The sales invoice model to update.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        public bool UpdateSaleInvoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {
                if (_invoicesDALBase.UpdateSaleInvoice(sales_Invoice))
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

        #endregion

        #region Area: Purchase Invoice

        #region Section: Display All Purchase Invoices

        /// <summary>
        /// Retrieves all purchase invoices from the database.
        /// </summary>
        /// <returns>A list of all purchase invoices.</returns>
        public List<InvoicesModel.Purchase_Invoice_Model> DisplayAllPurchaseInvoices()
        {
            return _invoicesDALBase.DisplayAllPurchaseInvoices();
        }

        #endregion

        #region Section: Insert Purchase Invoice Details

        /// <summary>
        /// Creates a new purchase invoice in the database.
        /// </summary>
        /// <param name="purchase_Invoice">The purchase invoice model to insert.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>
        public bool InsertPurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {
                if (_invoicesDALBase.InsertPurchaseInvoice(purchase_Invoice))
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

        #region Section: Delete Purchase Invoice

        /// <summary>
        /// Deletes a purchase invoice from the database using its ID.
        /// </summary>
        /// <param name="Purchase_Invoice_ID">The ID of the purchase invoice to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        public bool DeletePurchaseInvoice(int Purchase_Invoice_ID)
        {
            try
            {
                if (_invoicesDALBase.DeletePurchaseInvoice(Purchase_Invoice_ID))
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

        #region Section: Purchase Invoice By Invoice ID

        /// <summary>
        /// Retrieves details of a specific purchase invoice by its ID.
        /// </summary>
        /// <param name="Purchase_Invoice_ID">The ID of the purchase invoice to retrieve.</param>
        /// <returns>The purchase invoice model if found; otherwise, null.</returns>
        public InvoicesModel.Purchase_Invoice_Model? PurchasenInvoiceByID(int Purchase_Invoice_ID)
        {
            return _invoicesDALBase.PurchasenInvoiceByID(Purchase_Invoice_ID);
        }

        #endregion

        #region Section: Update Purchase Invoice Details

        /// <summary>
        /// Updates an existing purchase invoice in the database.
        /// </summary>
        /// <param name="purchase_Invoice">The purchase invoice model to update.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        public bool UpdatePurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {
                if (_invoicesDALBase.UpdatePurchaseInvoice(purchase_Invoice))
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

        #endregion
    }
}