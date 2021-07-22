using System;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Интерфейс объекта доменной модели
    /// </summary>
    public interface IDomainModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Признак удалённой записи
        /// </summary>
        bool IsDeleted { get; }
        
        /// <summary>
        /// Timestamp создания модели
        /// </summary>
        DateTime Created { get; }
        
        /// <summary>
        /// Timestamp последнего изменения модели
        /// </summary>
        DateTime LastEdit { get; }
    }
}