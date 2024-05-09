using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    /// <summary>
    /// A base class for Sales Business Access Layer operations.
    /// </summary>
    public class Sales_BALBase
    {
        public readonly Sales_DALBase sales_DALBase = new Sales_DALBase();

        #region Method : Insert Sale

        /// <summary>
        /// Inserts a sale with customer information into the database.
        /// </summary>
        /// <param name="sale_Customer_Combied_Model">The sale and customer information to be inserted.</param>
        /// <returns>True if the sale is inserted successfully, false otherwise.</returns>
        public bool InsertSale(Sale_Customer_Combied_Model sale_Customer_Combied_Model)
        {
            try
            {

                if (sales_DALBase.InsertSale(sale_Customer_Combied_Model))
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

        #region Method : Update Sale
        /// <summary>
        /// Updates a sale in the database.
        /// </summary>
        /// <param name="sale_Customer_Combied_Model">The updated sale and customer information.</param>
        /// <returns>True if the sale is updated successfully, false otherwise.</returns>
        public bool UpdateSale(Sale_Customer_Combied_Model sale_Customer_Combied_Model)
        {
            try
            {

                if (sales_DALBase.UpdateSale(sale_Customer_Combied_Model))
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

        #region Method : Delete Sale
        /// <summary>
        /// Deletes a sale from the database.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale to delete.</param>
        /// <returns>True if the sale is deleted successfully, false otherwise.</returns>
        public bool DeleteSale(int Sale_ID)
        {
            try
            {

                if (sales_DALBase.DeleteSale(Sale_ID))
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

        #region Method : Sale With Customer Information Data

        /// <summary>
        /// Retrieves the sale and customer details for a given sale and customer ID.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale.</param>
        /// <param name="Customer_ID">The ID of the customer.</param>
        /// <returns>A <see cref="Sale_Customer_Combied_Model"/> object containing the sale and customer details.</returns>
        public Sale_Customer_Combied_Model GetSaleByCustomerAndSaleID(int Sale_ID, int Customer_ID)
        {
            Sale_Customer_Combied_Model sale_Customer_Combied_Model = sales_DALBase.GetSaleByCustomerAndSaleID(Sale_ID, Customer_ID);

            return sale_Customer_Combied_Model;
        }

        #endregion

        #region Method : Get All Sales

        /// <summary>
        /// Retrieves a list of all sales from the database.
        /// </summary>
        /// <returns>A list of <see cref="Show_Sale"/> objects representing all sales.</returns>
        public List<Show_Sale> GetAllSales()
        {
            List<Show_Sale> List_of_Sales_Info = sales_DALBase.GetAllSales();

            return List_of_Sales_Info;
        }

        #endregion

        #region Method : Sale Information By ID 

        /// <summary>
        /// Retrieves a sale by its ID from the database.
        /// </summary>
        /// <param name="Sale_ID">The ID of the sale to retrieve.</param>
        /// <returns>A <see cref="Show_Sale"/> object representing the sale with the specified ID.</returns>
        public Show_Sale GetSaleByID(int Sale_ID)
        {
            Show_Sale sale = sales_DALBase.GetSaleByID(Sale_ID);

            return sale;
        }


        #endregion
    }
}