using MCDStorageChest.Json.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCDStorageChest.Extensions;
using MCDStorageChest.Classes;
using MCDStorageChest.Csv;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace MCDStorageChest.Models
{
    public class ExtraDataLibrary
    {
        private static CaseInSensitiveDictionary<string, Enchantment[]> _builtInEnchants = new CaseInSensitiveDictionary<string, Enchantment[]>();

        private static CaseInSensitiveDictionary<string, RuneData> _itemArchetypes = new CaseInSensitiveDictionary<string, RuneData>();

        static ExtraDataLibrary()
        {
            Init();
        }

        private static void Init()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "ItemTable.csv");
            if (File.Exists(fileName))
            {
                try
                {
                    using (var reader = new StreamReader(fileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        foreach (var entry in csv.GetRecords<ExtraDataEntry>())
                        {
                            if (!String.IsNullOrEmpty(entry.ITEM_FILENAME))
                            {
                                if (!_itemArchetypes.ContainsKey(entry.ITEM_FILENAME)) _itemArchetypes.Add(entry.ITEM_FILENAME, new RuneData(entry));
                                if (!_builtInEnchants.ContainsKey(entry.ITEM_FILENAME)) _builtInEnchants.Add(entry.ITEM_FILENAME, InitEnchants(entry.ENCHANTMENTS_FILENAME));
                            }
                        }
                    }
                }
                catch
                {

                }

            }


            Enchantment[] InitEnchants(string input)
            {
                List<string> strings = input.Split(';').ToList();
                List<Enchantment> results = new List<Enchantment>();
                foreach (var entry in strings)
                {
                    results.Add(new Enchantment() { Id = entry, Level = 1 });
                }
                return results.ToArray();
            }
        }

        public static Enchantment[] getBuiltInEnchantments(string itemName)
        {
            if (_builtInEnchants.ContainsKey(itemName)) return _builtInEnchants[itemName];
            return new Enchantment[0];
        }

        public static RuneData getItemRunes(string itemName)
        {
            if (_itemArchetypes.ContainsKey(itemName)) return _itemArchetypes[itemName];
            return new RuneData();
        }
    }
}
