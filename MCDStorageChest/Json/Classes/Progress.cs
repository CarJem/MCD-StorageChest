using MCDStorageChest.Json.Enums;
using Newtonsoft.Json;
using MCDStorageChest.Json;
#nullable enable

namespace MCDStorageChest.Json.Classes
{
    public partial class Progress : DynamicJSON
    {
        [JsonProperty(PropertyName = "completedDifficulty")]
        public string? CompletedDifficulty { get; set; } = null;

        [JsonProperty(PropertyName = "completedEndlessStruggle")]
        public int CompletedEndlessStruggle { get; set; } = default;

        [JsonProperty(PropertyName = "completedThreatLevel")]
        public string? CompletedThreatLevel { get; set; } = null;
    }
}
