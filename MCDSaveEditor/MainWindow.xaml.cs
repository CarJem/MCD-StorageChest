using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCDStorageChest.Models;
using MCDStorageChest.Logic;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace MCDStorageChest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel
        {
            get
            {
                return (DataContext as MainViewModel);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void InitUILeft()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                InventoryLeft.InitUI();
                HotbarLeft.InitUI();
                SaveTab.Header = string.Format("Save ({0})", System.IO.Path.GetFileName(ViewModel.SaveModel.CurrentSaveFilePath));
            });

        }

        private async void InitUIRight()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                InventoryRight.InitUI();
                HotbarRight.InitUI();
                StorageTab.Header = string.Format("Storage ({0})", System.IO.Path.GetFileName(ViewModel.StorageModel.CurrentSaveFilePath));
            });
        }

        private async void LoadLeft_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveModel.FileOpenAsync(false);
            InitUILeft();
        }

        private async void LoadRight_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.StorageModel.FileOpenAsync(true);
            InitUIRight();
        }

        private async void SaveRight_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.StorageModel.FileSaveAsync(ViewModel.StorageModel.CurrentSaveFilePath, ViewModel.StorageModel.CurrentSaveFile);

        }

        private async void SaveLeft_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveModel.FileSaveAsync(ViewModel.SaveModel.CurrentSaveFilePath, ViewModel.SaveModel.CurrentSaveFile);
        }

        private async void LoadGameData_Click(object sender, RoutedEventArgs e)
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
            }

            InitUILeft();
            InitUIRight();
        }
        private void UnloadGameDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AssetResolver.FileUnloadGameContent();
            InitUILeft();
            InitUIRight();
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }




    }
}
