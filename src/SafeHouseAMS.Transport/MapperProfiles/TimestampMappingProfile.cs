using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class TimestampMappingProfile : Profile
    {
        public TimestampMappingProfile()
        {
            CreateMap<DateTime, Timestamp>(MemberList.None)
                .ConstructUsing((d, _) =>
                    Timestamp.FromDateTime(d.Kind != DateTimeKind.Utc? d.ToUniversalTime() : d));

            CreateMap<Timestamp, DateTime>(MemberList.None)
                .ConstructUsing(d => d.ToDateTime().ToLocalTime());
        }
    }
}
