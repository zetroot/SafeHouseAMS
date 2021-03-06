using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда добавления записи о статусе регистрации
    /// </summary>
    public class SetRegistrationStatus : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Собственно статус регистрации
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор документ</param>
        /// <param name="details">статус регистрации</param>
        /// <exception cref="ArgumentNullException">если специальность была null</exception>
        public SetRegistrationStatus(Guid entityID, string details) : base(entityID)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.AddRecord(EntityID, new RegistrationStatusRecord(Guid.NewGuid(), Details));
        }
    }
}
