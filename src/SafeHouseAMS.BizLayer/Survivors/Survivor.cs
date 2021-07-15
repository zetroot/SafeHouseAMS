using System;

namespace SafeHouseAMS.BizLayer.Survivors
{
    /// <summary>
    /// Карточка пострадавшего
    /// </summary>
    public class Survivor : BaseDomainModel
    {
        /// <summary>
        /// Номер карточки п/п
        /// </summary>
        public int Num { get; }

        /// <summary>
        /// Имя пострадавшего
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Пол
        /// </summary>
        public SexEnum Sex { get; }

        /// <summary>
        /// Уточнение другого пола
        /// </summary>
        public string? OtherSex { get; }

        /// <summary>
        /// Пол для отображения
        /// </summary>
        public string SexDisplay => Sex switch
        {
            SexEnum.Female => "женский",
            SexEnum.Male => "мужской",
            _ => OtherSex ?? "другой"
        };

        /// <summary>
        /// Точная дата рождения - если известна
        /// </summary>
        public DateTimeOffset? BirthDateAccurate { get; }

        /// <summary>
        /// Вычисленная (приблизительная) дата рождения. Если неизвестна точная
        /// </summary>
        public DateTimeOffset? BirthDateCalculated { get; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int? Age =>
            BirthDateAccurate.HasValue ? CalcTodayAge(BirthDateAccurate.Value) :
            BirthDateCalculated.HasValue ? CalcTodayAge(BirthDateCalculated.Value) : null;


        private static int CalcTodayAge(DateTimeOffset dob)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - dob.Year;
            // Go back to the year in which the person was born in case of a leap year
            if (dob.Date > today.AddYears(-age)) return age - 1;
            return age;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="isDeleted">Признак удаленной записи</param>
        /// <param name="created">Дата-время создания</param>
        /// <param name="lastEdit">Дата время последнего редактирования</param>
        /// <param name="name">Имя пострадавшего</param>
        /// <param name="num">Номер карточки</param>
        /// <param name="sex">Пол</param>
        /// <param name="otherSex">Уточнение другого пола</param>
        /// <param name="birthDateAccurate">Точная дата рождения, если известно</param>
        /// <param name="birthDateCalculated">Приблизительная дата рождения</param>
        public Survivor(Guid id,
            bool isDeleted,
            DateTimeOffset created,
            DateTimeOffset lastEdit,
            string name,
            int num,
            SexEnum sex,
            string? otherSex,
            DateTimeOffset? birthDateAccurate,
            DateTimeOffset? birthDateCalculated) :
            base(id, isDeleted, created, lastEdit)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Num = num;
            Sex = sex;
            OtherSex = otherSex;
            BirthDateAccurate = birthDateAccurate;
            BirthDateCalculated = birthDateCalculated;
        }
    }
}