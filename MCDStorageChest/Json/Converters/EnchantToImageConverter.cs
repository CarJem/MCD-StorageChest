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
#nullable enable

namespace MCDStorageChest.Json.Converters
{

    public sealed class EnchantToImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Classes.Enchantment)) return ImageMappings.Instance.EnchantmentSet_LockedNode;
            else return ((Classes.Enchantment)value).Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
