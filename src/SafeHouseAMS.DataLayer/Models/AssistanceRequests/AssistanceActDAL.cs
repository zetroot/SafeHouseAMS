using System;

namespace SafeHouseAMS.DataLayer.Models.AssistanceRequests
{
    internal class AssistanceActDAL : BaseDalModel
    {
        public string Details { get; set; } = string.Empty;
        public decimal WorkHours { get; set; }
        public decimal Money { get; set; }
        public virtual AssistanceRequestDAL? Request { get; set; }
        public Guid RequestID { get; set; }
    }
}