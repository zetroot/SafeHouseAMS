using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда удаления документа
    /// </summary>
    public class DeleteDocument : LifeSituationDocumentCommand
    {
        /// <summary>
        /// default ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа для удаления</param>
        public DeleteDocument(Guid entityID) : base(entityID)
        {
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository) =>
            repository.DeleteDocument(EntityID);
    }
}
