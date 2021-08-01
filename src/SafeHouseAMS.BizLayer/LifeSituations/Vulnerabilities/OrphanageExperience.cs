using System.Diagnostics.CodeAnalysis;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Опыт интернатного учреждения
    /// </summary>
    public record OrphanageExperience : Vulnerability
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage] public override string ToString() => "Опыт интернатного учреждения";
    }
}