using MCDStorageChest.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Logic
{
    public static class SettingMapper
    {
        public static void UpdateRecentDirectoriesLists(IMainViewModel ParentModel, string newPath, bool isStorage)
        {
            if (isStorage)
            {
                Settings.Default.LastStorageDirectory = newPath;

                if (Settings.Default.RecentStorageDirectories == null)
                    Settings.Default.RecentStorageDirectories = new StringCollection();

                while (Settings.Default.RecentStorageDirectories.Contains(newPath)) Settings.Default.RecentStorageDirectories.Remove(newPath);
                Settings.Default.RecentStorageDirectories.Add(newPath);

                if (Settings.Default.RecentStorageDirectories.Count > Constants.MAX_RECENT_FILES)
                    Settings.Default.RecentStorageDirectories.RemoveAt(0);
            }
            else
            {
                Settings.Default.LastSaveGameDirectory = newPath;

                if (Settings.Default.RecentSaveGameDirectories == null)
                    Settings.Default.RecentSaveGameDirectories = new StringCollection();

                while (Settings.Default.RecentSaveGameDirectories.Contains(newPath)) Settings.Default.RecentSaveGameDirectories.Remove(newPath);
                Settings.Default.RecentSaveGameDirectories.Add(newPath);

                if (Settings.Default.RecentSaveGameDirectories.Count > Constants.MAX_RECENT_FILES)
                    Settings.Default.RecentSaveGameDirectories.RemoveAt(0);
            }

            ParentModel.Refresh();

            Settings.Default.Save();
        }
    }
}
