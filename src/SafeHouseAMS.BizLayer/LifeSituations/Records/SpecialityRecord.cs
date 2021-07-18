using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Специальность
    /// </summary>
    public class SpecialityRecord : BaseRecord
    {
        /// <summary>
        /// Название специальности
        /// </summary>
        public string Speciality { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="speciality">название специальности</param>
        /// <exception cref="ArgumentNullException">если специальность была пуста</exception>
        public SpecialityRecord(Guid id, string speciality) : base(id)
        {
            Speciality = speciality ?? throw new ArgumentNullException(nameof(speciality));
        }
    }
}