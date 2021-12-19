using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Models;
using MCDStorageChest.Save.Mapping;
using PostSharp.Patterns.Model;
using MCDStorageChest.Save.Json;

namespace MCDStorageChest.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public partial class Armorproperty : DynamicJSON
    {
        #region JSON

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("rarity")]
        public Rarity Rarity { get; set; }

        #endregion

        #region Extensions

        [JsonIgnore, IgnoreAutoChangeNotification]
        public string Name => StringLibrary.armorProperty(Id);
        [JsonIgnore, IgnoreAutoChangeNotification]
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
