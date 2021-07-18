using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Базовый класс записи о жизненной ситуации
    /// </summary>
    public abstract class BaseRecord
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        protected BaseRecord(Guid id)
        {
            ID = id;
        }
    }
}