using System.Diagnostics.CodeAnalysis;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Мигрант_ка
    /// </summary>
    public record Migration : Vulnerability
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage] public override string ToString() => "Мигрант_ка";
    }
}