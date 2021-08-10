using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Documents
{
    /// <summary>
    /// Документ обновляющий сразу коллекцию записей
    /// </summary>
    /// <typeparam name="T">тип записи</typeparam>
    public class MultiRecordsUpdate<T> : LifeSituationDocument
    {
        /// <summary>
        /// добавленные записи
        /// </summary>
        public IReadOnlyCollection<T> Records { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="records">собственно запись обновляемая в документе</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MultiRecordsUpdate(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            IEnumerable<T> records) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            Records = records?.ToList() ?? throw new ArgumentNullException(nameof(records));
        }
    }
}