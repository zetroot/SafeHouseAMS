using System.Collections.Generic;
using System.Linq;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class EducationLevelUpdateDAL : LifeSituationDocumentDAL
    {
        public IReadOnlyCollection<EducationLevelRecordDAL> Records =>
            AllRecords.OfType<EducationLevelRecordDAL>().ToList();
    }
}