using System.Text.Json.Serialization;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class ThreatLevels : DynamicJSON
    {
        [JsonPropertyName("unlocked")]
        public string Unlocked { get; set; } = null!;
    }
}
