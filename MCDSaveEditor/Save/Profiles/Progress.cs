using MCDStorageChest.Save.Enums;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class Progress : DynamicJSON
    {
        [JsonPropertyName("completedDifficulty")]
        public string CompletedDifficulty { get; set; }

        [JsonPropertyName("completedEndlessStruggle")]
        public int CompletedEndlessStruggle { get; set; }

        [JsonPropertyName("completedThreatLevel")]
        public string CompletedThreatLevel { get; set; }
    }
}
