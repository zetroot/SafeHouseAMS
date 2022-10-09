using System;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class DurationMappingProfile : Profile
    {
        public DurationMappingProfile()
        {
            CreateMap<TimeSpan, Duration>(MemberList.None)
                .ConvertUsing(src => src.ToDuration());

            CreateMap<Duration, TimeSpan>(MemberList.None)
                .ConvertUsing(src => src.ToTimeSpan());
        }
    }
}
