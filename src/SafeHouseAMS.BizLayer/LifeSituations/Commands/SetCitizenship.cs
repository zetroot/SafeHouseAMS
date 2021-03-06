using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Установить запись о гражданстве в соответствующем документе
    /// </summary>
    public class SetCitizenship : LifeSituationDocumentCommand
    {
        /// <summary>
        /// СОбственно значение гражданства
        /// </summary>
        public string Citizenship { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа для которого устанавливается гражданство</param>
        /// <param name="citizenship">значение</param>
        /// <exception cref="ArgumentNullException">если значение было null</exception>
        public SetCitizenship(Guid entityID, string citizenship) : base(entityID)
        {
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.AddRecord(EntityID, new CitizenshipRecord(Guid.NewGuid(), Citizenship));
        }
    }
}
