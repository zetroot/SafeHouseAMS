using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Запись о смене статуса регистрации
    /// </summary>
    public class RegistrationStatusChange : LifeSituationDocument
    {
        /// <summary>
        /// Новый регистрационный статус
        /// </summary>
        public RegistrationStatusRecord RegistrationStatus { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего изменения записи</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится этот документ</param>
        /// <param name="registrationStatus">собственно запись о новом регистрационном статусе</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RegistrationStatusChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            RegistrationStatusRecord registrationStatus) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            RegistrationStatus = registrationStatus ?? throw new ArgumentNullException(nameof(registrationStatus));
        }
    }
}