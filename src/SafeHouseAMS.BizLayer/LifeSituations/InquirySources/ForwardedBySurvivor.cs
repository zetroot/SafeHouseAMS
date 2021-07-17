namespace SafeHouseAMS.BizLayer.LifeSituations.InquirySources
{
    /// <summary>
    /// Перенаправлен другим пострадавшим
    /// </summary>
    public class ForwardedBySurvivor : ForwardedInquiry
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="forwardedBy">Перенаправивший пострадавший</param>
        public ForwardedBySurvivor(string forwardedBy) : base(forwardedBy)
        {
        }
    }
}