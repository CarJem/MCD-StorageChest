using MCDStorageChest.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Classes
{
    public class RuneData
    {
        public RuneData() { }

        public RuneData(ExtraDataEntry entry)
        {
            this.HasRune_U = entry.HasRune_U;
            this.HasRune_C = entry.HasRune_C;
            this.HasRune_O = entry.HasRune_O;
            this.HasRune_S = entry.HasRune_S;
            this.HasRune_R = entry.HasRune_R;
            this.HasRune_I = entry.HasRune_I;
            this.HasRune_P = entry.HasRune_P;
            this.HasRune_T = entry.HasRune_T;
            this.HasRune_A = entry.HasRune_A;
        }


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
