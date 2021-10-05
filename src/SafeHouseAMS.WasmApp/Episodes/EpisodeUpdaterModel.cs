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

            ForcedLabour = origin.ContactReason.ForcedLabour is not null;
            ForcedLabourDescription = origin.ContactReason.ForcedLabour?.Details ?? string.Empty;
            foreach (var item in ForcedLabourType)
            {
                if (origin.ContactReason.ForcedLabour is null) break;
                if (origin.ContactReason.ForcedLabour.Type.HasFlag(item.Item))
                    item.Selected = true;
            }

            ForcedMarriage = origin.ContactReason.ForcedMarriage is not null;
            ForcedMarriageDescription = origin.ContactReason.ForcedMarriage?.Details ?? string.Empty;
            ForcedMarriageKind = origin.ContactReason.ForcedMarriage?.Type ?? default;

            Cre = origin.ContactReason.Cre is not null;
            CreDescription = origin.ContactReason.Cre?.Details ?? string.Empty;

            Begging = origin.ContactReason.Begging is not null;
            BeggingDescription = origin.ContactReason.Begging?.Details ?? string.Empty;

            ForcedCriminalActivity = origin.ContactReason.ForcedCriminalActivity is not null;
            CriminalActivityDescription = origin.ContactReason.ForcedCriminalActivity?.Details ?? string.Empty;
            foreach (var item in CriminalActivityKind)
            {
                if (origin.ContactReason.ForcedCriminalActivity is null) break;
                if (origin.ContactReason.ForcedCriminalActivity.Type.HasFlag(item.Item))
                    item.Selected = true;
            }

            OtherExploitationKind = origin.ContactReason.OtherExploitationKind is not null;
            OtherExploitationKindDescription = origin.ContactReason.OtherExploitationKind?.Details ?? string.Empty;

            SexualViolence = origin.ContactReason.SexualViolence is not null;
            SexualViolenceDescription = origin.ContactReason.SexualViolence?.Details ?? string.Empty;

            DomesticViolence = origin.ContactReason.DomesticViolence is not null;
            DomesticViolenceDescription = origin.ContactReason.DomesticViolence?.Details ?? string.Empty;

            OtherViolence = origin.ContactReason.DomesticViolence is not null;
            OtherViolenceDescription = origin.ContactReason.DomesticViolence?.Details ?? string.Empty;

            Place = origin.Place;
            InvolvementDescriptionText = origin.InvolvementDescription;
            WasJuvenile = origin.WasJuvenile;

            // TODO: add duration parsing

            foreach (var item in ControlMethodKinds)
            {
                if (origin.ControlMethods.Methods.HasFlag(item.Item))
                    item.Selected = true;
            }
            foreach (var item in DebtKinds)
            {
                if (origin.ControlMethods.DebtKind is null) break;
                if (origin.ControlMethods.DebtKind.Value.HasFlag(item.Item))
                    item.Selected = true;
            }
            OtherControlMethods = origin.ControlMethods.OtherDetails ?? string.Empty;
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
