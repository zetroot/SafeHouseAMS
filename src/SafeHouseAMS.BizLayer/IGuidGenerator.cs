using System;

namespace SafeHouseAMS.BizLayer
{
    /// <summary>
    /// Guid generation wrapper. May be mocked in tests
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        Guid Generate();
    }
}