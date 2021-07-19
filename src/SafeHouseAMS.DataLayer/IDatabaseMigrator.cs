using System.Threading.Tasks;

namespace SafeHouseAMS.DataLayer
{
    /// <summary>
    /// Интерфейс службы выполнения миграций
    /// </summary>
    public interface IDatabaseMigrator
    {
        /// <summary>
        /// Выполнить все ожидающие миграции
        /// </summary>
        Task MigrateAsync();
    }
}