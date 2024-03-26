namespace Stock_Manage_System_API.Models
{
    public class Recent_Action_Model
    {
        public int Rec_Act_Id { get; set; }

        public string? Rec_Act_Table_Name { get; set; }

        public string? Rec_Act_Info { get; set; }

        public DateTime Rec_Act_Create { get; set; }
    }
}
