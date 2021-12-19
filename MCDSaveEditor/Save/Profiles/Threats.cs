using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class ThreatLevels : DynamicJSON
    {
        [JsonPropertyName("unlocked")]
        public string Unlocked { get; set; }
    }
}
