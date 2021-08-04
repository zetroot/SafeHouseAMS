using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace SafeHouseAMS.DataLayer
{
    [ExcludeFromCodeCoverage]
    internal class DatabaseContextDesignFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "migrations_builder_db",
                Username = "postgres",
                Password = "pass"
            };
            
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}