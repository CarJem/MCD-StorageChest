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
        void Command_FileClose(SaveModel saveModel);
        bool Command_FileClose_isAllowed(SaveModel saveModel);
        int GetIndexOfSave(SaveModel saveModel);
        List<SaveModel> GetOtherStorages(SaveModel saveModel);
        void AddItemToSave(int index, Item item);
        int GetSaveCount();
        void SyncRecentDirectoryLists();
    }
}
