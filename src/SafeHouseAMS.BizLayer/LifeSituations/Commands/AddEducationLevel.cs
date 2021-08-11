using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда добавления уровня образования
    /// </summary>
    public class AddEducationLevel : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Уровень образования
        /// </summary>
        public EducationLevelRecord.EduLevel Level { get; }

        /// <summary>
        /// Подробное описание
        /// </summary>
        public string? Details { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа</param>
        /// <param name="level">Уровень образования</param>
        /// <param name="details">Дополнительные детали</param>
        public AddEducationLevel(Guid entityID, EducationLevelRecord.EduLevel level, string? details) : base(entityID)
        {
            Level = level;
            Details = details;
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.AddRecord(EntityID, new EducationLevelRecord(Guid.NewGuid(), Level, Details));
        }
    }
}
