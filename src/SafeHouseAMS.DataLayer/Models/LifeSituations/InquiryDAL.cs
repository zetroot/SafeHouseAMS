using System.Collections.Generic;

namespace SafeHouseAMS.DataLayer.Models.LifeSituations
{
    internal class InquiryDAL : LifeSituationDocumentDAL
    {
        public bool IsJuvenile { get; set; }

        #region inquiry sources

        /// <summary>
        /// Самообращение?
        /// </summary>
        public bool IsSelfInquiry { get; set; }

        /// <summary>
        /// Битовая маска каналов самообращения
        /// </summary>
        public int? SelfInquirySourcesMask { get; set; }

        /// <summary>
        /// Направлен другим пострадавшим
        /// </summary>
        public bool IsForwardedBySurvivor { get; set; }

        /// <summary>
        /// Уточнение - каким 
        /// </summary>
        public string? ForwardedBySurvivor { get; set; }

        /// <summary>
        /// Направлен другим лицом
        /// </summary>
        public bool IsForwardedByPerson { get; set; }

        /// <summary>
        /// кто направил
        /// </summary>
        public string? ForwardedByPerson { get; set; }

        /// <summary>
        /// направлен другой организацией
        /// </summary>
        public bool IsForwardedByOrganization { get; set; }

        /// <summary>
        /// какой другой организацией
        /// </summary>
        public string? ForwardedByOrgannization { get; set; }

        #endregion
        
        public virtual CitizenshipRecordDAL? Citizenship { get; set; }
        public virtual DomicileRecordDAL? Domicile { get; set; }
        public virtual ChildrenRecordDAL? HasChildren { get; set; }

        public virtual List<EducationLevelRecordDAL>? EducationLevel { get; set; }
        public virtual List<SpecialityRecordDAL>? Specialities { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public string? WorkingExperience { get; set; }

        #region vulnerabilities

        /// <summary>
        /// Зависимость
        /// </summary>
        public bool HasAddiction { get; set; }

        /// <summary>
        /// Тип зависимости
        /// </summary>
        public string? AddictionKind { get; set; }

        /// <summary>
        /// насилие в детстве
        /// </summary>
        public bool ChildhoodViolence { get; set; }

        /// <summary>
        /// бездомность
        /// </summary>
        public bool Homelessness { get; set; }

        /// <summary>
        /// мигрант_ка
        /// </summary>
        public bool Migration { get; set; }

        /// <summary>
        /// опыт интернатного учреждения
        /// </summary>
        public bool OrphanageExperience { get; set; }

        /// <summary>
        /// другие факторы уязвимости
        /// </summary>
        public bool HasOtherVulnerability { get; set; }

        /// <summary>
        /// уточнение других факторов уязвимости
        /// </summary>
        public string? OtherVulnerabilityDetails { get; set; }

        /// <summary>
        /// битовая маска факторов уязвимости по здоровью
        /// </summary>
        public int HealthStatusVulnerabilityMask { get; set; }

        /// <summary>
        /// другие факторы уязвимости по здоровью - уточнение
        /// </summary>
        public string? OtherHealthStatusVulnerabilityDetail { get; set; }

        #endregion

    }
}