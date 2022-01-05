using MCDStorageChest.Json.Enums;
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
#nullable enable

namespace MCDStorageChest.Json.Mapping
{
    public class ImageMappings : INotifyPropertyChanged
    {
        public static ImageMappings Instance { get; private set; } = new ImageMappings();

        public event PropertyChangedEventHandler? PropertyChanged = default;
        void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static string EnchantmentPointsImageSourcePath = "/Dungeons/Content/UI/Materials/Mobs/enchant_common_icon";
        private static string EnchantCommonIconPath = "/Dungeons/Content/UI/Materials/Mobs/enchant_common_icon";
        private static string Inventory_AllItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_all_default";
        private static string Inventory_MeleeItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_melee_default";
        private static string Inventory_RangedItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_ranged_default";
        private static string Inventory_ArmorItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_armour_default";
        private static string Inventory_ArtifactItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_consume_default";
        private static string Inventory_EnchantedItemsButtonImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Filter/filter_enchant_default";
        private static string EnchantmentSet_BackgroundPath = "/Dungeons/Content/UI/Materials/StatusEffect/Enchantment/EnchantmentsBackground";
        private static string EnchantmentSet_LockedNodePath = "/Dungeons/Content/UI/Materials/MissionSelectMap/marker/locked_node";
        private static string Hotbar_EmeraldImagePath = "/Dungeons/Content/UI/Materials/Character/STATS_emerald";
        private static string Hotbar_GoldImagePath = "/Dungeons/Content/UI/Materials/Currency/GoldIndicator";
        private static string Hotbar_EnchantmentPointsImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Enchantment/enchantscore_background";
        private static string Hotbar_LevelFrameImagePath = "/Dungeons/Content/UI/Materials/Character/STATS_LV_frame";
        private static string Hotbar_PowerLevelImagePath = "/Dungeons/Content/UI/Materials/Inventory2/Power/gearstrenght_icon";
        private static string Hotbar_EyeOfEnderImagePath = "/Dungeons/Content/UI/Materials/Currency/T_EyeOfEnder_Currency";
        private static string Item_MarkedNewBackgroundPath = "/Dungeons/Content/UI/Materials/HotBar2/Icons/inventoryslot_newitem";
        private static string Item_GildedBackgroundPath = "/Dungeons/Content/Content_DLC4/UI/Materials/Inventory/Inventory_slot_gilded_plate";
        private static string Inventory_BackgroundPath = "/Dungeons/Content/Textures/InventoryNew/master_inventory_backdrop";

        public Brush Item_MarkedNewBackground
        {
            get
            {
                var img = Logic.AssetLoader.instance.imageSource(Item_MarkedNewBackgroundPath);
                if (img != null) return new ImageBrush(img);
                else return Brushes.Gold;
            }
        }
        public Brush Item_GildedBackground
        {
            get
            {
                var img = Logic.AssetLoader.instance.imageSource(Item_GildedBackgroundPath);
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
                    var img = Logic.AssetLoader.instance.imageSource(Inventory_BackgroundPath);
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

        public ImageSource? EnchantmentPointsImageSource
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(EnchantmentPointsImageSourcePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Enchantments.png");
                else return game;
            }
        }
        public ImageSource? EnchantCommonIcon
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(EnchantCommonIconPath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Enchantments.png");
                else return game;
            }
        }
        public ImageSource? Inventory_AllItemsButtonImage 
        { 
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_AllItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Unknown.png");
                else return game;
            }
        }
        public ImageSource? Inventory_MeleeItemsButtonImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_MeleeItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("MeleeWeapons.png");
                else return game;
            }
        }
        public ImageSource? Inventory_RangedItemsButtonImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_RangedItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("RangedWeapons.png");
                else return game;
            }
        }
        public ImageSource? Inventory_ArmorItemsButtonImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_ArmorItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Armor.png");
                else return game;
            }
        }
        public ImageSource? Inventory_ArtifactItemsButtonImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_ArtifactItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Artifacts.png");
                else return game;
            }
        }
        public ImageSource? Inventory_EnchantedItemsButtonImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Inventory_EnchantedItemsButtonImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Points.png");
                else return game;
            }
        }

        public ImageSource Inventory_SearchItemsButtonImage
        {
            get
            {
                return new BitmapImage(new Uri("pack://application:,,,/MCDStorageChest;component/Images/Search.png"));
            }
        }

        public ImageSource? EnchantmentSet_Background
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(EnchantmentSet_BackgroundPath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Unknown.png");
                else return game;
            }
        }
        public ImageSource? EnchantmentSet_LockedNode
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(EnchantmentSet_LockedNodePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Unknown.png");
                else return game;
            }
        }

        public ImageSource? Hotbar_EmeraldImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_EmeraldImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Emerald.png");
                else return game;
            }
        }
        public ImageSource? Hotbar_GoldImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_GoldImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Gold.png");
                else return game;
            }
        }
        public ImageSource? Hotbar_EnchantmentPointsImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_EnchantmentPointsImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Enchantments.png");
                else return game;
            }
        }

        public ImageSource? Hotbar_EyeOfEnderImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_EyeOfEnderImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Unknown.png");
                else return game;
            }
        }

        public ImageSource? Hotbar_LevelFrameImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_LevelFrameImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Unknown.png");
                else return game;
            }
        }
        public ImageSource? Hotbar_PowerLevelImage
        {
            get
            {
                var game = Logic.AssetLoader.instance.imageSource(Hotbar_PowerLevelImagePath);
                if (game == null) return Logic.AssetLoader.instance.imageSource("Enchantments.png");
                else return game;
            }
        }

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
            OnPropertyChanged(nameof(Hotbar_EyeOfEnderImage));
            OnPropertyChanged(nameof(Hotbar_EnchantmentPointsImage));
            OnPropertyChanged(nameof(Hotbar_LevelFrameImage));
            OnPropertyChanged(nameof(Hotbar_PowerLevelImage));

        }
    }
}
