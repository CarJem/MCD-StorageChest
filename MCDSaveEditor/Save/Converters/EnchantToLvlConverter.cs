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
    public sealed class EnchantToLvlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Profiles.Enchantment)) return "0";
            else return (value as Profiles.Enchantment).Level;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
