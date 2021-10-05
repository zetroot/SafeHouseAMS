using System;
using SafeHouseAMS.BizLayer.ExploitationEpisodes.Commands;

namespace SafeHouseAMS.WasmApp.Episodes
{
    /// <summary>
    /// Модель формы создания нового эпизода
    /// </summary>
    public class EpisodeCreaterModel : EpisodeEditorBaseModel
    {
        public CreateEpisode BuildCommand(Guid survivorId)
        {
            var contactReason = BuildContactReason();
            var controlMethods = BuildControlMethods();
            var duration = BuildDuration();
            var escapeStatus = BuildEscapeStatus();
            return new(Guid.NewGuid(), survivorId, contactReason, Place, InvolvementDescriptionText, WasJuvenile,
            duration, controlMethods, escapeStatus);
        }
    }
}