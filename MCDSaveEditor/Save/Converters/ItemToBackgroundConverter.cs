using MCDStorageChest.Save.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using MCDStorageChest.Models;
using MCDStorageChest.Save.Mapping;

namespace MCDStorageChest.Save.Converters
{
    public sealed class ItemToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Profiles.Item)) return Brushes.Transparent;
            var currentItem = (Profiles.Item)value;

            if (currentItem.IsGilded) return ImageMappings.Instance.Item_GildedBackground;


            var image = Models.AssetResolver.instance.imageSourceForRarity(currentItem.Rarity);
            if (image != null) return new ImageBrush(image);

            switch (currentItem.Rarity)
            {
                case Rarity.Common:
                    return Brushes.Gray;
                case Rarity.Rare:
                    return Brushes.Green;
                case Rarity.Unique:
                    return Brushes.Orange;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
