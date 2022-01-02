using System.Text.Json.Serialization;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class LobbychestProgress : DynamicJSON
    {
        [JsonPropertyName("unlockedTimes")]
        public long UnlockedTimes { get; set; }
    }
}
