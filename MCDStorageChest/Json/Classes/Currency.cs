using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using System;
using Newtonsoft.Json;
using MCDStorageChest.Json;
#nullable enable

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public class Currency : DynamicJSON
    {
        [JsonProperty(PropertyName = "count")]
        public ulong Count { get; set; } = default;
        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set; } = default;
    }
}
