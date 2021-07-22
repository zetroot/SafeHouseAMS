using System;

namespace SafeHouseAMS.DataLayer.Models
{
    internal class BaseDalModel
    {
        public Guid ID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastEdit { get; set; }
    }
}