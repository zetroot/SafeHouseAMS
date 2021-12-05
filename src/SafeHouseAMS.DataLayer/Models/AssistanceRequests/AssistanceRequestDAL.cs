using System;
using System.Collections.Generic;
using SafeHouseAMS.DataLayer.Models.Survivors;

namespace SafeHouseAMS.DataLayer.Models.AssistanceRequests
{
    internal class AssistanceRequestDAL : BaseDalModel
    {
        public virtual SurvivorDAL? Survivor { get; set; }
        public Guid SurvivorID { get; set; }
        public int AssistanceKind { get; set; }
        public string Details { get; set; } = string.Empty;
        public bool IsAccomplished { get; set; }
        public virtual ICollection<AssistanceActDAL>? AssistanceActs { get; set; }
        public DateTime DocumentDate { get; set; }
    }

}
