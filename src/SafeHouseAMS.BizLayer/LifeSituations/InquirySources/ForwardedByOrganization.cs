namespace SafeHouseAMS.BizLayer.LifeSituations.InquirySources
{
    /// <summary>
    /// Перенаправлен другой организацией
    /// </summary>
    public class ForwardedByOrganization : ForwardedInquiry
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="forwardedBy">организация, перенаправившая человека</param>
        public ForwardedByOrganization(string forwardedBy) : base(forwardedBy)
        {
        }
    }
}