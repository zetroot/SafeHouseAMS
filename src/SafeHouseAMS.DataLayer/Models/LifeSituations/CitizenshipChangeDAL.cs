using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class CitizenshipChangeDAL : LifeSituationDocumentDAL
    {
        public CitizenshipRecordDAL Citizenship => Records.OfType<CitizenshipRecordDAL>().Single();
    }
}
