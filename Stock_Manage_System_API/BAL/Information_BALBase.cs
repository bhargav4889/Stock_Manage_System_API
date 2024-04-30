using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Information_BALBase
    {
        public readonly Information_DALBase information_DALBase = new Information_DALBase();

        public bool Insert_Bank_Infromation(Information_Model infromation_Model)
        {
            if (information_DALBase.Insert_Bank_Infromation(infromation_Model))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Information_Model> Show_All_Save_Informations()
        {
            List<Information_Model> information_Models = information_DALBase.Show_All_Save_Informations();

            return information_Models;
        }

        public Information_Model Information_Model(int Information_ID)
        {
            Information_Model information_Model = information_DALBase.Information_Model(Information_ID);

            return information_Model;
        }

        public bool Delete_Save_Infromation(int Information_ID)
        {
            if (information_DALBase.Delete_Save_Information(Information_ID))
            {

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
