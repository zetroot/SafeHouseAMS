using System;
using System.Threading;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivor
{
    /// <summary>
    /// Репозиторий карточек пострадавших
    /// </summary>
    public interface ISurvivorRepository
    {
        /// <summary>
        /// Создать новую запись
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="name">имя</param>
        /// <param name="sex">пол</param>
        /// <param name="otherSex">уточнение пола</param>
        /// <param name="accurateDob">точная дата рождения</param>
        /// <param name="calculatedDob">вычисленная дата рождения</param>
        Task Create(Guid id, string name, SexEnum sex, string? otherSex, DateTimeOffset? accurateDob, DateTimeOffset? calculatedDob);

        /// <summary>
        /// Получить одиночную запись по идентификатору
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Найденая запись</returns>
        Task<Survivor> GetSingleAsync(Guid id, CancellationToken cancellationToken);
    }
}