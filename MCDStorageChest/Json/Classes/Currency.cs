using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using System;
using System.Text.Json.Serialization;
using MCDStorageChest.Json;

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public class Currency : DynamicJSON
    {
        [JsonPropertyName("count")]
        public ulong Count { get; set; } = default;
        [JsonPropertyName("type")]
        public string Type { get; set; } = default;
    }
}
