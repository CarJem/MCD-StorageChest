using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public partial class Cosmetic : DynamicJSON
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
