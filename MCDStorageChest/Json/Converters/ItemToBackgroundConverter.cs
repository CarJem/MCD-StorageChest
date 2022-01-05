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
using PostSharp.Patterns.Model;

namespace MCDStorageChest.Json.Converters
{

    public sealed class ItemToBackgroundConverter : IValueConverter
    {
        public static object ConvertBase(object value)
        {
            if (!(value is Enums.ItemBGEnum)) return Brushes.Transparent;
            var currentItem = (Enums.ItemBGEnum)value;

            if (currentItem == ItemBGEnum.Gilded) return ImageMappings.Instance.Item_GildedBackground;

            var rarity = GetRarity(currentItem);

            var image = Logic.AssetLoader.instance.imageSourceForRarity(rarity);
            if (image != null) return new ImageBrush(image);

            switch (rarity)
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

            RarityEnum GetRarity(ItemBGEnum item)
            {
                switch (item) {
                    case ItemBGEnum.Common:
                        return RarityEnum.Common;
                    case ItemBGEnum.Rare:
                        return RarityEnum.Rare;
                    case ItemBGEnum.Unique:
                        return RarityEnum.Unique;
                    default:
                        return RarityEnum.Common;
                }
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
