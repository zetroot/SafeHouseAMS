using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Обращение за помощью
    /// </summary>
    public class Inquiry : LifeSituationDocument
    {
        /// <summary>
        /// несовершеннолетний на момент обращения
        /// </summary>
        public bool IsJuvenile { get; }

        /// <summary>
        /// Источники обращения
        /// </summary>
        public IReadOnlyCollection<IInquirySource> InquirySources { get; }

        /// <summary>
        /// Гражданство в момент обращения
        /// </summary>
        public CitizenshipRecord Citizenship { get; }

        /// <summary>
        /// Место проживания в момент обращения + где проиживает
        /// </summary>
        public DomicileRecord? Domicile { get; }

        /// <summary>
        /// Наличие детей
        /// </summary>
        public ChildrenRecord? HasChildren { get; }

        /// <summary>
        /// Уровень образования
        /// </summary>
        public IReadOnlyCollection<EducationLevelRecord>? EducationLevel { get; }

        /// <summary>
        /// Специальности
        /// </summary>
        public IReadOnlyCollection<SpecialityRecord>? Specialities { get; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public string? WorkingExperience { get; }

        /// <summary>
        /// Иные факторы уязвимости
        /// </summary>
        public IReadOnlyCollection<Vulnerability>? VulnerabilityFactors { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="id">идентификатор</param>
        /// <param name="isDeleted">признак удалённой записи</param>
        /// <param name="created">дата создания записи</param>
        /// <param name="lastEdit">дата последнего редактирования</param>
        /// <param name="documentDate">дата обращения</param>
        /// <param name="survivor">пострадавший</param>
        /// <param name="isJuvenile">несовершеннолетний на момент обращения</param>
        /// <param name="inquirySources">источники обращения</param>
        /// <param name="citizenship">гразданство</param>
        /// <param name="domicile">место жительства, если известно</param>
        /// <param name="hasChildren">статус по детям</param>
        /// <param name="educationLevel">статус образования</param>
        /// <param name="specialities">специальности</param>
        /// <param name="workingExperience">опыт работы</param>
        /// <param name="vulnerabilityFactors">факторы уязвимости</param>
        /// <exception cref="ArgumentNullException">если нет гражданства или источников обращения</exception>
        public Inquiry(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
            DateTime documentDate, Survivor survivor,
            bool isJuvenile, IEnumerable<IInquirySource> inquirySources, 
            CitizenshipRecord citizenship, 
            DomicileRecord? domicile,
            ChildrenRecord? hasChildren,
            IEnumerable<EducationLevelRecord>? educationLevel,
            IEnumerable<SpecialityRecord>? specialities,
            string? workingExperience, 
            IEnumerable<Vulnerability>? vulnerabilityFactors) : 
            base(id, isDeleted, created, lastEdit, documentDate, survivor)
        {
            IsJuvenile = isJuvenile;
            InquirySources = inquirySources?.ToList() ?? throw new ArgumentNullException(nameof(inquirySources));
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
            Domicile = domicile;
            HasChildren = hasChildren;
            EducationLevel = educationLevel?.ToList();
            Specialities = specialities?.ToList();
            WorkingExperience = workingExperience;
            VulnerabilityFactors = vulnerabilityFactors?.ToList();
        }
    }
}