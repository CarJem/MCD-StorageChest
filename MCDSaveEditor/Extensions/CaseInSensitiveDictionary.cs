using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDSaveEditor.Extensions
{
    public class CaseInSensitiveDictionary<TKey, TValue> : Dictionary<string, TValue>
    {
        public CaseInSensitiveDictionary() : base(StringComparer.OrdinalIgnoreCase) { }


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
    }
}
