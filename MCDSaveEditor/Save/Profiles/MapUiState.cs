using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class MapUiState : DynamicJSON
    {
        [JsonPropertyName("panPosition")]
        public Dictionary<string, string> PanPosition { get; set; }

        [JsonPropertyName("selectedDifficulty")]
        public string SelectedDifficulty { get; set; }

        [JsonPropertyName("selectedMission")]
        public string SelectedMission { get; set; }

        [JsonPropertyName("selectedRealm")]
        public string SelectedRealm { get; set; }

        [JsonPropertyName("selectedThreatLevel")]
        public string SelectedThreatLevel { get; set; }
    }
}
