using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Records
{
    /// <summary>
    /// Уровень образования
    /// </summary>
    public class EducationLevelRecord : BaseRecord
    {
        /// <summary>
        /// Тип образования
        /// </summary>
        public enum EduLevel
        {
            /// <summary>
            /// нет
            /// </summary>
            None,
            /// <summary>
            /// школа меньше 9 классов
            /// </summary>
            SchoolLess9,
            /// <summary>
            /// 9 классов школы
            /// </summary>
            School9,
            /// <summary>
            /// 11 классов школы
            /// </summary>
            School11,
            /// <summary>
            /// Среднее-специальное
            /// </summary>
            Special,
            /// <summary>
            /// Неоконченное высшее
            /// </summary>
            HighNotEnded,
            /// <summary>
            /// Высшее
            /// </summary>
            High,
            /// <summary>
            /// курсы
            /// </summary>
            Courses
        }
        
        /// <summary>
        /// Уровень полученного образования
        /// </summary>
        public EduLevel Level { get; }
        
        /// <summary>
        /// Дополнительная информация об уровне образования
        /// </summary>
        public string? Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="id">идентификатор записи</param>
        /// <param name="level">Уровень полученного образования</param>
        /// <param name="details">Подробности - количество классов или названия курсов</param>
        public EducationLevelRecord(Guid id, EduLevel level, string? details) : base(id)
        {
            Level = level;
            Details = details;
        }

    }
}