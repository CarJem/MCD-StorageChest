using MCDStorageChest.Json.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using MCDStorageChest.Models;
using MCDStorageChest.Json.Mapping;

namespace MCDStorageChest.Json.Converters
{
    public sealed class ItemToBackgroundConverter : IValueConverter
    {
        public static object ConvertBase(object value)
        {
            if (!(value is Classes.Item)) return Brushes.Transparent;
            var currentItem = (Classes.Item)value;

            if (currentItem.IsGilded) return ImageMappings.Instance.Item_GildedBackground;


            var image = Logic.AssetResolver.instance.imageSourceForRarity(currentItem.Rarity);
            if (image != null) return new ImageBrush(image);

            switch (currentItem.Rarity)
            {
                case RarityEnum.Common:
                    return Brushes.Gray;
                case RarityEnum.Rare:
                    return Brushes.Green;
                case RarityEnum.Unique:
                    return Brushes.Orange;
                default:
                    return Brushes.Transparent;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBase(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
