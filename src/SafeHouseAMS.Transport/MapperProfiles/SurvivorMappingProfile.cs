using AutoMapper;
using SafeHouseAMS.BizLayer.Survivors;
using SafeHouseAMS.BizLayer.Survivors.Commands;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class SurvivorMappingProfile : Profile
    {
        public SurvivorMappingProfile()
        {
            MapSurvivors();
            MapCreateSurvivorCommand();
        }
        private void MapCreateSurvivorCommand()
        {
            CreateMap<CreateSurvivor, Protos.Models.Survivors.CreateSurvivor>();
            CreateMap<Protos.Models.Survivors.CreateSurvivor, CreateSurvivor>();
            // .ConstructUsing((src, ctx) =>
            // {
            //     var id = ctx.Mapper.Map<Guid>(src.ID);
            //     var dobAcc = ctx.Mapper.Map<DateTime?>(src.BirthDateAccurate);
            //     var dobCalc = ctx.Mapper.Map<DateTime?>(src.BirthDateCalculated);
            //     return new CreateSurvivor(id, src.Name, (SexEnum)src.Sex, src.OtherSex, dobAcc, dobCalc);
            // });
        }

        private void MapSurvivors()
        {
            CreateMap<Survivor, Protos.Models.Survivors.Survivor>();
            CreateMap<Protos.Models.Survivors.Survivor, Survivor>();
        }
    }
}