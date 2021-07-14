using System;

namespace SafeHouseAMS.BizLayer.Models
{
    /// <summary>
    /// Карточка пострадавшего
    /// </summary>
    public class Survivor : IBizRecord
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Признак удалённой записи
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Номер карточки п/п
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// Имя пострадавшего
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Пол
        /// </summary>
        public SexEnum Sex { get; set; } 
        
        /// <summary>
        /// Уточнение другого пола
        /// </summary>
        public string? OtherSex { get; set; }

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
        public DateTime? BirthDateAccurate { get; set; }
        
        /// <summary>
        /// Вычисленная (приблизительная) дата рождения. Если неизвестна точная
        /// </summary>
        public DateTime? BirthDateCalculated { get; set; }

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

    /// <summary>
    /// Перечисление типов пола
    /// </summary>
    public enum SexEnum
    {
        Female = 0,
        Male = 1,
        Other = 2
    }
}