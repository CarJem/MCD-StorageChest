using MCDStorageChest.Json.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MCDStorageChest.Json.Converters
{
    public sealed class RarityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RarityEnum)) return Brushes.Transparent;
            var currentRarity = (RarityEnum)value;
            switch (currentRarity)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}
