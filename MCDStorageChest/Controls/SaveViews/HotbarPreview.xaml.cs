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

namespace MCDStorageChest.Controls.SaveViews
{
    /// <summary>
    /// Interaction logic for HotbarPreview.xaml
    /// </summary>
    public partial class HotbarPreview : UserControl
    {
        private Point startPoint;

        public HotbarPreview()
        {
            InitializeComponent();
        }

        public void RefreshUI()
        {
            if ((DataContext as Models.SaveModel).CurrentSaveFile == null) return;
            (DataContext as Models.SaveModel).CurrentSaveFile.UpdateEquiptmentSlots();
        }



        #region Drag and Drop (Gear)


        private void GearItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;
            if (DataContext != null && DataContext is Models.SaveModel && sender is ListViewItem)
            {
                (DataContext as Models.SaveModel).CurrentItem = (Json.Classes.Item)(sender as ListViewItem).Content;
            }
        }

        private void Gear_Drop(object sender, DragEventArgs e)
        {

            if (DataContext is Models.SaveModel)
            {
                if (e.Data.GetDataPresent("MCDSaveEditor.Save.Profiles.Item"))
                {
                    Json.Classes.Item item = (Json.Classes.Item)e.Data.GetData("MCDSaveEditor.Save.Profiles.Item");
                    ListBoxItem button = (ListBoxItem)sender;
                    Json.Enums.EquipmentSlotEnum slot;
                    if (button == ArmorGearButton) slot = Json.Enums.EquipmentSlotEnum.ArmorGear;
                    else if (button == MeleeGearButton) slot = Json.Enums.EquipmentSlotEnum.MeleeGear;
                    else if (button == RangedGearButton) slot = Json.Enums.EquipmentSlotEnum.RangedGear;
                    else if (button == HotbarSlot1Button) slot = Json.Enums.EquipmentSlotEnum.HotbarSlot1;
                    else if (button == HotbarSlot2Button) slot = Json.Enums.EquipmentSlotEnum.HotbarSlot2;
                    else if (button == HotbarSlot3Button) slot = Json.Enums.EquipmentSlotEnum.HotbarSlot3;
                    else
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                    e.Effects = DragDropEffects.Move;

                    (DataContext as Models.SaveModel).CurrentSaveFile.equiptItem(item, slot);
                    (DataContext as Models.SaveModel).Update();

                }
            }


        }

        private void Gear_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get current mouse position
            startPoint = e.GetPosition(null);
        }

        private void Gear_MouseMove(object sender, MouseEventArgs e)
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
                    ListViewItem button = sender as ListViewItem;
                    if (button == null) return;
                    Json.Classes.Item item = (Json.Classes.Item)button.Content;
                    if (item == null) return;
                    DataObject dragData = new DataObject("MCDSaveEditor.Save.Profiles.Item", item);
                    DragDrop.DoDragDrop(button, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void EquiptedItemButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = GetItem(sender);
            if (item != null)
            {
                (DataContext as Models.SaveModel).CurrentSaveFile.unequiptItem(item);
                (DataContext as Models.SaveModel).Update();
            }

            Json.Classes.Item GetItem(object sender)
            {
                if (DataContext != null && DataContext is Models.SaveModel && sender is ListViewItem)
                    return (Json.Classes.Item)(sender as ListViewItem).Content;
                else return null;
            }
        }

        #endregion

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
