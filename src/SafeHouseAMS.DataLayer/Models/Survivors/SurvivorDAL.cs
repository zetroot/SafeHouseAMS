﻿using System;

namespace SafeHouseAMS.DataLayer.Models.Survivors
{
    internal class SurvivorDAL : BaseDalModel
    {
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
        public int Sex { get; set; }

        /// <summary>
        /// Уточнение другого пола
        /// </summary>
        public string? OtherSex { get; set; }
        
        /// <summary>
        /// Точная дата рождения - если известна
        /// </summary>
        public DateTime? BirthDateAccurate { get; set; }

        /// <summary>
        /// Вычисленная (приблизительная) дата рождения. Если неизвестна точная
        /// </summary>
        public DateTime? BirthDateCalculated { get; set; }

    }
}