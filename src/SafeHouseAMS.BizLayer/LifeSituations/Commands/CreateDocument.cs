using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// абстрактный класс команды создания документа
    /// </summary>
    public abstract class CreateDocument : LifeSituationDocumentCommand
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
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа</param>
        /// <param name="survivorID">идентификатор пострадавшего, к которому относится документ</param>
        /// <param name="documentDate">дата докуменета</param>
        protected CreateDocument(Guid entityID, Guid survivorID, DateTime documentDate) : base(entityID)
        {
            SurvivorID = survivorID;
            DocumentDate = documentDate;
        }
    }
}