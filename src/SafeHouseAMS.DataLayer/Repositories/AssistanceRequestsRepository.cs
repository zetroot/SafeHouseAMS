using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.BizLayer.AssistanceRequests;

namespace SafeHouseAMS.DataLayer.Repositories;

internal class AssistanceRequestsRepository : IAssistanceRequestsRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AssistanceRequestsRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AssistanceRequest?> GetSingle(Guid requestId)
    {
        var item = await _context.AssistanceRequests.SingleOrDefaultAsync(x => x.ID == requestId).ConfigureAwait(false);
        return _mapper.Map<AssistanceRequest>(item);
    }

    public async Task CreateAssistanceRequest(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
        Guid survivorId, AssistanceKind assistanceKind, string details, bool isAccomplished)
    {
        await _context.AssistanceRequests.AddAsync(new()
        {
            ID = id, Created = created, LastEdit = lastEdit, IsDeleted = isDeleted,
            SurvivorID = survivorId, Details = details, AssistanceKind = (int)assistanceKind,
            IsAccomplished = isAccomplished
        }).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task CreateAssistanceAct(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
        Guid requestId, string details, decimal workHours, decimal money)
    {
        await _context.AssistanceActs.AddAsync(new()
        {
            ID = id, IsDeleted = isDeleted, Created = created, LastEdit = lastEdit,
            RequestID = requestId, Details = details, WorkHours = workHours, Money = money
        }).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async IAsyncEnumerable<AssistanceRequest> GetAllBySurvivor(Guid survivorId,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var items = _context.AssistanceRequests
            .Include(x => x.Survivor)
            .Include(x => x.AssistanceActs)
            .Where(x => !x.IsDeleted && x.SurvivorID == survivorId)
            .OrderByDescending(x => x.Created)
            .AsAsyncEnumerable();

        await foreach (var item in items.WithCancellation(cancellationToken))
            yield return _mapper.Map<AssistanceRequest>(item);
    }
}
