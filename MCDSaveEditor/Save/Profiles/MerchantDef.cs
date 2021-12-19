using System.Collections.Generic;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    public class MerchantDef : DynamicJSON
    {
        [JsonPropertyName("everInteracted")]
        public bool EverInteracted { get; set; }

        [JsonPropertyName("pricing")]
        public MerchantPricing Pricing { get; set; }

        [JsonPropertyName("quests")]
        public Dictionary<string, MerchantQuest> Quests { get; set; }

        [JsonPropertyName("slots")]
        public Dictionary<string, MerchantSlot> Slots { get; set; }
    }

    public class MerchantPricing : DynamicJSON
    {
        [JsonPropertyName("timesRestocked")]
        public long TimesRestocked { get; set; }

    }

    public class MerchantQuest : DynamicJSON
    {
        [JsonPropertyName("dynamicQuestState")]
        public object DynamicQuestState { get; set; }

        [JsonPropertyName("questState")]
        public object QuestState { get; set; }

    }

    public class MerchantSlot : DynamicJSON
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }

        [JsonPropertyName("priceMultiplier")]
        public double PriceMultiplier { get; set; }

        [JsonPropertyName("rebateFraction")]
        public double RebateFraction { get; set; }

        [JsonPropertyName("reserved")]
        public bool Reserved { get; set; }

    }
}
