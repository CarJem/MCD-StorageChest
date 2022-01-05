using Newtonsoft.Json;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;
#pragma warning disable CS8618

namespace MCDStorageChest.Json.Classes
{
    public partial class Cosmetic : DynamicJSON
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
