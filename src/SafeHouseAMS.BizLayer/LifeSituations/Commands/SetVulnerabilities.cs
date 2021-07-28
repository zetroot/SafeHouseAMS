using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда установки факторов уязвимости
    /// </summary>
    public class SetVulnerabilities : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Коллекция факторов уязвимости
        /// </summary>
        public IReadOnlyCollection<Vulnerability> Vulnerabilities { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор документа обращения, для которого регистрируются уязвимости</param>
        /// <param name="vulnerabilities">коллекция факторов уязвимости</param>
        /// <exception cref="ArgumentNullException">если вместо коллекции был null</exception>
        public SetVulnerabilities(Guid entityID, IEnumerable<Vulnerability> vulnerabilities) : base(entityID)
        {
            Vulnerabilities = vulnerabilities?.ToList() ?? throw new ArgumentNullException(nameof(vulnerabilities));
        }

        internal override async Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));

            if (Vulnerabilities.Any(x => x is Homelessness))
                await repository.SetHomeless(EntityID);
            else
                await repository.ClearHomeless(EntityID);
            
            if (Vulnerabilities.Any(x => x is Migration))
                await repository.SetMigration(EntityID);
            else
                await repository.ClearMigration(EntityID);
            
            if (Vulnerabilities.Any(x => x is ChildhoodViolence))
                await repository.SetChildhoodViolence(EntityID);
            else
                await repository.ClearChildhoodViolence(EntityID);
            
            if (Vulnerabilities.Any(x => x is OrphanageExperience))
                await repository.SetOrphanageExperience(EntityID);
            else
                await repository.ClearOrphanageExperience(EntityID);
            

            if (Vulnerabilities.FirstOrDefault(x => x is Addiction) is Addiction addiction)
                await repository.SetAddiction(EntityID, addiction.AddictionKind);
            else
                await repository.ClearAddiction(EntityID);
            
            if (Vulnerabilities.FirstOrDefault(x => x is Other) is Other other)
                await repository.SetOther(EntityID, other.Details);
            else
                await repository.ClearOther(EntityID);

            if (Vulnerabilities.FirstOrDefault(x => x is HealthStatus) is HealthStatus healthStatus)
                await repository.SetHealthStatusVulnerability(EntityID, healthStatus.Kind, healthStatus.OtherDetailed);
            else
                await repository.ClearHealthStatusVulnerability(EntityID);

        }
    }
}