﻿using System.Runtime.InteropServices;

namespace Stock_Manage_System_API.Models
{
    public class DropDowns_Model
    {
        public List<Product_DropDown_Model> Products_DropDowns_List { get;  set; }
    
        public List<Product_Grade_DropDown_Model> Products_Grade_DropDowns_List { get; set; }

        public List<Vehicle_DropDown_Model> Vehicle_DropDowns_List { get; set; }

       
    }

    public class Product_DropDown_Model
    {
        public int ProductId { get; set; }

        public string? ProductNameInGujarati { get; set; }


        public string? ProductNameInEnglish { get; set; }

    }




    public class Product_Grade_DropDown_Model
    {
        public int ProductGradeId { get; set; }

        public string? ProductGrade { get; set; }

    }


    public class Vehicle_DropDown_Model
    {
        public int VehicleId { get; set; }

        public string? VehicleName { get; set; }

    }
    

  


}
