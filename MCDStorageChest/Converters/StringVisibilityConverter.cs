using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MCDStorageChest.Converters
{
    public class StringVisibilityConverter : IValueConverter
    {
        public StringVisibilityConverter() : this(Visibility.Hidden, Visibility.Visible) { }

        public StringVisibilityConverter(Visibility trueValue, Visibility falseValue)
        {
            IsEmpty = trueValue;
            IsNotEmpty = falseValue;
        }

        public Visibility IsEmpty { get; set; }
        public Visibility IsNotEmpty { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return IsEmpty;
            return String.IsNullOrEmpty(value.ToString()) ? IsEmpty : IsNotEmpty;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
