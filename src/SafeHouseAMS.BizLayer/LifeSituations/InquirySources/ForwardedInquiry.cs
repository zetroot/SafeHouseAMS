using System;

namespace SafeHouseAMS.BizLayer.LifeSituations.InquirySources
{
    /// <summary>
    /// Общий класс перенаправленного обращения
    /// </summary>
    public abstract class ForwardedInquiry : IInquirySource
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="forwardedBy">кем перенаправлен - название организации или человек</param>
        protected ForwardedInquiry(string forwardedBy)
        {
            ForwardedBy = forwardedBy ?? throw new ArgumentNullException(nameof(forwardedBy));
        }

        /// <summary>
        /// Источник перенаправления - организация, человек
        /// </summary>
        public string ForwardedBy { get; }
    }

}