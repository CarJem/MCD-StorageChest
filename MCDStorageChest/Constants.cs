using MCDStorageChest.Json.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System;
using System.Windows.Media.Imaging;


namespace MCDStorageChest
{
    #region Constants
    public static partial class Constants
    {
        public const string CURRENT_VERSION_NUMBER = "0.1.1";
        public const string LATEST_RELEASE_GITHUB_URL = "https://github.com/CarJem/MCD-StorageChest";
        public const string LATEST_RELEASE_MOD_URL_FORMAT = "";

        public const string APPLICATION_DISPLAY_NAME = "Minecraft Dungeons: Storage Chest (" + CURRENT_VERSION_NUMBER + ")";

        public const string NO_RECENT_DIRECTORIES_TEXT = "No Recent Directories";
        public static readonly List<string> NO_RECENT_DIRECTORIES = new List<string>() { NO_RECENT_DIRECTORIES_TEXT };

        public const int MAX_RECENT_FILES = 10;

        public const int MAXIMUM_INVENTORY_ITEM_COUNT = 300;

        public static string MAXIMUM_INVENTORY_ITEM_COUNT_STRING
        {
            get => MAXIMUM_INVENTORY_ITEM_COUNT.ToString();
        }

        public const int MINIMUM_ENCHANTMENT_TIER = 0;
        public const int MAXIMUM_ENCHANTMENT_TIER = 3;

        public const int MINIMUM_CHARACTER_LEVEL = 0;
        public const int MAXIMUM_CHARACTER_LEVEL = 1_000_000_000;

        public const int MINIMUM_ITEM_LEVEL = 0;
        public const int MAXIMUM_ITEM_LEVEL = 1_000_000_000;

        public const int MAXIMUM_ENCHANTMENT_OPTIONS_PER_ITEM = 9;
        public const string DEFAULT_ENCHANTMENT_ID = "Unset";

        public const string EMERALD_CURRENCY_NAME = "Emerald";
        public const string GOLD_CURRENCY_NAME = "Gold";
        public const string EYEOFENDER_CURRENCY_NAME = "EyeOfEnder";

        public static readonly Version CURRENT_VERSION = new Version(CURRENT_VERSION_NUMBER);
    }
    #endregion

    #region Constants.FileSystemData

    public static partial class Constants
    {
        public const string PAKS_FILTER_STRING = "/Dungeons/Content";

        public const string FIRST_PAK_FILENAME = "pakchunk0-WindowsNoEditor.pak";

        //NOTE: default location of files for Launcher version: %localappdata%\Mojang\products\dungeons\dungeons\Dungeons\Content\Paks
        public static string PAKS_FOLDER_PATH
        {
            get
            {
                var appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(appDataFolderPath, "Mojang", "products", "dungeons", "dungeons", "Dungeons", "Content", "Paks");
            }
        }

        //NOTE: default location of save game files: %userprofile%\Saved Games\Mojang Studios\Dungeons\2533274911688652\Characters
        public static string FILE_DIALOG_INITIAL_DIRECTORY
        {
            get
            {
                var userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                return Path.Combine(userFolderPath, "Saved Games", "Mojang Studios", "Dungeons");
            }
        }
        public const string ENCRYPTED_FILE_EXTENSION = ".dat";
        public const string DECRYPTED_FILE_EXTENSION = ".json";
    }

    #endregion

    #region Constants.ItemData

    public static partial class Constants
    {
        public const string DEFAULT_ARMOR_PROPERTY_ID = "AllyDamageBoost";
        public const string DEFAULT_MELEE_WEAPON_ID = "Sword";
        public const string DEFAULT_ARMOR_ID = "ArchersStrappings";
        public const string DEFAULT_RANGED_WEAPON_ID = "Bow";
        public const string DEFAULT_ARTIFACT_ID = "FireworksArrowItem";

        public static string defaultItemIDForEquipmentSlot(EquipmentSlotEnum equipmentSlot)
        {
            return defaultItemIDForFilter(itemFilterForEquipmentSlot(equipmentSlot));
        }

        public static string defaultItemIDForFilter(ItemFilterEnum filter)
        {
            switch (filter)
            {
                case ItemFilterEnum.MeleeWeapons: return Constants.DEFAULT_MELEE_WEAPON_ID;
                case ItemFilterEnum.Armor: return Constants.DEFAULT_ARMOR_ID;
                case ItemFilterEnum.RangedWeapons: return Constants.DEFAULT_RANGED_WEAPON_ID;
                case ItemFilterEnum.Artifacts: return Constants.DEFAULT_ARTIFACT_ID;
            }
            throw new ArgumentException($"Invalid filter value {filter}", "filter");
        }

        public static ItemFilterEnum itemFilterForEquipmentSlot(EquipmentSlotEnum equipmentSlot)
        {
            switch (equipmentSlot)
            {
                case EquipmentSlotEnum.MeleeGear: return ItemFilterEnum.MeleeWeapons;
                case EquipmentSlotEnum.ArmorGear: return ItemFilterEnum.Armor;
                case EquipmentSlotEnum.RangedGear: return ItemFilterEnum.RangedWeapons;
                case EquipmentSlotEnum.HotbarSlot1:
                case EquipmentSlotEnum.HotbarSlot2:
                case EquipmentSlotEnum.HotbarSlot3:
                    return ItemFilterEnum.Artifacts;
            }
            throw new ArgumentException($"Invalid equipmentSlot value {equipmentSlot}", "equipmentSlot");
        }
    }

    #endregion
}
