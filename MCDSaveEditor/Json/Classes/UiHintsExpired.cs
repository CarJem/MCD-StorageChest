using MCDStorageChest.Json;
using System.Text.Json.Serialization;

namespace MCDStorageChest.Json.Classes
{
    public partial class UiHintsExpired : DynamicJSON
    {
        [JsonPropertyName("hintType")]
        public string HintType { get; set; } = null!;
    }
}
