using AutoMapper;
using SafeHouseAMS.Backend.Server.IdentityProvider.Models;
using SafeHouseAMS.BizLayer.Users.Commands;

namespace SafeHouseAMS.Backend.Server.IdentityProvider
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUserCommand, CreateUserRequestIdentityProviderModel>();
        }
    }
}