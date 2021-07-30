using System;
using AutoMapper;
using Google.Protobuf;
using SafeHouseAMS.Backend.Protos.Models.Common;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class GuidMappingProfile : Profile
    {
        public GuidMappingProfile()
        {
            CreateMap<Guid, UUID>()
                .ConstructUsing(g => new UUID {Value = ByteString.CopyFrom(g.ToByteArray())});
            
            CreateMap<UUID, Guid>()
                .ConstructUsing(g => new Guid(g.Value.ToByteArray()));
        }
    }
}