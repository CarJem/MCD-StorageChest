using MCDStorageChest.Json.Enums;
using MCDStorageChest.Json.Classes;
using PakReader.Pak;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using MCDStorageChest.Extensions;
using MCDStorageChest.Models;
using System.Runtime.Serialization;
using MCDStorageChest.Libraries;
using System;
#nullable enable

namespace MCDStorageChest.Logic.ImageResolver
{
    public class PakImageResolver : IImageResolver
    {


        private readonly LocalImageResolver _backupResolver;
        private readonly PakIndex _pakIndex;
        private readonly CaseInSensitiveDictionary<string, string> _enchantments = new CaseInSensitiveDictionary<string, string>();
        private readonly CaseInSensitiveDictionary<string, string> _equipment = new CaseInSensitiveDictionary<string, string>();
        private readonly CaseInSensitiveDictionary<string, BitmapImage> _bitmaps = new CaseInSensitiveDictionary<string, BitmapImage>();
        private readonly List<string> _levels = new List<string>();
        public string path { get; private set; }



        public PakImageResolver(PakIndex pakIndex, string path)
        {
            this.path = path;
            _pakIndex = pakIndex;
            _backupResolver = new LocalImageResolver();
        }

        #region Loading

        public Task loadPakFilesAsync(bool preloadBitmaps = false)
        {
            var tcs = new TaskCompletionSource<object>();
            Task.Run(() => {
                this.loadPakFiles(preloadBitmaps);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                tcs.SetResult(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });
            return tcs.Task;
        }
        public void loadPakFiles(bool preloadBitmaps = false)
        {
            Debug.WriteLine($"Loading Pak Files");
            foreach (var item in _pakIndex)
            {
                if (item == null) continue;
                //Drop the mount point prefix
                var startIndex = item.IndexOf("//") + 1;
                var fullPath = item.Substring(startIndex);

                if (fullPath.Contains("Localization") && fullPath.EndsWith("Game"))
                {
                    //Get the folder name because it will be the language specifier
                    string lang = Path.GetDirectoryName(fullPath).Split(Path.DirectorySeparatorChar).Last();
                    //Handle english language strings
                    if (lang == "en")
                    {
                        var stringLibrary = _pakIndex.extractLocResFile(fullPath);
                        if (stringLibrary != null)
                        {
                            StringLibrary.loadExternalStrings(stringLibrary);
                            long totalStringCount = stringLibrary.Sum(pair => pair.Value.LongCount());
                            Debug.WriteLine($"Loaded {totalStringCount} {lang} LocRes");
                        }
                    }
                    continue;
                }

                if (fullPath.Contains("ArmorProperties") && !fullPath.Contains("Cues"))
                {
                    //Get the folder name because it will be the name of the armor property
                    string armorProperty = Path.GetDirectoryName(fullPath).Split(Path.DirectorySeparatorChar).Last();
                    if (armorProperty == "ArmorProperties" || armorProperty == "ReviveChance") { continue; } //Exception
                    TypeLibrary.ArmorProperties.Add(armorProperty);
                    //Debug.WriteLine($"Found ArmorProperty {armorProperty} in: {fullPath}");
                    continue;
                }

                var filename = Path.GetFileName(fullPath);

                if (fullPath.Contains("data") && fullPath.Contains("levels"))
                {
                    _levels.Add(filename);
                    //Debug.WriteLine($"Found level {filename}");
                    continue;
                }

                if (!filename.StartsWith("T") || !fullPath.Contains("_Icon"))
                {
                    //Debug.WriteLine($"Package is not an icon {fullPath}");
                    continue;
                }

                if (preloadBitmaps)
                {
                    var bitmap = _pakIndex.extractBitmap(fullPath);
                    if (bitmap != null)
                    {
                        _bitmaps[fullPath] = bitmap;
                    }
                }

                var foldername = Path.GetDirectoryName(fullPath).Split(Path.DirectorySeparatorChar).Last();
                if (fullPath.Contains("Enchantments") && fullPath.EndsWith("_Icon"))
                {
                    var enchantmentName = foldername;
                    if (enchantmentName.EndsWith("Shine")) continue;
                    if (!_enchantments.ContainsKey(enchantmentName))
                    {
                        var correctedEnchantmentName = handleEnchantmentExceptions(enchantmentName, fullPath);
                        TypeLibrary.AllEnchantments.Add(correctedEnchantmentName);
                        //Debug.WriteLine($"{enchantmentName} - {fullPath}");
                    }
                    continue;
                }

                if (fullPath.EndsWith("_Icon_inventory") || fullPath.EndsWith("_Icon_Inventory"))
                {
                    var itemName = foldername;
                    if (!_equipment.ContainsKey(itemName))
                    {
                        _equipment.Add(itemName, fullPath);
                        if (!itemName.StartsWith("MysteryBox")) TypeLibrary.Items_All.Add(itemName);
                        //Debug.WriteLine($"{itemName} - {fullPath}");
                    }

                    if (fullPath.Contains("Equipment") && fullPath.Contains("MeleeWeapons"))
                    {
                        var correctedItemName = handleItemExceptions(itemName, fullPath);
                        TypeLibrary.Items_MeleeWeapons.Add(correctedItemName);
                    }

                    if (fullPath.Contains("Equipment") && fullPath.Contains("RangedWeapons"))
                    {
                        var correctedItemName = handleItemExceptions(itemName, fullPath);
                        TypeLibrary.Items_RangedWeapons.Add(correctedItemName);
                    }
                    if (fullPath.Contains("Equipment") && fullPath.Contains("Armor"))
                    {
                        var correctedItemName = handleItemExceptions(itemName, fullPath);
                        TypeLibrary.Items_Armor.Add(correctedItemName);
                    }
                    if (fullPath.Contains("Items"))
                    {
                        if (!itemName.StartsWith("MysteryBox"))
                        {
                            var correctedItemName = handleItemExceptions(itemName, fullPath);
                            TypeLibrary.Items_Artifacts.Add(correctedItemName);
                        }
                    }
                }
            }

            Debug.WriteLine($"Found {_levels.Count()} levels");
            Debug.WriteLine($"Found {TypeLibrary.ArmorProperties.Count()} armor properties");
            Debug.WriteLine($"Loaded {_equipment.Count()} equipment images");
            Debug.WriteLine($"Loaded {_enchantments.Count()} enchantment images");
            if (preloadBitmaps)
            {
                Debug.WriteLine($"Preloaded {_bitmaps.Count()} bitmaps");
            }
        }

        #endregion

        #region Exception Handling

        private string handleItemExceptions(string itemName, string fullPath)
        {
            string correctedItemName = StringLibrary.handlePakItemMismatches(itemName, fullPath, out CaseInSensitiveDictionary<string, string> results);
            foreach (var entry in results) _equipment.Add(entry.Key, entry.Value);
            return correctedItemName;
        }

        private string handleEnchantmentExceptions(string enchantmentName, string fullPath)
        {
            string correctedEnchantmentName = StringLibrary.handlePakEnchantmentMismatches(enchantmentName, fullPath, out CaseInSensitiveDictionary<string, string> results);
            foreach (var entry in results) _enchantments.Add(entry.Key, entry.Value);
            return correctedEnchantmentName;
        }

        #endregion

        #region Interface Implementation

        public BitmapImage? imageSource(string pathWithoutExtension)
        {
            var path = pathWithoutExtension;
            if (!_bitmaps.ContainsKey(path))
            {
                var bitmap = _pakIndex.extractBitmap(path);
                if (bitmap == null) return null;
                _bitmaps[path] = bitmap;
            }
            return _bitmaps[path];
        }

        public BitmapImage? imageSourceForItem(Item item)
        {
            return imageSourceForItem(item.Type);
        }

        public BitmapImage? imageSourceForItem(string itemType) {

            if (_equipment.ContainsKey(itemType))
            {
                var image = imageSource(_equipment[itemType]);
                if (image != null) return image;
            }
            EventLogger.logError($"Could not find full path for item {itemType}");
            return _backupResolver.imageSourceForItem(itemType);
        }

        public BitmapImage? imageSourceForRarity(RarityEnum rarity)
        {
            switch (rarity)
            {
                case RarityEnum.Common: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_gear_frame");
                case RarityEnum.Rare: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_rare_frame");
                case RarityEnum.Unique: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_unique_frame");
            }
            return _backupResolver.imageSourceForRarity(rarity);
        }

        public BitmapImage? imageSourceForEnchantment(Enchantment enchantment)
        {
            return imageSourceForEnchantment(enchantment.Id);
        }

        public BitmapImage? imageSourceForEnchantment(string enchantmentId)
        {
            if (enchantmentId == Constants.DEFAULT_ENCHANTMENT_ID)
            {
                var img = imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/marker/locked_node");
                return img;
            }

            if (_enchantments.TryGetValue(enchantmentId, out string fullPath))
            {
                var image = imageSource(fullPath);
                if (image != null)
                {
                    return image;
                }
            }
            EventLogger.logError($"Could not find full path for enchantment {enchantmentId}");
            return _backupResolver.imageSourceForEnchantment(enchantmentId);
        }

        #endregion
    }
}
