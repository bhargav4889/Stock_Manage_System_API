namespace Stock_Manage_System_API.DAL
{
    public class DAL_Helpers
    {
        public string Database_Connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("MyConnection");
    }
}
