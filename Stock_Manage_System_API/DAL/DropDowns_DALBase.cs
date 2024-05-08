using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    /// <summary>
    /// Provides data access functionalities to retrieve dropdown values from the database.
    /// </summary>
    public class DropDowns_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        /// <summary>
        /// Initializes a new instance of the DropDowns_DALBase class, setting up the database connection.
        /// </summary>
        public DropDowns_DALBase()
        {
            // 'Database_Connection' is a predefined string or obtained elsewhere in your application.
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

        #region Section: Get Dropdowns Data

        /// <summary>
        /// Asynchronously retrieves all dropdown data from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, which upon completion returns a DropDowns_Model containing all dropdown lists.</returns>
        public DropDowns_Model GetAllDropdownsAsync()
        {
            var allDropDownsModel = new DropDowns_Model
            {
                Products_DropDowns_List = new List<Product_DropDown_Model>(),
                Products_Grade_DropDowns_List = new List<Product_Grade_DropDown_Model>(),
                Vehicle_DropDowns_List = new List<Vehicle_DropDown_Model>(),
            };

            DbCommand dbCommand = Command_Name("API_ALL_DROPDOWNS");

            // Execute the command and process results.
            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                // Populate Product dropdown list from the data reader.
                while (reader.Read())
                {
                    allDropDownsModel.Products_DropDowns_List.Add(new Product_DropDown_Model
                    {
                        ProductId = Convert.ToInt32(reader["PRODUCT_ID"].ToString()),
                        ProductNameInGujarati = reader["PRODUCT_NAME_IN_GUJARATI"].ToString(),
                        ProductNameInEnglish = reader["PRODUCT_NAME_IN_ENGLISH"].ToString()
                    });
                }

                // Proceed to the next result set for Product Grade dropdown data.
                reader.NextResult();

                while (reader.Read())
                {
                    allDropDownsModel.Products_Grade_DropDowns_List.Add(new Product_Grade_DropDown_Model
                    {
                        ProductGradeId = Convert.ToInt32(reader["PRODUCT_GRADE_ID"].ToString()),
                        ProductGrade = reader["PRODUCT_GRADE"].ToString()
                    });
                }

                // Proceed to the next result set for Vehicle dropdown data.
                reader.NextResult();

                while (reader.Read())
                {
                    allDropDownsModel.Vehicle_DropDowns_List.Add(new Vehicle_DropDown_Model
                    {
                        VehicleId = Convert.ToInt32(reader["VEHICLE_ID"].ToString()),
                        VehicleName = reader["VEHICLE_NAME"].ToString()
                    });
                }
            }

            return allDropDownsModel;
        }

        #endregion
    }
}