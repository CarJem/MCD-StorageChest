using DungeonTools.Save.File;
using MCDStorageChest.Json.Classes;
using MCDStorageChest.Json;
using MCDStorageChest.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using PostSharp.Patterns.Model;
using System.Windows.Input;
using MCDStorageChest.Properties;
using System.Collections.Specialized;
#nullable disable

namespace MCDStorageChest.Models
{
    [NotifyPropertyChanged]
    public class SaveModel : INotifyPropertyChanged
    {

        #region Commands

        private ICommand _closeCommand;
        private ICommand _moveToCommand;

        [SafeForDependencyAnalysis]
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(
                        param => this.ParentModel.Command_FileClose(this),
                        param => this.ParentModel.Command_FileClose_isAllowed(this)
                    );
                }
                return _closeCommand;
            }
        }

        [SafeForDependencyAnalysis]
        public ICommand MoveToCommand
        {
            get
            {
                if (_moveToCommand == null)
                {
                    _moveToCommand = new RelayCommand(
                        param => this.Command_MoveTo(param),
                        param => this.Command_MoveTo_isAllowed(this)
                    );
                }
                return _moveToCommand;
            }
        }
        public bool Command_MoveTo_isAllowed(object param)
        {
            if (ParentModel != null && param != null && (param is SaveModel)) return true;
            else return false;
        }
        public void Command_MoveTo(object param)
        {
            var moveToIndex = ParentModel.GetIndexOfSave((param as SaveModel));
            MoveItem(moveToIndex, CurrentItem);
        }

        #endregion

        #region Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ListUpdated;

        private void OnListUpdated()
        {
            ListUpdated?.Invoke(this, EventArgs.Empty);
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Connectors
        private IMainViewModel ParentModel { get; set; }
        private bool isParentModelLoaded = false;
        public void Init(IMainViewModel _ModelRef)
        {
            if (!isParentModelLoaded)
            {
                ParentModel = _ModelRef;
                isParentModelLoaded = true;
            }
        }
        #endregion

        #region Read Only Properties

        public string TabTitle
        {
            get
            {
                string fileType = IsStorage ? "Storage" : "Save";
                string fileName = Path.GetFileName(CurrentSaveFilePath);
                return string.Format("{0} ({1})", fileType, fileName);
            }
        }

        [SafeForDependencyAnalysis]
        public List<SaveModel> OtherStorages
        {
            get
            {
                if (ParentModel != null) return ParentModel.GetOtherStorages(this);
                else return new List<SaveModel>();
            }
        }

        #endregion

        #region Properties

        private ProfileSaveFile _CurrentSaveFile;
        private Item _CurrentItem;
        private Json.Enums.ItemFilterEnum _CurrentFiler = Json.Enums.ItemFilterEnum.All;
        private string _CurrentSaveFilePath = string.Empty;
        public bool _isStorage = false;
        private Models.SearchModel _SearchSettings = new SearchModel();


        public ProfileSaveFile CurrentSaveFile
        {
            get { return _CurrentSaveFile; }
            private set { _CurrentSaveFile = value; OnPropertyChanged(nameof(CurrentSaveFile)); }
        }

        public Models.SearchModel SearchSettings
        {
            get { return _SearchSettings; }
            set { _SearchSettings = value; OnPropertyChanged(nameof(SearchSettings)); }
        }

        public Item CurrentItem
        {
            get { return _CurrentItem; }
            set { _CurrentItem = value; OnPropertyChanged(nameof(CurrentItem)); }
        }
        public Json.Enums.ItemFilterEnum CurrentFilter
        {
            get { return _CurrentFiler; }
            set { _CurrentFiler = value; OnPropertyChanged(nameof(CurrentFilter)); }
        }
        public string CurrentSaveFilePath
        {
            get { return _CurrentSaveFilePath; }
            private set { _CurrentSaveFilePath = value; OnPropertyChanged(nameof(CurrentSaveFilePath)); OnPropertyChanged(nameof(TabTitle)); }
        }
        public bool IsStorage
        {
            get { return _isStorage; }
            set { _isStorage = value; OnPropertyChanged(nameof(IsStorage)); OnPropertyChanged(nameof(TabTitle)); }
        }

        #endregion

        #region Methods

        private void UpdateRecentDirectoriesLists(string newPath, bool isStorage)
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

            ParentModel.SyncRecentDirectoryLists();

            Settings.Default.Save();
        }
        public async Task<bool> FileOpenAsync(string path, bool isStorage)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = path,
                Filter = "dat files (*.dat)|*.dat|json files (*.json)|*.json|All files (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true
            };
            if (ofd.ShowDialog().Value)
            {
                UpdateRecentDirectoriesLists(Path.GetDirectoryName(ofd.FileName), isStorage);
                IsStorage = isStorage;

                CurrentSaveFile = await Logic.FileLoader.FileOpenAsync(ofd.FileName);
                CurrentSaveFilePath = ofd.FileName;
                OnPropertyChanged(nameof(CurrentSaveFile));
                return true;
            }
            else return false;
        }
        public async Task<bool> FileOpenAsync(bool isStorage)
        {
            string path = isStorage ? Settings.Default.LastStorageDirectory : Settings.Default.LastSaveGameDirectory;
            return await FileOpenAsync(path, isStorage);
        }
        public async Task FileSaveAsync(string filePath, ProfileSaveFile profile)
        {
            var result = MessageBox.Show("Would you like to make a backup", "Saving...", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.None) return;
            if (result == MessageBoxResult.Yes) File.Copy(filePath, Path.ChangeExtension(filePath, Path.GetExtension(filePath) + ".bak"));
            await Logic.FileLoader.FileSaveAsync(filePath, profile);
        }
        public void RequestListUpdate()
        {
            OnListUpdated();
        }
        public void MoveItem(int index, Item item)
        {
            if (item.IsEquiped || item.IsEnchanted) return;
            if (ParentModel.GetSaveCount() > index && index > -1)
            {
                ParentModel.AddItemToSave(index, item.Clone() as Item);
                CurrentSaveFile.Items.Remove(item);
            }
        }

        #endregion
    }
}
