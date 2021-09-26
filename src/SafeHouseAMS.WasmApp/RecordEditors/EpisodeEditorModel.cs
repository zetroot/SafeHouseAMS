using System;
using System.Collections.Generic;
using System.Linq;
using SafeHouseAMS.BizLayer;
using SafeHouseAMS.BizLayer.ExploitationEpisodes;

namespace SafeHouseAMS.WasmApp.RecordEditors
{
    public class EpisodeEditorModel
    {
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

        public EpisodeEditorModel()
        {
            CseType = Enum.GetValues<CseType>()
                .Where(x => x != BizLayer.ExploitationEpisodes.CseType.None)
                .Select(x => new EnumDetails<CseType>(x, x.GetDescription())).ToList();
            ForcedLabourType = Enum.GetValues<ForcedLabourType>()
                .Where(x => x != BizLayer.ExploitationEpisodes.ForcedLabourType.None)
                .Select(x => new EnumDetails<ForcedLabourType>(x, x.GetDescription())).ToList();
            ForcedMarriageKinds = Enum.GetValues<ForcedMarriageKind>()
                .Select(x => new EnumDetails<ForcedMarriageKind>(x, x.GetDescription())).ToList();
            CriminalActivityKind = Enum.GetValues<CriminalActivityType>()
                .Where(x => x != CriminalActivityType.None)
                .Select(x => new EnumDetails<CriminalActivityType>(x, x.GetDescription())).ToList();
        }
    }

}
