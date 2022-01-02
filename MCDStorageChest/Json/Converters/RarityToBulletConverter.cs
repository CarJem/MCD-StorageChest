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
#nullable disable

namespace MCDStorageChest.Json.Converters
{
    public sealed class RarityToBulletConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is RarityEnum)) return Brushes.Black;
            var currentRarity = (RarityEnum)value;

            ImageSource image;

            switch (currentRarity)
            {
                case RarityEnum.Common:
                    image = Logic.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/regular_bullit");
                    break;
                case RarityEnum.Rare:
                    image = Logic.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/rare_bullit");
                    break;
                case RarityEnum.Unique:
                    image = Logic.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/unique_bullit");
                    break;
                default:
                    image = Logic.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/regular_bullit");
                    break;
            }

            if (image != null) return new ImageBrush(image);

            switch (currentRarity)
            {
                case RarityEnum.Common:
                    return Brushes.Gray;
                case RarityEnum.Rare:
                    return Brushes.Green;
                case RarityEnum.Unique:
                    return Brushes.Orange;
                default:
                    return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
