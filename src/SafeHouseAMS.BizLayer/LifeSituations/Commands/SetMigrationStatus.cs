using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда добавления записи о миграционном стсатусе
    /// </summary>
    public class SetMigrationStatus : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Собственно миграционный статус
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор документ</param>
        /// <param name="details">миграционный статус</param>
        /// <exception cref="ArgumentNullException">если специальность была null</exception>
        public SetMigrationStatus(Guid entityID, string details) : base(entityID)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.AddRecord(EntityID, new MigrationStatusRecord(Guid.NewGuid(), Details));
        }
    }

}
