using System.Collections.Generic;
using Newtonsoft.Json;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class MapUiState : DynamicJSON
    {
        [JsonProperty(PropertyName = "panPosition")]
        public Dictionary<string, string> PanPosition { get; set; } = null!;

        [JsonProperty(PropertyName = "selectedDifficulty")]
        public string SelectedDifficulty { get; set; } = null!;

        [JsonProperty(PropertyName = "selectedMission")]
        public string SelectedMission { get; set; } = null!;

        [JsonProperty(PropertyName = "selectedRealm")]
        public string SelectedRealm { get; set; } = null!;

        [JsonProperty(PropertyName = "selectedThreatLevel")]
        public string SelectedThreatLevel { get; set; } = null!;
    }
}
