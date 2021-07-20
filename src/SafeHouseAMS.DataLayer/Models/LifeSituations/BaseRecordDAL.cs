using System;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    /// <summary>
    /// Класс-заголовок 
    /// </summary>
    internal abstract class BaseRecordDAL
    {
        /// <summary>
        /// идентификатор записи
        /// </summary>
        public Guid ID { get; set; }
        
        /// <summary>
        /// Идентификатор документа породившего запись
        /// </summary>
        public Guid DocumentID { get; set; }
        
        /// <summary>
        /// Сериализованное в JSON содержимое записи
        /// </summary>
        public string Content { get; set; } = "{}";
    }

    internal class ChildrenRecordDAL : BaseRecordDAL { }
    internal class CitizenshipRecordDAL : BaseRecordDAL { }
    internal class DomicileRecordDAL : BaseRecordDAL { }
    internal class EducationLevelRecordDAL : BaseRecordDAL { }
    internal class SpecialityRecordDAL : BaseRecordDAL { }
    
}