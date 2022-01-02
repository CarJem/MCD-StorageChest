using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Extensions;
using MCDStorageChest.Models;
using MCDStorageChest.Logic;
using System.ComponentModel;
using System.Windows.Media;
using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using Newtonsoft.Json.Linq;
using MCDStorageChest.Json;
using System.Text.Json;
using MCDStorageChest.Json.Converters;
using MCDStorageChest.Classes;
using MCDStorageChest.Controls.ItemTemplates;

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class Item : DynamicJSON, INotifyPropertyChanged
    {
        #region JSON

        [JsonPropertyName("armorproperties")] //TODO: See if changing this from an Array changes anything significantly
        public ObservableCollection<Armorproperty> Armorproperties { get; set; } = null;

        [JsonPropertyName("enchantments")] //TODO: See if changing this from an Array changes anything significantly
        public EnchantmentCollection Enchantments { get; set; } = null;

        [JsonPropertyName("equipmentSlot")]
        public string EquipmentSlot { get; set; } = null;

        [JsonPropertyName("gifted")]
        public bool Gifted { get; set; } = default;

        [JsonPropertyName("inventoryIndex")]
        public long InventoryIndex { get; set; } = default;

        [JsonPropertyName("markedNew")]
        public bool MarkedNew { get; set; } = default;

        [JsonPropertyName("netheriteEnchant")]
        public Enchantment NetheriteEnchant { get; set; } = null;

        [JsonPropertyName("power")]
        public double Power { get; set; } = default;

        [JsonPropertyName("rarity")]
        public RarityEnum Rarity { get; set; } = default;

        [JsonPropertyName("type")]
        public string Type { get; set; } = null;

        [JsonPropertyName("upgraded")]
        public bool Upgraded { get; set; } = default;

        #endregion

        #region Extensions

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void RefreshProperties()
        {
            OnPropertyChanged(nameof(Armorproperties));
            OnPropertyChanged(nameof(Enchantments));
            OnPropertyChanged(nameof(EquipmentSlot));
            OnPropertyChanged(nameof(Gifted));
            OnPropertyChanged(nameof(InventoryIndex));
            OnPropertyChanged(nameof(MarkedNew));
            OnPropertyChanged(nameof(NetheriteEnchant));
            OnPropertyChanged(nameof(Power));
            OnPropertyChanged(nameof(Rarity));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Upgraded));

            OnPropertyChanged(nameof(Level));
            OnPropertyChanged(nameof(HasEnchantmentPoints));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(IsArmor));
            OnPropertyChanged(nameof(IsArtifact));
            OnPropertyChanged(nameof(IsMeleeWeapon));
            OnPropertyChanged(nameof(IsRangedWeapon));
            OnPropertyChanged(nameof(IsEquiped));
            OnPropertyChanged(nameof(IsEnchanted));
            OnPropertyChanged(nameof(IsEnchantable));
            OnPropertyChanged(nameof(EnchantmentPoints));
            OnPropertyChanged(nameof(IsGilded));

            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(DisplayText));
            OnPropertyChanged(nameof(Images_BG));
            OnPropertyChanged(nameof(Images_EnchantIcon));
            OnPropertyChanged(nameof(Images_StatusNew));
        }

        [JsonIgnore, SafeForDependencyAnalysis]
        public List<Enchantment> BuiltInEnchantments => ExtraDataLibrary.getBuiltInEnchantments(this.Type).ToList();

        [JsonIgnore, SafeForDependencyAnalysis]
        public int Level => GameCalculator.levelFromPower(Power);

        [JsonIgnore, SafeForDependencyAnalysis]
        public bool HasEnchantmentPoints => this.EnchantmentPoints != 0;

        [JsonIgnore, SafeForDependencyAnalysis]
        public string Description => StringLibrary.itemDesc(Type);

        [JsonIgnore, SafeForDependencyAnalysis]
        public string Name => StringLibrary.itemName(this.Type);

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
        public RuneData Runes => ExtraDataLibrary.getItemRunes(this.Type);



        [JsonIgnore, SafeForDependencyAnalysis]
        public JObject RawData => JObject.Parse(JsonSerializer.Serialize(this));

        #region Image Data

        [JsonIgnore, SafeForDependencyAnalysis]
        public ImageSource Image => AssetResolver.instance.imageSourceForItem(this);

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
        public object Images_BG => ItemToBackgroundConverter.ConvertBase(this);

        [JsonIgnore, SafeForDependencyAnalysis]
        public Brush Images_StatusNew => ImageMappings.Instance.Item_MarkedNewBackground;

        [JsonIgnore, SafeForDependencyAnalysis]
        public ImageSource Images_EnchantIcon => ImageMappings.Instance.EnchantCommonIcon;

        #endregion

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
            if (this.Armorproperties != null) copy.Armorproperties = new ObservableCollection<Armorproperty>(this.Armorproperties.deepClone().ToList());
            if (this.Enchantments != null) copy.Enchantments = new EnchantmentCollection(this.Enchantments.deepClone().ToList());
            copy.Gifted = this.Gifted;
            copy.MarkedNew = this.MarkedNew;
            if (this.NetheriteEnchant != null) copy.NetheriteEnchant = this.NetheriteEnchant.Copy();
            copy.Power = this.Power;
            copy.Rarity = this.Rarity;
            copy.Type = this.Type;
            copy.Upgraded = this.Upgraded;
            copy.Data = this.Data;
            return copy;
        }

        public bool HasRune(RuneIcon.Archetype a)
        {
            var Runes = this.Runes;
            if (Runes == null) return false;

            switch (a)
            {
                case RuneIcon.Archetype.A:
                    return Runes.HasRune_A;
                case RuneIcon.Archetype.C:
                    return Runes.HasRune_C;
                case RuneIcon.Archetype.I:
                    return Runes.HasRune_I;
                case RuneIcon.Archetype.O:
                    return Runes.HasRune_O;
                case RuneIcon.Archetype.P:
                    return Runes.HasRune_P;
                case RuneIcon.Archetype.S:
                    return Runes.HasRune_S;
                case RuneIcon.Archetype.T:
                    return Runes.HasRune_T;
                case RuneIcon.Archetype.U:
                    return Runes.HasRune_U;
                case RuneIcon.Archetype.R:
                    return Runes.HasRune_R;
                default:
                    return false;
            }
        }
    }
}
