using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Save.Profiles
{
    public class EnchantmentCollection : ObservableCollection<Enchantment>
    {
        public EnchantmentCollection() : base()
        {

        }

        public EnchantmentCollection(IEnumerable<Enchantment> enchantments) : base(enchantments)
        {

        }

        public Enchantment Slot1A
        {
            get
            {
                int slot = 1;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot1B
        {
            get
            {
                int slot = 2;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot1C
        {
            get
            {
                int slot = 3;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }

        public Enchantment Slot2A
        {
            get
            {
                int slot = 4;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot2B
        {
            get
            {
                int slot = 5;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot2C
        {
            get
            {
                int slot = 6;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }

        public Enchantment Slot3A
        {
            get
            {
                int slot = 7;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot3B
        {
            get
            {
                int slot = 8;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }
        public Enchantment Slot3C
        {
            get
            {
                int slot = 9;
                if (this.Count >= slot) return this[slot - 1];
                else return null;
            }
        }

        public Enchantment ChosenSlot1
        {
            get
            {
                long levelA = Slot1A?.Level ?? 0;
                long levelB = Slot1B?.Level ?? 0;
                long levelC = Slot1C?.Level ?? 0;

                if (levelA != 0) return Slot1A;
                else if (levelB != 0) return Slot1B;
                else if (levelC != 0) return Slot1C;
                else return null;
            }
        }
        public Enchantment ChosenSlot2
        {
            get
            {
                long levelA = Slot2A?.Level ?? 0;
                long levelB = Slot2B?.Level ?? 0;
                long levelC = Slot2C?.Level ?? 0;

                if (levelA != 0) return Slot2A;
                else if (levelB != 0) return Slot2B;
                else if (levelC != 0) return Slot2C;
                else return null;
            }
        }
        public Enchantment ChosenSlot3
        {
            get
            {
                long levelA = Slot3A?.Level ?? 0;
                long levelB = Slot3B?.Level ?? 0;
                long levelC = Slot3C?.Level ?? 0;

                if (levelA != 0) return Slot3A;
                else if (levelB != 0) return Slot3B;
                else if (levelC != 0) return Slot3C;
                else return null;
            }
        }

        public bool Slot1IsUnset
        {
            get
            {
                return ChosenSlot1 == null;
            }
        }
        public bool Slot2IsUnset
        {
            get
            {
                return ChosenSlot2 == null;
            }
        }
        public bool Slot3IsUnset
        {
            get
            {
                return ChosenSlot3 == null;
            }
        }

        public bool Slot1IsSet
        {
            get
            {
                return ChosenSlot1 != null;
            }
        }
        public bool Slot2IsSet
        {
            get
            {
                return ChosenSlot2 != null;
            }
        }
        public bool Slot3IsSet
        {
            get
            {
                return ChosenSlot3 != null;
            }
        }

    }
}
