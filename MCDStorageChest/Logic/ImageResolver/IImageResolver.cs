using System.Windows.Media.Imaging;
using MCDStorageChest.Json.Classes;
using MCDStorageChest.Json.Enums;

namespace MCDStorageChest.Logic.ImageResolver
{
    public interface IImageResolver
    {
        string path { get; }
        BitmapImage imageSourceForItem(Item item);
        BitmapImage imageSourceForItem(string itemType);
        BitmapImage imageSourceForRarity(RarityEnum rarity);
        BitmapImage imageSourceForEnchantment(Enchantment enchantment);
        BitmapImage imageSourceForEnchantment(string enchantmentType);
        BitmapImage imageSource(string path);
    }
}
