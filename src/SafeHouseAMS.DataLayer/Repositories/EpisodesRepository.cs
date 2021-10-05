using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.DataLayer.Models.ExploitationEpisodes;

namespace SafeHouseAMS.DataLayer.Repositories
{
    internal class EpisodesRepository : IEpisodesRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public EpisodesRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task Create(Guid id, bool isDeleted, DateTime created, DateTime lastEdit,
            Guid survivorId, ContactReason contactReason, string place, string involvement, bool wasJuvenile, TimeSpan duration, ControlMethods controlMethods, EscapeStatus escapeStatus)
        {
            var addingEpisode = new EpisodeDAL
            {
                ID = id, IsDeleted = isDeleted, Created = created, LastEdit = lastEdit,
                SurvivorID = survivorId,
                Place = place, InvolvementDescription = involvement, WasJuvenile = wasJuvenile, Duration = duration,
                EscapeStatus = (int)escapeStatus
            };
            addingEpisode.UpdateContactReason(contactReason);
            addingEpisode.UpdateControlMethods(controlMethods);
            _dataContext.Episodes.Add(addingEpisode);
            await _dataContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Guid entityID, DateTime editTimestamp,
            ContactReason contactReason, string place, string involvement, bool wasJuvenile, TimeSpan duration, ControlMethods controlMethods, EscapeStatus escapeStatus)
        {
            var episode = await _dataContext.Episodes.SingleAsync(x => x.ID == entityID).ConfigureAwait(false);

            episode.LastEdit = editTimestamp;
            episode.Place = place;
            episode.InvolvementDescription = involvement;
            episode.WasJuvenile = wasJuvenile;
            episode.Duration = duration;
            episode.EscapeStatus = (int)escapeStatus;
            episode.UpdateContactReason(contactReason);
            episode.UpdateControlMethods(controlMethods);

            await _dataContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Episode?> GetSingleAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await _dataContext.Episodes
                .Include(x => x.Survivor)
                .SingleOrDefaultAsync(x => x.ID == id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<Episode>(item);
        }

        public async IAsyncEnumerable<Episode> GetAllBySurvivor(Guid survivorId, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var episodes =_dataContext.Episodes.Include(x => x.Survivor).Where(x => x.SurvivorID == survivorId && !x.IsDeleted)
                .AsAsyncEnumerable();

            await foreach (var item in episodes.WithCancellation(cancellationToken))
                yield return _mapper.Map<Episode>(item);
        }
    }
}
