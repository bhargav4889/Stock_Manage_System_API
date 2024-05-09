/// <summary>
/// Represents the base class for managing reminders in the business logic layer.
/// </summary>
using Stock_Manage_System_API.DAL;
using Stock_Manage_System_API.Models;

namespace Stock_Manage_System_API.BAL
{
    public class Reminder_BALBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reminder_BALBase"/> class.
        /// </summary>
        /// <remarks>
        /// The <see cref="reminder_DALBase"/> field is initialized to a new instance of <see cref="Reminder_DALBase"/>.
        /// </remarks>
        public Reminder_BALBase()
        {
            reminder_DALBase = new Reminder_DALBase();
        }

        /// <summary>
        /// Gets or sets the <see cref="Reminder_DALBase"/> object used to interact with the database.
        /// </summary>
        private Reminder_DALBase reminder_DALBase = new Reminder_DALBase();


        #region Method : Insert Reminder
        /// <summary>
        /// Inserts a new reminder into the database.
        /// </summary>
        /// <param name="reminder">The reminder information to insert.</param>
        /// <returns><c>true</c> if the reminder was inserted successfully, otherwise <c>false</c>.</returns>
        public bool InsertReminder(Reminder_Model reminder)
        {
            if (reminder_DALBase.InsertReminder(reminder))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Method : Update Reminder

        /// <summary>
        /// Updates an existing reminder in the database.
        /// </summary>
        /// <param name="reminder">The reminder information to update.</param>
        /// <returns><c>true</c> if the reminder was updated successfully, otherwise <c>false</c>.</returns>
        public bool UpdateReminder(Reminder_Model reminder)
        {
            if (reminder_DALBase.UpdateReminder(reminder))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Method : Delete Reminder

        /// <summary>
        /// Deletes a reminder from the database.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to delete.</param>
        /// <returns><c>true</c> if the reminder was deleted successfully, otherwise <c>false</c>.</returns>
        public bool DeleteReminder(int Reminder_ID)
        {
            if (reminder_DALBase.DeleteReminder(Reminder_ID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region Method : Get All Upcoming Reminder 

        /// <summary>
        /// Retrieves a list of all reminders in the database.
        /// </summary>
        /// <returns>A list of <see cref="Reminder_Model"/> objects containing all reminders in the database.</returns>
        public List<Reminder_Model> GetReminders()
        {
            List<Reminder_Model> reminders = reminder_DALBase.GetReminders();

            return reminders;
        }

        #endregion


        #region Method : Get Reminder By ID 
        /// <summary>
        /// Retrieves a single reminder from the database by its ID.
        /// </summary>
        /// <param name="Reminder_ID">The ID of the reminder to retrieve.</param>
        /// <returns>A <see cref="Reminder_Model"/> object containing the reminder information.</returns>
        public Reminder_Model GetReminderByID(int Reminder_ID)
        {
            Reminder_Model reminder = reminder_DALBase.GetReminderByID(Reminder_ID);

            return reminder;
        }

        #endregion

    }
}