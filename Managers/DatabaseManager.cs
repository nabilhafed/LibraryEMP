using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;


namespace LibraryEMP.Managers
{
    #pragma warning disable CS8618
    public class DatabaseManager
    {
        private static string databaseName;
        private static string userName;
        private static string userPassword;

        private static string connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={databaseName})));User ID={userName};Password={userPassword};";

        private static OracleConnection connection;
        public static bool connect(string databaseName, string userName, string userPassword)
        {
            DatabaseManager.databaseName = databaseName;
            DatabaseManager.userName = userName;
            DatabaseManager.userPassword = userPassword;

            connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={databaseName})));User ID={userName};Password={userPassword};";

            connection = new OracleConnection(connectionString);

            try
            {
                connection.Open();
                return (connection.State == ConnectionState.Open);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }

}
