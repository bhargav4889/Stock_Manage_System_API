/// <summary>
/// Represents the base class for managing information in the business access layer.
/// </summary>
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Information_BALBase
    {
        public readonly Information_DALBase information_DALBase = new Information_DALBase();


        #region Method : Bank Information Insert
        /// <summary>
        /// Inserts a new bank information.
        /// </summary>
        /// <param name="infromation_Model">The <see cref="Information_Model"/> object containing the bank information to insert.</param>
        /// <returns><c>true</c> if the bank information was inserted successfully; otherwise, <c>false</c>.</returns>
        public bool InsertBankInformation(Information_Model infromation_Model)
        {
            if (information_DALBase.InsertBankInformation(infromation_Model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Method : Bank Information Update
        /// <summary>
        /// Update a  bank information.
        /// </summary>
        /// <param name="infromation_Model">The <see cref="Information_Model"/> object containing the bank information to Update.</param>
        /// <returns><c>true</c> if the bank information was update successfully; otherwise, <c>false</c>.</returns>
        public bool UpdateBankInformation(Information_Model infromation_Model)
        {
            if (information_DALBase.UpdateBankInformation(infromation_Model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Method : Show All Informations 
        /// <summary>
        /// Retrieves all saved information.
        /// </summary>
        /// <returns>A list of <see cref="Information_Model"/> objects containing all saved information.</returns>
        public List<Information_Model> GetAllSaveInformation()
        {
            List<Information_Model> information_Models = information_DALBase.GetAllSaveInformation();

            return information_Models;
        }

        #endregion


        #region Method : Information Show By ID 
        /// <summary>
        /// Retrieves the saved information with the specified information ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the saved information to retrieve.</param>
        /// <returns>A <see cref="Information_Model"/> object containing the saved information with the specified information ID.</returns>
        public Information_Model InformationByID(int Information_ID)
        {
            Information_Model information_Model = information_DALBase.InformationByID(Information_ID);

            return information_Model;
        }
        #endregion


        #region Method : Delete Information

        /// <summary>
        /// Deletes the saved information with the specified information ID.
        /// </summary>
        /// <param name="Information_ID">The ID of the saved information to delete.</param>
        /// <returns><c>true</c> if the saved information was deleted successfully; otherwise, <c>false</c>.</returns>
        public bool DeleteInformation(int Information_ID)
        {
            if (information_DALBase.DeleteInformation(Information_ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}