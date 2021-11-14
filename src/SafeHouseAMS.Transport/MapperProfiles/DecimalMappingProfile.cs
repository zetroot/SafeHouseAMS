using System.Linq;
using AutoMapper;
using SafeHouseAMS.Transport.Protos.Models.Common;

namespace SafeHouseAMS.Transport.MapperProfiles;

internal class DecimalMappingProfile : Profile
{
    public DecimalMappingProfile()
    {
        CreateMap<decimal, Decimal>()
            .ConstructUsing(src => new Decimal{Bits = { decimal.GetBits(src) }});

        CreateMap<Decimal, decimal>()
            .ConstructUsing(src => new decimal(src.Bits.ToArray()));
    }
}
