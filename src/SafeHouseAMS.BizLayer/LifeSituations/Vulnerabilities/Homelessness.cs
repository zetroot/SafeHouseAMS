using System.Diagnostics.CodeAnalysis;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Бездомность
    /// </summary>
    public record Homelessness : Vulnerability
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage] public override string ToString() => "Бездомность";
    }
}