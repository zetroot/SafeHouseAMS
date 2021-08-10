using System;
using System.Collections.Generic;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// документ об изменении специальностей
    /// </summary>
    public class SpecialitiesChange : LifeSituationDocument
    {
        /// <summary>
        /// новые записи о специальностях
        /// </summary>
        public IReadOnlyCollection<SpecialityRecord> Specialities { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="specialities">собственно новые записи о специальностях</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SpecialitiesChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            IReadOnlyCollection<SpecialityRecord> specialities) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            Specialities = specialities ?? throw new ArgumentNullException(nameof(specialities));
        }
    }
}