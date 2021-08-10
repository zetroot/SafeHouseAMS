using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// документ о изменении ситуации с жильём
    /// </summary>
    public class DomicileChange : LifeSituationDocument
    {
        /// <summary>
        /// Собственно запись о ситуации с жильём
        /// </summary>
        public DomicileRecord DomicileRecord { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="domicileRecord">собственно запись о жилье</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DomicileChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            DomicileRecord domicileRecord) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            DomicileRecord = domicileRecord ?? throw new ArgumentNullException(nameof(domicileRecord));
        }
    }
}