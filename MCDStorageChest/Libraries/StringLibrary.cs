using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#nullable enable

namespace MCDStorageChest.Libraries
{
    public class StringLibrary : Properties.Resources
    {
        private static CaseInSensitiveDictionary<string, string> _itemNameMismatches = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _itemDescriptionNameMismatches = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _itemImageNameMismatches = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _armorPropertyNameMismatches = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _enchantmentNameMismatches = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _enchantmentImageNameMismatches = new CaseInSensitiveDictionary<string, string>();

        private static CaseInSensitiveDictionary<string, string> _itemType = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _enchantment = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _armorProperties = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _mission = new CaseInSensitiveDictionary<string, string>();

        public static bool isStringsLoaded { get; private set; } = false;

        #region Mismatches

        private static string correctMismatches(string? key, CaseInSensitiveDictionary<string, string> mismatchMap)
        {
            if (key == null) return string.Empty;
            if (mismatchMap.ContainsKey(key)) key = mismatchMap[key];
            return key;
        }
        public static string handlePakItemMismatches(string itemName, string fullPath, out CaseInSensitiveDictionary<string, string> _equipment)
        {
            _equipment = new CaseInSensitiveDictionary<string, string>();
            if (_itemImageNameMismatches.ContainsKey(itemName))
            {
                var correctedItemName = _itemImageNameMismatches[itemName];
                if (!_equipment.ContainsKey(correctedItemName)) _equipment.Add(correctedItemName, fullPath);
                return correctedItemName;
            }
            else
                return itemName;
        }
        public static string handlePakEnchantmentMismatches(string enchantmentName, string fullPath, out CaseInSensitiveDictionary<string, string> _enchantments)
        {
            _enchantments = new CaseInSensitiveDictionary<string, string>();
            if (!_enchantments.ContainsKey(enchantmentName)) _enchantments.Add(enchantmentName, fullPath);
            if (_enchantmentImageNameMismatches.ContainsKey(enchantmentName))
            {
                var correctedEnchantmentName = _enchantmentImageNameMismatches[enchantmentName];
                if (!_enchantments.ContainsKey(correctedEnchantmentName)) _enchantments.Add(correctedEnchantmentName, fullPath);
                return correctedEnchantmentName;
            }
            else
                return enchantmentName;
        }

        #endregion

        #region Init

        static StringLibrary()
        {
            loadMismatches();
        }
        public static void loadExternalStrings(Dictionary<string, Dictionary<string, string>> stringLibrary)
        {
            if (stringLibrary.TryGetValue("ItemType", out var itemDict))
                _itemType = new CaseInSensitiveDictionary<string, string>(itemDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));

            if (stringLibrary.TryGetValue("Enchantment", out var enchantmentDict))
                _enchantment = new CaseInSensitiveDictionary<string, string>(enchantmentDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));

            if (stringLibrary.TryGetValue("ArmorProperties", out var armorPropertyDict))
                _armorProperties = new CaseInSensitiveDictionary<string, string>(armorPropertyDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));

            if (stringLibrary.TryGetValue("Mission", out var missionDict))
                _mission = new CaseInSensitiveDictionary<string, string>(missionDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));

            isStringsLoaded = true;
        }
        public static void loadMismatches()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "StringMismatches.json");
            if (File.Exists(fileName))
            {
                try
                {
                    string json = File.ReadAllText(fileName);
                    JObject jObject = JObject.Parse(json);

                    _itemNameMismatches = GetProperty(jObject, "itemNames");
                    _itemDescriptionNameMismatches = GetProperty(jObject, "itemDescriptionNames");
                    _itemImageNameMismatches = GetProperty(jObject, "itemImageNames");
                    _armorPropertyNameMismatches = GetProperty(jObject, "armorPropertyNames");
                    _enchantmentNameMismatches = GetProperty(jObject, "enchantmentNames");
                    _enchantmentImageNameMismatches = GetProperty(jObject, "enchantmentImageNames");
                }
                catch (Exception ex)
                {
                    EventLogger.logError($"Error occured while loading StringMismatches.json!\r\nMessage: {ex}");
                }

            }

            CaseInSensitiveDictionary<string, string> GetProperty(JObject source, string propertyName)
            {
                bool propertyFound = source.TryGetValue(propertyName, out JToken? value);
                if (!propertyFound || value == null) return new CaseInSensitiveDictionary<string, string>();
                var result = value.ToObject<CaseInSensitiveDictionary<string, string>>() ?? new CaseInSensitiveDictionary<string, string>();
                return result;
            }
        }

        #endregion

        #region Items

        public static string itemName(string? type)
        {
            type = correctMismatches(type, _itemNameMismatches);
            return getItemString(type) ?? type ?? string.Empty;
        }
        public static string itemDesc(string? type)
        {
            type = correctMismatches(type, _itemDescriptionNameMismatches);

            var attempt1 = getItemString("Flavour_" + type);
            if (attempt1 != null) return attempt1;

            var attempt2 = getItemString("Desc_" + type);
            if (attempt2 != null) return attempt2;

            return type ?? string.Empty;
        }
        private static string getItemString(string? key)
        {
            if (key == null) return string.Empty;
            if (_itemType.TryGetValue(key, out string value)) return value;
            EventLogger.logError($"Could not find string for item {key}");
            return key;
        }

        #endregion

        #region Enchantments

        public static string enchantmentName(string enchantment)
        {
            var key = correctMismatches(enchantment, _enchantmentNameMismatches);
            if (_enchantment.TryGetValue(key, out string value))
            {
                if(enchantment.ToLowerInvariant().Contains("ranged")) return $"{value} ({StringLibrary.RANGED_ITEMS_FILTER})";
                else if (enchantment.ToLowerInvariant().Contains("melee")) return $"{value} ({StringLibrary.MELEE_ITEMS_FILTER})";
                else return value;
            }
            EventLogger.logError($"Could not find string for enchantment {key}");
            return enchantment;
        }
        public static string enchantmentDescription(string enchantment)
        {
            var key = correctMismatches(enchantment, _enchantmentNameMismatches) + "_desc";
            return getEnchantmentString(key) ?? enchantment;
        }
        public static string enchantmentEffect(string enchantment)
        {
            var key = correctMismatches(enchantment, _enchantmentNameMismatches) + "_effect";
            return getEnchantmentString(key) ?? enchantment;
        }
        private static string? getEnchantmentString(string key)
        {
            if (_enchantment.TryGetValue(key, out string value)) return value;
            EventLogger.logError($"Could not find string for enchantment {key}");
            return null;
        }

        #endregion

        #region Armor Properties

        public static string armorProperty(string armorPropertyId)
        {
            var key = correctMismatches(armorPropertyId, _armorPropertyNameMismatches);
            return getArmorPropertyString(key) ?? key;
        }
        public static string armorPropertyDescription(string armorPropertyId)
        {
            var key = correctMismatches(armorPropertyId, _armorPropertyNameMismatches) + "_description";
            return getArmorPropertyString(key) ?? armorPropertyId;
        }
        private static string? getArmorPropertyString(string key)
        {
            if (_armorProperties.TryGetValue(key, out string value)) return value;
            EventLogger.logError($"Could not find string for armor {key}");
            return null;
        }

        #endregion

    }

}

