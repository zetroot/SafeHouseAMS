using System.Diagnostics.CodeAnalysis;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Мигрант_ка
    /// </summary>
    public class Migration : Vulnerability
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage] public override string ToString() => "Мигрант_ка";
    }
}