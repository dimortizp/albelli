
using SimpleInjector;
using System.Data;
using System.Data.Common;

namespace Data
{
    public static class DataAdapter
    {
        public static void RegisterPersistence(this Container container, string connectionString)
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

            var dbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = dbFactory.CreateConnection();
            connection.ConnectionString = connectionString;

            container.RegisterInstance<IDbConnection>(connection);
        }
    }
}
