using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Payment_BALBase
    {
        private readonly Payment_DALBase payment_DALBase = new Payment_DALBase();

        #region Method : Get Payment Info By Stock Id And Customer Id 

        public Payment_Model Get_Payment_Info_By_Stock_Customer_PK(int Stock_ID , int Customer_ID)
        {
            Payment_Model payment_Model = payment_DALBase.Get_Payment_Info_By_Stock_Customer_PK(Stock_ID, Customer_ID);

            return payment_Model;
        }

        public Remain_Payment_Model Remain_Get_Payment_Info_By_Customer_FK_And_Stock_Id_And_Payment_Id(int Customer_ID,int Stock_ID)
        {
            Remain_Payment_Model remain_Payment_Model = payment_DALBase.Remain_Get_Payment_Info_By_Customer_FK_And_Stock_Id_And_Payment_Id(Customer_ID,Stock_ID);

            return remain_Payment_Model;
        }


        public List<Pending_Customers_Payments> Pending_Customers_Payments()
        {
            List<Pending_Customers_Payments> pending_Customers_Payments = payment_DALBase.Pending_Customers_Payments();

            return pending_Customers_Payments;
        }

        public List<Remain_Payment_Model> Remain_Customers_Payments()
        {
            List<Remain_Payment_Model> remain_Payment_Model = payment_DALBase.Remain_Customers_Payments();

            return remain_Payment_Model;
        }

        public List<Show_Payment_Info> Paid_Customers_Payments()
        {
            List<Show_Payment_Info> show_Payment_Infos = payment_DALBase.Paid_Customers_Payments();

            return show_Payment_Infos;
        }


        #endregion


        #region Method : Show Payment By Customer,Stock 


        public Show_Payment_Info Show_All_Payment_Info(int Customer_ID, int Stock_ID)
        {
            Show_Payment_Info show_Payment_Info = payment_DALBase.Show_All_Payment_Info(Customer_ID,Stock_ID);

            return show_Payment_Info;
        }

        #endregion

        #region Method : Create Payment 

        public bool Create_Payment(Payment_Model payment_Model)
        {
            try
            {
                if (payment_DALBase.Create_Payment(payment_Model))
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

        #region Method : Create Remain Payment 

        public bool Create_Remain_Payment(Remain_Payment_Model remain_Payment_Model)
        {
            try
            {
                if (payment_DALBase.Create_Remain_Payment(remain_Payment_Model))
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
    #endregion


}
