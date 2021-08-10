using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Документ о смене миграционного статуса
    /// </summary>
    public class MigrationStatusChange : LifeSituationDocument
    {
        /// <summary>
        /// Новая запись и миграционном статусе
        /// </summary>
        public MigrationStatusRecord MigrationStatus { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="migrationStatus">собственно запись о миграционном статусе</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MigrationStatusChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            MigrationStatusRecord migrationStatus) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            MigrationStatus = migrationStatus ?? throw new ArgumentNullException(nameof(migrationStatus));
        }
    }
}