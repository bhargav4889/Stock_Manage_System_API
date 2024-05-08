using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Reminder_BALBase
    {
        private readonly Reminder_DALBase reminder_DALBase = new Reminder_DALBase();    



        public bool Insert_Reminder(Reminder_Model reminder)
        {
            if(reminder_DALBase.Insert_Reminder(reminder))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Update_Reminder(Reminder_Model reminder)
        {
            if (reminder_DALBase.Update_Reminder(reminder))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public bool Delete_Reminder(int Reminder_ID)
        {
            if (reminder_DALBase.Delete_Reminder(Reminder_ID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public List<Reminder_Model> Get_Reminders()
        {
            List<Reminder_Model> reminders = reminder_DALBase.Reminders();

            return reminders;
        }


        public Reminder_Model Get_Reminder_By_ID(int Reminder_ID)
        {
            Reminder_Model reminder =  reminder_DALBase.Get_Reminder_By_ID(Reminder_ID);

            return reminder;
        }

    }
}
