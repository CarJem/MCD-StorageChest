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
#nullable disable

namespace MCDStorageChest.Save.Converters
{
    public sealed class EnchantToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Profiles.Enchantment)) return StringLibrary.enchantmentName(Constants.DEFAULT_ENCHANTMENT_ID);
            else return (value as Profiles.Enchantment).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
