using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Запись о миграционном статусе
    /// </summary>
    public class MigrationStatusRecord : BaseRecord
    {
        /// <summary>
        /// собственно информация о миграционном статусе
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="details">информация о миграционном статусе</param>
        /// <exception cref="ArgumentNullException">если передали пустую строку</exception>
        public MigrationStatusRecord(Guid id, string details) : base(id)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }
    }

}
