using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Records;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using SafeHouseAMS.BizLayer.Survivors;

namespace SafeHouseAMS.BizLayer.LifeSituations.Documents
{
    /// <summary>
    /// Обращение за помощью
    /// </summary>
    /// <param name="ID">идентификатор</param>
    /// <param name="IsDeleted">признак удалённой записи</param>
    /// <param name="Created">дата создания записи</param>
    /// <param name="LastEdit">дата последнего редактирования</param>
    /// <param name="DocumentDate">дата обращения</param>
    /// <param name="Survivor">пострадавший</param>
    /// <param name="IsJuvenile">несовершеннолетний на момент обращения</param>
    /// <param name="InquirySources">источники обращения</param>
    /// <param name="Citizenship">гразданство</param>
    /// <param name="Domicile">место жительства, если известно</param>
    /// <param name="HasChildren">статус по детям</param>
    /// <param name="EducationLevel">статус образования</param>
    /// <param name="Specialities">специальности</param>
    /// <param name="WorkingExperience">опыт работы</param>
    /// <param name="VulnerabilityFactors">факторы уязвимости</param>
    /// <param name="MigrationStatus">запись о миграционном статусе</param>
    /// <param name="RegistrationStatus">запись о статусе регистрации</param>
    public record Inquiry(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        DateTime DocumentDate, Survivor Survivor,
        bool IsJuvenile, IReadOnlyCollection<IInquirySource> InquirySources,
        CitizenshipRecord Citizenship,
        DomicileRecord? Domicile,
        ChildrenRecord? HasChildren,
        IReadOnlyCollection<EducationLevelRecord>? EducationLevel,
        IReadOnlyCollection<SpecialityRecord>? Specialities,
        string? WorkingExperience,
        IReadOnlyCollection<Vulnerability>? VulnerabilityFactors,
        MigrationStatusRecord? MigrationStatus,
        RegistrationStatusRecord? RegistrationStatus) :
        LifeSituationDocument(ID, IsDeleted, Created, LastEdit, DocumentDate, Survivor);
}
