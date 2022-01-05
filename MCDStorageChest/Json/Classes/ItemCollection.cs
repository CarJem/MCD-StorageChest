using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Json.Classes
{

    [Serializable]
    public class ItemCollection : ObservableCollection<Item>
    {
        public new void Add(Item item)
        {
            base.Add(item);
            item.InventoryIndex = base.IndexOf(item);
        }
    }
}
