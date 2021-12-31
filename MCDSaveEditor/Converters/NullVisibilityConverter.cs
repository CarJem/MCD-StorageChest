using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MCDStorageChest.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {
        public NullVisibilityConverter() : this(Visibility.Hidden, Visibility.Visible) { }

        public NullVisibilityConverter(Visibility trueValue, Visibility falseValue)
        {
            IsNull = trueValue;
            IsNotNull = falseValue;
        }

        public Visibility IsNull { get; set; }
        public Visibility IsNotNull { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? IsNull : IsNotNull;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
