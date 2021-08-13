using MCDSaveEditor.Save.Profiles;
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
using MCDSaveEditor.Models;
using Microsoft.Win32;

namespace MCDSaveEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(this);
            this.DataContext = ViewModel;
        }

        private void InitUI()
        {
            Inventory.InitUI();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private async void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "dat files (*.dat)|*.dat|json files (*.json)|*.json|All files (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true
            };
            if (ofd.ShowDialog().Value)
            {
                await ViewModel.FileOpenAsync(ofd.FileName);
                this.Dispatcher.Invoke(() => InitUI());
            }

        }

        private async void LoadGameDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            await AssetResolver.FileLoadGameContent(string.Empty);
            this.Dispatcher.Invoke(() => InitUI());
        }
    }
}
