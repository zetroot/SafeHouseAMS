using FluentAssertions;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.LifeSituations.Vulnerabilities
{
    public class HealthStatusVulnearbilityTest
    {
        [Fact, UnitTest]
        public void ToString_WhenKindIsNone_ReturnsEmptyString() =>
            new HealthStatus(HealthStatus.HealthStatusVulnerabilityType.None, null!).ToString()
                .Should().BeEmpty();
        
        [Fact, UnitTest]
        public void ToString_WhenKindHasOther_ReturnsOtherDetails() =>
            new HealthStatus(HealthStatus.HealthStatusVulnerabilityType.Other, "details").ToString()
                .Should().Contain("(details)");
        
        [Fact, UnitTest]
        public void ToString_WhenKindHasOtherWithoutDetails_ReturnsNoBrackets() =>
            new HealthStatus(HealthStatus.HealthStatusVulnerabilityType.Other, null!).ToString()
                .Should().NotContain("(").And.NotContain(")");

        [Fact, UnitTest]
        public void ToString_WhenKindIsAll_Returns6Commas() =>
            new HealthStatus(
                HealthStatus.HealthStatusVulnerabilityType.Disability |
                HealthStatus.HealthStatusVulnerabilityType.SpecialNeeds |
                HealthStatus.HealthStatusVulnerabilityType.MentalDisorder |
                HealthStatus.HealthStatusVulnerabilityType.Tuberculosis |
                HealthStatus.HealthStatusVulnerabilityType.HIV |
                HealthStatus.HealthStatusVulnerabilityType.HepatitisB |
                HealthStatus.HealthStatusVulnerabilityType.HepatitisC |
                HealthStatus.HealthStatusVulnerabilityType.Other,
                null!).ToString()
                .Should().Contain(", ", Exactly.Times(7));
    }
    
}