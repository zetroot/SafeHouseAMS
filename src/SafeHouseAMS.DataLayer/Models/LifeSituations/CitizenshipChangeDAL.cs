using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class CitizenshipChangeDAL : LifeSituationDocumentDAL
    {
        public CitizenshipRecordDAL? Record => AllRecords.OfType<CitizenshipRecordDAL>().SingleOrDefault();
    }

}
