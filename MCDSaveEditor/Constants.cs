using MCDSaveEditor.Save.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.IO;
using System;
using System.Windows.Media.Imaging;
#nullable enable

namespace MCDSaveEditor
{
    #region Constants
    public static partial class Constants
    {
        public const string CURRENT_VERSION_NUMBER = "1.3.7";
        public const string LATEST_RELEASE_GITHUB_URL = "https://github.com/CutFlame/MCDSaveEdit/releases/latest";
        public const string LATEST_RELEASE_MOD_URL_FORMAT = "https://www.nexusmods.com/minecraftdungeons/mods/{0}?tab=files";

        // The application's name used for identification in the registry.
        public const string APPLICATION_NAME = "MCDSaveEdit";
        public const string PAK_FILE_LOCATION_REGISTRY_KEY = "PakFilesPath";
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

    #region Constants.MapData

    public static partial class Constants
    {
        public readonly static StaticLevelData[] DEBUG_LEVEL_DATA = new StaticLevelData[] {
            new StaticLevelData(".00", new Point(0.00, 0.00), LevelTypeEnum.mission),
            new StaticLevelData(".05", new Point(0.05, 0.05), LevelTypeEnum.mission),
            new StaticLevelData(".10", new Point(0.10, 0.10), LevelTypeEnum.mission),
            new StaticLevelData(".15", new Point(0.15, 0.15), LevelTypeEnum.mission),
            new StaticLevelData(".20", new Point(0.20, 0.20), LevelTypeEnum.mission),
            new StaticLevelData(".25", new Point(0.25, 0.25), LevelTypeEnum.mission),
            new StaticLevelData(".30", new Point(0.30, 0.30), LevelTypeEnum.mission),
            new StaticLevelData(".35", new Point(0.35, 0.35), LevelTypeEnum.mission),
            new StaticLevelData(".40", new Point(0.40, 0.40), LevelTypeEnum.mission),
            new StaticLevelData(".45", new Point(0.45, 0.45), LevelTypeEnum.mission),
            new StaticLevelData(".50", new Point(0.50, 0.50), LevelTypeEnum.mission),
            new StaticLevelData(".55", new Point(0.55, 0.55), LevelTypeEnum.mission),
            new StaticLevelData(".60", new Point(0.60, 0.60), LevelTypeEnum.mission),
            new StaticLevelData(".65", new Point(0.65, 0.65), LevelTypeEnum.mission),
            new StaticLevelData(".70", new Point(0.70, 0.70), LevelTypeEnum.mission),
            new StaticLevelData(".75", new Point(0.75, 0.75), LevelTypeEnum.mission),
            new StaticLevelData(".80", new Point(0.80, 0.80), LevelTypeEnum.mission),
            new StaticLevelData(".85", new Point(0.85, 0.85), LevelTypeEnum.mission),
            new StaticLevelData(".90", new Point(0.90, 0.90), LevelTypeEnum.mission),
            new StaticLevelData(".95", new Point(0.95, 0.95), LevelTypeEnum.mission),
            new StaticLevelData("1.0", new Point(1.00, 1.00), LevelTypeEnum.mission),
        };

        public readonly static StaticLevelData[] MAINLAND_LEVEL_DATA = new StaticLevelData[] {
            new StaticLevelData("creepycrypt", new Point(.19, .37), LevelTypeEnum.dungeon),
            new StaticLevelData("mooshroomisland", new Point(.26, .24), LevelTypeEnum.dungeon),
            new StaticLevelData("creeperwoods", new Point(.28, .40), LevelTypeEnum.mission),
            new StaticLevelData("soggycave", new Point(.27, .65), LevelTypeEnum.dungeon),
            new StaticLevelData("soggyswamp", new Point(.29, .74), LevelTypeEnum.mission),
            new StaticLevelData("mooncorecaverns", new Point(.44, .35), LevelTypeEnum.mission),
            new StaticLevelData("cacticanyon", new Point(.42, .56), LevelTypeEnum.mission),
            new StaticLevelData("pumpkinpastures", new Point(.46, .70), LevelTypeEnum.mission),
            new StaticLevelData("archhaven", new Point(.57, .73), LevelTypeEnum.dungeon),
            new StaticLevelData("deserttemple", new Point(.59, .45), LevelTypeEnum.mission),
            new StaticLevelData("fieryforge", new Point(.63, .20), LevelTypeEnum.mission),
            new StaticLevelData("lowertemple", new Point(.67, .59), LevelTypeEnum.dungeon),
            new StaticLevelData("highblockhalls", new Point(.75, .40), LevelTypeEnum.mission),
            new StaticLevelData("obsidianpinnacle", new Point(.86, .25), LevelTypeEnum.mission),
            new StaticLevelData("underhalls", new Point(.87, .49), LevelTypeEnum.dungeon),
            new StaticLevelData("squidcoast", new Point(.15, .64), LevelTypeEnum.mission),
        };

        public readonly static MapImageData MAINLAND_MAP_IMAGE_DATA = new MapImageData
        {
            titleLookupString = "ArchIllagerRealm_name",
            titleBackupText = "R.MAINLAND",
            mapImageSourcePath = "/Dungeons/Content/UI/Materials/MissionSelectMap/background/missionselect_map_center_xbox",
            backgroundColor = Colors.White,
            cropToRect = new Int32Rect(10, 83, 6136, 2975),
            levelData = MAINLAND_LEVEL_DATA!,
        };

