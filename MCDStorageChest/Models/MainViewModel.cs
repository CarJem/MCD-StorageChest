using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Patterns.Model;
using System.Windows.Input;
using MCDStorageChest.Extensions;
using MCDStorageChest.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using MCDStorageChest.Logic;
using MCDStorageChest.Json.Classes;
using MCDStorageChest.Properties;
#nullable enable

namespace MCDStorageChest.Models
{
    [NotifyPropertyChanged]
    public class MainViewModel : IMainViewModel
    {
        #region Properties

        public ObservableCollection<SaveModel> SaveModelsList { get; set; } = new ObservableCollection<SaveModel>();
        public int CurrentIndex { get; set; } = 0;
        public ObservableCollection<string> RecentStorageDirectories { get; set; } = null;
        public ObservableCollection<string> RecentSaveGameDirectories { get; set; } = null;

        #endregion

        #region Commands

        private ICommand _loadRecentSaveGameCommand; 
        private ICommand _loadRecentStorageCommand;

        [SafeForDependencyAnalysis]
        public ICommand LoadRecentSaveGameCommand
        {
            get
            {
                if (_loadRecentSaveGameCommand == null)
                {
                    _loadRecentSaveGameCommand = new RelayCommand(
                        param => this.OpenFromDirectory(param.ToString(), false),
                        param => this.OpenFromDirectoryCheck(param)
                    );
                }
                return _loadRecentSaveGameCommand;
            }
        }
        
        [SafeForDependencyAnalysis]
        public ICommand LoadRecentStorageCommand
        {
            get
            {
                if (_loadRecentStorageCommand == null)
                {
                    _loadRecentStorageCommand = new RelayCommand(
                        param => this.OpenFromDirectory(param.ToString(), true),
                        param => this.OpenFromDirectoryCheck(param)
                    );
                }
                return _loadRecentStorageCommand;
            }
        }

        public bool OpenFromDirectoryCheck(object param)
        {
            if ((param is string) && !(param as string).StartsWith(Constants.NO_RECENT_DIRECTORIES_TEXT)) return true;
            else return false;
        }
        public void OpenFromDirectory(string directory, bool isStorage)
        {
            Parent.Dispatcher.Invoke(async () =>
            {
                var result = await FileOpenAsync(directory, isStorage);
                if (result) await Parent.RefreshUI();
            });

        }

        #endregion

        #region Connector

        private IMainWindow Parent;
        public void Init(IMainWindow parent)
        {
            Parent = parent;
        }

        #endregion

        #region IO Methods

        public async Task<bool> FileOpenAsync(bool isStorage)
        {
            string path = isStorage ? Settings.Default.LastStorageDirectory : Settings.Default.LastSaveGameDirectory;
            return await FileOpenAsync(path, isStorage);

        }
        public async Task<bool> FileOpenAsync(string path, bool isStorage)
        {
            SaveModel newModel = new SaveModel();
            newModel.Init(this);
            bool result = await newModel.OpenAsync(path, isStorage);
            if (result)
            {
                SaveModelsList.Add(newModel);
                return true;
            }
            else return false;
        }

        public async Task FileSaveAsync()
        {
            if (CurrentIndex > -1 && CurrentIndex < SaveModelsList.Count) await SaveModelsList[CurrentIndex].SaveAsync();
        }
        public async Task FileSaveAsAsync()
        {
            if (CurrentIndex > -1 && CurrentIndex < SaveModelsList.Count) await SaveModelsList[CurrentIndex].SaveAsAsync();
        }

        public async Task<bool> FileLoadDataAsync()
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog()
            {
                InitialDirectory = Properties.Settings.Default.PakFileLocation,
                IsFolderPicker = true
            };

            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.PakFileLocation = cofd.FileName;
                Properties.Settings.Default.Save();
                await AssetResolver.FileLoadGameContent(Properties.Settings.Default.PakFileLocation);
                return true;
            }
            else return false;

        }
        public bool FileUnloadDataAsync()
        {
            AssetResolver.FileUnloadGameContent();
            return true;
        }

        public void FileClose(SaveModel save)
        {
            SaveModelsList.Remove(save);
        }

        #endregion

        #region Indexing Methods

        public int IndexOf(SaveModel saveModel)
        {
            return SaveModelsList.IndexOf(saveModel);
        }
        public int Count()
        {
            return SaveModelsList.Count();
        }

        #endregion

        #region Listing Methods

        public List<SaveModel> OtherSaves(SaveModel saveModel)
        {
            var storages = SaveModelsList.ToList();
            storages.Remove(saveModel);
            return storages;
        }

        #endregion

        #region Action Methods

        public void InsertItem(SaveModel Source, int index, Item item)
        {
            if (this.Count() > index && index > -1)
            {
                SaveModelsList[index].CurrentSaveFile.Items.Add(item);
            }
        }
        public void Refresh()
        {
            var savegames = Properties.Settings.Default.RecentSaveGameDirectories;
            var storages = Properties.Settings.Default.RecentStorageDirectories;

            if (savegames != null) RecentSaveGameDirectories = new ObservableCollection<string>(savegames.Cast<string>().Reverse().ToList());
            else RecentSaveGameDirectories = null;

            if (storages != null) RecentStorageDirectories = new ObservableCollection<string>(storages.Cast<string>().Reverse().ToList());
            else RecentStorageDirectories = null;
        }

        #endregion
    }
}
