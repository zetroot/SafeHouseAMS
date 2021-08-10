using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Документ о изменении статуса по детям
    /// </summary>
    public class ChildrenChange : LifeSituationDocument
    {
        /// <summary>
        /// Обновлённая информация о детях
        /// </summary>
        public ChildrenRecord ChildrenRecord { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="childrenRecord">собственно запись о детях</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ChildrenChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            ChildrenRecord childrenRecord) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            ChildrenRecord = childrenRecord ?? throw new ArgumentNullException(nameof(childrenRecord));
        }
    }

}
