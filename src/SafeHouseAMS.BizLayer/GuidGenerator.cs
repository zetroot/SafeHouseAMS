using System;

namespace SafeHouseAMS.BizLayer
{
    internal class GuidGenerator : IGuidGenerator
    {
        public Guid Generate() => Guid.NewGuid();
    }
}