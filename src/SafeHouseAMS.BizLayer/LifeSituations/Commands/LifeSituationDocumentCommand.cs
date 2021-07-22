using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Базовая команда изменения документа жизненной ситуации
    /// </summary>
    public abstract class LifeSituationDocumentCommand : BaseCommand<ILifeSituationDocumentsRepository>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа на котором выполняется команда</param>
        protected LifeSituationDocumentCommand(Guid entityID) : base(entityID) {}
    }

}