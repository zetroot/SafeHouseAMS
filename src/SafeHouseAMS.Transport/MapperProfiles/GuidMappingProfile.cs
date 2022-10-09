using System;
using AutoMapper;
using Google.Protobuf;
using SafeHouseAMS.Transport.Protos.Models.Common;


namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class GuidMappingProfile : Profile
    {
        public GuidMappingProfile()
        {
            CreateMap<Guid, UUID>(MemberList.None)
                .ConstructUsing(g => new UUID {Value = ByteString.CopyFrom(g.ToByteArray())});
            
            CreateMap<UUID, Guid>(MemberList.None)
                .ConstructUsing(g => new Guid(g.Value.ToByteArray()));
        }
    }
}
