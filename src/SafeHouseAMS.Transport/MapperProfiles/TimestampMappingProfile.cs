using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class TimestampMappingProfile : Profile
    {
        public TimestampMappingProfile()
        {
            CreateMap<DateTime, Timestamp>()
                .ConstructUsing((d, _) =>
                    Timestamp.FromDateTime(d.Kind != DateTimeKind.Utc? d.ToUniversalTime() : d));

            CreateMap<Timestamp, DateTime>()
                .ConstructUsing(d => d.ToDateTime().ToLocalTime());
        }
    }
}
