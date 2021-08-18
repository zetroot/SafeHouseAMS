using System;
using System.Collections.Generic;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations
{
    /// <summary>
    /// Отчёт о текущенм состоянии пострадавшего
    /// </summary>
    /// <param name="SurvivorID">идентификатор пострадавшего</param>
    /// <param name="Children">Актуальная запись о детях, если есть</param>
    /// <param name="HasChangedChildren">Были ли изменения статуса по детям?</param>
    /// <param name="Citizenship">Актуальная запись о гражданстве</param>
    /// <param name="HasChangedCitizenship">Были ли изменения записи о гражданстве</param>
    /// <param name="Domicile">Актуальная запись о проживании</param>
    /// <param name="HasChangedDomicile">Были ли изменения запимси о проживании</param>
    /// <param name="Education">Коллекция записей об уровне образования</param>
    /// <param name="HasChangedEducation">Были ли изменения?</param>
    /// <param name="Migration">Актуальная запись о миграционном статусе</param>
    /// <param name="HasChangedMigration">Были ли изменения миграционного статуса</param>
    /// <param name="Registration">Актуальная запись о регистрации</param>
    /// <param name="HasChangedRegistration">Были ли изменения статуса регистрации</param>
    /// <param name="Specialities">Коллекция записей о специальностях</param>
    /// <param name="HasChangedSpecialities">Были ли изменения специальностей</param>
    public record SurvivorStateReport(Guid SurvivorID,
        ChildrenRecord? Children, bool HasChangedChildren,
        CitizenshipRecord? Citizenship, bool HasChangedCitizenship,
        DomicileRecord? Domicile, bool HasChangedDomicile,
        IReadOnlyCollection<EducationLevelRecord> Education, bool HasChangedEducation,
        MigrationStatusRecord? Migration, bool HasChangedMigration,
        RegistrationStatusRecord? Registration, bool HasChangedRegistration,
        IReadOnlyCollection<SpecialityRecord> Specialities, bool HasChangedSpecialities
    );
}
