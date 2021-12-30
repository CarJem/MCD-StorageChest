using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json.Classes;
using System;
using System.IO;
using System.Net.Cache;
using System.Windows.Media.Imaging;
using MCDStorageChest.Extensions;

namespace MCDStorageChest.Logic.ImageResolver
{
    public class LocalImageResolver: IImageResolver
    {
        //private const string ASSETS_URI = @"https://img.rankedboost.com/wp-content/plugins/minecraft-dungeons/assets/";
        private const string IMAGES_URI = @"pack://application:,,/Images";
        private readonly RequestCachePolicy REQUEST_CACHE_POLICY = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);

        public string path { get { return null!; } }

        public BitmapImage imageSource(string path)
        {
            if (path.StartsWith("/Dungeons/Content/")) return null;
            else return tryBitmapImageForUri(GetImageURI(path));
        }

        private BitmapImage tryBitmapImageForUri(Uri uri)
        {
            try
            {
                if (uri == null) return null;
                //Console.WriteLine("Requesting Uri: {0}", uri);
                var image = new BitmapImage(uri, REQUEST_CACHE_POLICY);
                return image;
            }
            catch (Exception e)
            {
                EventLogger.logError($"Error creating bitmap for {uri}");
                Console.Write(e);
                //Debug.Assert(false);
                return null!;
            }
        }

        #region Items

        public BitmapImage imageSourceForItem(Item item)
        {
            return null!;
        }

        public BitmapImage imageSourceForItem(string itemType)
        {
            var itemTypeStr = folderNameForItemType(itemType);
            return tryBitmapImageForUri(GetImageURI(string.Format("{0}.png", itemTypeStr)));
        }

        private string folderNameForItemType(string type)
        {
            if (Models.TypeLibrary.Items_Artifacts.Contains(type))
            {
                return "Artifacts";
            }
            if (Models.TypeLibrary.Items_Armor.Contains(type))
            {
                return "Armor";
            }
            if (Models.TypeLibrary.Items_MeleeWeapons.Contains(type))
            {
                return "MeleeWeapons";
            }
            if (Models.TypeLibrary.Items_RangedWeapons.Contains(type))
            {
                return "RangedWeapons";
            }

            return "Unknown";
        }

        private string imageNameForItem(Item item)
        {
            var itemName = item.Type;
            //var encodedString = Uri.EscapeDataString(stringFromItemName(itemName));
            return string.Format("T_{0}_Icon_inventory.png", itemName);
        }

        #endregion

        #region Raritys

        public BitmapImage imageSourceForRarity(RarityEnum rarity)
        {
            return null!;
            //var filename = imageNameFromRarity(rarity);
            //if(filename == null) { return null; }
            //var path = Path.Combine(IMAGES_URI, "UI", "ItemRarity", filename);
            //var uri = new Uri(path);
            //return tryBitmapImageForUri(uri);
        }

        private string imageNameFromRarity(RarityEnum rarity)
        {
            switch (rarity)
            {
                case RarityEnum.Common: return "drops_items_frame.png";
                case RarityEnum.Rare: return "drops_rare_frame.png";
                case RarityEnum.Unique: return "drops_unique_frame.png";
            }
            throw new NotImplementedException();
        }

        #endregion

        #region Enchantments

        public BitmapImage imageSourceForEnchantment(Enchantment enchantment)
        {
            return imageSourceForEnchantment(enchantment.Id);
        }

        public BitmapImage imageSourceForEnchantment(string enchantment)
        {
            var enchantmentId = enchantment;
            if (enchantmentId == Constants.DEFAULT_ENCHANTMENT_ID)
            {
                return null!;
            }
            return tryBitmapImageForUri(GetImageURI("Enchantments.png"));
        }

        #endregion

        private Uri GetImageURI(string filename)
        {
            var path = Path.Combine(IMAGES_URI, filename);
            if (Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out Uri uri)) return uri;
            else return null!;
        }
    }
}
