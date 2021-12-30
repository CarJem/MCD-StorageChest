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
    public sealed class EnchantToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Classes.Enchantment)) return StringLibrary.enchantmentName(Constants.DEFAULT_ENCHANTMENT_ID);
            else return (value as Classes.Enchantment).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
