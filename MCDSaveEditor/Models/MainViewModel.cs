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
                        param => this.LoadRecentCommand_Execute(param, false),
                        param => this.LoadRecentCommand_Validate(param)
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
                        param => this.LoadRecentCommand_Execute(param, true),
                        param => this.LoadRecentCommand_Validate(param)
                    );
                }
                return _loadRecentStorageCommand;
            }
        }

        public bool LoadRecentCommand_Validate(object param)
        {
            if ((param is string) && !(param as string).StartsWith(Constants.NO_RECENT_DIRECTORIES_TEXT)) return true;
            else return false;
        }
        public async void LoadRecentCommand_Execute(object param, bool isStorage)
        {
            string directory = (param as string);
            var result = await FileOpenAsync(directory, isStorage);
            if (result) await Parent.RefreshUI();
        }

        #endregion

        #region Connections

        private IMainWindow Parent;

        public void Init(IMainWindow parent)
        {
            Parent = parent;
        }

        #endregion

        #region Methods

        public async Task<bool> FileOpenAsync(bool isStorage)
        {
            SaveModel newModel = new SaveModel();
            newModel.Init(this);
            bool result = await newModel.FileOpenAsync(isStorage);
            if (result)
            {
                SaveModelsList.Add(newModel);
                return true;
            }
            else return false;
        }
        public async Task<bool> FileOpenAsync(string path, bool isStorage)
        {
            SaveModel newModel = new SaveModel();
            newModel.Init(this);
            bool result = await newModel.FileOpenAsync(path, isStorage);
            if (result)
            {
                SaveModelsList.Add(newModel);
                return true;
            }
            else return false;
        }
        public async Task FileSaveAsync()
        {
            if (CurrentIndex > -1 && CurrentIndex < SaveModelsList.Count)
            {
                await SaveModelsList[CurrentIndex].FileSaveAsync(SaveModelsList[CurrentIndex].CurrentSaveFilePath, SaveModelsList[CurrentIndex].CurrentSaveFile);
            }
        }
        public async Task<bool> LoadGameDataAsync()
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
        public bool UnloadGameData()
        {
            AssetResolver.FileUnloadGameContent();
            return true;
        }

        #endregion

        #region Interface Implementation

        public bool Command_FileClose_isAllowed(SaveModel save)
        {
            return true;
        }

        public void Command_FileClose(SaveModel save)
        {
            SaveModelsList.Remove(save);
        }

        public int GetIndexOfSave(SaveModel saveModel)
        {
            return SaveModelsList.IndexOf(saveModel);
        }

        public List<SaveModel> GetOtherStorages(SaveModel saveModel)
        {
            var storages = SaveModelsList.ToList();
            storages.Remove(saveModel);
            return storages;
        }

        public void AddItemToSave(int index, Item item)
        {
            SaveModelsList[index].CurrentSaveFile.Items.Add(item);
        }

        public int GetSaveCount()
        {
            return SaveModelsList.Count();
        }

        public void SyncRecentDirectoryLists()
        {
            var savegames = Properties.Settings.Default.RecentSaveGameDirectories;
            var storages = Properties.Settings.Default.RecentStorageDirectories;

            if (savegames != null) RecentSaveGameDirectories = new ObservableCollection<string>(savegames.Cast<string>().ToList());
            else RecentSaveGameDirectories = null;


            if (storages != null) RecentStorageDirectories = new ObservableCollection<string>(storages.Cast<string>().ToList());
            else RecentStorageDirectories = null;
        }

        #endregion
    }
}
