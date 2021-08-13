using System.Text.Json.Serialization;
using MCDSaveEditor.Save.Enums;

namespace MCDSaveEditor.Save.Profiles
{
    public partial class Cosmetic
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
