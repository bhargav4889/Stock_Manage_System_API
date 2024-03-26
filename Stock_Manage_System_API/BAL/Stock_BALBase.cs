using Microsoft.Extensions.Logging.Abstractions;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Stock_BALBase
    {
        private readonly Stock_DALBase _stockDalBase = new Stock_DALBase();


        #region INSERT
        public bool PURCHASE_STOCK_INSERT(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {

                if (_stockDalBase.PURCHASE_STOCK_INSERT(purchase_Stock_With_Customer_Model))
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


        #region UPDATE

        public bool PURCHASE_STOCK_UPDATE(Purchase_Stock_With_Customer_Model purchase_Stock_With_Customer_Model)
        {
            try
            {
                if (_stockDalBase.PURCHASE_STOCK_UPDATE(purchase_Stock_With_Customer_Model))
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


        #region DELETE
        public bool PURCHASE_STOCK_DELETE(int STOCK_ID)
        {
            try
            {

                if (_stockDalBase.PURCHASE_STOCK_DELETE(STOCK_ID))
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


        #region DISPLAY ALL
        public List<Show_Purchase_Stock>? DISPLAY_ALL_PURCHASE_STOCK()
        {
            try
            {
                
                List<Show_Purchase_Stock>? List_Of_Stocks = _stockDalBase.DISPLAY_ALL_PURCHASE_STOCK();
                return List_Of_Stocks;
            }
            catch
            {
                return null;
            }

        }

        #endregion


        #region DISPLAY BY ID
        public Show_Purchase_Stock? PURCHASE_STOCKS_BY_PK(int Stock_ID)
        {
            try
            {

                Show_Purchase_Stock? show_Purchase_Stock = _stockDalBase.PURCHASE_STOCKS_BY_PK(Stock_ID);


                return show_Purchase_Stock;
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
