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
#nullable disable

namespace MCDSaveEditor.Save.Converters
{
    public sealed class RarityToBulletConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Rarity)) return Brushes.Black;
            var currentRarity = (Rarity)value;

            ImageSource image;

            switch (currentRarity)
            {
                case Rarity.Common:
                    image = Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/regular_bullit");
                    break;
                case Rarity.Rare:
                    image = Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/rare_bullit");
                    break;
                case Rarity.Unique:
                    image = Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/unique_bullit");
                    break;
                default:
                    image = Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Inspector/regular_bullit");
                    break;
            }

            if (image != null) return new ImageBrush(image);

            switch (currentRarity)
            {
                case Rarity.Common:
                    return Brushes.Gray;
                case Rarity.Rare:
                    return Brushes.Green;
                case Rarity.Unique:
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
