using System.Windows.Media.Imaging;
using MCDSaveEditor.Save.Profiles;
using MCDSaveEditor.Save.Enums;

namespace MCDSaveEditor.Logic.ImageResolver
{
    public interface IImageResolver
    {
        string path { get; }
        BitmapImage imageSourceForItem(Item item);
        BitmapImage imageSourceForItem(string itemType);
        BitmapImage imageSourceForRarity(Rarity rarity);
        BitmapImage imageSourceForEnchantment(Enchantment enchantment);
        BitmapImage imageSourceForEnchantment(string enchantmentType);
        BitmapImage imageSource(string path);
    }
}
