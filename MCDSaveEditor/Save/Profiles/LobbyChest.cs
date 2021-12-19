using System.Text.Json.Serialization;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class LobbychestProgress : DynamicJSON
    {
        [JsonPropertyName("unlockedTimes")]
        public long UnlockedTimes { get; set; }
    }
}
