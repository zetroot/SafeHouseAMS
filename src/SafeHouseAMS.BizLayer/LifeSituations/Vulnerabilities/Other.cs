using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Другое
    /// </summary>
    public record Other : Vulnerability
    {
        /// <summary>
        /// Уточнение что другое
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="details">Уточнение</param>
        /// <exception cref="ArgumentNullException">в случае, если текст уточнения был null</exception>
        public Other(string details)
        {
            Details = details ?? throw new ArgumentNullException(nameof(details));
        }

        /// <inheritdoc />
        public override string ToString() => string.IsNullOrEmpty(Details) ? "Иное" : $"Иное ({Details})";
    }
}