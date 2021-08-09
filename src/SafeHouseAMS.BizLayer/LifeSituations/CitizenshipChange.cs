using System;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Документ о смене граждаснтва
    /// </summary>
    public class CitizenshipChange : LifeSituationDocument
    {
        /// <summary>
        /// Новая запись о гражданстве
        /// </summary>
        public CitizenshipRecord Citizenship { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор документа</param>
        /// <param name="isDeleted">признак удалённого документа</param>
        /// <param name="created">дата создания</param>
        /// <param name="lastEdit">дата последнего редактирования</param>
        /// <param name="documentDate">дата документа</param>
        /// <param name="survivor">пострадавший, к которому относится документ</param>
        /// <param name="citizenship">собственно запись о гражданстве</param>
        /// <exception cref="ArgumentNullException">если передали null, куда не следует</exception>
        public CitizenshipChange(Guid id,
            bool isDeleted,
            DateTime created,
            DateTime lastEdit,
            DateTime documentDate,
            Survivor survivor,
            CitizenshipRecord citizenship) :
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
        }
    }
}
