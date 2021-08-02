using System;
using System.Linq;
using System.Text;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Уязвимость по состоянию здоровья
    /// </summary>
    public record HealthStatus : Vulnerability
    {
        /// <summary>
        /// Тип уязвимости по состоянию здоровья
        /// </summary>
        [Flags] public enum HealthStatusVulnerabilityType
        {
            /// <summary>
            /// нет
            /// </summary>
            None = 0,
            
            /// <summary>
            /// Инвалидность
            /// </summary>
            Disability = 1,
            
            /// <summary>
            /// ОВЗ - ограниченные возможности здоровья
            /// </summary>
            SpecialNeeds = 2,
            
            /// <summary>
            /// Психическое расстройство
            /// </summary>
            MentalDisorder = 4,
            
            /// <summary>
            /// Туберкулёз
            /// </summary>
            Tuberculosis = 8,
            
            /// <summary>
            /// ВИЧ
            /// </summary>
            HIV = 16,
            
            /// <summary>
            /// Гепатит B
            /// </summary>
            HepatitisB = 32,
            
            /// <summary>
            /// Гепатит C
            /// </summary>
            HepatitisC = 64,
            
            /// <summary>
            /// Другое
            /// </summary>
            Other = 128
        }
        
        /// <summary>
        /// Тип уязвимости по здоровью
        /// </summary>
        public HealthStatusVulnerabilityType Kind { get; }
        
        /// <summary>
        /// Уточнение в случае, если другое
        /// </summary>
        public string? OtherDetailed { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="kind">Типы уязвимости по состоянию здоровья</param>
        /// <param name="otherDetailed">Уточнение для иного</param>
        public HealthStatus(HealthStatusVulnerabilityType kind, string? otherDetailed)
        {
            Kind = kind;
            OtherDetailed = otherDetailed;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (Kind == HealthStatusVulnerabilityType.None) return string.Empty;
            var enumVals = Enum.GetValues<HealthStatusVulnerabilityType>()
                .Where(x => x != HealthStatusVulnerabilityType.None)
                .Where(x => Kind.HasFlag(x));
            var sb = new StringBuilder();
            foreach (var candidate in enumVals)
            {
                if (sb.Length != 0) sb.Append(", ");
                sb.Append(candidate switch
                {
                    HealthStatusVulnerabilityType.Disability => "инвалидность",
                    HealthStatusVulnerabilityType.SpecialNeeds => "ОВЗ",
                    HealthStatusVulnerabilityType.MentalDisorder => "психическое раастройство",
                    HealthStatusVulnerabilityType.Tuberculosis => "туберкулёз",
                    HealthStatusVulnerabilityType.HIV => "ВИЧ",
                    HealthStatusVulnerabilityType.HepatitisB => "гепатит B",
                    HealthStatusVulnerabilityType.HepatitisC => "гепатит C",
                    HealthStatusVulnerabilityType.Other when !string.IsNullOrWhiteSpace(OtherDetailed) => $"другое ({OtherDetailed})",
                    _ => "другое"
                });
            }

            return $"Уязвимости по здоровью: {sb}";
        }
        
    }
}