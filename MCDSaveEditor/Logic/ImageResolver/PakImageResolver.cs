using MCDStorageChest.Save.Enums;
using MCDStorageChest.Save.Profiles;
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
using System;

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

        public Task loadPakFilesAsync(bool preloadBitmaps = false)
        {
            var tcs = new TaskCompletionSource<object>();
            Task.Run(() => {
                this.loadPakFiles(preloadBitmaps);
                tcs.SetResult(null);
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
                var startIndex = item!.IndexOf("//") + 1;
                var fullPath = item!.Substring(startIndex);

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
                    Models.TypeLibrary.ArmorProperties.Add(armorProperty);
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
                        _enchantments.Add(enchantmentName, fullPath);
                        //Handle exceptions
                        if (StringLibrary.ImageMismatches.ContainsKey(enchantmentName))
                        {
                            var correctedEnchantmentName = StringLibrary.ImageMismatches[enchantmentName];
                            _enchantments.Add(correctedEnchantmentName, fullPath);
                            Models.TypeLibrary.AllEnchantments.Add(correctedEnchantmentName);
                        }
                        else
                        {
                            Models.TypeLibrary.AllEnchantments.Add(enchantmentName);
                        }
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
                        if (!itemName.StartsWith("MysteryBox"))
                        {
                            Models.TypeLibrary.Items_All.Add(itemName);
                        }
                        //Debug.WriteLine($"{itemName} - {fullPath}");
                    }

                    if (fullPath.Contains("Equipment") && fullPath.Contains("MeleeWeapons"))
                    {
                        //Handle exceptions
                        if (StringLibrary.ImageMismatches.ContainsKey(itemName))
                        {
                            var correctedItemName = StringLibrary.ImageMismatches[itemName];
                            if (!_equipment.ContainsKey(correctedItemName)) _equipment.Add(correctedItemName, fullPath);
                            Models.TypeLibrary.Items_MeleeWeapons.Add(correctedItemName);
                        }
                        else
                        {
                            Models.TypeLibrary.Items_MeleeWeapons.Add(itemName);
                        }
                    }

                    if (fullPath.Contains("Equipment") && fullPath.Contains("RangedWeapons"))
                    {
                        //Handle exceptions
                        if (StringLibrary.ImageMismatches.ContainsKey(itemName))
                        {
                            var correctedItemName = StringLibrary.ImageMismatches[itemName];
                            if (!_equipment.ContainsKey(correctedItemName)) _equipment.Add(correctedItemName, fullPath);
                            Models.TypeLibrary.Items_RangedWeapons.Add(correctedItemName);
                        }
                        else
                        {
                            Models.TypeLibrary.Items_RangedWeapons.Add(itemName);
                        }
                    }
                    if (fullPath.Contains("Equipment") && fullPath.Contains("Armor"))
                    {
                        //Handle exceptions
                        if (StringLibrary.ImageMismatches.ContainsKey(itemName))
                        {
                            var correctedItemName = StringLibrary.ImageMismatches[itemName];
                            if (!_equipment.ContainsKey(correctedItemName)) _equipment.Add(correctedItemName, fullPath);
                            Models.TypeLibrary.Items_Armor.Add(correctedItemName);
                        }
                        else
                        {
                            Models.TypeLibrary.Items_Armor.Add(itemName);
                        }
                    }
                    if (fullPath.Contains("Items"))
                    {
                        if (!itemName.StartsWith("MysteryBox"))
                        {
                            if (StringLibrary.ImageMismatches.ContainsKey(itemName))
                            {
                                var correctedItemName = StringLibrary.ImageMismatches[itemName];
                                if (!_equipment.ContainsKey(correctedItemName)) _equipment.Add(correctedItemName, fullPath);
                                Models.TypeLibrary.Items_Artifacts.Add(correctedItemName);
                            }
                            else
                            {
                                Models.TypeLibrary.Items_Artifacts.Add(itemName);
                            }
                        }
                    }
                }
            }

            Debug.WriteLine($"Found {_levels.Count()} levels");
            Debug.WriteLine($"Found {Models.TypeLibrary.ArmorProperties.Count()} armor properties");
            Debug.WriteLine($"Loaded {_equipment.Count()} equipment images");
            Debug.WriteLine($"Loaded {_enchantments.Count()} enchantment images");
            if (preloadBitmaps)
            {
                Debug.WriteLine($"Preloaded {_bitmaps.Count()} bitmaps");
            }
        }

        public BitmapImage imageSource(string pathWithoutExtension)
        {
            var path = pathWithoutExtension;
            if (!_bitmaps.ContainsKey(path))
            {
                var bitmap = _pakIndex.extractBitmap(path);
                if (bitmap == null) return null;
                _bitmaps[path] = bitmap!;
            }
            return _bitmaps[path];
        }

        public BitmapImage imageSourceForItem(Item item)
        {
            return imageSourceForItem(item.Type);
        }

        public BitmapImage imageSourceForItem(string itemType) {
            if (_equipment.TryGetValue(itemType, out string fullPath))
            {
                var image = imageSource(fullPath);
                if (image != null)
                {
                    return image;
                }
            }
            EventLogger.logError($"Could not find full path for item {itemType}");
            return _backupResolver.imageSourceForItem(itemType);
        }

        public BitmapImage imageSourceForRarity(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Common: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_gear_frame");
                case Rarity.Rare: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_rare_frame");
                case Rarity.Unique: return imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/inspector/loot/drops_unique_frame");
            }
            return _backupResolver.imageSourceForRarity(rarity);
        }

        public BitmapImage imageSourceForEnchantment(Enchantment enchantment)
        {
            return imageSourceForEnchantment(enchantment.Id);
        }

        public BitmapImage imageSourceForEnchantment(string enchantment)
        {
            var enchantmentId = enchantment;
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
    }
}
