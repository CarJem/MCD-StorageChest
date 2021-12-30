using System.Text.Json.Serialization;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;
#pragma warning disable CS8618

namespace MCDStorageChest.Json.Classes
{
    public partial class Cosmetic : DynamicJSON
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
