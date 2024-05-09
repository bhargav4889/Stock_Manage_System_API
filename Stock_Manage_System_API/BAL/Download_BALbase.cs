using Stock_Manage_System_API.DAL;
using static Stock_Manage_System_API.Models.InvoicesModel;

namespace Stock_Manage_System_API.BAL
{
    public class Download_BALbase
    {
        private readonly Download_DALBase download_DALBase = new Download_DALBase();

        #region Section : Download Invoice PDF & Excel

        #region Download PDF & Excel For Purchase Invoice

        public byte[] PurchaseInvoiceStatementPDF()
        {
            byte[] PurchaseInvoiceStatementPDF = download_DALBase.PurchaseInvoiceStatementPDF();

            return PurchaseInvoiceStatementPDF;

        }

        public byte[] PurchaseInvoiceStatementEXCEL()
        {
            byte[] PurchaseInvoiceStatementEXCEL = download_DALBase.PurchaseInvoiceStatementEXCEL();

            return PurchaseInvoiceStatementEXCEL;
        }


        #endregion

        #region Download PDF & Excel For Sales Invoice

        public byte[] SalesInvoiceStatementPDF()
        {
            byte[] SalesInvoiceStatementPDF = download_DALBase.SalesInvoiceStatementPDF();

            return SalesInvoiceStatementPDF;

        }

        public byte[] SalesInvoiceStatementEXCEL()
        {
            byte[] SalesInvoiceStatementEXCEL = download_DALBase.SalesInvoiceStatementEXCEL();

            return SalesInvoiceStatementEXCEL;
        }

        #endregion

        #endregion

        #region  Section : Download Customers Statement & Customer Account Statements PDF & Excel

        #region Method : Download PDF & Excel Customers Statements
        public byte[] CustomersStatementPDF()
        {
            byte[] CustomersStatementPDF = download_DALBase.CustomersStatementPDF();

            return CustomersStatementPDF;
        }

        public byte[] CustomersStatementEXCEL()
        {
            byte[] CustomersStatementEXCEL = download_DALBase.CustomersStatementEXCEL();

            return CustomersStatementEXCEL;
        }

        #endregion

        #region Method : Download PDF & Excel Customer Account Statements

        public byte[] CustomerAccountStatementPDF(int Customer_ID, string Customer_Type)
        {
            byte[] CustomerAccountStatementPDF = download_DALBase.CustomerAccountStatementPDF(Customer_ID, Customer_Type);

            return CustomerAccountStatementPDF;
        }

        public byte[] CustomerAccountStatementEXCEL(int Customer_ID, string Customer_Type)
        {
            byte[] CustomerAccountStatementEXCEL = download_DALBase.CustomerAccountStatementEXCEL(Customer_ID, Customer_Type);

            return CustomerAccountStatementEXCEL;
        }


        #endregion

        #endregion

        #region Section : Download Purchase Stock Statement PDF & Excel
        public byte[] PurchaseStocksStatementPDF()
        {
            byte[] PurchaseStocksStatementPDF = download_DALBase.PurchaseStocksStatementPDF();

            return PurchaseStocksStatementPDF;
        }

        public byte[] PurchaseStocksStatementEXCEL()
        {
            byte[] PurchaseStocksStatementEXCEL = download_DALBase.PurchaseStocksStatementEXCEL();

            return PurchaseStocksStatementEXCEL;
        }

        #endregion


        #region Section : Download Sales Statement PDF & Excel

        public byte[] SalesStatementPDF()
        {
            byte[] SalesStatementPDF = download_DALBase.SalesStatementPDF();

            return SalesStatementPDF;
        }

        public byte[] SalesStatementEXCEL()
        {
            byte[] SalesStatementEXCEL = download_DALBase.SalesStatementEXCEL();

            return SalesStatementEXCEL;
        }

        #endregion


        #region Section : Download Pending Payments PDF & Excel

        public byte[] PendingPaymentsPDF()
        {
            byte[] PendingPaymentsPDF = download_DALBase.PendingPaymentsPDF();

            return PendingPaymentsPDF;
        }

        public byte[] PendingPaymentsEXCEL()
        {
            byte[] PendingPaymentsEXCEL = download_DALBase.PendingPaymentsEXCEL();

            return PendingPaymentsEXCEL;
        }

        #endregion

        #region Section : Download Remain Payments PDF & Excel

        public byte[] RemainPaymentsPDF()
        {
            byte[] RemainPaymentsPDF = download_DALBase.RemainPaymentsPDF();

            return RemainPaymentsPDF;
        }

        public byte[] RemainPaymentsEXCEL()
        {
            byte[] RemainPaymentsEXCEL = download_DALBase.RemainPaymentsEXCEL();

            return RemainPaymentsEXCEL;
        }

        #endregion

        #region Section : Download Paid Payments PDF & Excel

        public byte[] PaidPaymentsPDF()
        {
            byte[] PaidPaymentsPDF = download_DALBase.PaidPaymentsPDF();

            return PaidPaymentsPDF;
        }

        public byte[] PaidPaymentsEXCEL()
        {
            byte[] PaidPaymentsEXCEL = download_DALBase.PaidPaymentsEXCEL();

            return PaidPaymentsEXCEL;
        }

        #endregion











    }
}
