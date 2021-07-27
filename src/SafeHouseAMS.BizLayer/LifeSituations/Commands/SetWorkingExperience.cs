using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда установки атрибута "опыт работы"
    /// </summary>
    public class SetWorkingExperience : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Собственно информация об опыте работы
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">Идентификатор документа</param>
        /// <param name="details">Опыт работы</param>
        /// <exception cref="ArgumentNullException">если опыт был null</exception>
        public SetWorkingExperience(Guid entityID, string details) : base(entityID)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.SetWorkingExperience(EntityID, Details);
        }
    }
}