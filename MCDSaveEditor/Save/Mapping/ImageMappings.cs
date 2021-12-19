using MCDStorageChest.Save.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using MCDStorageChest.Models;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MCDStorageChest.Save.Mapping
{
    public class ImageMappings : INotifyPropertyChanged
    {
        public static ImageMappings Instance { get; private set; } = new ImageMappings();

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        public ImageSource EnchantmentPointsImageSource => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Mobs/enchant_common_icon");
        public ImageSource EnchantCommonIcon => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Mobs/enchant_common_icon");

        public Brush Item_MarkedNewBackground
        {
            get
            {
                var img = Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/HotBar2/Icons/inventoryslot_newitem");
                if (img != null) return new ImageBrush(img);
                else return Brushes.Gold;
            }
        }
        public Brush Item_GildedBackground
        {
            get
            {
                var img = Models.AssetResolver.instance.imageSource("/Dungeons/Content/Content_DLC4/UI/Materials/Inventory/Inventory_slot_gilded_plate");
                if (img != null) return new ImageBrush(img);
                else return Brushes.Yellow;
            }
        }


        public Brush Inventory_Background
        {
            get
            {
                try
                {
                    var img = Models.AssetResolver.instance.imageSource("/Dungeons/Content/Textures/InventoryNew/master_inventory_backdrop");
                    if (img != null)
                    {
                        var img2 = new CroppedBitmap(img, new Int32Rect(0, 0, 1920, 1080));
                        var brush = new ImageBrush(img2);
                        brush.Stretch = Stretch.UniformToFill;
                        return brush;
                    }
                }
                catch { }
                return Brushes.Black;
            }
        }
        public ImageSource Inventory_AllItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_all_default");
        public ImageSource Inventory_MeleeItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_melee_default");
        public ImageSource Inventory_RangedItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_ranged_default");
        public ImageSource Inventory_ArmorItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_armour_default");
        public ImageSource Inventory_ArtifactItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_consume_default");
        public ImageSource Inventory_EnchantedItemsButtonImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_enchant_default");

        public ImageSource EnchantmentSet_Background => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/StatusEffect/Enchantment/EnchantmentsBackground");
        public ImageSource EnchantmentSet_LockedNode => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/MissionSelectMap/marker/locked_node");

        public ImageSource Hotbar_EmeraldImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Character/STATS_emerald");
        public ImageSource Hotbar_GoldImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Currency/GoldIndicator");
        public ImageSource Hotbar_EnchantmentPointsImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Enchantment/enchantscore_background");
        public ImageSource Hotbar_LevelFrameImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Character/STATS_LV_frame");
        public ImageSource Hotbar_PowerLevelImage => Models.AssetResolver.instance.imageSource("/Dungeons/Content/UI/Materials/Inventory2/Power/gearstrenght_icon");

        public void UpdateProperties()
        {
            OnPropertyChanged(nameof(EnchantCommonIcon));
            OnPropertyChanged(nameof(EnchantmentPointsImageSource));

            OnPropertyChanged(nameof(Item_MarkedNewBackground));
            OnPropertyChanged(nameof(Item_GildedBackground));

            OnPropertyChanged(nameof(Inventory_Background));
            OnPropertyChanged(nameof(Inventory_AllItemsButtonImage));
            OnPropertyChanged(nameof(Inventory_MeleeItemsButtonImage));
            OnPropertyChanged(nameof(Inventory_RangedItemsButtonImage));
            OnPropertyChanged(nameof(Inventory_ArmorItemsButtonImage));
            OnPropertyChanged(nameof(Inventory_ArtifactItemsButtonImage));
            OnPropertyChanged(nameof(Inventory_EnchantedItemsButtonImage));

            OnPropertyChanged(nameof(EnchantmentSet_Background));
            OnPropertyChanged(nameof(EnchantmentSet_LockedNode));

            OnPropertyChanged(nameof(Hotbar_EmeraldImage));
            OnPropertyChanged(nameof(Hotbar_GoldImage));
            OnPropertyChanged(nameof(Hotbar_EnchantmentPointsImage));
            OnPropertyChanged(nameof(Hotbar_LevelFrameImage));
            OnPropertyChanged(nameof(Hotbar_PowerLevelImage));

        }
    }
}
