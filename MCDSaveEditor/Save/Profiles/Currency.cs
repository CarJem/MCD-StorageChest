using MCDStorageChest.Save.Mapping;
using PostSharp.Patterns.Model;
using System;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public class Currency : DynamicJSON
    {
        [JsonPropertyName("count")]
        public ulong Count { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
