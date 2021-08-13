using MCDSaveEditor.Save.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MCDSaveEditor.Save.Converters
{
    public sealed class RarityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Rarity)) return Brushes.Transparent;
            var currentRarity = (Rarity)value;
            switch (currentRarity)
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
