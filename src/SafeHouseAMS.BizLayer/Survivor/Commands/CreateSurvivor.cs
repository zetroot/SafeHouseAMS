using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivor.Commands
{
    /// <summary>
    /// Команда создания нового пострадавшего
    /// </summary>
    public class CreateSurvivor : SurvivorCommand
    {
        private readonly string name;
        private readonly SexEnum sex;
        private readonly string? otherSex;
        private readonly DateTimeOffset? birthDateAccurate;
        private readonly DateTimeOffset? birthDateCalculated;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="entityID">идентификатор записи</param>
        /// <param name="name">Имя</param>
        /// <param name="sex">Пол</param>
        /// <param name="otherSex">уточнение пола</param>
        /// <param name="birthDateAccurate">точная дата рождения</param>
        /// <param name="birthDateCalculated">вычисленная приблизительная дата рождения</param>
        /// <exception cref="ArgumentNullException">если имя было null</exception>
        public CreateSurvivor(Guid entityID, 
            string name,
            SexEnum sex,
            string? otherSex,
            DateTimeOffset? birthDateAccurate,
            DateTimeOffset? birthDateCalculated) : base(entityID)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.sex = sex;
            this.otherSex = otherSex;
            this.birthDateAccurate = birthDateAccurate;
            this.birthDateCalculated = birthDateCalculated;
        }

        /// <inheritdoc />
        internal override Task ApplyOn(ISurvivorRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.Create(EntityID, name, sex, otherSex, birthDateAccurate, birthDateCalculated);
        }
    }
}