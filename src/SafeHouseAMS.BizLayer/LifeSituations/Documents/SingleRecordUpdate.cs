using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Documents
{
    /// <summary>
    /// Документ обновления записи о пострадавшем
    /// </summary>
    /// <typeparam name="T">Тип обновляемой записи</typeparam>
    public class SingleRecordUpdate<T> : LifeSituationDocument
        where T : BaseRecord
    {
        /// <summary>
        /// Добавленная запись
        /// </summary>
        public T Record { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="record">собственно запись обновляемая в документе</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SingleRecordUpdate(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            T record) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            Record = record ?? throw new ArgumentNullException(nameof(record));
        }
    }

}
