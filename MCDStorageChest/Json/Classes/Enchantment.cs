using System;
using System.ComponentModel;
using Newtonsoft.Json;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Models;
using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using System.Windows.Media;
using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using MCDStorageChest.Libraries;
using MCDStorageChest.Json;
#nullable enable

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class Enchantment : DynamicJSON
    {
        #region JSON

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = default!;

        [JsonProperty(PropertyName = "level")]
        public long Level { get; set; } = default!;

        #endregion

        #region Extensions

        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public ImageSource Image => AssetLoader.instance.imageSourceForEnchantment(this.Id)!;
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public string Name => StringLibrary.enchantmentName(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public string Description => StringLibrary.enchantmentDescription(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public string Effect => StringLibrary.enchantmentEffect(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public bool isPowerful => TypeLibrary.PowerfulEnchantments.Contains(this.Id);
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
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
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
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
