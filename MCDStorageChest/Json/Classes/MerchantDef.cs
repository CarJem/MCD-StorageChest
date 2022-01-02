using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCDStorageChest.Json;


namespace MCDStorageChest.Json.Classes
{
    public class MerchantDef : DynamicJSON
    {
        [JsonPropertyName("everInteracted")]
        public bool EverInteracted { get; set; } = default;

        [JsonPropertyName("pricing")]
        public MerchantPricing Pricing { get; set; } = null;

        [JsonPropertyName("quests")]
        public Dictionary<string, MerchantQuest> Quests { get; set; } = null;

        [JsonPropertyName("slots")]
        public Dictionary<string, MerchantSlot> Slots { get; set; } = null;
    }

    public class MerchantPricing : DynamicJSON
    {
        [JsonPropertyName("timesRestocked")]
        public long TimesRestocked { get; set; } = default;

    }

    public class MerchantQuest : DynamicJSON
    {
        [JsonPropertyName("dynamicQuestState")]
        public object DynamicQuestState { get; set; } = null;

        [JsonPropertyName("questState")]
        public object QuestState { get; set; } = null;

    }

    public class MerchantSlot : DynamicJSON
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; } = null;

        [JsonPropertyName("priceMultiplier")]
        public double PriceMultiplier { get; set; } = default;

        [JsonPropertyName("rebateFraction")]
        public double RebateFraction { get; set; } = default;

        [JsonPropertyName("reserved")]
        public bool Reserved { get; set; } = default;

    }
}
