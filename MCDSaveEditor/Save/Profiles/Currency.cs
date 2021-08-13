using MCDSaveEditor.Save.Mapping;
using PostSharp.Patterns.Model;
using System;
using System.Text.Json.Serialization;

namespace MCDSaveEditor.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public class Currency
    {
        [JsonPropertyName("count")]
        public ulong Count { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
