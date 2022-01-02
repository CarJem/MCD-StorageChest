using System;
using System.Collections.Generic;
using System.Linq;
using MCDStorageChest.Extensions;
using MCDStorageChest.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCDStorageChest.Models
{
    public class StringLibrary : Properties.Resources
    {
        private static readonly CaseInSensitiveDictionary<string, string> Mismatches = new CaseInSensitiveDictionary<string, string>() {
            //Armor Properties
            { "ItemCooldownDecrease","ArtifactCooldownDecrease" },
            { "ItemDamageBoost","ArtifactDamageBoost" },
            { "SlowResistance","FreezingResistance" },
            //{ "ReviveChance","Unset" }, //Missing

            //Item Names
            { "CorruptedSeeds","CorruptedSeeds_Unique1" },
            { "Pickaxe_Steel","Pickaxe" },
            { "Pickaxe_Unique1_Steel","Pickaxe_Unique1" },
            { "Sword_Steel","Sword" },
            { "EmeraldArmor_Unique1","Emerald_Armor_Unique1" },
            { "EmeraldArmor_Unique2","Emerald_Armor_Unique2" },
            //{ "HighlanderLongSword","Unset" }, //Missing

            //Enchantment Names
            { "Accelerating","Accelerate" },
            { "AnimaConduitMelee","Anima" },
            { "AnimaConduitRanged","Anima" },
            { "Shockwave","Shock" },
            { "SoulSiphon","Soul" },
            { "TempoTheft","Tempo" },
            { "CriticalHit","Critical" },
            { "PoisonedMelee","Poisoned" },
            { "PoisonedRanged","Poisoned" },
            { "JunglePoisonRanged","JunglePoison" },
            { "Celerity","Cool Down" },
            { "Deflecting","Deflect" },
            { "EnigmaResonatorMelee","EnigmaMelee" },
            { "EnigmaResonatorRanged","EnigmaRanged" },
            { "VoidTouchedMelee","VoidStrikeMelee" },
            { "VoidTouchedRanged","VoidStrikeRanged" },
            { "FireAspect","Fire" },
            { "MultiShot","Multi" },
        };

        public static readonly CaseInSensitiveDictionary<string, string> ImageMismatches = new CaseInSensitiveDictionary<string, string>() {
            //{"Imagefile foldername","Savefile reference"},
            { "Sword_Steel","Sword" },
            { "Pickaxe_Steel","Pickaxe" },
            { "Pickaxe_Unique1_Steel","Pickaxe_Unique1" },
        };


        private static void MergeDictonary(ref CaseInSensitiveDictionary<string, string> dictionaryFrom, CaseInSensitiveDictionary<string, string> dictionaryTo)
        {
            dictionaryFrom.ToList().ForEach(x => dictionaryTo.Add(x.Key, x.Value));
        }

        private static CaseInSensitiveDictionary<string, string> _itemType = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _enchantment = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _armorProperties = new CaseInSensitiveDictionary<string, string>();
        private static CaseInSensitiveDictionary<string, string> _mission = new CaseInSensitiveDictionary<string, string>();

        public static bool isStringsLoaded { get; private set; } = false;

        public static void loadExternalStrings(Dictionary<string, Dictionary<string, string>> stringLibrary)
        {
            if (stringLibrary.TryGetValue("ItemType", out var itemDict))
            {
                _itemType = new CaseInSensitiveDictionary<string, string>(itemDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));
            }
            if (stringLibrary.TryGetValue("Enchantment", out var enchantmentDict))
            {
                _enchantment = new CaseInSensitiveDictionary<string, string>(enchantmentDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));
            }
            if (stringLibrary.TryGetValue("ArmorProperties", out var armorPropertyDict))
            {
                _armorProperties = new CaseInSensitiveDictionary<string, string>(armorPropertyDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));
            }
            if (stringLibrary.TryGetValue("Mission", out var missionDict))
            {
                _mission = new CaseInSensitiveDictionary<string, string>(missionDict.ToDictionary(pair => pair.Key.Trim(), pair => pair.Value));
            }
            isStringsLoaded = true;
        }

        public static string itemName(string type)
        {
            return getItemString(type) ?? type;
        }
        public static string itemDesc(string type)
        {
            var attempt1 = getItemString("Flavour_" + type);
            if (attempt1 != null) return attempt1;
            else return getItemString("Desc_" + type) ?? type;
        }
        private static string getItemString(string key)
        {
            if (!isStringsLoaded) { return key; }
            if (Mismatches.ContainsKey(key))
            {
                key = Mismatches[key];
            }
            if (_itemType.TryGetValue(key, out string value))
            {
                return value;
            }
            EventLogger.logError($"Could not find string for item {key}");
            return null;
        }     
        public static string enchantmentName(string enchantment)
        {
            var key = enchantment;
            if (!isStringsLoaded) { return key; }
            if (Mismatches.ContainsKey(key))
            {
                key = Mismatches[key];
            }
            if (_enchantment.TryGetValue(key, out string value))
            {
                if(enchantment.ToLowerInvariant().Contains("ranged"))
                {
                    return $"{value} ({StringLibrary.RANGED_ITEMS_FILTER})";
                }
                else if (enchantment.ToLowerInvariant().Contains("melee"))
                {
                    return $"{value} ({StringLibrary.MELEE_ITEMS_FILTER})";
                }
                else
                {
                    return value;
                }
            }
            EventLogger.logError($"Could not find string for enchantment {key}");
            return enchantment;
        }
        public static string enchantmentDescription(string enchantment)
        {
            var key = enchantment + "_desc";
            return getEnchantmentString(key) ?? enchantment;
        }
        public static string enchantmentEffect(string enchantment)
        {
            var key = enchantment + "_effect";
            return getEnchantmentString(key) ?? enchantment;
        }
        private static string getEnchantmentString(string key)
        {
            if (!isStringsLoaded) { return key; }
            if (Mismatches.ContainsKey(key))
            {
                key = Mismatches[key];
            }
            if (_enchantment.TryGetValue(key, out string value))
            {
                return value;
            }
            EventLogger.logError($"Could not find string for enchantment {key}");
            return null;
        }
        public static string armorProperty(string armorPropertyId)
        {
            return getArmorPropertyString(armorPropertyId) ?? armorPropertyId;
        }
        public static string armorPropertyDescription(string armorPropertyId)
        {
            var key = armorPropertyId + "_description";
            return getArmorPropertyString(key) ?? armorPropertyId;
        }
        private static string getArmorPropertyString(string key)
        {
            if (!isStringsLoaded) { return key; }
            if (Mismatches.ContainsKey(key))
            {
                key = Mismatches[key];
            }
            if (_armorProperties.TryGetValue(key, out string value))
            {
                return value;
            }
            EventLogger.logError($"Could not find string for armor {key}");
            return null;
        }

        public static void Debug_Export()
        {
            string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "MCDStorageChestStrings.json");

            JObject output = new JObject();
            output.Add("_itemType", JToken.Parse(JsonConvert.SerializeObject(_itemType)));
            output.Add("_enchantment", JToken.Parse(JsonConvert.SerializeObject(_enchantment)));
            output.Add("_armorProperties", JToken.Parse(JsonConvert.SerializeObject(_armorProperties)));
            output.Add("_mission", JToken.Parse(JsonConvert.SerializeObject(_mission)));

            System.IO.File.WriteAllText(fileName, output.ToString());
        }

    }

}

