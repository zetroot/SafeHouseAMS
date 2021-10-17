using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace SafeHouseAMS.IdentityProvider.Data
{
    [ExcludeFromCodeCoverage]
    internal class DatabaseContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = "localhost",
                Database = "users_migration_builder_db",
                Username = "postgres",
                Password = "pass"
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionStringBuilder.ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
