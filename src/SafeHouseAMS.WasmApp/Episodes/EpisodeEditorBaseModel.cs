using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;

namespace SafeHouseAMS.WasmApp.Episodes
{
    public abstract class EpisodeEditorBaseModel
    {
        private int _durationLength = 0;

        #region contact reason description
        public bool Involvement { get; set; }
        public string InvolvementDescription { get; set; } = string.Empty;

        public bool Cse { get; set; }
        public IReadOnlyList<EnumDetails<CseType>> CseType { get; }
        public string CseDescription { get; set; } = string.Empty;

        public bool ForcedLabour { get; set; }
        public IReadOnlyList<EnumDetails<ForcedLabourType>> ForcedLabourType { get; }
        public string ForcedLabourDescription { get; set; } = string.Empty;

        public bool ForcedMarriage { get; set; }
        public ForcedMarriageKind ForcedMarriageKind { get; set; }
        public IReadOnlyList<EnumDetails<ForcedMarriageKind>> ForcedMarriageKinds { get; }
        public string ForcedMarriageDescription { get; set; } = string.Empty;

        public bool Cre { get; set; }
        public string CreDescription { get; set; } = string.Empty;

        public bool Begging { get; set; }
        public string BeggingDescription { get; set; } = string.Empty;

        public bool ForcedCriminalActivity { get; set; }
        public IReadOnlyList<EnumDetails<CriminalActivityType>> CriminalActivityKind { get; }
        public string CriminalActivityDescription { get; set; } = string.Empty;

        public bool OtherExploitationKind { get; set; }
        public string OtherExploitationKindDescription { get; set; } = string.Empty;

        public bool SexualViolence { get; set; }
        public string SexualViolenceDescription { get; set; } = string.Empty;
        public bool DomesticViolence { get; set; }
        public string DomesticViolenceDescription { get; set; } = string.Empty;
        public bool OtherViolence { get; set; }
        public string OtherViolenceDescription { get; set; } = string.Empty;
        #endregion

        public string Place { get; set; } = string.Empty;

        public string InvolvementDescriptionText { get; set; } = string.Empty;

        public bool WasJuvenile { get; set; } = false;

        #region duration eeditor
        public int DurationLength
        {
            get => _durationLength;
            set
            {
                if(value > 0)
                    _durationLength = value;
            }
        }
        public enum DurationIntervalKind { Day, Month, Year }
        public DurationIntervalKind DurationKind { get; set; }
        #endregion

        #region control methods

        public IReadOnlyList<EnumDetails<ControlMethodKind>> ControlMethodKinds { get; }
        public IReadOnlyList<EnumDetails<DebtKind>> DebtKinds { get; }
        public string OtherControlMethods { get; set; } = string.Empty;
        #endregion

        public IReadOnlyList<EnumDetails<EscapeStatus>> EscapeStatus { get; }

        protected EpisodeEditorBaseModel()
        {
            CseType = Enum.GetValues<CseType>()
                .Where(x => x != BizLayer.ExploitationEpisodes.CseType.None)
                .Select(x => new EnumDetails<CseType>(x)).ToList();
            ForcedLabourType = Enum.GetValues<ForcedLabourType>()
                .Where(x => x != BizLayer.ExploitationEpisodes.ForcedLabourType.None)
                .Select(x => new EnumDetails<ForcedLabourType>(x)).ToList();
            ForcedMarriageKinds = Enum.GetValues<ForcedMarriageKind>()
                .Select(x => new EnumDetails<ForcedMarriageKind>(x)).ToList();
            CriminalActivityKind = Enum.GetValues<CriminalActivityType>()
                .Where(x => x != CriminalActivityType.None)
                .Select(x => new EnumDetails<CriminalActivityType>(x)).ToList();

            ControlMethodKinds = Enum.GetValues<ControlMethodKind>()
                .Where(x => x != ControlMethodKind.None)
                .Select(x => new EnumDetails<ControlMethodKind>(x)).ToList();
            DebtKinds = Enum.GetValues<DebtKind>()
                .Where(x => x != DebtKind.None)
                .Select(x => new EnumDetails<DebtKind>(x)).ToList();

            EscapeStatus = Enum.GetValues<EscapeStatus>()
                .Where(x => x != BizLayer.ExploitationEpisodes.EscapeStatus.None)
                .Select(x => new EnumDetails<EscapeStatus>(x)).ToList();
        }

