using System;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Базовый класс документов, меняющих жизненную ситуацию
    /// </summary>
    public abstract class LifeSituationDocument : BaseDomainModel
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; }

        /// <summary>
        /// Пострадавший к которому относится эта запись
        /// </summary>
        public Survivor Survivor { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания</param>
        /// <param name="lastEdit">дата последнего редактирования</param>
        /// <param name="documentDate">дата действия документа</param>
        /// <param name="survivor">Пострадавший, к которому относится этот документ</param>
        protected LifeSituationDocument(Guid id, bool isDeleted, DateTime created, DateTime lastEdit, DateTime documentDate, Survivor survivor) :
            base(id, isDeleted, created, lastEdit)
        {
            DocumentDate = documentDate;
            Survivor = survivor;
        }
    }
}