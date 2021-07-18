namespace SafeHouseAMS.BizLayer.LifeSituations.InquirySources
{
    /// <summary>
    /// Перенаправлен другим человеком
    /// </summary>
    public class ForwardedByPerson : ForwardedInquiry
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="forwardedBy">перенаправивший человек</param>
        public ForwardedByPerson(string forwardedBy) : base(forwardedBy)
        {
        }
    }
}