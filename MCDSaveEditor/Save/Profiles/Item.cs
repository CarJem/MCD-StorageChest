using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Extensions;
using MCDStorageChest.Models;
using MCDStorageChest.Logic;
using System.ComponentModel;
using System.Windows.Media;
using MCDStorageChest.Save.Mapping;
using PostSharp.Patterns.Model;
using Newtonsoft.Json.Linq;
using MCDStorageChest.Save.Json;
using System.Text.Json;

namespace MCDStorageChest.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public partial class Item : DynamicJSON
    {
        #region JSON

        [JsonPropertyName("armorproperties")] //TODO: See if changing this from an Array changes anything significantly
        public ObservableCollection<Armorproperty> Armorproperties { get; set; }       
        
        [JsonPropertyName("enchantments")] //TODO: See if changing this from an Array changes anything significantly
        public EnchantmentCollection Enchantments { get; set; }         

        [JsonPropertyName("equipmentSlot")]
        public string EquipmentSlot { get; set; }

        [JsonPropertyName("gifted")]
        public bool Gifted { get; set; }

        [JsonPropertyName("inventoryIndex")]
        public long InventoryIndex { get; set; }

        [JsonPropertyName("markedNew")]
        public bool MarkedNew { get; set; }

        [JsonPropertyName("netheriteEnchant")]
        public Enchantment NetheriteEnchant { get; set; }

        [JsonPropertyName("power")]
        public double Power { get; set; }

        [JsonPropertyName("rarity")]
        public Rarity Rarity { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("upgraded")]
        public bool Upgraded { get; set; }

        #endregion

        #region Extensions

        [JsonIgnore, SafeForDependencyAnalysis]
        public int Level => GameCalculator.levelFromPower(Power);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool HasEnchantmentPoints => this.EnchantmentPoints != 0;

        [JsonIgnore, SafeForDependencyAnalysis]
        public string Description => StringLibrary.itemDesc(Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public string Name => StringLibrary.itemName(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public ImageSource Image => AssetResolver.instance.imageSourceForItem(this);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsArmor => TypeLibrary.Items_Armor.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsArtifact => TypeLibrary.Items_Artifacts.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsMeleeWeapon => TypeLibrary.Items_MeleeWeapons.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsRangedWeapon => TypeLibrary.Items_RangedWeapons.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsEquiped => this.EquipmentSlot != null;
        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsEnchanted => this.EnchantmentPoints != 0;

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool IsEnchantable => !this.IsArtifact;

        [JsonIgnore, SafeForDependencyAnalysis]
        public int EnchantmentPoints
        {
            get
            {
                if (this.Enchantments == null) { return 0; }
                int points = 0;
                foreach (var enchantment in this.Enchantments)
                {
                    if (this.NetheriteEnchant != null)
                    {
                        points += enchantment.gildedPointsCost;
                    }
                    else
                    {
                        points += enchantment.pointsCost;
                    }
                }
                return points;
            }
        }

        [JsonIgnore]
        public bool IsGilded
        {
            get => NetheriteEnchant != null;
            set
            {
                if (value) NetheriteEnchant = new Enchantment();
                else NetheriteEnchant = null;
            }
        }

        [JsonIgnore, SafeForDependencyAnalysis]
        public string DisplayText
        {
            get
            {
                if (Image == null) return Name;
                else return string.Empty;
            }
        }

        [JsonIgnore, SafeForDependencyAnalysis]
        public JObject RawData => JObject.Parse(JsonSerializer.Serialize(this));

        #endregion
    }

    public partial class Item : DynamicJSON, ICloneable
    {
        public object Clone()
        {
            return Copy();
        }

        public Item Copy()
        {
            var copy = new Item();
            //NOTE: deliberately skipping EquipmentSlot and InventoryIndex
            if (this.Armorproperties != null) copy.Armorproperties = new ObservableCollection<Armorproperty>(this.Armorproperties?.deepClone()?.ToList());
            if (this.Enchantments != null) copy.Enchantments = new EnchantmentCollection(this.Enchantments?.deepClone()?.ToList());
            copy.Gifted = this.Gifted;
            copy.MarkedNew = this.MarkedNew;
            copy.NetheriteEnchant = this.NetheriteEnchant?.Copy();
            copy.Power = this.Power;
            copy.Rarity = this.Rarity;
            copy.Type = this.Type;
            copy.Upgraded = this.Upgraded;
            copy.Data = this.Data;
            return copy;
        }
    }
}
