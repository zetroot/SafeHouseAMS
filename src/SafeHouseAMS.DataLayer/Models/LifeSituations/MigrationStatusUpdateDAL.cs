using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class MigrationStatusUpdateDAL : LifeSituationDocumentDAL
    {
        public MigrationStatusRecordDAL Record => AllRecords.OfType<MigrationStatusRecordDAL>().Single();
    }
}