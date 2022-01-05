using MCDStorageChest.Json.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCDStorageChest.Extensions;
using MCDStorageChest.Classes;
using System.IO;
using CsvHelper;
using System.Globalization;
using MCDStorageChest.Logic;
#nullable enable

namespace MCDStorageChest.Libraries
{
    public class ItemDataLibrary
    {
        private static CaseInSensitiveDictionary<string, Enchantment[]> _builtInEnchants = new CaseInSensitiveDictionary<string, Enchantment[]>();

        private static CaseInSensitiveDictionary<string, RuneData> _itemArchetypes = new CaseInSensitiveDictionary<string, RuneData>();

        static ItemDataLibrary()
        {
            loadItemTable();
        }

        private static void loadItemTable()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "ItemTable.csv");
            if (File.Exists(fileName))
            {
                try
                {
                    using (var reader = new StreamReader(fileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        foreach (var entry in csv.GetRecords<ItemTableEntry>())
                        {
                            if (!String.IsNullOrEmpty(entry.ITEM_FILENAME) && entry.ITEM_FILENAME != null)
                            {
                                if (!_itemArchetypes.ContainsKey(entry.ITEM_FILENAME)) 
                                    _itemArchetypes.Add(entry.ITEM_FILENAME, new RuneData(entry));
                                if (!_builtInEnchants.ContainsKey(entry.ITEM_FILENAME)) 
                                    _builtInEnchants.Add(entry.ITEM_FILENAME, InitEnchants(entry.ENCHANTMENTS_FILENAME));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    EventLogger.logError($"Error occured while loading ItemTable.csv!\r\nMessage: {ex}");
                }

            }


            Enchantment[] InitEnchants(string? input)
            {
                if (input == null) return new Enchantment[0];
                List<string> strings = input.Split(';').ToList();
                List<Enchantment> results = new List<Enchantment>();
                foreach (var entry in strings)
                {
                    results.Add(new Enchantment() { Id = entry, Level = 1 });
                }
                return results.ToArray();
            }
        }
        public static Enchantment[] getBuiltInEnchantments(string? itemName)
        {
            if (itemName == null) return new Enchantment[0];
            if (_builtInEnchants.ContainsKey(itemName)) return _builtInEnchants[itemName];
            return new Enchantment[0];
        }
        public static RuneData getItemRunes(string? itemName)
        {
            if (itemName == null) return new RuneData();
            if (_itemArchetypes.ContainsKey(itemName)) return _itemArchetypes[itemName];
            return new RuneData();
        }
    }

    public class ItemTableEntry
    {
        public string? ID { get; set; }
        public string? ITEM_FILENAME { get; set; }
        public string? ENCHANTMENTS_FILENAME { get; set; }
        public bool HasRune_U { get; set; } = false;
        public bool HasRune_C { get; set; } = false;
        public bool HasRune_O { get; set; } = false;
        public bool HasRune_S { get; set; } = false;
        public bool HasRune_R { get; set; } = false;
        public bool HasRune_I { get; set; } = false;
        public bool HasRune_P { get; set; } = false;
        public bool HasRune_T { get; set; } = false;
        public bool HasRune_A { get; set; } = false;
    }
}
