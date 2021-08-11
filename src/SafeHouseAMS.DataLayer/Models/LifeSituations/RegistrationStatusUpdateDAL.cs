using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class RegistrationStatusUpdateDAL : LifeSituationDocumentDAL
    {
        public RegistrationStatusRecordDAL Record => AllRecords.OfType<RegistrationStatusRecordDAL>().Single();
    }
}