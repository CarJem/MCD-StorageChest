using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class MapUiState : DynamicJSON
    {
        [JsonPropertyName("panPosition")]
        public Dictionary<string, string> PanPosition { get; set; } = null!;

        [JsonPropertyName("selectedDifficulty")]
        public string SelectedDifficulty { get; set; } = null!;

        [JsonPropertyName("selectedMission")]
        public string SelectedMission { get; set; } = null!;

        [JsonPropertyName("selectedRealm")]
        public string SelectedRealm { get; set; } = null!;

        [JsonPropertyName("selectedThreatLevel")]
        public string SelectedThreatLevel { get; set; } = null!;
    }
}
