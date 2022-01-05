using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Extensions
{
    public class CaseInSensitiveDictionary<TKey, TValue> : Dictionary<string, TValue>
    {
        public CaseInSensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase) { }

        public CaseInSensitiveDictionary(Dictionary<string, TValue> dictionary) : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (var set in dictionary) base.Add(set.Key, set.Value);
        }

        public new bool ContainsKey(string key)
        {
            bool? keyExists;

            var keyString = key as string;
            if (keyString != null)
            {
                // Key is a string.
                // Using string.Equals to perform case insensitive comparison of the dictionary key.
                keyExists = this.Keys.OfType<string>().Any(k => string.Equals(k, keyString, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {
                // Key is any other type, use default comparison.
                keyExists = this.ContainsKey(key);
            }

            return keyExists ?? false;
        }

        public new bool TryGetValue(string key, out TValue value)
        {
            bool result;
            if (ContainsKey(key))
            {
                result = true;
                value = this[key];
            }
            else
            {
                result = false;
                value = default(TValue);
            }
            return result;
        }

        private static void MergeDictonary(ref CaseInSensitiveDictionary<string, string> dictionaryFrom, CaseInSensitiveDictionary<string, string> dictionaryTo)
        {
            dictionaryFrom.ToList().ForEach(x => dictionaryTo.Add(x.Key, x.Value));
        }

    }
}
