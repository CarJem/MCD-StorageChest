using System.Text.Json.Serialization;

namespace MCDSaveEditor.Save.Profiles
{
    public partial class LobbychestProgress
    {
        [JsonPropertyName("unlockedTimes")]
        public long UnlockedTimes { get; set; }
    }
}
