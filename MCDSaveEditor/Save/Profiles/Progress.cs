using MCDSaveEditor.Save.Enums;
using System.Text.Json.Serialization;

namespace MCDSaveEditor.Save.Profiles
{
    public partial class Progress
    {
        [JsonPropertyName("completedDifficulty")]
        public string CompletedDifficulty { get; set; }

        [JsonPropertyName("completedEndlessStruggle")]
        public int CompletedEndlessStruggle { get; set; }

        [JsonPropertyName("completedThreatLevel")]
        public string CompletedThreatLevel { get; set; }
    }
}
