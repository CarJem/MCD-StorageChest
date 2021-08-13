using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MCDSaveEditor.Save.Enums;
using MCDSaveEditor.Models;
using MCDSaveEditor.Save.Mapping;
using PostSharp.Patterns.Model;

namespace MCDSaveEditor.Save.Profiles
{
    [Serializable, NotifyPropertyChanged]
    public partial class Armorproperty
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

    public partial class Armorproperty : ICloneable
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
            return copy;
        }
    }
}
