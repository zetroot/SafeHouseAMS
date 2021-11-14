using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.Transport.MapperProfiles;

namespace SafeHouseAMS.Test.Transport.MapperProfiles;

public class AssistanceRequestsMappingTests
{
    private Mapper BuildMapper()
    {
        var cfg = new MapperConfiguration(c => c.AddMaps(typeof(AssistanceRequestsMappingProfile).Assembly));
        return new(cfg);
    }

    [Property]
    public void RoundTripMapping_ReturnsSameAssistanceAct()
    {
        Arb.Register<DateTimeGenerators>();
        Arb.Register<NotNullStringsGenerators>();

        var mapper = BuildMapper();

        Prop.ForAll<AssistanceAct>(src =>
        {
            var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.AssistanceAct>(src);
            var result = mapper.Map<AssistanceAct>(dto);

            result.Should()
                .BeEquivalentTo(src, opts => opts.IncludingProperties());
        }).QuickCheckThrowOnFailure();
    }

    [Property]
    public void RoundTripMapping_ReturnsSameAssistanceRequest()
    {
        Arb.Register<DateTimeGenerators>();
        Arb.Register<NotNullStringsGenerators>();
        Arb.Register<AssistanceActListGenerators>();

        var mapper = BuildMapper();

        Prop.ForAll<AssistanceRequest>(src =>
        {
            var dto = mapper.Map<SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.AssistanceRequest>(src);
            var result = mapper.Map<AssistanceRequest>(dto);

            result.Should()
                .BeEquivalentTo(src, opts => opts.IncludingProperties());
        }).QuickCheckThrowOnFailure();
    }

    [Property]
    public void CreateEpisode_OnRoundTripMapping_DoesNotChanges()
    {
        Arb.Register<NotNullStringsGenerators>();

        var mapper = BuildMapper();
        Prop.ForAll<CreateAssistanceRequest>(src =>
        {
            var dto =
                mapper.Map<SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.Commands.CreateAssistanceRequest>(src);
            var result = mapper.Map<CreateAssistanceRequest>(dto);

            result.Should().BeEquivalentTo(src);
        }).QuickCheckThrowOnFailure();
    }

    [Property]
    public void AttachAct_OnRoundTripMapping_DoesNotChanges()
    {
        Arb.Register<NotNullStringsGenerators>();

        var mapper = BuildMapper();
        Prop.ForAll<AttachAssistanceAct>(src =>
        {
            var dto =
                mapper.Map<SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.Commands.AttachAssistanceAct>(src);
            var result = mapper.Map<AttachAssistanceAct>(dto);

            result.Should().BeEquivalentTo(src);
        }).QuickCheckThrowOnFailure();
    }

    [Property]
    public void AssistanceRequestCommand_OnRoundTripMapping_DoesNotChanges()
    {
        Arb.Register<NotNullStringsGenerators>();

        var commandsArb = Gen
            .OneOf(Arb.From<CreateAssistanceRequest>().Generator.Select(x => x as AssistanceRequestCommand),
                Arb.From<AttachAssistanceAct>().Generator.Select(x => x as AssistanceRequestCommand))
            .ToArbitrary();

        var mapper = BuildMapper();
        Prop.ForAll(commandsArb, src =>
        {
            var dto =
                mapper.Map<SafeHouseAMS.Transport.Protos.Models.AssistanceRequests.Commands.AssistanceRequestCommand>(src);
            var result = mapper.Map<AssistanceRequestCommand>(dto);

            result.Should().BeOfType(src.GetType());
            result.Should().BeEquivalentTo(src, opt => opt.RespectingRuntimeTypes());
        }).QuickCheckThrowOnFailure();
    }
}

public class AssistanceActListGenerators
{
    public static Arbitrary<IReadOnlyCollection<AssistanceAct>> ActsList =>
        Arb.From(Arb.From<AssistanceAct>().Generator.ListOf().Select(x => x.ToList() as IReadOnlyCollection<AssistanceAct>));
}
