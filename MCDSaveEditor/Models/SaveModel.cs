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
    public class SaveModel : INotifyPropertyChanging
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
                        param => this.ParentModel.FileClose(this),
                        param => true
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
                        param => this.MoveItem(param),
                        param => this.MoveItemCheck(this)
                    );
                }
                return _moveToCommand;
            }
        }
        public bool MoveItemCheck(object param)
        {
            if (ParentModel != null && param != null && (param is SaveModel)) return true;
            else return false;
        }
        public void MoveItem(object param)
        {
            var moveToIndex = ParentModel.IndexOf((param as SaveModel));
            Transfer(moveToIndex, CurrentItem);
        }

        #endregion

        #region Event Handlers

        public event EventHandler ListUpdated;
        public event PropertyChangingEventHandler PropertyChanging;
        protected void OnPropertyChanging(string propertyName)
        {
            this.PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion

        #region Connector

        private IMainViewModel ParentModel { get; set; }
        public void Init(IMainViewModel _ModelRef)
        {
            if (ParentModel == null) ParentModel = _ModelRef;
        }

        #endregion

        #region Properties

        public ProfileSaveFile CurrentSaveFile { get; set; }
        public SearchModel SearchSettings { get; set; } = new SearchModel();
        public Item CurrentItem { get; set; }
        public Json.Enums.ItemFilterEnum CurrentFilter { get; set; } = Json.Enums.ItemFilterEnum.All;
        public string CurrentSaveFilePath { get; set; } = string.Empty;
        public bool IsStorage { get; set; } = false;

        #endregion

        #region Read Only Properties

        public string TabTitle
        {
            get
            {
                Depends.On(IsStorage);
                Depends.On(CurrentSaveFile);
                string fileType = IsStorage ? "Storage" : "Save";
                string fileName = Path.GetFileName(CurrentSaveFilePath);
                return string.Format("{0} ({1})", fileType, fileName);
            }
        }

        [SafeForDependencyAnalysis]
        public List<SaveModel> OtherSaves => ParentModel == null ? ParentModel.OtherSaves(this) : new List<SaveModel>();

        #endregion

        #region IO Methods

        public async Task<bool> OpenAsync(string path, bool isStorage)
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
                Logic.SettingMapper.UpdateRecentDirectoriesLists(ParentModel, Path.GetDirectoryName(ofd.FileName), isStorage);
                IsStorage = isStorage;

                CurrentSaveFile = await Logic.FileLoader.FileOpenAsync(ofd.FileName);
                CurrentSaveFilePath = ofd.FileName;
                return true;
            }
            else return false;
        }
        public async Task<bool> OpenAsync(bool isStorage)
        {
            string path = isStorage ? Settings.Default.LastStorageDirectory : Settings.Default.LastSaveGameDirectory;
            return await OpenAsync(path, isStorage);
        }
        public async Task SaveAsync(string filePath, ProfileSaveFile profile)
        {
            var result = MessageBox.Show("Would you like to make a backup", "Saving...", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Cancel || result == MessageBoxResult.None) return;
            if (result == MessageBoxResult.Yes) File.Copy(filePath, Path.ChangeExtension(filePath, Path.GetExtension(filePath) + ".bak"));
            await Logic.FileLoader.FileSaveAsync(filePath, profile);
        }

        #endregion

        #region Action Methods
        public void Update()
        {
            ListUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void Transfer(int index, Item item)
        {
            if (item.IsEquiped || item.IsEnchanted) return;
            ParentModel.Transfer(this, index, item.Clone() as Item);
        }

        #endregion
    }
}