        protected EscapeStatus BuildEscapeStatus() =>
            EscapeStatus
                .Where(x => x.Selected)
                .Select(x => x.Item)
                .Aggregate(BizLayer.ExploitationEpisodes.EscapeStatus.None, (acc, item) => acc | item);

        protected TimeSpan BuildDuration() =>
            DurationKind switch
            {
                DurationIntervalKind.Day => TimeSpan.FromDays(DurationLength),
                DurationIntervalKind.Month => TimeSpan.FromDays(DurationLength * 31),
                DurationIntervalKind.Year => TimeSpan.FromDays(DurationLength * 366),
                _ => throw new InvalidOperationException()
            };

        protected ControlMethods BuildControlMethods()
        {
            var controlMethods = ControlMethodKinds
                .Where(x => x.Selected)
                .Select(x => x.Item)
                .Aggregate(ControlMethodKind.None, (acc, item) => acc | item);
            var debtKind = DebtKinds
                .Where(x => x.Selected)
                .Select(x => x.Item)
                .Aggregate(DebtKind.None, (acc, item) => acc | item,
                    r => controlMethods.HasFlag(ControlMethodKind.Debt) ? r : null as DebtKind?);
            var otherControlKindDescr = controlMethods.HasFlag(ControlMethodKind.Other) ? OtherControlMethods : null;

            return new(controlMethods, debtKind, otherControlKindDescr);
        }

        protected ContactReason BuildContactReason()
        {
            var involvement = Involvement ? new DetailedContactReason(InvolvementDescription) : null;
            DetailedContactReason<CseType>? cse = null;
            if (Cse)
            {
                var cseType = CseType
                    .Where(x => x.Selected)
                    .Select(x => x.Item)
                    .Aggregate(BizLayer.ExploitationEpisodes.CseType.None, (acc, item) => acc | item);
                cse = new DetailedContactReason<CseType>(CseDescription, cseType);
            }

            DetailedContactReason<ForcedLabourType>? forcedLabour = null;
            if (ForcedLabour)
            {
                var labourType = ForcedLabourType
                    .Where(x => x.Selected)
                    .Select(x => x.Item)
                    .Aggregate(BizLayer.ExploitationEpisodes.ForcedLabourType.None, (acc, item) => acc | item);
                forcedLabour = new DetailedContactReason<ForcedLabourType>(ForcedLabourDescription, labourType);
            }

            var forcedMarriage = ForcedMarriage ?
                new DetailedContactReason<ForcedMarriageKind>(ForcedMarriageDescription, ForcedMarriageKind) : null;
            var cre = Cre ? new DetailedContactReason(CreDescription) : null;
            var begging = Begging ? new DetailedContactReason(BeggingDescription) : null;

            DetailedContactReason<CriminalActivityType>? forcedCriminalActivity = null;
            if (ForcedCriminalActivity)
            {
                var activityKind = CriminalActivityKind
                    .Where(x => x.Selected)
                    .Select(x => x.Item)
                    .Aggregate(BizLayer.ExploitationEpisodes.CriminalActivityType.None, (acc, item) => acc | item);
                forcedCriminalActivity =
                    new DetailedContactReason<CriminalActivityType>(CriminalActivityDescription, activityKind);
            }

            var otherExploitation = OtherExploitationKind
                ? new DetailedContactReason(OtherExploitationKindDescription)
                : null;
            var sexualViolence = SexualViolence ? new DetailedContactReason(SexualViolenceDescription) : null;
            var domesticViolence = DomesticViolence ? new DetailedContactReason(DomesticViolenceDescription) : null;
            var otherViolence = OtherViolence ? new DetailedContactReason(OtherViolenceDescription) : null;

            return new(involvement, cse, forcedLabour, forcedMarriage, cre, begging, forcedCriminalActivity,
            otherExploitation, sexualViolence, domesticViolence, otherViolence);
        }
    }

}
