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
using MCDStorageChest.Json.Enums;
using MCDStorageChest.Extensions;
using System.Collections.ObjectModel;
using PostSharp.Patterns.Model;
#nullable enable

namespace MCDStorageChest.Controls.SaveViews
{
    /// <summary>
    /// Interaction logic for InventoryPreview.xaml
    /// </summary>

    [NotifyPropertyChanged]
    public partial class InventoryPreview : UserControl
    {
        private Point startPoint;

        public InventoryPreview()
        {
            InitializeComponent();
        }

        public bool MultiselectMode { get; set; } = false;

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Models.SaveModel)
            {
                ((Models.SaveModel)DataContext).ListUpdated += (sender2, args) => UpdateList();
            }
        }


        #region Filters

        private async void allItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.All);
        }

        private async void allMeleeItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.MeleeWeapons);
        }

        private async void allRangedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.RangedWeapons);
        }

        private async void allArmorItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.Armor);
        }

        private async void allArtifactItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.Artifacts);
        }

        private async void allEnchantedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.Enchanted);
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.Custom);
        }

        private async void UpdateList()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                if (Items.ItemsSource == null) return;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Items.ItemsSource);
                if (view == null) return;
                view.Refresh();
            });
        }

        private async Task UpdateFilter(Json.Enums.ItemFilterEnum filter)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                if (DataContext is Models.SaveModel)
                    ((Models.SaveModel)DataContext).CurrentFilter = filter;

                if (Items.ItemsSource == null) return;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Items.ItemsSource);
                if (view == null) return;

                switch (filter)
                {
                    case ItemFilterEnum.All:
                        view.Filter = allItemsFilter;
                        break;
                    case ItemFilterEnum.RangedWeapons:
                        view.Filter = allRangedItemsFilter;
                        break;
                    case ItemFilterEnum.MeleeWeapons:
                        view.Filter = allMeleeItemsFilter;
                        break;
                    case ItemFilterEnum.Artifacts:
                        view.Filter = allArtifactItemsFilter;
                        break;
                    case ItemFilterEnum.Armor:
                        view.Filter = allArmorItemsFilter;
                        break;
                    case ItemFilterEnum.Enchanted:
                        view.Filter = allEnchantedItemsFilter;
                        break;
                    default:
                        view.Filter = searchItemsFilter;
                        break;
                }
            });

        }

        private bool allItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return !item1.IsEquiped;
            }
            else return false;
        }

        private bool allMeleeItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return item1.IsMeleeWeapon && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allRangedItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return item1.IsRangedWeapon && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allArmorItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return item1.IsArmor && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allArtifactItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return item1.IsArtifact && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allEnchantedItemsFilter(object item)
        {
            if (item is Json.Classes.Item)
            {
                var item1 = (Json.Classes.Item)item;
                return item1.EnchantmentPoints != 0 && !item1.IsEquiped;
            }
            else return false;
        }

        public bool searchItemsFilter(object item)
        {
            if (item == null || !(item is Json.Classes.Item)) return false;
            var realItem = item as Json.Classes.Item;
            if (realItem != null && DataContext is Models.SaveModel)
            {
                return ((Models.SaveModel)DataContext).SearchSettings.Filter(realItem);
            }
            else return false;
        }



        #endregion

        #region Drag and Drop

        private void Items_Drop(object sender, DragEventArgs e)
        {

            if (DataContext is Models.SaveModel)
            {
                int index = -1;
                int droppedIndex = -1;
                if (e.Data.GetDataPresent("MCDSaveEditor.Save.Profiles.Item"))
                {
                    Json.Classes.Item droppedItem = (Json.Classes.Item)e.Data.GetData("MCDSaveEditor.Save.Profiles.Item");
                    if (!((Models.SaveModel)DataContext).CurrentSaveFile.Items.Contains(droppedItem))
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }

                    if ((sender as ListBox) == null) return;
                    ListBox listView = (ListBox)sender;
                    DependencyObject? depencyObj = e.OriginalSource as DependencyObject;
                    var listViewItem = FindAnchestor<ListBoxItem>(depencyObj);
                    if (listViewItem == null)
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }

                    Json.Classes.Item item = (Json.Classes.Item)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);


                    e.Effects = DragDropEffects.Move;

                    index = (int)item.InventoryIndex;
                    droppedIndex = (int)droppedItem.InventoryIndex;

                    if (droppedIndex >= 0 && index >= 0)
                    {
                        bool wasEquipped = droppedItem.IsEquiped;
                        if (wasEquipped)
                        {
                            var slot = droppedItem.EquipmentSlot;
                            ((Models.SaveModel)DataContext).CurrentSaveFile.unequiptItem(droppedItem);
                            ((Models.SaveModel)DataContext).CurrentSaveFile.equiptItem(item, (EquipmentSlotEnum)Enum.Parse(typeof(EquipmentSlotEnum), slot));
                        }

                        droppedItem.InventoryIndex = index;
                        item.InventoryIndex = droppedIndex;

                        if (wasEquipped)
                        {
                            UpdateList();
                        }
                    }
                }
            }


        }

        private void Items_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get current mouse position
            startPoint = e.GetPosition(null);
        }

        // Helper to search up the VisualTree
        private static T? FindAnchestor<T>(DependencyObject? current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void Items_MouseMove(object sender, MouseEventArgs e)
        {
            if (DataContext is Models.SaveModel && sender != null)
            {
                // Get the current mouse position
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                           Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    // Get the dragged ListViewItem
                    ListBox listView = (ListBox)sender;
                    if (listView == null) return;
                    DependencyObject? depencyObj = e.OriginalSource as DependencyObject;
                    var listViewItem = FindAnchestor<ListBoxItem>(depencyObj);
                    if (listViewItem == null) return;

                    Json.Classes.Item item = (Json.Classes.Item)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    if (item == null) return;

                    DataObject dragData = new DataObject("MCDSaveEditor.Save.Profiles.Item", item);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        #endregion

        private void searchItemsButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void searchItemsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                button.ContextMenu.DataContext = button.DataContext;
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
                button.ContextMenu.HorizontalOffset = button.ActualWidth;
                button.ContextMenu.VerticalOffset = button.ActualHeight;
                button.ContextMenu.IsOpen = true;
            }
        }

        private async void UserControl_Initialized(object sender, EventArgs e)
        {
            await UpdateFilter(ItemFilterEnum.All);
        }
    }
}
