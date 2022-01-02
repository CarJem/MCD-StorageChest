using MCDStorageChest.Json.Enums;
using System.Text.Json.Serialization;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class Progress : DynamicJSON
    {
        [JsonPropertyName("completedDifficulty")]
        public string CompletedDifficulty { get; set; } = null;

        [JsonPropertyName("completedEndlessStruggle")]
        public int CompletedEndlessStruggle { get; set; } = default;

        [JsonPropertyName("completedThreatLevel")]
        public string CompletedThreatLevel { get; set; } = null;
    }
}
