using System;

namespace SafeHouseAMS.BizLayer.Survivors
{
    /// <summary>
    /// Карточка пострадавшего
    /// </summary>
    /// <param name="ID">Идентификатор записи</param>
    /// <param name="IsDeleted">Признак удаленной записи</param>
    /// <param name="Created">Дата-время создания</param>
    /// <param name="LastEdit">Дата время последнего редактирования</param>
    /// <param name="Name">Имя пострадавшего</param>
    /// <param name="Num">Номер карточки</param>
    /// <param name="Sex">Пол</param>
    /// <param name="OtherSex">Уточнение другого пола</param>
    /// <param name="BirthDateAccurate">Точная дата рождения, если известно</param>
    /// <param name="BirthDateCalculated">Приблизительная дата рождения</param>
    public record Survivor(Guid ID, bool IsDeleted, DateTime Created, DateTime LastEdit,
        string Name, int Num, SexEnum Sex, string? OtherSex,
        DateTime? BirthDateAccurate, DateTime? BirthDateCalculated) :
        BaseDomainModel(ID, IsDeleted, Created, LastEdit)
    {
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
        /// Возраст
        /// </summary>
        public int? Age =>
            BirthDateAccurate.HasValue ? CalcTodayAge(BirthDateAccurate.Value) :
            BirthDateCalculated.HasValue ? CalcTodayAge(BirthDateCalculated.Value) : null;

        private static int CalcTodayAge(DateTime dob)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - dob.Year;
            // Go back to the year in which the person was born in case of a leap year
            if (dob.Date > today.AddYears(-age)) return age - 1;
            return age;
        }

    }
}
