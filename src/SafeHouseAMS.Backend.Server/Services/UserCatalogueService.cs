using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.BizLayer.Users;
using SafeHouseAMS.BizLayer.Users.Commands;
using SafeHouseAMS.Transport.Protos.Services;
using SafeHouseAMS.Transport.Protos.Models.Users;

namespace SafeHouseAMS.Backend.Server.Services
{
    [ExcludeFromCodeCoverage, Authorize]
    internal class UserCatalogueService : UserCatalogue.UserCatalogueBase
    {
        private readonly IUserCatalogue _userCatalogue;
        private readonly IGuidGenerator _guidGenerator;

        public UserCatalogueService(IUserCatalogue userCatalogue, IGuidGenerator guidGenerator)
        {
            _userCatalogue = userCatalogue;
            _guidGenerator = guidGenerator;
        }

        public override Task<Empty> Create(CreateUserRequest request, ServerCallContext context)
        {
            var command = new CreateUserCommand(_guidGenerator.Generate(), request.Email, request.FirstName, request.LastName);
            _userCatalogue.CreateAsync(command, context.CancellationToken);
            return Task.FromResult(new Empty());
        }
    }
}