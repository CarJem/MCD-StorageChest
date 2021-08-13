using System.Text.Json.Serialization;

namespace MCDSaveEditor.Save.Profiles
{
    public partial class UiHintsExpired
    {
        [JsonPropertyName("hintType")]
        public string HintType { get; set; }
    }
}
