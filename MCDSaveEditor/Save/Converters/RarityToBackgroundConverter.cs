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

namespace MCDStorageChest.Save.Converters
{
    public sealed class RarityToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Rarity)) return Brushes.Black;
            var currentRarity = (Rarity)value;
            var image = Models.AssetResolver.instance.imageSourceForRarity(currentRarity);
            if (image != null) return new ImageBrush(image);

            switch (currentRarity)
            {
                case Rarity.Common:
                    return Brushes.Gray;
                case Rarity.Rare:
                    return Brushes.Green;
                case Rarity.Unique:
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
