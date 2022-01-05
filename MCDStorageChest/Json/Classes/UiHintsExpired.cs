using MCDStorageChest.Json;
using Newtonsoft.Json;

namespace MCDStorageChest.Json.Classes
{
    public partial class UiHintsExpired : DynamicJSON
    {
        [JsonProperty(PropertyName = "hintType")]
        public string HintType { get; set; } = null!;
    }
}
