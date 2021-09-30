using System;
using System.Collections.Generic;
using System.Linq;
using EnumDesriptor;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;

namespace SafeHouseAMS.WasmApp.RecordEditors
{
    public class EpisodeEditorModel
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

        public EpisodeEditorModel()
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
        }
    }

}
