using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Запись о гражданстве
    /// </summary>
    public class CitizenshipRecord : BaseRecord
    {
        /// <summary>
        /// Гражданство
        /// </summary>
        public string Citizenship { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="citizenship">гражданство</param>
        /// <exception cref="ArgumentNullException">если передан null</exception>
        public CitizenshipRecord(Guid id, string citizenship) : base(id)
        {
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
        }
    }
}