using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace MyRecipeBook.Infrastructe.Migrations
{
    public static class DataBaseMigration
    {
        public static void Migrate(string connetionString, IServiceProvider serviceProvider)
        {
            CreateDataBase(connetionString);
            MigrationDataBase(serviceProvider);
        }

        private static void CreateDataBase(string connetionString)
        {
            var connectionsBuilder = new SqlConnectionStringBuilder(connetionString);

            var databaseName = connectionsBuilder.InitialCatalog;

            connectionsBuilder.Remove("Initial Catalog");

            using (var connection = new SqlConnection(connectionsBuilder.ToString()))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{databaseName}') CREATE DATABASE [{databaseName}]";
                command.ExecuteNonQuery();
            }

        }

        private static void MigrationDataBase(IServiceProvider serviceProvider)
        {
           var runer  = serviceProvider.GetRequiredService<IMigrationRunner>();
            runer.ListMigrations();
            runer.MigrateUp();
        }
    }
}
