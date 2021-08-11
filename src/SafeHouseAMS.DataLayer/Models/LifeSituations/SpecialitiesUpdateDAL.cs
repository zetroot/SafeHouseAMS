using System.Collections.Generic;
using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class SpecialitiesUpdateDAL : LifeSituationDocumentDAL
    {
        public IReadOnlyCollection<SpecialityRecordDAL> Records =>
            AllRecords.OfType<SpecialityRecordDAL>().ToList();
    }
}