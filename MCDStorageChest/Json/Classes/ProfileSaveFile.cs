using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PostSharp.Patterns.Model;
using System;
using Newtonsoft.Json;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
#nullable enable

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class ProfileSaveFile : DynamicJSON, INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [JsonProperty(PropertyName = "items")]
        public ItemCollection Items { get; set; } = new ItemCollection();

        [JsonProperty(PropertyName = "bonus_prerequisites")]
        public string[] BonusPrerequisites { get; set; } = default!;

        [JsonProperty(PropertyName = "clone")]
        public bool Clone { get; set; }

        [JsonProperty(PropertyName = "cosmetics")]
        public Cosmetic[] Cosmetics { get; set; } = default!;

        [JsonProperty(PropertyName = "cosmeticsEverEquipped")]
        public string[] CosmeticsEverEquipped { get; set; } = default!;

        [JsonProperty(PropertyName = "creationDate")]
        public string CreationDate { get; set; } = default!;

        [JsonProperty(PropertyName = "currenciesFound")]
        public string[] CurrenciesFound { get; set; } = default!;

        [JsonProperty(PropertyName = "currency")]
        public Currency[] Currency { get; set; } = default!;

        [JsonProperty(PropertyName = "customized")]
        public bool Customized { get; set; }

        [JsonProperty(PropertyName = "difficulties"), ExpandableObject]
        public Difficulties Difficulties { get; set; } = default!;

        [JsonProperty(PropertyName = "endGameContentProgress"), ExpandableObject]
        public object EndGameContentProgress { get; set; } = default!;

        [JsonProperty(PropertyName = "finishedObjectiveTags")]
        public Dictionary<string, long> FinishedObjectiveTags { get; set; } = default!;

        [JsonProperty(PropertyName = "itemsFound")]
        public string[] ItemsFound { get; set; } = new string[0];

        [JsonProperty(PropertyName = "legendaryStatus")]
        public decimal LegendaryStatus { get; set; } = default;

        [JsonProperty(PropertyName = "lobbychest_progress")]
        public Dictionary<string, LobbychestProgress> LobbychestProgress { get; set; } = default!;

        [JsonProperty(PropertyName = "mapUIState"), ExpandableObject]
        public object MapUiState { get; set; } = default!;

        [JsonProperty(PropertyName = "merchantData")]
        public Dictionary<string, MerchantDef> MerchantData { get; set; } = default!;

        [JsonProperty(PropertyName = "missionStatesMap"), ExpandableObject]
        public object MissionStatesMap { get; set; } = default!;

        [JsonProperty(PropertyName = "mob_kills")]
        public Dictionary<string, long> MobKills { get; set; } = default!;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = default!;

        [JsonProperty(PropertyName = "pendingRewardItem"), ExpandableObject]
        public object PendingRewardItem { get; set; } = default!;

        [JsonProperty(PropertyName = "pendingRewardItems"), ExpandableObject]
        public object PendingRewardItems { get; set; } = default!;

        [JsonProperty(PropertyName = "playerId")]
        public string PlayerId { get; set; } = default!;

        [JsonProperty(PropertyName = "progress")]
        public Dictionary<string, Progress> Progress { get; set; } = default!;

        [JsonProperty(PropertyName = "progressStatCounters")]
        public Dictionary<string, long> ProgressStatCounters { get; set; } = default!;

        [JsonProperty(PropertyName = "progressionKeys")]
        public string[] ProgressionKeys { get; set; } = default!;

        [JsonProperty(PropertyName = "skin")]
        public string Skin { get; set; } = default!;

        [JsonProperty(PropertyName = "threatLevels"), ExpandableObject]
        public ThreatLevels ThreatLevels { get; set; } = default!;

        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "totalGearPower")]
        public long TotalGearPower { get; set; }

        [JsonProperty(PropertyName = "trialsCompleted")]
        public object[] TrialsCompleted { get; set; } = default!;

        [JsonProperty(PropertyName = "uiHintsExpired")]
        public object[] UiHintsExpired { get; set; } = default!;

        [JsonProperty(PropertyName = "version")]
        public long Version { get; set; }

        [JsonProperty(PropertyName = "xp")]
        public long Xp { get; set; }

        #region Extensions

        public void UpdateEquiptmentSlots()
        {
            OnPropertyChanged(nameof(MeleeGearItem));
            OnPropertyChanged(nameof(ArmorGearItem));
            OnPropertyChanged(nameof(RangedGearItem));
            OnPropertyChanged(nameof(HotbarSlot1Item));
            OnPropertyChanged(nameof(HotbarSlot2Item));
            OnPropertyChanged(nameof(HotbarSlot3Item));

            MeleeGearItem?.RefreshProperties();
            ArmorGearItem?.RefreshProperties();
            RangedGearItem?.RefreshProperties();
            HotbarSlot1Item?.RefreshProperties();
            HotbarSlot2Item?.RefreshProperties();
            HotbarSlot3Item?.RefreshProperties();
        }

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item MeleeGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.MeleeGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item ArmorGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.ArmorGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item RangedGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.RangedGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item HotbarSlot1Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot1);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item HotbarSlot2Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot2);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public Item HotbarSlot3Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot3);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public ulong Emeralds
        {
            get => this.Currency.FirstOrDefault(c => c.Type == Constants.EMERALD_CURRENCY_NAME)?.Count ?? 0;
            set
            {
                Currency currency = this.Currency.FirstOrDefault(c => c.Type == Constants.EMERALD_CURRENCY_NAME) ?? new Currency() { Type = Constants.EMERALD_CURRENCY_NAME };
                currency.Count = value;
                this.Currency = (new[] { currency }).Concat(this.Currency.Where(c => c.Type != Constants.EMERALD_CURRENCY_NAME)).OrderBy(c => c.Type).ToArray();
            }
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public ulong Gold
        {
            get => this.Currency.FirstOrDefault(c => c.Type == Constants.GOLD_CURRENCY_NAME)?.Count ?? 0;
            set
            {
                Currency currency = this.Currency.FirstOrDefault(c => c.Type == Constants.GOLD_CURRENCY_NAME) ?? new Currency() { Type = Constants.GOLD_CURRENCY_NAME };
                currency.Count = value;
                this.Currency = (new[] { currency }).Concat(this.Currency.Where(c => c.Type != Constants.GOLD_CURRENCY_NAME)).OrderBy(c => c.Type).ToArray();
            }
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public ulong EyesOfEnder
        {
            get => this.Currency.FirstOrDefault(c => c.Type == Constants.EYEOFENDER_CURRENCY_NAME)?.Count ?? 0;
            set
            {
                Currency currency = this.Currency.FirstOrDefault(c => c.Type == Constants.EYEOFENDER_CURRENCY_NAME) ?? new Currency() { Type = Constants.EYEOFENDER_CURRENCY_NAME };
                currency.Count = value;
                this.Currency = (new[] { currency }).Concat(this.Currency.Where(c => c.Type != Constants.EYEOFENDER_CURRENCY_NAME)).OrderBy(c => c.Type).ToArray();
            }
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public int Level
        {
            get => this.level();
            set => this.Xp = GameCalculator.experienceForLevel(value);
        }
        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public int AvaliableEnchantmentPoints
        {
            get => this.remainingEnchantmentPoints();
        }


        #endregion
    }
}
