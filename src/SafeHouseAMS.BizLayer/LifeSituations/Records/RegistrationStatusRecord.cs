using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Запись о статусе регистрации
    /// </summary>
    public class RegistrationStatusRecord : BaseRecord
    {
        /// <summary>
        /// собственно информация о статусе регистрации
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="details">информация о статусе регистрации</param>
        /// <exception cref="ArgumentNullException">если передали пустую строку</exception>
        public RegistrationStatusRecord(Guid id, string details) : base(id)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }
    }
}