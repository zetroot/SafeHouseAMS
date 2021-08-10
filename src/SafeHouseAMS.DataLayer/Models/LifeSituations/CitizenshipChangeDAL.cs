using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class CitizenshipChangeDAL : LifeSituationDocumentDAL
    {
        public CitizenshipRecordDAL Record => Records.OfType<CitizenshipRecordDAL>().Single();
    }
}
