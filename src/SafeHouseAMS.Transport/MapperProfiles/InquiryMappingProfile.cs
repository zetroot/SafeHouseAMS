using System;
using System.Collections.Generic;
using AutoMapper;
using SafeHouseAMS.BizLayer.LifeSituations;
using SafeHouseAMS.BizLayer.LifeSituations.Documents;
using SafeHouseAMS.BizLayer.LifeSituations.InquirySources;
using SafeHouseAMS.BizLayer.LifeSituations.Vulnerabilities;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class InquiryMappingProfile : Profile
    {
        public InquiryMappingProfile()
        {
            MapInquirySources();
            MapVulnerabilityFactors();
            MapInquiries();
        }
        
        private void MapInquiries()
        {
            CreateMap<Inquiry, Protos.Models.LifeSituations.Inquiry>();
            CreateMap<Protos.Models.LifeSituations.Inquiry, Inquiry>();
        }
        
        private void MapVulnerabilityFactors()
        {
            CreateMap<IEnumerable<Vulnerability>, Protos.Models.LifeSituations.VulnerabilityFactors>()
                .ConstructUsing((src, _) =>
                {
                    var result = new Protos.Models.LifeSituations.VulnerabilityFactors();
                    foreach (var vulnerability in src)
                    {
                        switch (vulnerability)
                        {
                            case Addiction addiction:
                                result.Addiction = true;
                                result.AddictionKind = addiction.AddictionKind;
                                break;
                            case ChildhoodViolence:
                                result.ChildhoodViolence = true;
                                break;
                            case Homelessness:
                                result.Homelessness = true;
                                break;
                            case Migration:
                                result.Migration = true;
                                break;
                            case OrphanageExperience:
                                result.OrphanageExperience = true;
                                break;
                            case Other other:
                                result.Other = true;
                                result.OtherDetails = other.Details;
                                break;
                            case HealthStatus healthStatus:
                                result.HealthStatus = true;
                                result.HealthStatusKind = (int) healthStatus.Kind;
                                result.HealthStatusDetails = healthStatus.OtherDetailed;
                                break;
                        }
                    }    
                    return result;
                });
            
            CreateMap<Protos.Models.LifeSituations.VulnerabilityFactors, IEnumerable<Vulnerability>>()
                .ConstructUsing((src, _) =>
                {
                    var result = new List<Vulnerability>();
                    
                    if(src.Addiction) result.Add(new Addiction(src.AddictionKind));
                    if (src.ChildhoodViolence) result.Add(new ChildhoodViolence());
                    if (src.Homelessness) result.Add(new Homelessness());
                    if (src.Migration) result.Add(new Migration());
                    if (src.OrphanageExperience) result.Add(new OrphanageExperience());
                    if (src.Other) result.Add(new Other(src.OtherDetails));
                    if (src.HealthStatus) result.Add(new HealthStatus((HealthStatus.HealthStatusVulnerabilityType) (src.HealthStatusKind ?? 0), src.HealthStatusDetails));
                    
                    return result;
                });
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