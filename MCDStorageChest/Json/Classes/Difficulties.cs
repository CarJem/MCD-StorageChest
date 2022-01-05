using Newtonsoft.Json;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;
#nullable enable


namespace MCDStorageChest.Json.Classes
{
    public partial class Difficulties : DynamicJSON
    {
        [JsonProperty(PropertyName = "announced")]
        public string? Announced { get; set; } = default;

        [JsonProperty(PropertyName = "selected")]
        public string? Selected { get; set; } = default;

        [JsonProperty(PropertyName = "unlocked")]
        public string? Unlocked { get; set; } = default;
    }
}
