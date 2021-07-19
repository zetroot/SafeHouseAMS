using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.DataLayer.Models;

namespace SafeHouseAMS.DataLayer
{
    internal class DataContext : DbContext, IDatabaseMigrator
    {
        public DbSet<SurvivorDAL>? Survivors { get; set; }

        
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);
        }
        
        /// <inheritdoc />
        public Task MigrateAsync() => Database.MigrateAsync();
    }
}