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

namespace MCDStorageChest.Converters
{
    public sealed class LongToRomanNumeral : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is long)) return string.Empty;
            return ToRomanNumeral((long)value);

        }

        public static List<string> romanNumerals = new List<string>() { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        public static List<long> numerals = new List<long>() { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        public static string ToRomanNumeral(long number)
        {
            var romanNumeral = string.Empty;
            while (number > 0)
            {
                // find biggest numeral that is less than equal to number
                var index = numerals.FindIndex(x => x <= number);
                // subtract it's value from your number
                number -= numerals[index];
                // tack it onto the end of your roman numeral
                romanNumeral += romanNumerals[index];
            }
            return romanNumeral;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ret = 0;
            return int.TryParse((string)value, out ret) ? ret : 0;
        }
    }
}
