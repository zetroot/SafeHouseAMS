using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
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
        await using var context = TestHelper.CreateInMemoryDatabase();
        await context.Survivors.AddAsync(new() { ID = surId, Num = 42}).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        var sut = new Create(Guid.NewGuid(), surId, AssistanceKind.Accomodation, "request details");

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
    }
}
