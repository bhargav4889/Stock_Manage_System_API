using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Sales_BALBase
    {
        public readonly Sales_DALBase sales_DALBase = new Sales_DALBase();


        public bool Insert_Sale_With_Customer(Sale_Customer_Combied_Model sale_Customer_Combied_Model)
        {
            try
            {

                if (sales_DALBase.Insert_Sale_With_Customer(sale_Customer_Combied_Model))
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

        public List<Show_Sale> Show_All_Sales()
        {
            List<Show_Sale> List_of_Sales_Info = sales_DALBase.Show_All_Sales();

            return List_of_Sales_Info;
        }

        public Show_Sale Show_Sale_By_ID(int Sale_ID)
        {
            Show_Sale sale = sales_DALBase.Show_Sale_By_ID(Sale_ID);

            return sale;
        }

    }
}
