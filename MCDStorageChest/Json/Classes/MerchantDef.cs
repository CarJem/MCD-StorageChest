using System.Collections.Generic;
using Newtonsoft.Json;
using MCDStorageChest.Json;
#nullable enable


namespace MCDStorageChest.Json.Classes
{
    public class MerchantDef : DynamicJSON
    {
        [JsonProperty(PropertyName = "everInteracted")]
        public bool EverInteracted { get; set; } = default;

        [JsonProperty(PropertyName = "pricing")]
        public MerchantPricing? Pricing { get; set; } = null;

        [JsonProperty(PropertyName = "quests")]
        public Dictionary<string, MerchantQuest>? Quests { get; set; } = null;

        [JsonProperty(PropertyName = "slots")]
        public Dictionary<string, MerchantSlot>? Slots { get; set; } = null;
    }

    public class MerchantPricing : DynamicJSON
    {
        [JsonProperty(PropertyName = "timesRestocked")]
        public long TimesRestocked { get; set; } = default;

    }

    public class MerchantQuest : DynamicJSON
    {
        [JsonProperty(PropertyName = "dynamicQuestState")]
        public object? DynamicQuestState { get; set; } = null;

        [JsonProperty(PropertyName = "questState")]
        public object? QuestState { get; set; } = null;

    }

    public class MerchantSlot : DynamicJSON
    {
        [JsonProperty(PropertyName = "item")]
        public Item? Item { get; set; } = null;

        [JsonProperty(PropertyName = "priceMultiplier")]
        public decimal PriceMultiplier { get; set; } = default;

        [JsonProperty(PropertyName = "rebateFraction")]
        public decimal RebateFraction { get; set; } = default;

        [JsonProperty(PropertyName = "reserved")]
        public bool Reserved { get; set; } = default;

    }
}
