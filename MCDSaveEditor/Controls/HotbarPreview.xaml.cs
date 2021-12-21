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
#nullable disable

namespace MCDStorageChest.Controls
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

        public void InitUI()
        {
            ResetBinding(MeleeGearButton);
            ResetBinding(ArmorGearButton);
            ResetBinding(RangedGearButton);
            ResetBinding(HotbarSlot1Button);
            ResetBinding(HotbarSlot2Button);
            ResetBinding(HotbarSlot3Button);

            SetBinding("CurrentSaveFile.MeleeGearItem", MeleeGearButton);
            SetBinding("CurrentSaveFile.ArmorGearItem", ArmorGearButton);
            SetBinding("CurrentSaveFile.RangedGearItem", RangedGearButton);
            SetBinding("CurrentSaveFile.HotbarSlot1Item", HotbarSlot1Button);
            SetBinding("CurrentSaveFile.HotbarSlot2Item", HotbarSlot2Button);
            SetBinding("CurrentSaveFile.HotbarSlot3Item", HotbarSlot3Button);

            void ResetBinding(ListViewItem refrence)
            {
                BindingOperations.ClearBinding(refrence, ListViewItem.DataContextProperty);
            }

            void SetBinding(string name, ListViewItem refrence)
            {
                BindingOperations.SetBinding(refrence, ListViewItem.DataContextProperty, new Binding(name));
            }

        }



        #region Drag and Drop (Gear)


        private void GearItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released) return;
            if (DataContext != null && DataContext is Models.SaveModel && sender is ListViewItem)
            {
                (DataContext as Models.SaveModel).CurrentItem = (Save.Profiles.Item)(sender as ListViewItem).DataContext;
            }
        }

        private void Gear_Drop(object sender, DragEventArgs e)
        {

            if (DataContext is Models.SaveModel)
            {
                if (e.Data.GetDataPresent("MCDSaveEditor.Save.Profiles.Item"))
                {
                    Save.Profiles.Item item = (Save.Profiles.Item)e.Data.GetData("MCDSaveEditor.Save.Profiles.Item");
                    ListBoxItem button = (ListBoxItem)sender;
                    Save.Enums.EquipmentSlotEnum slot;
                    if (button == ArmorGearButton) slot = Save.Enums.EquipmentSlotEnum.ArmorGear;
                    else if (button == MeleeGearButton) slot = Save.Enums.EquipmentSlotEnum.MeleeGear;
                    else if (button == RangedGearButton) slot = Save.Enums.EquipmentSlotEnum.RangedGear;
                    else if (button == HotbarSlot1Button) slot = Save.Enums.EquipmentSlotEnum.HotbarSlot1;
                    else if (button == HotbarSlot2Button) slot = Save.Enums.EquipmentSlotEnum.HotbarSlot2;
                    else if (button == HotbarSlot3Button) slot = Save.Enums.EquipmentSlotEnum.HotbarSlot3;
                    else
                    {
                        e.Effects = DragDropEffects.None;
                        return;
                    }
                    e.Effects = DragDropEffects.Move;

                    (DataContext as Models.SaveModel).CurrentSaveFile.equiptItem(item, slot);
                    (DataContext as Models.SaveModel).RequestListUpdate();

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
                    Save.Profiles.Item item = (Save.Profiles.Item)button.DataContext;
                    if (item == null) return;
                    DataObject dragData = new DataObject("MCDSaveEditor.Save.Profiles.Item", item);
                    DragDrop.DoDragDrop(button, dragData, DragDropEffects.Copy | DragDropEffects.Move);
                }
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
