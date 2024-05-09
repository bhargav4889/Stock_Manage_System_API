using Stock_Manage_System_API.DAL;

using Stock_Manage_System_API.Models;

/// <summary>
/// Represents the base Business Access Layer (BAL) class for managing counts.
/// </summary>
namespace Stock_Manage_System_API.BAL
{
    public class Counts_BALBase
    {
        #region DAL Class Instance

        /// <summary>
        /// An instance of the <see cref="Counts_DALBase"/> class for managing counts in the data access layer.
        /// </summary>
        private readonly Counts_DALBase allCountsDALBase = new Counts_DALBase();

        #endregion


        #region Method : All Counts 

        /// <summary>
        /// Retrieves the total counts for all relevant entities.
        /// </summary>
        /// <returns>An AllCountsModel object containing the total counts and amounts for all relevant entities.</returns>
        public AllCountsModel GetTotalCounts()
        {
            AllCountsModel allCountsModel = allCountsDALBase.GetTotalCounts();

            return allCountsModel;
        }

        #endregion


    }
}