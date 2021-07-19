using System;

namespace SafeHouseAMS.DataLayer.Models
{
    internal class BaseDalModel
    {
        public Guid ID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }
    }
}