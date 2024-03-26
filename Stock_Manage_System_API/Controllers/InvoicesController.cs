using Microsoft.AspNetCore.Mvc;
using Stock_Manage_System_API.BAL;
using Stock_Manage_System_API.Models;


namespace Stock_Manage_System_API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public class InvoicesController : Controller
    {

        private readonly Invoices_BALBase invoices_BALBase = new Invoices_BALBase();


        #region Sell Invoice



        #region INSERT

        [HttpPost]

        public IActionResult Insert_Sales_Invoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            Invoices_BALBase invoices_BALBase = new Invoices_BALBase();

            bool IsSuccess = invoices_BALBase.CREATE_SALES_INVOICE(sales_Invoice);


            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();


            if (IsSuccess)
            {

                res.Add("status", true);
                res.Add("message", "Data Insert Successfully!");
                return Ok(res);
            }
            else
            {

                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }

        }

        #endregion


        #region DISPLAY ALL

        [HttpGet]

        public IActionResult Sales_Invoices()
        {
            Invoices_BALBase invoices_BAL = new Invoices_BALBase();

            List<InvoicesModel.Sales_Invoice_Model> List_Of_Sales_Invoice = invoices_BAL.SHOW_ALL_SALES_INVOICES();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (List_Of_Sales_Invoice.Count > 0 && List_Of_Sales_Invoice != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", List_Of_Sales_Invoice);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);
            }


        }

        #endregion

        #region DELETE 

        [HttpDelete]

        public IActionResult Delete_Sales_Invoice(int Sales_Invoice_ID)
        {
            Invoices_BALBase invoices_BALBase = new Invoices_BALBase();

            bool IsSuccess = invoices_BALBase.DELETE_SALES_INVOICE(Sales_Invoice_ID);


            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Delete Successfully");
                return Ok(res);

            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }

        }

        #endregion


        #region DISPLAY BY ID 

        [HttpGet("{Sales_Invoice_ID}")]

        public IActionResult Get_Sales_Invoice_By_Id(int Sales_Invoice_ID)
        {
            Invoices_BALBase invoices_BALBase = new Invoices_BALBase();

            InvoicesModel.Sales_Invoice_Model sales_Invoice_Model = new InvoicesModel.Sales_Invoice_Model();


            sales_Invoice_Model = invoices_BALBase.SALES_INVOICE_DETAILS_BY_ID(Sales_Invoice_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();


            if (sales_Invoice_Model.SalesInvoiceId != 0 && sales_Invoice_Model != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", sales_Invoice_Model);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);
            }

        }


        #endregion


        #region UPDATE 

        [HttpPut]

        public IActionResult Update_Sales_Invoice(InvoicesModel.Sales_Invoice_Model sales_Invoice)
        {
            // Update operation
            bool is_Success = invoices_BALBase.SALES_INVOICE_UPDATE(sales_Invoice);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check update success
            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }


        #endregion








        #endregion



        #region Purchase Invoice 



        #region DISPLAY ALL

        [HttpGet]

        public IActionResult Purchase_Invoices()
        {
            Invoices_BALBase invoices_BAL = new Invoices_BALBase();

            List<InvoicesModel.Purchase_Invoice_Model> purchase_Invoices = invoices_BALBase.SHOW_ALL_PURCHASE_INVOICES();

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (purchase_Invoices.Count > 0 && purchase_Invoices != null)
            {
                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", purchase_Invoices);

                return Ok(res);
            }
            else
            {

                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);

            }


        }

        #endregion

        #region DISPLAY BY ID 

        [HttpGet("{Purchase_Invoice_ID}")]

        public IActionResult Get_Purchase_Invoice_By_Id(int Purchase_Invoice_ID)
        {

            InvoicesModel.Purchase_Invoice_Model? purchase_Invoice = invoices_BALBase.PURCHASE_INVOICE_DETAILS_BY_ID(Purchase_Invoice_ID);

            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();


            if (purchase_Invoice != null && purchase_Invoice.PurchaseInvoiceId != 0)
            {

                res.Add("status", true);

                res.Add("message", "Data Found !.");

                res.Add("data", purchase_Invoice);

                return Ok(res);
            }
            else
            {
                res.Add("status", false);

                res.Add("message", "Data Not Found !.");

                res.Add("data", null);

                return NotFound(res);
            }



        }


        #endregion

        #region INSERT 
        [HttpPost]

        public IActionResult Insert_Purchase_Invoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            

            bool IsSuccess = invoices_BALBase.CREATE_PURCHASE_INVOICE(purchase_Invoice);


            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();


            if (IsSuccess)
            {

                res.Add("status", true);
                res.Add("message", "Data Insert Successfully!");
                return Ok(res);
            }
            else
            {

                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }

        }

        #endregion

        #region UPDATE 

        [HttpPut]

        public IActionResult Update_Purchase_Invoice(InvoicesModel.Purchase_Invoice_Model purchase_Invoice)
        {
            // Update operation
            bool is_Success = invoices_BALBase.PURCHASE_INVOICE_UPDATE(purchase_Invoice);

            // Response container
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();

            // Check update success
            if (is_Success)
            {
                response.Add("status", true);
                response.Add("message", "Data Updated Successfully!");
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Error Occurred!");
                return Ok(response);
            }

        }


        #endregion

        #region DELETE 

        [HttpDelete]

        public IActionResult Delete_Purchase_Invoice(int Purchase_Invoice_ID)
        {
            Invoices_BALBase invoices_BALBase = new Invoices_BALBase();

            bool IsSuccess = invoices_BALBase.DELETE_PURCHASE_INVOICE(Purchase_Invoice_ID);


            Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

            if (IsSuccess)
            {
                res.Add("status", true);
                res.Add("message", "Data Delete Successfully");
                return Ok(res);

            }
            else
            {
                res.Add("status", false);
                res.Add("message", "Some Error Occured !");
                return Ok(res);
            }

        }

        #endregion


    




        #endregion

    }
}
