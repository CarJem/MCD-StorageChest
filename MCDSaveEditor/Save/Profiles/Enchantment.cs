using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Models;
using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using System.Windows.Media;
using MCDStorageChest.Save.Mapping;
using PostSharp.Patterns.Model;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public partial class Enchantment : DynamicJSON
    {
        #region JSON

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("level")]
        public long Level { get; set; }

        #endregion

        #region Extensions

        [JsonIgnore, IgnoreAutoChangeNotification]
        public ImageSource Image => AssetResolver.instance.imageSourceForEnchantment(this);
        [JsonIgnore, IgnoreAutoChangeNotification]
        public string Name => StringLibrary.enchantmentName(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification]
        public string Description => StringLibrary.enchantmentDescription(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification]
        public string Effect => StringLibrary.enchantmentEffect(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification]
        public bool isPowerful => TypeLibrary.PowerfulEnchantments.Contains(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification]
        public int pointsCost
        {
            get
            {
                int level = (int)this.Level;
                int cost;
                if (this.isPowerful)
                {
                    cost = GameCalculator.powerfulEnchantmentCostForLevel(level);
                }
                else
                {
                    cost = GameCalculator.enchantmentCostForLevel(level);
                }
                return cost;
            }

        }
        [JsonIgnore, IgnoreAutoChangeNotification]
        public int gildedPointsCost
        {
            get
            {
                int level = (int)this.Level;
                int cost = pointsCost;
                return cost + level;
            }
        }

        #endregion
    }

    public partial class Enchantment : DynamicJSON, ICloneable
    {
        public object Clone()
        {
            return Copy();
        }

        public Enchantment Copy()
        {
            var copy = new Enchantment();
            copy.Id = this.Id;
            copy.Level = this.Level;
            copy.Data = this.Data;
            return copy;
        }
    }
}
