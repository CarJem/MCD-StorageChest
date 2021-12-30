using System.Text.Json.Serialization;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;


namespace MCDStorageChest.Json.Classes
{
    public partial class Difficulties : DynamicJSON
    {
        [JsonPropertyName("announced")]
        public string Announced { get; set; } = default;

        [JsonPropertyName("selected")]
        public string Selected { get; set; } = default;

        [JsonPropertyName("unlocked")]
        public string Unlocked { get; set; } = default;
    }
}
