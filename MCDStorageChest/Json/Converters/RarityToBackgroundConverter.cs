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

namespace MCDStorageChest.Json.Converters
{
    public sealed class RarityToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RarityEnum)) return Brushes.Black;
            var currentRarity = (RarityEnum)value;
            var image = Logic.AssetResolver.instance.imageSourceForRarity(currentRarity);
            if (image != null) return new ImageBrush(image);

            switch (currentRarity)
            {
                case RarityEnum.Common:
                    return Brushes.Gray;
                case RarityEnum.Rare:
                    return Brushes.Green;
                case RarityEnum.Unique:
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
