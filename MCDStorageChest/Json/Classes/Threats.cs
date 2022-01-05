using Newtonsoft.Json;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class ThreatLevels : DynamicJSON
    {
        [JsonProperty(PropertyName = "unlocked")]
        public string Unlocked { get; set; } = null!;
    }
}
