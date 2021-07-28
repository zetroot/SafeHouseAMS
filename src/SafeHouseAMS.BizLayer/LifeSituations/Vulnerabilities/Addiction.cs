using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Зависимость
    /// </summary>
    public class Addiction : Vulnerability
    {
        /// <summary>
        /// Тип зависимости
        /// </summary>
        public string AddictionKind { get; }
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="addictionKind">Тип зависимости</param>
        /// <exception cref="ArgumentNullException">если переданный тип был null</exception>
        public Addiction(string addictionKind)
        {
            AddictionKind = addictionKind ?? throw new ArgumentNullException(nameof(addictionKind));
        }

        /// <inheritdoc />
        public override string ToString() => string.IsNullOrEmpty(AddictionKind) ? "Зависимость" : $"Зависимость ({AddictionKind})";
    }
}