using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Data;

namespace Stock_Manage_System_API.BAL
{
    public class Invoices_BALBase
    {
        private readonly Invoices_DALBase Invoice_dALBase = new Invoices_DALBase();



        #region Sell Invoice


        #region INSERT 

        public bool CREATE_SALES_INVOICE(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {
                if (Invoice_dALBase.CREATE_SALES_INVOICE(sales_Invoice))
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

        public bool DELETE_SALES_INVOICE(int Sales_Invoice_ID)
        {
            try
            {

                if (Invoice_dALBase.DELETE_SALES_INVOICE(Sales_Invoice_ID))
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

        public List<InvoicesModel.Sales_Invoice_Model> SHOW_ALL_SALES_INVOICES()
        {
            List<InvoicesModel.Sales_Invoice_Model> List_Of_Sales_Invoice = Invoice_dALBase.SHOW_ALL_SALES_INVOICES();

            return List_Of_Sales_Invoice;

        }

        #endregion

        #region DISPLAY BY ID

        public InvoicesModel.Sales_Invoice_Model SALES_INVOICE_DETAILS_BY_ID(int Sales_Invoice_ID)
        {

            try
            {

                InvoicesModel.Sales_Invoice_Model sales_Invoice = Invoice_dALBase.SALES_INVOICE_DETAILS_BY_ID(Sales_Invoice_ID);

                return sales_Invoice;
            }
            catch
            {
                return null;
            }



        }

        #endregion

        #region UPDATE 

        public bool SALES_INVOICE_UPDATE(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {
                if (Invoice_dALBase.SALES_INVOICE_UPDATE(sales_Invoice))
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


        #region Purchase Invoice



        #region DISPLAY ALL


        public List<InvoicesModel.Purchase_Invoice_Model> SHOW_ALL_PURCHASE_INVOICES()
        {
            List<InvoicesModel.Purchase_Invoice_Model> List_Of_Purchase_Invoice = Invoice_dALBase.DISPLAY_ALL_PURCHASE_INVOICE();

            return List_Of_Purchase_Invoice;

        }


        #endregion


        #region DELETE

        public bool DELETE_PURCHASE_INVOICE(int Purchase_Invoice_ID)
        {
            try
            {

                if (Invoice_dALBase.DELETE_PURCHASE_INVOICE(Purchase_Invoice_ID))
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



        #region DISPLAY BY ID 

        public InvoicesModel.Purchase_Invoice_Model? PURCHASE_INVOICE_DETAILS_BY_ID(int Purchase_Invoice_ID)
        {
            InvoicesModel.Purchase_Invoice_Model? purchase_Invoice = Invoice_dALBase.PURCHASE_INVOICE_DETAILS_BY_ID(Purchase_Invoice_ID);


            return purchase_Invoice;
        }


        #endregion



        #region INSERT 

        public bool CREATE_PURCHASE_INVOICE(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {
                if (Invoice_dALBase.CREATE_PURCHASE_INVOICE(purchase_Invoice))
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

        public bool PURCHASE_INVOICE_UPDATE(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {
                if (Invoice_dALBase.PURCHASE_INVOICE_UPDATE(purchase_Invoice))
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
