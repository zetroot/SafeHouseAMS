using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities
{
    /// <summary>
    /// Уязвимость по состоянию здоровья
    /// </summary>
    public class HealthStatus : Vulnerability
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
    }
}