using MCDStorageChest.Json.Classes;
using MCDStorageChest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest
{
    public interface IMainViewModel
    {



        void FileClose(SaveModel saveModel);

        int IndexOf(SaveModel saveModel);
        int Count();
        void Refresh();

        List<SaveModel> OtherSaves(SaveModel saveModel);
        List<Item> GetClipboard();
        void SetClipboard(List<Item> contents);

        void InsertItem(SaveModel source, int index, Item item);

    }
}
