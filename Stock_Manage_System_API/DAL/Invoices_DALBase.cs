﻿using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;
using static Stock_Manage_System_API.Models.InvoicesModel;
using System.Text;
using System.Drawing;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// Base Data Access Layer class responsible for managing database operations related to stock.
    /// </summary>
    public class Invoices_DALBase : DAL_Helpers
    {

        #region Section: SetUp Of Database Connection and Initialization

        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the Invoices_DALBase class, setting up the database connection.
        /// </summary>
        public Invoices_DALBase()
        {
            // Assuming 'Database_Connection' is a predefined string or obtained elsewhere in your application.

            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        /// <summary>
        /// Retrieves a DbCommand object configured for executing the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure for which to get the DbCommand.</param>
        /// <returns>A DbCommand object configured to execute the named stored procedure.</returns>
        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        #endregion



        #region Area: Purchase Invoice

        #region Section: Delete Purchase Invoice

        /// <summary>
        /// Deletes a specific purchase invoice based on its ID.
        /// </summary>
        /// <param name="Purchase_Invoice_ID">The ID of the purchase invoice to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        public bool DeletePurchaseInvoice(int Purchase_Invoice_ID)
        {
            try
            {
                DbCommand dbCommand = Command_Name("API_PURCHASE_INVOICE_DELETE");
                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, Purchase_Invoice_ID);
                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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


        #region Section: Display All Purchase Invoices
        /// <summary>
        /// Retrieves all purchase invoice entries from the database.
        /// </summary>
        /// <returns>A list of Purchase_Invoice_Model objects if successful; otherwise, null.</returns>

        public List<InvoicesModel.Purchase_Invoice_Model> DisplayAllPurchaseInvoices()
        {

            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_PURCHASE_INVOICE");

            List<InvoicesModel.Purchase_Invoice_Model> List_Of_Purchase_Invoice = new List<InvoicesModel.Purchase_Invoice_Model>();

            using (IDataReader row = sqlDatabase.ExecuteReader(dbCommand))
            {
                while (row.Read())
                {
                    InvoicesModel.Purchase_Invoice_Model purchase_Invoice = new InvoicesModel.Purchase_Invoice_Model();

                    purchase_Invoice.PurchaseInvoiceId = Convert.ToInt32(row[0]);

                    purchase_Invoice.PurchaseInvoiceDate = Convert.ToDateTime(row[1].ToString());

                    purchase_Invoice.CustomerName = row[2].ToString();

                    purchase_Invoice.ProductId = Convert.ToInt32(row[3].ToString());

                    purchase_Invoice.ProductName = row[4].ToString();



                    purchase_Invoice.ProductGrade = row[5].ToString();


                    // Handling nullable decimal for Bags
                    string bagsStr = row["BAGS"].ToString();
                    if (decimal.TryParse(bagsStr, out decimal bags))
                    {
                        purchase_Invoice.Bags = bags;
                    }
                    else
                    {
                        purchase_Invoice.Bags = null; // Or handle "--" specifically if needed
                    }

                    // Handling nullable decimal for BagPerKg
                    string bagPerKgStr = row["BAG_PER_KG"].ToString();
                    if (decimal.TryParse(bagPerKgStr, out decimal bagPerKg))
                    {
                        purchase_Invoice.BagPerKg = bagPerKg;
                    }
                    else
                    {
                        purchase_Invoice.BagPerKg = null; // Or handle "--" specifically if needed
                    }


                    purchase_Invoice.TotalWeight = Convert.ToDecimal(row[8].ToString());

                    purchase_Invoice.ProductPrice = Convert.ToDecimal(row[9].ToString());

                    purchase_Invoice.TotalPrice = Convert.ToDecimal(row[10].ToString());

                    purchase_Invoice.VehicleId = Convert.ToInt32(row[11].ToString());

                    purchase_Invoice.VehicleName = (row[12].ToString());

                    purchase_Invoice.VehicleNo = row[13].ToString();

                    purchase_Invoice.DriverName = (row[14].ToString());

                    purchase_Invoice.TolatName = (row[15].ToString());


                    List_Of_Purchase_Invoice.Add(purchase_Invoice);



                }

                return List_Of_Purchase_Invoice;


            }





        }

        #endregion

        #region Section: Purchase Invoice By Invoice ID

        /// <summary>
        /// Fetches details of a specific purchase invoice by its ID.
        /// </summary>
        /// <param name="purchaseInvoiceId">The ID of the purchase invoice to retrieve.</param>
        /// <returns>An instance of <see cref="InvoicesModel.Purchase_Invoice_Model"/> if found; otherwise, null.</returns>

        public InvoicesModel.Purchase_Invoice_Model? PurchasenInvoiceByID(int Purchase_Invoice_ID)
        {

            try
            {

                DbCommand dbCommand = Command_Name("API_PURCHASE_INVOICE_BY_PK");


                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, Purchase_Invoice_ID);

                InvoicesModel.Purchase_Invoice_Model purchase_Invoice = new InvoicesModel.Purchase_Invoice_Model();


                using (IDataReader row = sqlDatabase.ExecuteReader(dbCommand))
                {
                    while (row.Read())
                    {



                        purchase_Invoice.PurchaseInvoiceId = Convert.ToInt32(row[0]);

                        purchase_Invoice.PurchaseInvoiceDate = Convert.ToDateTime(row[1].ToString());

                        purchase_Invoice.CustomerName = row[2].ToString();

                        purchase_Invoice.ProductId = Convert.ToInt32(row[3].ToString());

                        purchase_Invoice.ProductName = row[4].ToString();



                        purchase_Invoice.ProductGrade = row[5].ToString();


                        // Handling nullable decimal for Bags
                        string bagsStr = row["BAGS"].ToString();
                        if (decimal.TryParse(bagsStr, out decimal bags))
                        {
                            purchase_Invoice.Bags = bags;
                        }
                        else
                        {
                            purchase_Invoice.Bags = null; // Or handle "--" specifically if needed
                        }

                        // Handling nullable decimal for BagPerKg
                        string bagPerKgStr = row["BAG_PER_KG"].ToString();
                        if (decimal.TryParse(bagPerKgStr, out decimal bagPerKg))
                        {
                            purchase_Invoice.BagPerKg = bagPerKg;
                        }
                        else
                        {
                            purchase_Invoice.BagPerKg = null; // Or handle "--" specifically if needed
                        }


                        purchase_Invoice.TotalWeight = Convert.ToDecimal(row[8].ToString());

                        purchase_Invoice.ProductPrice = Convert.ToDecimal(row[9].ToString());

                        purchase_Invoice.TotalPrice = Convert.ToDecimal(row[10].ToString());

                        purchase_Invoice.VehicleId = Convert.ToInt32(row[11].ToString());

                        purchase_Invoice.VehicleName = (row[12].ToString());

                        purchase_Invoice.VehicleNo = row[13].ToString();

                        purchase_Invoice.DriverName = (row[14].ToString());

                        purchase_Invoice.TolatName = (row[15].ToString());







                    }

                    return purchase_Invoice;


                }





            }
            catch
            {
                return null;
            }




        }

        #endregion


        #region Section: Insert Purchase Invoice Details

        /// <summary>
        /// Inserts a new purchase invoice entry into the database.
        /// </summary>
        /// <param name="purchase_Invoice">The model containing all necessary data for the purchase invoice.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>

        public bool InsertPurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {


                DbCommand dbCommand = Command_Name("API_PURCHASE_INVOICE_INSERT");

                DateTime? invoiceDate = purchase_Invoice.PurchaseInvoiceDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_DATE", SqlDbType.Date, onlyDate);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, purchase_Invoice.CustomerName);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, purchase_Invoice.ProductId);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_GRADE_ID", SqlDbType.Int, purchase_Invoice.ProductGradeId);

                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, purchase_Invoice.Bags);

                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, purchase_Invoice.BagPerKg);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, purchase_Invoice.TotalWeight);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, purchase_Invoice.ProductPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, purchase_Invoice.TotalPrice);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, purchase_Invoice.VehicleId);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.NVarChar, purchase_Invoice.VehicleNo);

                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, purchase_Invoice.DriverName);

                sqlDatabase.AddInParameter(dbCommand, "@TOLAT_NAME", SqlDbType.NVarChar, purchase_Invoice.TolatName);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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



        #region Section: Update Purchase Invoice Details


        /// <summary>
        /// Updates a purchase invoice entry with details.
        /// </summary>
        /// <param name="purchase_Invoice">The model containing all data necessary for updating the purchase invoice details.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>

        public bool UpdatePurchaseInvoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            try
            {


                DbCommand dbCommand = Command_Name("API_PURCHASE_INVOICE_UPDATE");

                DateTime? invoiceDate = purchase_Invoice.PurchaseInvoiceDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, purchase_Invoice.PurchaseInvoiceId);

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_DATE", SqlDbType.Date, onlyDate);

                sqlDatabase.AddInParameter(dbCommand, "@CUSTOMER_NAME", SqlDbType.NVarChar, purchase_Invoice.CustomerName);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, purchase_Invoice.ProductId);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_GRADE_ID", SqlDbType.Int, purchase_Invoice.ProductGradeId);

                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, purchase_Invoice.Bags);

                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, purchase_Invoice.BagPerKg);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, purchase_Invoice.TotalWeight);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, purchase_Invoice.ProductPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, purchase_Invoice.TotalPrice);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, purchase_Invoice.VehicleId);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.NVarChar, purchase_Invoice.VehicleNo);

                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, purchase_Invoice.DriverName);

                sqlDatabase.AddInParameter(dbCommand, "@TOLAT_NAME", SqlDbType.NVarChar, purchase_Invoice.TolatName);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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


        #region Area: Sale Invoice



        #region Section: Insert Sale Invoice Details

        /// <summary>
        /// Inserts a new sale invoice entry into the database.
        /// </summary>
        /// <param name="sales_Invoice">The model containing all necessary data for the sale invoice.</param>
        /// <returns>True if the insertion is successful; otherwise, false.</returns>

        public bool InsertSaleInvoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {


                DbCommand dbCommand = Command_Name("API_SALES_INVOICE_INSERT");

                DateTime? invoiceDate = sales_Invoice.SalesInvoiceDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_DATE", SqlDbType.Date, onlyDate);


                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_TYPE", SqlDbType.VarChar, sales_Invoice.InvoiceType);


                sqlDatabase.AddInParameter(dbCommand, "@BROKER_NAME", SqlDbType.NVarChar, sales_Invoice.BrokerName);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_NAME", SqlDbType.NVarChar, sales_Invoice.PartyName);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_GSTNO", SqlDbType.NVarChar, sales_Invoice.PartyGstNo);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_ADDRESS", SqlDbType.NVarChar, sales_Invoice.PartyAddress);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, sales_Invoice.ProductId);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_BRAND_NAME", SqlDbType.NVarChar, sales_Invoice.ProductBrandName);

                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, sales_Invoice.Bags);

                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, sales_Invoice.BagPerKg);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, sales_Invoice.TotalWeight);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, sales_Invoice.ProductPrice);

                sqlDatabase.AddInParameter(dbCommand, "@CGST", SqlDbType.Decimal, sales_Invoice.CGST);

                sqlDatabase.AddInParameter(dbCommand, "@SGST", SqlDbType.Decimal, sales_Invoice.SGST);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_CGST_PRICE", SqlDbType.Decimal, sales_Invoice.TotalCGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_SGST_PRICE", SqlDbType.Decimal, sales_Invoice.TotalSGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@WITHOUT_GST_PRICE", SqlDbType.Decimal, sales_Invoice.WithoutGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, sales_Invoice.TotalPrice);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, sales_Invoice.VehicleId);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.NVarChar, sales_Invoice.VehicleNo);

                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, sales_Invoice.DriverName);

                sqlDatabase.AddInParameter(dbCommand, "@CONTAINER_NO", SqlDbType.NVarChar, sales_Invoice.ContainerNo);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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


        #region Section: Update Sale Invoice Details

        /// <summary>
        /// Updates a sale invoice entry with details.
        /// </summary>
        /// <param name="sales_Invoice">The model containing all data necessary for updating the sale invoice details.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>

        public bool UpdateSaleInvoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            try
            {


                DbCommand dbCommand = Command_Name("API_SALES_INVOICE_UPDATE");

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, sales_Invoice.SalesInvoiceId);

                DateTime? invoiceDate = sales_Invoice.SalesInvoiceDate;

                DateTime? onlyDate = invoiceDate?.Date;

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_DATE", SqlDbType.Date, onlyDate);

                if (sales_Invoice.InvoiceType == "other" && !string.IsNullOrEmpty(sales_Invoice.OtherInvoiceType))
                {
                    sqlDatabase.AddInParameter(dbCommand, "@INVOICE_TYPE", SqlDbType.VarChar, sales_Invoice.OtherInvoiceType);
                }
                else
                {
                    sqlDatabase.AddInParameter(dbCommand, "@INVOICE_TYPE", SqlDbType.VarChar, sales_Invoice.InvoiceType);
                }

                sqlDatabase.AddInParameter(dbCommand, "@BROKER_NAME", SqlDbType.NVarChar, sales_Invoice.BrokerName);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_NAME", SqlDbType.NVarChar, sales_Invoice.PartyName);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_GSTNO", SqlDbType.NVarChar, sales_Invoice.PartyGstNo);

                sqlDatabase.AddInParameter(dbCommand, "@PARTY_ADDRESS", SqlDbType.NVarChar, sales_Invoice.PartyAddress);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_ID", SqlDbType.Int, sales_Invoice.ProductId);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_BRAND_NAME", SqlDbType.NVarChar, sales_Invoice.ProductBrandName);

                sqlDatabase.AddInParameter(dbCommand, "@BAGS", SqlDbType.Decimal, sales_Invoice.Bags);

                sqlDatabase.AddInParameter(dbCommand, "@BAG_PER_KG", SqlDbType.Decimal, sales_Invoice.BagPerKg);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_WEIGHT", SqlDbType.Decimal, sales_Invoice.TotalWeight);

                sqlDatabase.AddInParameter(dbCommand, "@PRODUCT_PRICE", SqlDbType.Decimal, sales_Invoice.ProductPrice);

                sqlDatabase.AddInParameter(dbCommand, "@CGST", SqlDbType.Decimal, sales_Invoice.CGST);

                sqlDatabase.AddInParameter(dbCommand, "@SGST", SqlDbType.Decimal, sales_Invoice.SGST);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_CGST_PRICE", SqlDbType.Decimal, sales_Invoice.TotalCGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_SGST_PRICE", SqlDbType.Decimal, sales_Invoice.TotalSGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@WITHOUT_GST_PRICE", SqlDbType.Decimal, sales_Invoice.WithoutGSTPrice);

                sqlDatabase.AddInParameter(dbCommand, "@TOTAL_PRICE", SqlDbType.Decimal, sales_Invoice.TotalPrice);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_ID", SqlDbType.Int, sales_Invoice.VehicleId);

                sqlDatabase.AddInParameter(dbCommand, "@VEHICLE_NO", SqlDbType.NVarChar, sales_Invoice.VehicleNo);



                sqlDatabase.AddInParameter(dbCommand, "@DRIVER_NAME", SqlDbType.NVarChar, sales_Invoice.DriverName);



                sqlDatabase.AddInParameter(dbCommand, "@CONTAINER_NO", SqlDbType.NVarChar, sales_Invoice.ContainerNo);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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

        #region Section: Display All Sale Invoices

        /// <summary>
        /// Retrieves all sale invoice entries from the database.
        /// </summary>
        /// <returns>A list of Sale_Invoice_Model objects if successful; otherwise, null.</returns>

        public List<InvoicesModel.Sales_Invoice_Model> DisplayAllSaleInvoices()
        {


            DbCommand dbCommand = Command_Name("API_DISPLAY_ALL_SALES_INVOICE");

            List<InvoicesModel.Sales_Invoice_Model> List_Of_Sales_Invoice = new List<InvoicesModel.Sales_Invoice_Model>();

            using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {
                    InvoicesModel.Sales_Invoice_Model sales_Invoice = new InvoicesModel.Sales_Invoice_Model();

                    sales_Invoice.SalesInvoiceId = Convert.ToInt32(dataReader[0]);

                    sales_Invoice.SalesInvoiceDate = Convert.ToDateTime(dataReader[1].ToString());

                    sales_Invoice.InvoiceType = dataReader[2].ToString();

                    sales_Invoice.BrokerName = dataReader[3].ToString();

                    sales_Invoice.PartyName = dataReader[4].ToString();

                    sales_Invoice.PartyGstNo = dataReader[5].ToString();

                    sales_Invoice.PartyAddress = dataReader[6].ToString();

                    sales_Invoice.ProductId = Convert.ToInt32(dataReader[7].ToString());

                    sales_Invoice.ProductName = dataReader[8].ToString();

                    sales_Invoice.ProductBrandName = dataReader[9].ToString();

                    // Handling nullable decimal for Bags
                    string bagsStr = dataReader[10].ToString();
                    if (decimal.TryParse(bagsStr, out decimal bags))
                    {
                        sales_Invoice.Bags = bags;
                    }
                    else
                    {
                        sales_Invoice.Bags = null; // Or handle "--" specifically if needed
                    }

                    // Handling nullable decimal for BagPerKg
                    string bagPerKgStr = dataReader[11].ToString();
                    if (decimal.TryParse(bagPerKgStr, out decimal bagPerKg))
                    {
                        sales_Invoice.BagPerKg = bagPerKg;
                    }
                    else
                    {
                        sales_Invoice.BagPerKg = null; // Or handle "--" specifically if needed
                    }



                    sales_Invoice.TotalWeight = Convert.ToDecimal(dataReader[12]);

                    sales_Invoice.ProductPrice = Convert.ToDecimal(dataReader[13]);



                    // Handling nullable decimal for Bags
                    string sgstStr = dataReader[14].ToString();
                    if (decimal.TryParse(sgstStr, out decimal sgst))
                    {
                        sales_Invoice.SGST = sgst;
                    }
                    else
                    {
                        sales_Invoice.SGST = null; // Or handle "--" specifically if needed
                    }


                    // Handling nullable decimal for Bags
                    string cgstStr = dataReader[15].ToString();
                    if (decimal.TryParse(cgstStr, out decimal cgst))
                    {
                        sales_Invoice.CGST = cgst;
                    }
                    else
                    {
                        sales_Invoice.CGST = null; // Or handle "--" specifically if needed
                    }

                    // Handling nullable decimal for Bags
                    string totalsgstStr = dataReader[16].ToString();
                    if (decimal.TryParse(totalsgstStr, out decimal totalsgst))
                    {
                        sales_Invoice.TotalSGSTPrice = totalsgst;
                    }
                    else
                    {
                        sales_Invoice.TotalSGSTPrice = null; // Or handle "--" specifically if needed
                    }

                    // Handling nullable decimal for Bags
                    string totalcgstStr = dataReader[17].ToString();
                    if (decimal.TryParse(totalcgstStr, out decimal totalcgst))
                    {
                        sales_Invoice.TotalCGSTPrice = cgst;
                    }
                    else
                    {
                        sales_Invoice.TotalCGSTPrice = null; // Or handle "--" specifically if needed
                    }

                    // Handling nullable decimal for Bags
                    string WithoutGstStr = dataReader[18].ToString();
                    if (decimal.TryParse(WithoutGstStr, out decimal withoutGst))
                    {
                        sales_Invoice.WithoutGSTPrice = withoutGst;
                    }
                    else
                    {
                        sales_Invoice.WithoutGSTPrice = null; // Or handle "--" specifically if needed
                    }




                    /*  sales_Invoice.TotalSGSTPrice = Convert.ToDecimal(dataReader[16]);

					  sales_Invoice.TotalCGSTPrice = Convert.ToDecimal(dataReader[17]);

					  sales_Invoice.WithoutGSTPrice = Convert.ToDecimal(dataReader[18]);*/

                    sales_Invoice.TotalPrice = Convert.ToDecimal(dataReader[19]);

                    sales_Invoice.VehicleId = Convert.ToInt32(dataReader[20]);

                    sales_Invoice.VehicleName = (dataReader[21].ToString());

                    sales_Invoice.VehicleNo = (dataReader[22].ToString());

                    sales_Invoice.DriverName = (dataReader[23].ToString());

                    sales_Invoice.ContainerNo = (dataReader[24].ToString());




                    List_Of_Sales_Invoice.Add(sales_Invoice);

                };





            }

            return List_Of_Sales_Invoice;

        }


        #endregion

        #region Section: Delete Sale Invoice

        /// <summary>
        /// Deletes a specific sales invoice based on its ID.
        /// </summary>
        /// <param name="Sales_Invoice_ID">The ID of the sales invoice to delete.</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>

        public bool DeleteSaleInvoice(int Sale_Invoice_ID)
        {
            try
            {

                DbCommand dbCommand = Command_Name("API_SALES_INVOICE_DELETE");

                sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, Sale_Invoice_ID);


                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(dbCommand)))
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
        /// Fetches details of a specific sales invoice by its ID.
        /// </summary>
        /// <param name="Sales_Invoice_ID">The ID of the sales invoice to retrieve.</param>
        /// <returns>An instance of <see cref="InvoicesModel.Sales_Invoice_Model"/> if found; otherwise, null.</returns>

        public InvoicesModel.Sales_Invoice_Model SaleInvoiceByID(int Sale_Invoice_ID)
        {
            try
            {
                {


                    DbCommand dbCommand = Command_Name("API_SALES_INVOICE_BY_PK");


                    sqlDatabase.AddInParameter(dbCommand, "@INVOICE_ID", SqlDbType.Int, Sale_Invoice_ID);

                    InvoicesModel.Sales_Invoice_Model sales_Invoice = new InvoicesModel.Sales_Invoice_Model();

                    using (IDataReader dataReader = sqlDatabase.ExecuteReader(dbCommand))
                    {

                        if (dataReader.Read())
                        {


                            sales_Invoice.SalesInvoiceId = Convert.ToInt32(dataReader[0]);

                            sales_Invoice.SalesInvoiceDate = Convert.ToDateTime(dataReader[1].ToString());

                            sales_Invoice.InvoiceType = dataReader[2].ToString();

                            sales_Invoice.BrokerName = dataReader[3].ToString();

                            sales_Invoice.PartyName = dataReader[4].ToString();

                            sales_Invoice.PartyGstNo = dataReader[5].ToString();

                            sales_Invoice.PartyAddress = dataReader[6].ToString();

                            sales_Invoice.ProductId = Convert.ToInt32(dataReader[7].ToString());

                            sales_Invoice.ProductName = dataReader[8].ToString();

                            sales_Invoice.ProductBrandName = dataReader[9].ToString();

                            // Handling nullable decimal for Bags
                            string bagsStr = dataReader[10].ToString();
                            if (decimal.TryParse(bagsStr, out decimal bags))
                            {
                                sales_Invoice.Bags = bags;
                            }
                            else
                            {
                                sales_Invoice.Bags = null; // Or handle "--" specifically if needed
                            }

                            // Handling nullable decimal for BagPerKg
                            string bagPerKgStr = dataReader[11].ToString();
                            if (decimal.TryParse(bagPerKgStr, out decimal bagPerKg))
                            {
                                sales_Invoice.BagPerKg = bagPerKg;
                            }
                            else
                            {
                                sales_Invoice.BagPerKg = null; // Or handle "--" specifically if needed
                            }



                            sales_Invoice.TotalWeight = Convert.ToDecimal(dataReader[12]);

                            sales_Invoice.ProductPrice = Convert.ToDecimal(dataReader[13]);



                            // Handling nullable decimal for Bags
                            string sgstStr = dataReader[14].ToString();
                            if (decimal.TryParse(sgstStr, out decimal sgst))
                            {
                                sales_Invoice.SGST = sgst;
                            }
                            else
                            {
                                sales_Invoice.SGST = null; // Or handle "--" specifically if needed
                            }


                            // Handling nullable decimal for Bags
                            string cgstStr = dataReader[15].ToString();
                            if (decimal.TryParse(cgstStr, out decimal cgst))
                            {
                                sales_Invoice.CGST = cgst;
                            }
                            else
                            {
                                sales_Invoice.CGST = null; // Or handle "--" specifically if needed
                            }

                            // Handling nullable decimal for Bags
                            string totalsgstStr = dataReader[16].ToString();
                            if (decimal.TryParse(totalsgstStr, out decimal totalsgst))
                            {
                                sales_Invoice.TotalSGSTPrice = totalsgst;
                            }
                            else
                            {
                                sales_Invoice.TotalSGSTPrice = null; // Or handle "--" specifically if needed
                            }

                            // Handling nullable decimal for Bags
                            string totalcgstStr = dataReader[17].ToString();
                            if (decimal.TryParse(totalcgstStr, out decimal totalcgst))
                            {
                                sales_Invoice.TotalCGSTPrice = cgst;
                            }
                            else
                            {
                                sales_Invoice.TotalCGSTPrice = null; // Or handle "--" specifically if needed
                            }

                            // Handling nullable decimal for Bags
                            string WithoutGstStr = dataReader[18].ToString();
                            if (decimal.TryParse(WithoutGstStr, out decimal withoutGst))
                            {
                                sales_Invoice.WithoutGSTPrice = withoutGst;
                            }
                            else
                            {
                                sales_Invoice.WithoutGSTPrice = null; // Or handle "--" specifically if needed
                            }




                            /*  sales_Invoice.TotalSGSTPrice = Convert.ToDecimal(dataReader[16]);

							  sales_Invoice.TotalCGSTPrice = Convert.ToDecimal(dataReader[17]);

							  sales_Invoice.WithoutGSTPrice = Convert.ToDecimal(dataReader[18]);*/

                            sales_Invoice.TotalPrice = Convert.ToDecimal(dataReader[19]);

                            sales_Invoice.VehicleId = Convert.ToInt32(dataReader[20]);

                            sales_Invoice.VehicleName = (dataReader[21].ToString());

                            sales_Invoice.VehicleNo = (dataReader[22].ToString());

                            sales_Invoice.DriverName = (dataReader[23].ToString());

                            sales_Invoice.ContainerNo = (dataReader[24].ToString());





                        };


                        return sales_Invoice;


                    }





                }
            }
            catch
            {
                return null;
            }
        }

        #endregion










        #endregion






    }
}