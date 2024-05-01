using Stock_Manage_System_API.DAL;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.BAL
{
    public class Download_BALbase
    {
        private readonly Download_DALBase download_DALBase = new Download_DALBase();

        #region Section : Download Invoice PDF & Excel

        #region Download PDF & Excel For Purchase Invoice

        public byte[] Purchase_Invoice_Statement_PDF()
        {
            byte[] Purchase_Invoie_Statement_PDF = download_DALBase.Purchase_Invoice_Statement_PDF();

            return Purchase_Invoie_Statement_PDF;

        }

        public byte[] Purchase_Invoice_Statement_EXCEL()
        {
            byte[] Purchase_Invoice_Statement_EXCEL = download_DALBase.Purchase_Invoice_Statement_EXCEL();

            return Purchase_Invoice_Statement_EXCEL;
        }


        #endregion

        #region Download PDF & Excel For Sales Invoice

        public byte[] Sales_Invoice_Statement_PDF()
        {
            byte[] Sales_Invoice_Statement_PDF = download_DALBase.Sales_Invoice_Statement_PDF();

            return Sales_Invoice_Statement_PDF;

        }

        public byte[] Sales_Invoice_Statement_EXCEL()
        {
            byte[] Sales_Invoice_Statement_EXCEL = download_DALBase.Sales_Invoice_Statement_EXCEL();

            return Sales_Invoice_Statement_EXCEL;
        }

        #endregion

        #endregion

        #region  Section : Download Customers Statement & Customer Account Statements PDF & Excel

        #region Method : Download PDF & Excel Customers Statements
        public byte[] Customers_Statement_PDF()
        {
            byte[] Customers_Statement_PDF = download_DALBase.Customers_Statement_PDF();

            return Customers_Statement_PDF;
        }

        public byte[] Customers_Statement_EXCEL()
        {
            byte[] Customers_Statement_EXCEL = download_DALBase.Customers_Statement_EXCEL();

            return Customers_Statement_EXCEL;
        }

        #endregion

        #region Method : Download PDF & Excel Customer Account Statements

        public byte[] Customer_Account_Statement_PDF(int Customer_ID, string Customer_Type)
        {
            byte[] Customer_Account_Statement_PDF = download_DALBase.Customer_Account_Statement_PDF(Customer_ID, Customer_Type);

            return Customer_Account_Statement_PDF;
        }

        public byte[] Customer_Account_Statement_EXCEL(int Customer_ID,string Customer_Type)
        {
            byte[] Customer_Account_Statement_EXCEL = download_DALBase.Customer_Account_Statement_EXCEL(Customer_ID, Customer_Type);

            return Customer_Account_Statement_EXCEL;
        }


        #endregion

        #endregion

        #region Section : Download Purchase Stock Statement PDF & Excel
        public byte[] Purchase_Stocks_Statement_PDF()
        {
            byte[] Purchase_Stocks_Statement_PDF = download_DALBase.Purchase_Stocks_Statement_PDF();

            return Purchase_Stocks_Statement_PDF;
        }

        public byte[] Purchase_Stocks_Statement_EXCEL()
        {
            byte[] Purchase_Stocks_Statement_EXCEL = download_DALBase.Purchase_Stocks_Statement_EXCEL();

            return Purchase_Stocks_Statement_EXCEL;
        }

        #endregion


        #region Section : Download Pending Payments PDF & Excel

        public byte[] Pending_Payments_PDF()
        {
            byte[] Pending_Payments = download_DALBase.Pending_Payments_PDF();

            return Pending_Payments;
        }

        public byte[] Pending_Payments_EXCEL()
        {
            byte[] Pending_Payments = download_DALBase.Pending_Payments_EXCEL();

            return Pending_Payments;
        }

        #endregion

        #region Section : Download Remain Payments PDF & Excel

        public byte[] Remain_Payments_PDF()
        {
            byte[] Remain_Payments = download_DALBase.Remain_Payments_PDF();

            return Remain_Payments;
        }

        public byte[] Remain_Payments_EXCEL()
        {
            byte[] Remain_Payments = download_DALBase.Remain_Payments_EXCEL();

            return Remain_Payments;
        }

        #endregion

        #region Section : Download Paid Payments PDF & Excel

        public byte[] Paid_Payments_PDF()
        {
            byte[] Paid_Payments = download_DALBase.Paid_Payments_PDF();

            return Paid_Payments;
        }

        public byte[] Paid_Payments_EXCEL()
        {
            byte[] Paid_Payments = download_DALBase.Paid_Payments_EXCEL();

            return Paid_Payments;
        }

        #endregion











    }
}
