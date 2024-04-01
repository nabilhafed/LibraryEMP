using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;


namespace LibraryEMP.Managers
{
    #pragma warning disable CS8618
    public class DatabaseManager
    {
        private static string databaseName;
        private static string databaseHost;
        private static string databasePort;

        private static string userName;
        private static string userPassword;

        private static string connectionString;

        private static OracleConnection connection;

        public static bool connect(string databaseName, string host, string port, string username, string password)
        {
            DatabaseManager.databaseName = databaseName;
            DatabaseManager.databaseHost = host;
            DatabaseManager.databasePort = port;
            DatabaseManager.userName = username;
            DatabaseManager.userPassword = password;

            connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={databaseName})));User ID={username};Password={password};";

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
