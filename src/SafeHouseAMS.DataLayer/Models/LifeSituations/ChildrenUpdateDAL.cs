using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class ChildrenUpdateDAL : LifeSituationDocumentDAL
    {
        public ChildrenRecordDAL Record => AllRecords.OfType<ChildrenRecordDAL>().Single();
    }
}