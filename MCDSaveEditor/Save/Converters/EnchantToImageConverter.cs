using MCDSaveEditor.Save.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using MCDSaveEditor.Models;
using MCDSaveEditor.Save.Mapping;

namespace MCDSaveEditor.Save.Converters
{

    public sealed class EnchantToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Profiles.Enchantment)) return ImageMappings.Instance.EnchantmentSet_LockedNode;
            else return (value as Profiles.Enchantment).Image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
