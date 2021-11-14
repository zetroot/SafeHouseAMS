using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.DataLayer.Models.AssistanceRequests;
using SafeHouseAMS.DataLayer.Models.ExploitationEpisodes;
using SafeHouseAMS.DataLayer.Models.LifeSituations;
using SafeHouseAMS.DataLayer.Models.Survivors;

namespace SafeHouseAMS.DataLayer
{
    internal class DataContext : DbContext, IDatabaseMigrator
    {
        public DbSet<SurvivorDAL> Survivors => Set<SurvivorDAL>();
        public DbSet<LifeSituationDocumentDAL> LifeSituationDocuments => Set<LifeSituationDocumentDAL>();
        public DbSet<BaseRecordDAL> Records => Set<BaseRecordDAL>();
        public DbSet<EpisodeDAL> Episodes => Set<EpisodeDAL>();
        public DbSet<AssistanceRequestDAL> AssistanceRequests => Set<AssistanceRequestDAL>();
        public DbSet<AssistanceActDAL> AssistanceActs => Set<AssistanceActDAL>();

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
