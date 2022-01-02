using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Models;
using MCDStorageChest.Json.Mapping;
using PostSharp.Patterns.Model;

namespace MCDStorageChest.Json.Classes
{
    [Serializable, NotifyPropertyChanged]
    public partial class Armorproperty : DynamicJSON
    {
        #region JSON

        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("rarity")]
        public RarityEnum Rarity { get; set; } = default!;

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
