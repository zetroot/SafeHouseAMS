using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда создания документа обновления статуса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CreateRecordUpdateDocument<T> : CreateDocument where T : BaseRecord
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор создаваемого документа</param>
        /// <param name="survivorID">идентификатор пострадавшего</param>
        /// <param name="documentDate">дата документа</param>
        public CreateRecordUpdateDocument(Guid entityID, Guid survivorID, DateTime documentDate) :
            base(entityID, survivorID, documentDate)
        {
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.CreateRecordUpdateDocument(EntityID, false,
            DateTime.Now, DateTime.Now,
            SurvivorID, DocumentDate, typeof(T));
        }
    }
}