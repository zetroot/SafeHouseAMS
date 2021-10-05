using System;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;
using SafeHouseAMS.BizLayer.ExploitationEpisodes.Commands;

namespace SafeHouseAMS.WasmApp.Episodes
{
    /// <summary>
    /// Модель формы редактирования существующего эпизода
    /// </summary>
    public class EpisodeUpdaterModel : EpisodeEditorBaseModel
    {
        private readonly Episode origin;

        public EpisodeUpdaterModel(Episode origin) : base()
        {
            this.origin = origin;
            Involvement = origin.ContactReason.Involvement is not null;
            InvolvementDescription = origin.ContactReason.Involvement?.Details ?? string.Empty;

            Cse = origin.ContactReason.Cse is not null;
            CseDescription = origin.ContactReason.Cse?.Details ?? string.Empty;
            foreach (var item in CseType)
            {
                if (origin.ContactReason.Cse is null) break;
                if (origin.ContactReason.Cse.Type.HasFlag(item.Item))
                    item.Selected = true;
            }

        }

        public UpdateEpisode BuildCommand()
        {
            var contactReason = BuildContactReason();
            var controlMethods = BuildControlMethods();
            var duration = BuildDuration();
            var escapeStatus = BuildEscapeStatus();
            return new(origin.ID, contactReason, Place, InvolvementDescriptionText, WasJuvenile, duration,
            controlMethods, escapeStatus);
        }
    }
}
