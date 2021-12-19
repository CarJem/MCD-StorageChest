using MCDStorageChest.Save.Json;
using System.Text.Json.Serialization;

namespace MCDStorageChest.Save.Profiles
{
    public partial class UiHintsExpired : DynamicJSON
    {
        [JsonPropertyName("hintType")]
        public string HintType { get; set; }
    }
}
