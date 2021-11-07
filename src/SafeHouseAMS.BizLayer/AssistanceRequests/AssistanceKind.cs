using System.ComponentModel;

namespace SafeHouseAMS.BizLayer.AssistanceRequests
{
    /// <summary>
    /// виды помощи / запросов помощи
    /// </summary>
    public enum AssistanceKind
    {
        /// <summary>
        /// default undefined value
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Освобождение
        /// </summary>
        [Description("Освобождение")] Release,

        /// <summary>
        /// Проживание
        /// </summary>
        [Description("Проживание")] Accomodation,

        /// <summary>
        /// Питание
        /// </summary>
        [Description("Питание")] Food,

        /// <summary>
        /// Гигиена
        /// </summary>
        [Description("Гигиена")] Hygiene,

        /// <summary>
        /// Одежда / гуманитарная помощь
        /// </summary>
        [Description("Одежда")] Clothes,

        /// <summary>
        /// Связь
        /// </summary>
        [Description("Связь")] Phone,

        /// <summary>
        /// Проезд
        /// </summary>
        [Description("Проезд")] Transport,

        /// <summary>
        /// помощь с документами
        /// </summary>
        [Description("Оформление или восстановление документов")] Documents,

        /// <summary>
        /// медицинская помощь
        /// </summary>
        [Description("Медицинская помощь")] MedicalHelp,

        /// <summary>
        /// работа с психологом
        /// </summary>
        [Description("Психологическая помощь")] PsychologicalHelp,

        /// <summary>
        /// юристы
        /// </summary>
        [Description("Юридическая помощь")] LegalAid,

        /// <summary>
        /// соцпомощь
        /// </summary>
        [Description("Социальное сопровождение")] SocialSupport,

        /// <summary>
        /// обучение
        /// </summary>
        [Description("Обучение")] Education,

        /// <summary>
        /// помощь с работой
        /// </summary>
        [Description("Помощь в трудоустройстве")] EmploymentAssistance,

        /// <summary>
        /// возвращение
        /// </summary>
        [Description("Возвращение")] Returning,

        /// <summary>
        /// наставник
        /// </summary>
        [Description("Наставничество")] Mentoring,

        /// <summary>
        /// сотрудничество с правоохранителями
        /// </summary>
        [Description("Сотрудничество с правоохранительными органами")] PoliceAssistance,

        /// <summary>
        /// сотрудничество с журналистами
        /// </summary>
        [Description("Сотрудничество с журналистами")] JournalistsAssistance
    }
}
