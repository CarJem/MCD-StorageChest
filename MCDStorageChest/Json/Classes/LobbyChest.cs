using Newtonsoft.Json;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class LobbychestProgress : DynamicJSON
    {
        [JsonProperty(PropertyName = "unlockedTimes")]
        public long UnlockedTimes { get; set; }
    }
}
