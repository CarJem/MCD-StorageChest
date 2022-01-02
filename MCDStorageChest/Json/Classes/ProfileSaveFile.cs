using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using PostSharp.Patterns.Model;
using System;
using System.Text.Json;

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class ProfileSaveFile : DynamicJSON, INotifyPropertyChanged
    {
        private ObservableCollection<Item> _items;
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        [JsonPropertyName("items")]
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        [JsonPropertyName("bonus_prerequisites")]
        public string[] BonusPrerequisites { get; set; }

        [JsonPropertyName("clone")]
        public bool Clone { get; set; }


        [JsonPropertyName("cosmetics")]
        public Cosmetic[] Cosmetics { get; set; }

        [JsonPropertyName("cosmeticsEverEquipped")]
        public string[] CosmeticsEverEquipped { get; set; }

        [JsonPropertyName("creationDate")]
        public string CreationDate { get; set; }

        [JsonPropertyName("currenciesFound")]
        public string[] CurrenciesFound { get; set; }

        [JsonPropertyName("currency")]
        public Currency[] Currency { get; set; }

        [JsonPropertyName("customized")]
        public bool Customized { get; set; }

        [JsonPropertyName("difficulties")]
        public Difficulties Difficulties { get; set; }

        [JsonPropertyName("endGameContentProgress")]
        public object EndGameContentProgress { get; set; }

        [JsonPropertyName("finishedObjectiveTags")]
        public Dictionary<string, long> FinishedObjectiveTags { get; set; }

        [JsonPropertyName("itemsFound")]
        public string[] ItemsFound { get; set; }

        [JsonPropertyName("legendaryStatus")]
        public double? LegendaryStatus { get; set; }

        [JsonPropertyName("lobbychest_progress")]
        public Dictionary<string, LobbychestProgress> LobbychestProgress { get; set; }

        [JsonPropertyName("mapUIState")]
        public object MapUiState { get; set; }

        [JsonPropertyName("merchantData")]
        public Dictionary<string, MerchantDef> MerchantData { get; set; }

        [JsonPropertyName("missionStatesMap")]
        public object MissionStatesMap { get; set; }

        [JsonPropertyName("mob_kills")]
        public Dictionary<string, long> MobKills { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pendingRewardItem")]
        public object PendingRewardItem { get; set; }
        
        [JsonPropertyName("pendingRewardItems")]
        public object PendingRewardItems { get; set; }

        [JsonPropertyName("playerId")]
        public string PlayerId { get; set; }

        [JsonPropertyName("progress")]
        public Dictionary<string, Progress> Progress { get; set; }

        [JsonPropertyName("progressStatCounters")]
        public Dictionary<string, long> ProgressStatCounters { get; set; }

        [JsonPropertyName("progressionKeys")]
        public string[] ProgressionKeys { get; set; }

        [JsonPropertyName("skin")]
        public string Skin { get; set; }

        [JsonPropertyName("threatLevels")]
        public ThreatLevels ThreatLevels { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("totalGearPower")]
        public long TotalGearPower { get; set; }

        [JsonPropertyName("trialsCompleted")]
        public object[] TrialsCompleted { get; set; }

        [JsonPropertyName("uiHintsExpired")]
        public object[] UiHintsExpired { get; set; }

        [JsonPropertyName("version")]
        public long Version { get; set; }

        [JsonPropertyName("xp")]
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

        [JsonIgnore, SafeForDependencyAnalysis]
        public Item MeleeGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.MeleeGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public Item ArmorGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.ArmorGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public Item RangedGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.RangedGear);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public Item HotbarSlot1Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot1);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
        public Item HotbarSlot2Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot2);
        }
        [JsonIgnore, SafeForDependencyAnalysis]
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
        [JsonIgnore, SafeForDependencyAnalysis]
        public int AvaliableEnchantmentPoints
        {
            get => this.remainingEnchantmentPoints();
        }

        [JsonIgnore, SafeForDependencyAnalysis]
        public JObject RawData => JObject.Parse(JsonSerializer.Serialize(this));

        #endregion
    }
}
