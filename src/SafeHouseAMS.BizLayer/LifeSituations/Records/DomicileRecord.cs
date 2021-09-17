using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Место жительства
    /// </summary>
    public class DomicileRecord : BaseRecord
    {
        /// <summary>
        /// Тип места проживания
        /// </summary>
        public enum PlaceKind
        {
            /// <summary>
            /// Бездомный
            /// </summary>
            Homeless,

            /// <summary>
            /// Временно живёт у кого то
            /// </summary>
            Temporary,

            /// <summary>
            /// Общежитие
            /// </summary>
            Dorm,

            /// <summary>
            /// Аренда комнаты
            /// </summary>
            RentRoom,

            /// <summary>
            /// Аренда квартиры
            /// </summary>
            RentFlat,

            /// <summary>
            /// Собственное жильё
            /// </summary>
            OwnHome
        }
        /*
     - Место проживания на момент обращения
     - Где проживает (выбор из: нет жилья, временно живет у кого-то, общежитие, аренда комнаты, аренда квартиры, свое жильё (указать подробнее какое))
      (фича: если установлен "нет жилья",  то проставлять уязвимость - "бездомность")
     - С кем проживает: (множественный выбор галочками с подробностями:
     од_на, супруг/партнер, дети(указать количество), родители (подробнее), другие родственники (указать какие), другие люди (указать какие))
     */
        /// <summary>
        /// место жительства
        /// </summary>
        public string Place { get; }

        /// <summary>
        /// Тип места проживания
        /// </summary>
        public PlaceKind? Kind { get; }

        /// <summary>
        /// комментарий к типу места жительства
        /// </summary>
        public string LivingPlaceComment { get; }

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
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="place">место жительства</param>
        /// <param name="kind">тип места жительства</param>
        /// <param name="livingPlaceComment">комментраий к типу места жительства</param>
        /// <param name="livesAlone">живет од_на</param>
        /// <param name="withPartner">с партнером</param>
        /// <param name="withChildren">с детьми</param>
        /// <param name="childrenDetails">о детях</param>
        /// <param name="withParents">с родителями</param>
        /// <param name="parentsDetails">о родителях</param>
        /// <param name="withOtherRelatives">с другими родственниками</param>
        /// <param name="otherRelativesDetails">о других родственниках</param>
        /// <param name="withOtherPeople">с другими людьми</param>
        /// <param name="otherPeopleDetails">о других людях</param>
        /// <exception cref="ArgumentNullException">если место жительства было null</exception>
        public DomicileRecord(Guid id, string place, PlaceKind? kind, string livingPlaceComment,
            bool livesAlone,
            bool withPartner,
            bool withChildren, string? childrenDetails,
            bool withParents, string? parentsDetails,
            bool withOtherRelatives, string? otherRelativesDetails,
            bool withOtherPeople, string? otherPeopleDetails) : base(id)
        {
            Place = place ?? throw new ArgumentNullException(nameof(place));
            Kind = kind;
            LivingPlaceComment = livingPlaceComment;
            LivesAlone = livesAlone;
            WithPartner = withPartner;
            WithChildren = withChildren;
            ChildrenDetails = childrenDetails;
            WithParents = withParents;
            ParentsDetails = parentsDetails;
            WithOtherRelatives = withOtherRelatives;
            OtherRelativesDetails = otherRelativesDetails;
            WithOtherPeople = withOtherPeople;
            OtherPeopleDetails = otherPeopleDetails;
        }
    }
}
