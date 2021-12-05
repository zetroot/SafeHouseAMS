using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using SafeHouseAMS.BizLayer.AssistanceRequests;
using SafeHouseAMS.BizLayer.AssistanceRequests.Commands;
using SafeHouseAMS.DataLayer.Repositories;
using Xunit;
using Xunit.Categories;

namespace SafeHouseAMS.Test.BizLayer.AssistanceRequests;

public class EndToEndTests
{
    [Fact, IntegrationTest]
    public async Task CreateCommand_Always_CreatesNewRequest()
    {
        //arrange
        var surId = Guid.NewGuid();
        var documentDate = new DateTime(2020, 02, 02, 02, 02, 02);
        await using var context = TestHelper.CreateInMemoryDatabase();
        await context.Survivors.AddAsync(new() { ID = surId, Num = 42}).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var sut = new CreateAssistanceRequest(Guid.NewGuid(), surId, AssistanceKind.Accomodation, "request details", documentDate);

        var repo = new AssistanceRequestsRepository(context, TestHelper.CreateMapper());
        var aggregate = new AssistanceRequestAggregate(repo);

        //act
        await aggregate.ApplyCommand(sut, CancellationToken.None).ConfigureAwait(false);
        var request = await aggregate.GetSingleAsync(sut.EntityID, CancellationToken.None);

        //assert
        request.Should().NotBeNull();

        Debug.Assert(request != null, nameof(request) + " != null");
        request.ID.Should().Be(sut.EntityID);
        request.Survivor.ID.Should().Be(surId);
        request.IsAccomplished.Should().BeFalse();
        request.IsDeleted.Should().BeFalse();
        request.AssistanceActs.Should().BeEmpty();
        request.AssistanceKind.Should().Be(AssistanceKind.Accomodation);
        request.DocumentDate.Should().Be(documentDate);
    }

    [Fact, IntegrationTest]
    public async Task AttachAssistanceAct_Always_AddsAssistanceActToRequest()
    {
        //arrange
        var surId = Guid.NewGuid();
        var reqId = Guid.NewGuid();
        var documentDate = new DateTime(2020, 02, 02, 02, 02, 02);
        await using var context = TestHelper.CreateInMemoryDatabase();
        await context.Survivors.AddAsync(new() { ID = surId, Num = 42}).ConfigureAwait(false);
        await context.AssistanceRequests
            .AddAsync(new()
            {
                ID = reqId,
                SurvivorID = surId,
                AssistanceKind = (int)AssistanceKind.Accomodation,
                DocumentDate = DateTime.Now
            })
            .ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        const decimal money = 2;
        const decimal workHours = 1;
        const string actDetails = "act details";
        var sut = new AttachAssistanceAct(reqId, Guid.NewGuid(), actDetails, workHours, money, documentDate);

        var repo = new AssistanceRequestsRepository(context, TestHelper.CreateMapper());
        var aggregate = new AssistanceRequestAggregate(repo);

        //act
        await aggregate.ApplyCommand(sut, CancellationToken.None).ConfigureAwait(false);
        var request = await aggregate.GetSingleAsync(sut.EntityID, CancellationToken.None);

        //assert
        Debug.Assert(request != null, nameof(request) + " != null");
        request.AssistanceActs.Should().SatisfyRespectively(x =>
        {
            x.ID.Should().Be(sut.ActID);
            x.Details.Should().Be(actDetails);
            x.Money.Should().Be(money);
            x.WorkHours.Should().Be(workHours);
            x.DocumentDate.Should().Be(documentDate);
        });
    }
}
