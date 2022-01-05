using System;
using System.ComponentModel;
using Newtonsoft.Json;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Models;
using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;
using MCDStorageChest.Libraries;

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class Armorproperty : DynamicJSON
    {
        #region JSON

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = null!;
        [JsonProperty(PropertyName = "rarity")]
        public RarityEnum Rarity { get; set; } = default!;

        #endregion

        #region Extensions

        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public string Name => StringLibrary.armorProperty(Id);
        [JsonIgnore, IgnoreAutoChangeNotification, Browsable(false)]
        public string Description => StringLibrary.armorPropertyDescription(Id);

        #endregion
    }

    public partial class Armorproperty : DynamicJSON, ICloneable
    {
        public object Clone()
        {
            return Copy();
        }

        public Armorproperty Copy()
        {
            var copy = new Armorproperty();
            copy.Id = this.Id;
            copy.Rarity = this.Rarity;
            copy.Data = this.Data;
            return copy;
        }
    }
}
