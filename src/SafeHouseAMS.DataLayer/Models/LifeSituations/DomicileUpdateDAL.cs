using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class DomicileUpdateDAL : LifeSituationDocumentDAL
    {
        public DomicileRecordDAL Record => AllRecords.OfType<DomicileRecordDAL>().Single();
    }
}