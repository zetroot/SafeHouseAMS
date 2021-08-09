using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда создания документа о смене гражданства
    /// </summary>
    public class CreateCitizenshipChange : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Идентификатор пострадавшего
        /// </summary>
        public Guid SurvivorID { get; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; }

        /// <summary>
        /// Новое значение для гражданства
        /// </summary>
        public string Citizenship { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор создаваемого документа</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="citizenship">Новое гражданство</param>
        /// <param name="survivorID">Идентификатор пострадавшего</param>
        /// <exception cref="ArgumentNullException">Если переданное гражданство было null</exception>
        public CreateCitizenshipChange(Guid entityID, Guid survivorID, DateTime documentDate, string citizenship) :
            base(entityID)
        {
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
            SurvivorID = survivorID;
            DocumentDate = documentDate;
        }

        internal override async Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));

            await repository.CreateCitizenshipChange(EntityID, false,
            DateTime.Now, DateTime.Now,
            SurvivorID, DocumentDate);

            await repository.AddRecord(EntityID, new CitizenshipRecord(Guid.NewGuid(), Citizenship));
        }
    }
}
