using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Документ о получении образования
    /// </summary>
    public class EducationChange : LifeSituationDocument
    {
        /// <summary>
        /// Коллекция записей об уровне образования
        /// </summary>
        public IReadOnlyCollection<EducationLevelRecord> EducationRecords { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="educationRecords">коллекция записей о полученном образовании</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EducationChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            IEnumerable<EducationLevelRecord> educationRecords) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            EducationRecords = educationRecords?.ToList() ?? throw new ArgumentNullException(nameof(educationRecords));
        }
    }
}