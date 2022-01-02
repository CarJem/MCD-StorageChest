using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Csv
{
    public class ExtraDataEntry
    {
        public string ID { get; set; }
        public string ITEM_FILENAME { get; set; }
        public string ENCHANTMENTS_FILENAME { get; set; }
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