        public readonly static StaticLevelData[] JUNGLE_AWAKENS_LEVEL_DATA = new StaticLevelData[] {
            new StaticLevelData("dingyjungle", new Point(.24, .44), LevelTypeEnum.mission),
            new StaticLevelData("overgrowntemple", new Point(.62, .15), LevelTypeEnum.mission),
            new StaticLevelData("bamboobluff", new Point(.65, .55), LevelTypeEnum.dungeon),
        };

        public readonly static MapImageData JUNGLE_AWAKENS_MAP_IMAGE_DATA = new MapImageData
        {
            titleLookupString = "TheJungleAwakens_name",
            titleBackupText = "R.JUNGLE_AWAKENS",
            mapImageSourcePath = "/Dungeons/Content/UI/Materials/MissionSelectMap/background/islands/DLC_Jungle_Island",
            backgroundColor = Colors.LightCyan,
            cropToRect = new Int32Rect(0, 0, 2166, 1455),
            levelData = JUNGLE_AWAKENS_LEVEL_DATA!,
        };

        public readonly static StaticLevelData[] CREEPING_WINTER_LEVEL_DATA = new StaticLevelData[] {
            new StaticLevelData("frozenfjord", new Point(.24, .44), LevelTypeEnum.mission),
            new StaticLevelData("lonelyfortress", new Point(.62, .15), LevelTypeEnum.mission),
            new StaticLevelData("lostsettlement", new Point(.65, .55), LevelTypeEnum.dungeon),
        };

        public readonly static MapImageData CREEPING_WINTER_MAP_IMAGE_DATA = new MapImageData
        {
            titleLookupString = "TheCreepingWinter_name",
            titleBackupText = "R.CREEPING_WINTER",
            mapImageSourcePath = "/Dungeons/Content/UI/Materials/MissionSelectMap/background/islands/DLC_Snowy_Island",
            backgroundColor = Colors.LightCyan,
            cropToRect = new Int32Rect(0, 0, 2211, 1437),
            levelData = CREEPING_WINTER_LEVEL_DATA!,
        };

        public readonly static StaticLevelData[] HOWLING_PEAKS_LEVEL_DATA = new StaticLevelData[] {
            new StaticLevelData("windsweptpeaks", new Point(.24, .44), LevelTypeEnum.mission),
            new StaticLevelData("galesanctum", new Point(.62, .15), LevelTypeEnum.mission),
            new StaticLevelData("endlessrampart", new Point(.65, .55), LevelTypeEnum.dungeon),
        };

        public readonly static MapImageData HOWLING_PEAKS_MAP_IMAGE_DATA = new MapImageData
        {
            titleLookupString = "TheHowlingPeaks_name",
            titleBackupText = "R.HOWLING_PEAKS",
            mapImageSourcePath = "/Dungeons/Content/UI/Materials/MissionSelectMap/background/islands/Mountain_base_NOTPOTWO",
            backgroundColor = Colors.LightCyan,
            cropToRect = new Int32Rect(0, 0, 2466, 2414),
            levelData = HOWLING_PEAKS_LEVEL_DATA!,
        };

        public readonly static Dictionary<string, StaticLevelData> LEVEL_DATA_LOOKUP =
            MAINLAND_LEVEL_DATA
            .Concat(JUNGLE_AWAKENS_LEVEL_DATA)
            .Concat(CREEPING_WINTER_LEVEL_DATA)
            .Concat(HOWLING_PEAKS_LEVEL_DATA)
            .ToDictionary(data => data.key);

        public readonly static List<MapImageData> ALL_MAP_IMAGE_DATA = new List<MapImageData>() {
            MAINLAND_MAP_IMAGE_DATA,
            JUNGLE_AWAKENS_MAP_IMAGE_DATA,
            CREEPING_WINTER_MAP_IMAGE_DATA,
            HOWLING_PEAKS_MAP_IMAGE_DATA,
        };
    }

    #endregion

    #region MapData Structures

    public struct StaticLevelData
    {
        public string key;
        public Point mapPosition;
        public LevelTypeEnum levelType;

        public StaticLevelData(string key, Point mapPosition, LevelTypeEnum levelType)
        {
            this.key = key;
            this.mapPosition = mapPosition;
            this.levelType = levelType;
        }
    }

    public struct MapImageData
    {
        public string titleLookupString;
        public string titleBackupText;
        public string mapImageSourcePath;
        public Color backgroundColor;
        public Int32Rect? cropToRect;
        public StaticLevelData[] levelData;

        public BitmapSource? mapImageSource;

        public string title()
        {
            return "Title";
        }

        public void preload()
        {
            //this.mapImageSource = AppModel.instance.imageSource(mapImageSourcePath);
        }

        public ImageSource? usableImageSource()
        {
            if (this.mapImageSource == null)
            {
                preload();
            }

            if (this.mapImageSource != null && this.cropToRect.HasValue)
            {
                return new CroppedBitmap(this.mapImageSource!, this.cropToRect.Value);
            }
            else
            {
                return this.mapImageSource;
            }
        }
    }

    #endregion
}
