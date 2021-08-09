using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class LifeSituationDocumentsMappingProfile : Profile
    {
        public LifeSituationDocumentsMappingProfile()
        {
            CreateMap<LifeSituationDocument, SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument>()
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.LifeSituations.LifeSituationDocument();
                    switch (src)
                    {
                        case Inquiry inquiry:
                            result.Inquiry = ctx.Mapper.Map<Protos.Models.LifeSituations.Inquiry>(inquiry);
                            break;
                        case CitizenshipChange citizenshipChange:
                            result.CitizenshipChange =
                                ctx.Mapper.Map<Protos.Models.LifeSituations.CitizenshipChange>(citizenshipChange);
                            break;
                    }
                    return result;
                });

            CreateMap<SafeHouseAMS.Transport.Protos.Models.LifeSituations.LifeSituationDocument, LifeSituationDocument>()
                .ConstructUsing((src, ctx) => src.DocumentCase switch
                {
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.Inquiry =>
                        ctx.Mapper.Map<Inquiry>(src.Inquiry),
                    Protos.Models.LifeSituations.LifeSituationDocument.DocumentOneofCase.CitizenshipChange =>
                        ctx.Mapper.Map<CitizenshipChange>(src.CitizenshipChange),
                    _ => throw new InvalidOperationException()
                });
        }
    }
}
