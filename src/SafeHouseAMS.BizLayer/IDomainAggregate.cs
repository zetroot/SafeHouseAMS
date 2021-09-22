using System;
using System.Threading;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Базовый интерфейс доменного огрегата
    /// </summary>
    /// <typeparam name="TDomainEntity">Тип доменной сущности</typeparam>
    /// <typeparam name="TCommand">Базовый тип команды</typeparam>
    public interface IDomainAggregate<TDomainEntity, in TCommand>
        where TDomainEntity : IDomainModel
        where TCommand : ICommand
    {
        /// <summary>
        /// Найти сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Найденая сущность, или null - если по идентификатору ничего найти не удалось</returns>
        Task<TDomainEntity?> GetSingleAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="command">Команда для выполнения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Задача, которая будет завершена, когда команда будет выполнена</returns>
        Task ApplyCommand(TCommand command, CancellationToken cancellationToken);
    }
}
