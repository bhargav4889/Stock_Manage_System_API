using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.BAL
{
    public class Customers_BALBase
    {
        private readonly Customers_DALBase Customers_DAL = new Customers_DALBase();

        public bool CREATE_CUSTOMER(Customer_Model customers)
        {
            try
            {
                if (Customers_DAL.CREATE_CUSTOMER(customers))
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



        public Customer_Model Customer_Info_By_PK(int Customer_ID,string Customer_Type)
        {
            Customer_Model customers = Customers_DAL.Customer_Info_By_PK(Customer_ID, Customer_Type);

            return customers;

        }

        public List<Customer_Model> BUYER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
        {
            List<Customer_Model> customerinfo = Customers_DAL.BUYER_CUSTOMER_EXIST_IN_SYSTEM(Customer_Name);

            return customerinfo;
        }

        public List<Customer_Model> SELLER_CUSTOMER_EXIST_IN_SYSTEM(string Customer_Name)
        {
            List<Customer_Model> customerinfo = Customers_DAL.SELLER_CUSTOMER_EXIST_IN_SYSTEM(Customer_Name);

            return customerinfo;
        }



        public List<Customer_Model> SHOW_ALL_CUSTOMERS()
        {
            List<Customer_Model> customerslist = Customers_DAL.SHOW_ALL_CUSTOMERS();

            return customerslist;


        }

        public CustomerDetails_With_Purchased_Stock_Model Account_Details(int Customer_ID, string Customer_Type)
        {
            CustomerDetails_With_Purchased_Stock_Model customerDetails_With_Purchased_Stock = Customers_DAL.Account_Details(Customer_ID,Customer_Type);

            return customerDetails_With_Purchased_Stock;
        }


        public bool Delete_Customer(int Customer_ID, string Customer_Type)
        {
            try
            {
                if(Customers_DAL.Delete_Customer(Customer_ID,Customer_Type))
               
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

        public bool Update_Customer(Customer_Model customers)
        {
            try
            {
                if (Customers_DAL.Update_Customer(customers))

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

    }
}
