using DungeonTools.Save.File;
using MCDStorageChest.Json.Classes;
using MCDStorageChest.Json;
using MCDStorageChest.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FModel;
using PakReader;
using PakReader.Pak;
using PakReader.Parsers.Objects;
using SkiaSharp;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using MCDStorageChest;
using System.ComponentModel;
using MCDStorageChest.Logic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;
using Microsoft.Win32;
using PostSharp.Patterns.Model;
#nullable disable

namespace MCDStorageChest.Models
{
    [NotifyPropertyChanged]
    public class SaveModel : INotifyPropertyChanged
    {

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
        private MainViewModel ModelRef { get; set; }
        private bool isInit = false;
        public void Init(MainViewModel _ModelRef)
        {
            if (!isInit)
            {
                ModelRef = _ModelRef;
                isInit = true;
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
            private set { _CurrentSaveFilePath = value; OnPropertyChanged(nameof(CurrentSaveFilePath)); }
        }
        public bool IsStorage
        {
            get { return _isStorage; }
            set { _isStorage = value; OnPropertyChanged(nameof(IsStorage)); }
        }

        #endregion

        #region Methods

        public async Task FileOpenAsync(bool isStorage)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                InitialDirectory = isStorage ? Properties.Settings.Default.LastStorageDirectory : Properties.Settings.Default.LastSaveGameDirectory,
                Filter = "dat files (*.dat)|*.dat|json files (*.json)|*.json|All files (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true
            };
            if (ofd.ShowDialog().Value)
            {
                if (isStorage) Properties.Settings.Default.LastStorageDirectory = Path.GetDirectoryName(ofd.FileName);
                else Properties.Settings.Default.LastSaveGameDirectory = Path.GetDirectoryName(ofd.FileName);
                Properties.Settings.Default.Save();

                CurrentSaveFile = await Logic.FileLoader.FileOpenAsync(ofd.FileName);
                CurrentSaveFilePath = ofd.FileName;
                OnPropertyChanged(nameof(CurrentSaveFile));
            };
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
        public void MoveToStorage(Item item)
        {
            if (item.IsEquiped || item.IsEnchanted) return;
            ModelRef.StorageModel.CurrentSaveFile.Items.Add(item.Clone() as Item);
            CurrentSaveFile.Items.Remove(item);
        }
        public void MoveToSavegame(Item item)
        {
            if (item.IsEquiped || item.IsEnchanted) return;
            ModelRef.SaveModel.CurrentSaveFile.Items.Add(item.Clone() as Item);
            CurrentSaveFile.Items.Remove(item);
        }

        #endregion
    }
}
