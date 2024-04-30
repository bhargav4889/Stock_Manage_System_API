using System.ComponentModel.DataAnnotations;

namespace Stock_Manage_System_API.Models
{
    public class Information_Model
    {
        public int InformationID { get; set; }  

        public int BankId { get; set; }

        public string? AccountNo { get; set; }

        public string? ifsc_code { get; set; }

        public string? AccountHolderName { get; set; }

        public string? BankName { get; set; }

        public string? BankIcon { get; set; }

    }
}
