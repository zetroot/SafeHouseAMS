using System;
using SafeHouseAMS.DataLayer.Models.Survivors;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal abstract class LifeSituationDocumentDAL : BaseDalModel
    {
        public DateTime DocumentDate { get; set; }
        public Guid SurvivorID { get; set; }
        public virtual SurvivorDAL? Survivor { get; set; }
    }

}