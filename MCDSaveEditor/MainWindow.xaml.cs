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
using MCDStorageChest.Controls;

namespace MCDStorageChest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private MainViewModel ViewModel
        {
            get
            {
                return (DataContext as MainViewModel);
            }
        }

        public async Task RefreshUI()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                foreach (var saveView in Extensions.Extensions.FindVisualChildren<SaveView>(TabControl))
                    saveView.RefreshUI();
            });
        }


        public MainWindow()
        {
            InitializeComponent();
            ViewModel.Init(this);
        }
        private async void LoadLeft_Click(object sender, RoutedEventArgs e)
        {
            bool result = await ViewModel.FileOpenAsync(false);
            if (result) await RefreshUI();
        }

        private async void LoadRight_Click(object sender, RoutedEventArgs e)
        {
            bool result = await ViewModel.FileOpenAsync(true);
            if (result) await RefreshUI();
        }

        private async void SaveRight_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.FileSaveAsync();
        }

        private async void SaveLeft_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.FileSaveAsync();
        }

        private async void LoadGameData_Click(object sender, RoutedEventArgs e)
        {
            bool result = await ViewModel.LoadGameDataAsync();
            if (result) await RefreshUI();
        }
        private async void UnloadGameDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool result = ViewModel.UnloadGameData();
            if (result) await RefreshUI();
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingChanged(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            ViewModel.SyncRecentDirectoryLists();
            if (Properties.Settings.Default.AutoLoadLastGameData) await AssetResolver.FileLoadGameContent(Properties.Settings.Default.PakFileLocation);
        }
    }
}
