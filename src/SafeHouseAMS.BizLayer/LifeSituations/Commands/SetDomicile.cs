using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда установки места жительства
    /// </summary>
    public class SetDomicile : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Место жительства
        /// </summary>
        public string Place { get; }

        /// <summary>
        /// Тип места жительства
        /// </summary>
        public DomicileRecord.PlaceKind? Kind { get; }

        /// <summary>
        /// Живет од_на
        /// </summary>
        public bool LivesAlone { get; }

        /// <summary>
        /// С супругом/партнером
        /// </summary>
        public bool WithPartner { get; }

        /// <summary>
        /// С детьми
        /// </summary>
        public bool WithChildren { get; }

        /// <summary>
        /// Подробнее о детях
        /// </summary>
        public string? ChildrenDetails { get; }

        /// <summary>
        /// с родителями
        /// </summary>
        public bool WithParents { get; }

        /// <summary>
        /// подробнее о родителях
        /// </summary>
        public string? ParentsDetails { get; }

        /// <summary>
        /// с другими родственниками
        /// </summary>
        public bool WithOtherRelatives { get; }

        /// <summary>
        /// подробнее о других родственниках
        /// </summary>
        public string? OtherRelativesDetails { get; }

        /// <summary>
        /// с какими то другими людьми
        /// </summary>
        public bool WithOtherPeople { get; }

        /// <summary>
        /// подробнее о других людях
        /// </summary>
        public string? OtherPeopleDetails { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа в рамках которого создана запись</param>
        /// <param name="place">место жительства</param>
        /// <param name="kind">тип места проживания</param>
        /// <param name="livesAlone">живет од_на</param>
        /// <param name="withPartner">с партнером</param>
        /// <param name="childrenDetails">если заполнено - то с детьми</param>
        /// <param name="parentsDetails">если заполнено - то с родитеялми</param>
        /// <param name="otherRelativesDetails">если заполнено - то с другими родственниками</param>
        /// <param name="otherPeopleDetails">если заполнено - то с другими людьми</param>
        /// <exception cref="ArgumentNullException">если место жительства было null</exception>
        public SetDomicile(Guid entityID, 
            string place, DomicileRecord.PlaceKind? kind,
            bool livesAlone,
            bool withPartner,
            string? childrenDetails, 
            string? parentsDetails,
            string? otherRelativesDetails,
            string? otherPeopleDetails) :
            base(entityID)
        {
            Place = place ?? throw new ArgumentNullException(nameof(place));
            Kind = kind;
            LivesAlone = livesAlone;
            WithPartner = withPartner;
            WithChildren = childrenDetails != null;
            ChildrenDetails = childrenDetails;
            WithParents = parentsDetails != null;
            ParentsDetails = parentsDetails;
            WithOtherRelatives = otherRelativesDetails != null;
            OtherRelativesDetails = otherRelativesDetails;
            WithOtherPeople = otherPeopleDetails != null;
            OtherPeopleDetails = otherPeopleDetails;
        }

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));

            var record = new DomicileRecord(Guid.NewGuid(), Place, Kind,
            LivesAlone, WithPartner,
            WithChildren, ChildrenDetails,
            WithParents, ParentsDetails,
            WithOtherRelatives, OtherRelativesDetails,
            WithOtherPeople, OtherPeopleDetails);

            return repository.AddRecord(EntityID, record);
        }
    }
}