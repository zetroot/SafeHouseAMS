using System.Linq;
using AutoMapper;
using SafeHouseAMS.Transport.Protos.Models.Common;

namespace SafeHouseAMS.Transport.MapperProfiles;

internal class DecimalMappingProfile : Profile
{
    public DecimalMappingProfile()
    {
        CreateMap<decimal, Decimal>(MemberList.None)
            .ConstructUsing(src => new Decimal{Bits = { decimal.GetBits(src) }});

        CreateMap<Decimal, decimal>(MemberList.None)
            .ConstructUsing(src => new decimal(src.Bits.ToArray()));
    }
}
