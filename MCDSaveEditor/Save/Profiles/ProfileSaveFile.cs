﻿using MCDSaveEditor.Extensions;
using MCDSaveEditor.Logic;
using MCDSaveEditor.Save.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
#nullable disable

namespace MCDSaveEditor.Save.Profiles
{
    public partial class ProfileSaveFile : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
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

        [JsonPropertyName("items")]
        public ObservableCollection<Item> Items { get; set; }

        [JsonPropertyName("itemsFound")]
        public string[] ItemsFound { get; set; }

        [JsonPropertyName("legendaryStatus")]
        public double? LegendaryStatus { get; set; }

        [JsonPropertyName("lobbychest_progress")]
        public Dictionary<string, LobbychestProgress> LobbychestProgress { get; set; }

        [JsonPropertyName("mapUIState")]
        public object MapUiState { get; set; }
        //public MapUiState MapUiState { get; set; }

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
        //public UiHintsExpired[] UiHintsExpired { get; set; }

        [JsonPropertyName("version")]
        public long Version { get; set; }

        [JsonPropertyName("xp")]
        public long Xp { get; set; }
    }

    public partial class ProfileSaveFile
    {

        public void UpdateEquiptmentSlots()
        {
            OnPropertyChanged(nameof(MeleeGearItem));
            OnPropertyChanged(nameof(ArmorGearItem));
            OnPropertyChanged(nameof(RangedGearItem));
            OnPropertyChanged(nameof(HotbarSlot1Item));
            OnPropertyChanged(nameof(HotbarSlot2Item));
            OnPropertyChanged(nameof(HotbarSlot3Item));
        }

        public Item MeleeGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.MeleeGear);
        }
        public Item ArmorGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.ArmorGear);
        }
        public Item RangedGearItem
        {
            get => this.equipmentSlot(EquipmentSlotEnum.RangedGear);
        }
        public Item HotbarSlot1Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot1);
        }
        public Item HotbarSlot2Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot2);
        }
        public Item HotbarSlot3Item
        {
            get => this.equipmentSlot(EquipmentSlotEnum.HotbarSlot3);
        }

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

        public int Level
        {
            get => this.level();
            set => this.Xp = GameCalculator.experienceForLevel(value);
        }

        public int AvaliableEnchantmentPoints
        {
            get => this.remainingEnchantmentPoints();
        }
    }
}
