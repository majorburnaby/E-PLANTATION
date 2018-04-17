using System.Configuration;

namespace Plantation.Utility
{
    public class Constant
    {
        public static string DatabaseConnection =
            ConfigurationManager.ConnectionStrings["database-connection-2"].ConnectionString;
    }
}