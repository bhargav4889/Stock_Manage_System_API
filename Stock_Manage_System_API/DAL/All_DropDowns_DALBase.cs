using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Stock_Manage_System_API.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Stock_Manage_System_API.DAL
{
    public class All_DropDowns_DALBase : DAL_Helpers
    {
        private SqlDatabase sqlDatabase;

        public All_DropDowns_DALBase()
        {
            sqlDatabase = new SqlDatabase(Database_Connection);
        }

        private DbCommand Command_Name(string storedProcedureName)
        {
            return sqlDatabase.GetStoredProcCommand(storedProcedureName);
        }

        public All_DropDowns_Model GET_ALL_DROPDOWNS()
        {
            var allDropDownsModel = new All_DropDowns_Model
            {
                Products_DropDowns_List = new List<Product_DropDown_Model>(),
                Products_Grade_DropDowns_List = new List<Product_Grade_DropDown_Model>(),
                Vehicle_DropDowns_List = new List<Vehicle_DropDown_Model>(),
                
            };



            DbCommand dbCommand = Command_Name("API_ALL_DROPDOWNS");

            using (IDataReader reader = sqlDatabase.ExecuteReader(dbCommand))
            {
                // Product Dropdown
                while (reader.Read())
                {
                    allDropDownsModel.Products_DropDowns_List.Add(new Product_DropDown_Model
                    {
                        ProductId = Convert.ToInt32(reader["PRODUCT_ID"].ToString()),
                        ProductNameInGujarati = reader["PRODUCT_NAME_IN_GUJARATI"].ToString(),
                        ProductNameInEnglish = reader["PRODUCT_NAME_IN_ENGLISH"].ToString()
                    });
                }

                // Move to the next result set (Product Grade Dropdown)
                reader.NextResult();
                while (reader.Read())
                {
                    allDropDownsModel.Products_Grade_DropDowns_List.Add(new Product_Grade_DropDown_Model
                    {
                        ProductGradeId = Convert.ToInt32(reader["PRODUCT_GRADE_ID"].ToString()),
                        ProductGrade = reader["PRODUCT_GRADE"].ToString()
                    });
                }

                // Move to the next result set (Vehicle Dropdown)
                reader.NextResult();
                while (reader.Read())
                {
                    allDropDownsModel.Vehicle_DropDowns_List.Add(new Vehicle_DropDown_Model
                    {
                        VehicleId = Convert.ToInt32(reader["VEHICLE_ID"].ToString()),
                        VehicleName = reader["VEHICLE_NAME"].ToString()
                    });
                }

                // Move to the next result set (Vehicle Dropdown)
                
            }

            return allDropDownsModel;
        }
    }
}
