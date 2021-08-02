using System;
using System.Threading.Tasks;

namespace SafeHouseAMS.BizLayer.Survivors.Commands
{
    /// <summary>
    /// Команда создания нового пострадавшего
    /// </summary>
    public class CreateSurvivor : SurvivorCommand
    {
        /// <summary>
        /// Имя создаваемого пострадавшего
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Пол
        /// </summary>
        public SexEnum Sex{ get; }
        
        /// <summary>
        /// Уточнение пола
        /// </summary>
        public string? OtherSex{ get; }
        
        /// <summary>
        /// Точная дата рождения
        /// </summary>
        public DateTime? BirthDateAccurate{ get; }
        
        /// <summary>
        /// Вычисленная дата рождения
        /// </summary>
        public DateTime? BirthDateCalculated{ get; }

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
            DateTime? birthDateAccurate,
            DateTime? birthDateCalculated) : base(entityID)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Sex = sex;
            this.OtherSex = otherSex;
            this.BirthDateAccurate = birthDateAccurate;
            this.BirthDateCalculated = birthDateCalculated;
        }

        /// <inheritdoc />
        internal override Task ApplyOn(ISurvivorRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            return repository.Create(EntityID, false, DateTime.Now, DateTime.Now, 
                Name, 
                Sex, OtherSex, 
                BirthDateAccurate, BirthDateCalculated);
        }
    }
}