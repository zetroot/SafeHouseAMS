using System;
using System.Threading.Tasks;
using SafeHouseAMS.BizLayer.LifeSituations.Records;

namespace SafeHouseAMS.BizLayer.LifeSituations.Commands
{
    /// <summary>
    /// Команда добавления записи о специальности
    /// </summary>
    public class AddSpeciality : LifeSituationDocumentCommand
    {
        /// <summary>
        /// Собственно специальность
        /// </summary>
        public string Speciality { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="entityID">идентификатор документ</param>
        /// <param name="speciality">наименование специальности</param>
        /// <exception cref="ArgumentNullException">если специальность была null</exception>
        public AddSpeciality(Guid entityID, string speciality) : base(entityID)
        {
            Speciality = speciality ?? throw new ArgumentNullException(nameof(speciality));
        }
        internal override Task ApplyOn(ILifeSituationDocumentsRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.AddRecord(EntityID, new SpecialityRecord(Guid.NewGuid(), Speciality));
        }
    }
}