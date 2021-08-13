using System.Text.Json.Serialization;
using MCDSaveEditor.Save.Enums;

namespace MCDSaveEditor.Save.Profiles
{
    public partial class ThreatLevels
    {
        [JsonPropertyName("unlocked")]
        public string Unlocked { get; set; }
    }
}
