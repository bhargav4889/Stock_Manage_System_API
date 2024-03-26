using Stock_Manage_System_API.DAL;

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

        public byte[] Customer_Account_Statement_PDF(int Customer_ID)
        {
            byte[] Customer_Account_Statement_PDF = download_DALBase.Customer_Account_Statement_PDF(Customer_ID);

            return Customer_Account_Statement_PDF;
        }

        public byte[] Customer_Account_Statement_EXCEL(int Customer_ID)
        {
            byte[] Customer_Account_Statement_EXCEL = download_DALBase.Customer_Account_Statement_EXCEL(Customer_ID);

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













    }
}
