using System;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Базовая доменная модель
    /// </summary>
    public abstract class BaseDomainModel : IDomainModel
    {
        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public bool IsDeleted { get; }

        /// <inheritdoc />
        public DateTime Created { get; }

        /// <inheritdoc />
        public DateTime LastEdit { get; }
        
        /// <summary>
        /// Ctor 
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="isDeleted">признак удаленной записи</param>
        /// <param name="created">временная метка создания</param>
        /// <param name="lastEdit">временная метка последнего редактирования</param>
        protected BaseDomainModel(Guid id, bool isDeleted, DateTime created, DateTime lastEdit)
        {
            ID = id;
            IsDeleted = isDeleted;
            Created = created;
            LastEdit = lastEdit;
        }
    }
}