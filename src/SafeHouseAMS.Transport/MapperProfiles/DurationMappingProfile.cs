using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class DurationMappingProfile : Profile
    {
        public DurationMappingProfile()
        {
            CreateMap<TimeSpan, Duration>()
                .ConstructUsing(src => src.ToDuration());

            CreateMap<Duration, TimeSpan>()
                .ConstructUsing(src => src.ToTimeSpan());
        }
    }
}
