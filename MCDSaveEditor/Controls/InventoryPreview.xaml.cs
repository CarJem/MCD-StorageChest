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
using MCDStorageChest.Save.Enums;
using MCDStorageChest.Extensions;
using System.Collections.ObjectModel;
#nullable disable

namespace MCDStorageChest.Controls
{
    /// <summary>
    /// Interaction logic for InventoryPreview.xaml
    /// </summary>
    public partial class InventoryPreview : UserControl
    {
        private Point startPoint;

        public InventoryPreview()
        {
            InitializeComponent();
        }

        private void Items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Models.SaveModel)
            {
                (DataContext as Models.SaveModel).ListUpdated += (sender, args) => UpdateList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }


        #region Filters

        private void allItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.All);
        }

        private void allMeleeItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.MeleeWeapons);
        }

        private void allRangedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.RangedWeapons);
        }

        private void allArmorItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.Armor);
        }

        private void allArtifactItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.Artifacts);
        }

        private void allEnchantedItemsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilter(ItemFilterEnum.Enchanted);
        }

        private void UpdateList()
        {
            if (Items.ItemsSource == null) return;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Items.ItemsSource);
            if (view == null) return;
            view.Refresh();
        }

        private void UpdateFilter(Save.Enums.ItemFilterEnum filter)
        {
            if (DataContext is Models.SaveModel)
            {
                (DataContext as Models.SaveModel).CurrentFilter = filter;
            }

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
                    view.Filter = allItemsFilter;
                    break;
            }
        }

        private bool allItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return !item1.IsEquiped;
            }
            else return false;
        }

        private bool allMeleeItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return item1.IsMeleeWeapon && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allRangedItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return item1.IsRangedWeapon && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allArmorItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return item1.IsArmor && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allArtifactItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return item1.IsArtifact && !item1.IsEquiped;
            }
            else return false;
        }

        private bool allEnchantedItemsFilter(object item)
        {
            if (item is Save.Profiles.Item)
            {
                var item1 = item as Save.Profiles.Item;
                return item1.EnchantmentPoints != 0 && !item1.IsEquiped;
            }
            else return false;
        }


        #endregion

        #region UI Icons

        public void InitUI()
        {
            UpdateFilter(ItemFilterEnum.All);
        }

        public void UpdateFilter()
        {
            if (DataContext is Models.SaveModel) UpdateFilter((DataContext as Models.SaveModel).CurrentFilter);
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
                    Save.Profiles.Item droppedItem = (Save.Profiles.Item)e.Data.GetData("MCDSaveEditor.Save.Profiles.Item");
                    if (!(DataContext as Models.SaveModel).CurrentSaveFile.Items.Contains(droppedItem))
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }

                    ListBox listView = sender as ListBox;
                    ListBoxItem listViewItem = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null)
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }

                    Save.Profiles.Item item = (Save.Profiles.Item)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);


                    e.Effects = DragDropEffects.Move;

                    index = (int)item.InventoryIndex;
                    droppedIndex = (int)droppedItem.InventoryIndex;

                    if (droppedIndex >= 0 && index >= 0)
                    {
                        bool wasEquipped = droppedItem.IsEquiped;
                        if (wasEquipped)
                        {
                            var slot = droppedItem.EquipmentSlot;
                            (DataContext as Models.SaveModel).CurrentSaveFile.unequiptItem(droppedItem);
                            (DataContext as Models.SaveModel).CurrentSaveFile.equiptItem(item, (EquipmentSlotEnum)Enum.Parse(typeof(EquipmentSlotEnum), slot));
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
        private static T FindAnchestor<T>(DependencyObject current)
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
            if (DataContext is Models.SaveModel)
            {
                // Get the current mouse position
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                           Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    // Get the dragged ListViewItem
                    ListBox listView = sender as ListBox;
                    ListBoxItem listViewItem = FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);
                    if (listViewItem == null) return; 

                    Save.Profiles.Item item = (Save.Profiles.Item)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);
                    if (item == null) return;

                    DataObject dragData = new DataObject("MCDSaveEditor.Save.Profiles.Item", item);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        #endregion

        private void SendToSave_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Models.SaveModel)
            {
                (DataContext as Models.SaveModel).MoveToSavegame((DataContext as Models.SaveModel).CurrentItem);
            }
        }

        private void SendToStorage_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Models.SaveModel)
            {
                (DataContext as Models.SaveModel).MoveToStorage((DataContext as Models.SaveModel).CurrentItem);
            }
        }
    }
}
