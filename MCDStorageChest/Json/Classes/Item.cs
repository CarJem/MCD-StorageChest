using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Extensions;
using MCDStorageChest.Models;
using MCDStorageChest.Logic;
using System.ComponentModel;
using System.Windows.Media;
using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MCDStorageChest.Json;
using MCDStorageChest.Json.Converters;
using MCDStorageChest.Classes;
using MCDStorageChest.Controls.ItemTemplates;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using MCDStorageChest.Libraries;
#nullable enable

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class Item : DynamicJSON, INotifyPropertyChanged
    {
        #region JSON

        [JsonProperty(PropertyName = "armorproperties")] //TODO: See if changing this from an Array changes anything significantly
        public ObservableCollection<Armorproperty> Armorproperties { get; set; } = default!;

        [JsonProperty(PropertyName = "enchantments")] //TODO: See if changing this from an Array changes anything significantly
        public EnchantmentCollection Enchantments { get; set; } = default!;

        [JsonProperty(PropertyName = "equipmentSlot")]
        public string EquipmentSlot { get; set; } = null!;

        [JsonProperty(PropertyName = "gifted")]
        public bool Gifted { get; set; } = default;

        [JsonProperty(PropertyName = "inventoryIndex")]
        public long InventoryIndex { get; set; } = default;

        [JsonProperty(PropertyName = "markedNew")]
        public bool MarkedNew { get; set; } = default;

        [JsonProperty(PropertyName = "netheriteEnchant"), ExpandableObject]
        public Enchantment NetheriteEnchant { get; set; } = null!;

        [JsonProperty(PropertyName = "power")]
        public decimal Power { get; set; } = default;

        [JsonProperty(PropertyName = "rarity")]
        public RarityEnum Rarity { get; set; } = default;

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "upgraded")]
        public bool Upgraded { get; set; } = default;

        #endregion

        #region Extensions

        public event PropertyChangedEventHandler? PropertyChanged;

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
        }

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public List<Enchantment> BuiltInEnchantments => ItemDataLibrary.getBuiltInEnchantments(this.Type).ToList();

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public int Level => GameCalculator.levelFromPower((double)Power);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool HasEnchantmentPoints => this.EnchantmentPoints != 0;

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public string Description => StringLibrary.itemDesc(Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public string Name => StringLibrary.itemName(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsArmor => TypeLibrary.Items_Armor.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsArtifact => TypeLibrary.Items_Artifacts.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsMeleeWeapon => TypeLibrary.Items_MeleeWeapons.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsRangedWeapon => TypeLibrary.Items_RangedWeapons.Contains(this.Type);

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsEquiped => this.EquipmentSlot != null;

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsEnchanted => this.EnchantmentPoints != 0;

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public bool IsEnchantable => !this.IsArtifact;

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
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

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public ItemBGEnum Background
        {
            get
            {
                Depends.On(IsGilded);
                Depends.On(Rarity);

                if (IsGilded) return ItemBGEnum.Gilded;
                else
                {
                    switch (Rarity)
                    {
                        case RarityEnum.Common:
                            return ItemBGEnum.Common;
                        case RarityEnum.Rare:
                            return ItemBGEnum.Rare;
                        case RarityEnum.Unique:
                            return ItemBGEnum.Unique;
                        default:
                            return ItemBGEnum.Common;
                    }
                }
            }
        }

        [JsonIgnore, Browsable(false)]
        public bool IsGilded
        {
            get => NetheriteEnchant != null;
            set
            {
                if (value) NetheriteEnchant = new Enchantment();
                else NetheriteEnchant = null!;
            }
        }

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public RuneData Runes
        {
            get
            {
                Depends.On(this.Type);
                return ItemDataLibrary.getItemRunes(this.Type);
            }
        }

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public ImageSource? Image
        {
            get
            {
                Depends.On(this.Type);
                return AssetLoader.instance.imageSourceForItem(this.Type);
            }
        }

        [JsonIgnore, SafeForDependencyAnalysis, Browsable(false)]
        public string DisplayText
        {
            get
            {
                if (Image == null) return Name;
                else return string.Empty;
            }
        }

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
