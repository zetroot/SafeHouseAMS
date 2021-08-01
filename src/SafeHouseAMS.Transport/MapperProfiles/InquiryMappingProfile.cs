using System;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class InquiryMappingProfile : Profile
    {
        public InquiryMappingProfile()
        {
            MapInquirySources();
        }
        private void MapInquirySources()
        {
            CreateMap<SelfInquiry, Protos.Models.LifeSituations.SelfInquiry>();
            CreateMap<Protos.Models.LifeSituations.SelfInquiry, SelfInquiry>();
            
            CreateMap<ForwardedBySurvivor, Protos.Models.LifeSituations.ForwardedBySurvivor>();
            CreateMap<Protos.Models.LifeSituations.ForwardedBySurvivor, ForwardedBySurvivor>();
            
            CreateMap<ForwardedByPerson, Protos.Models.LifeSituations.ForwardedByPerson>();
            CreateMap<Protos.Models.LifeSituations.ForwardedByPerson, ForwardedByPerson>();
            
            CreateMap<ForwardedByOrganization, Protos.Models.LifeSituations.ForwardedByOrganization>();
            CreateMap<Protos.Models.LifeSituations.ForwardedByOrganization, ForwardedByOrganization>();

            CreateMap<IInquirySource, Protos.Models.LifeSituations.InquirySource>()
                .ConstructUsing((src, ctx) =>
                {
                    var result = new Protos.Models.LifeSituations.InquirySource();
                    switch (src)
                    {
                        case SelfInquiry selfInquiry:
                            result.SelfInquiry = ctx.Mapper.Map<Protos.Models.LifeSituations.SelfInquiry>(selfInquiry);
                            break;
                        case ForwardedBySurvivor forwardedBySurvivor:
                            result.ForwardedBySurvivor = ctx.Mapper.Map<Protos.Models.LifeSituations.ForwardedBySurvivor>(forwardedBySurvivor);
                            break;
                        case ForwardedByPerson forwardedByPerson:
                            result.ForwardedByPerson = ctx.Mapper.Map<Protos.Models.LifeSituations.ForwardedByPerson>(forwardedByPerson);
                            break;
                        case ForwardedByOrganization forwardedByOrganization:
                            result.ForwardedByOrganization = ctx.Mapper.Map<Protos.Models.LifeSituations.ForwardedByOrganization>(forwardedByOrganization);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                    return result;
                });
            
            CreateMap<Protos.Models.LifeSituations.InquirySource, IInquirySource>()
                .ConstructUsing((src, ctx) => src.KindCase switch
                {
                    Protos.Models.LifeSituations.InquirySource.KindOneofCase.SelfInquiry => ctx.Mapper.Map<SelfInquiry>(src.SelfInquiry),
                    Protos.Models.LifeSituations.InquirySource.KindOneofCase.ForwardedBySurvivor => ctx.Mapper.Map<ForwardedBySurvivor>(src.ForwardedBySurvivor),
                    Protos.Models.LifeSituations.InquirySource.KindOneofCase.ForwardedByPerson => ctx.Mapper.Map<ForwardedByPerson>(src.ForwardedByPerson),
                    Protos.Models.LifeSituations.InquirySource.KindOneofCase.ForwardedByOrganization => ctx.Mapper.Map<ForwardedByOrganization>(src.ForwardedByOrganization),
                    _ => throw new InvalidOperationException()
                });
        }
    }
}