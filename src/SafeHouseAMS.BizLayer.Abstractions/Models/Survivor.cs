using System;

namespace SafeHouseAMS.BizLayer.Abstractions.Models
{
    /// <summary>
    /// Карточка пострадавшего
    /// </summary>
    public class Survivor : IBizRecord
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Признак удалённой записи
        /// </summary>
        public bool IsDeleted { get; set; }
        
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}