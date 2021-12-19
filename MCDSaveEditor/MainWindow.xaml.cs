using MCDStorageChest.Models;
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

namespace MCDStorageChest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainViewModel LeftViewModel { get; set; } = new MainViewModel();
        public MainViewModel RightViewModel { get; set; } = new MainViewModel();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitUILeft()
        {
            InventoryLeft.InitUI();
        }

        private void InitUIRight()
        {
            InventoryRight.InitUI();
        }

        private async void LoadLeft_Click(object sender, RoutedEventArgs e)
        {
            await LeftViewModel.FileOpenAsync();
            InitUILeft();
        }

        private async void LoadRight_Click(object sender, RoutedEventArgs e)
        {
            await RightViewModel.FileOpenAsync();
            InitUIRight();
        }

        private async void SaveRight_Click(object sender, RoutedEventArgs e)
        {
            await RightViewModel.FileSaveAsync(RightViewModel.CurrentSaveFilePath, RightViewModel.CurrentSaveFile);

        }

        private async void SaveLeft_Click(object sender, RoutedEventArgs e)
        {
            await LeftViewModel.FileSaveAsync(LeftViewModel.CurrentSaveFilePath, LeftViewModel.CurrentSaveFile);
        }

        private async void LoadGameData_Click(object sender, RoutedEventArgs e)
        {
            await AssetResolver.FileLoadGameContent(string.Empty);
        }

        private void NewMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UnloadGameDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AssetResolver.FileUnloadGameContent();
        }
    }
}
