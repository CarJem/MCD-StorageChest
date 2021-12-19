using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class Difficulties : DynamicJSON
    {
        [JsonPropertyName("announced")]
        public string Announced { get; set; }

        [JsonPropertyName("selected")]
        public string Selected { get; set; }

        [JsonPropertyName("unlocked")]
        public string Unlocked { get; set; }
    }
}
