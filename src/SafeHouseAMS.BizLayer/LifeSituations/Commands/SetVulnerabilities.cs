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

        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}