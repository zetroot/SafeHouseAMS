using AutoMapper;
using SafeHouseAMS.BizLayer.Users.Commands;
using SafeHouseAMS.Transport.Protos.Models.Users;

namespace SafeHouseAMS.Transport.MapperProfiles
{
    internal class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserCommand, CreateUserRequest>();
        }
    }
}