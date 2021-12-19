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

using MCDStorageChest.Save.Profiles;

namespace MCDStorageChest.Controls.ItemViews
{
    /// <summary>
    /// Interaction logic for EnchantmentPreview.xaml
    /// </summary>
    public partial class ItemEnchantmentPanel : UserControl
    {

        public static Enchantment DefaultEnchantment
        {
            get
            {
                return new Enchantment()
                {
                    Id = Constants.DEFAULT_ENCHANTMENT_ID,
                    Level = 0
                };
            }
        }

        public ItemEnchantmentPanel()
        {
            InitializeComponent();
        }
        
        /*
        private void UpdateSet1(bool state)
        {
            if (state)
            {
                //backgroundImage1.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_Background;
                //topEnchantmentSymbolImage1.Source = Save.Mapping.ImageMappings.Instance.EnchantCommonIcon;

                enchantment1Button.Visibility = Visibility.Visible;
                enchantment2Button.Visibility = Visibility.Visible;
                enchantment3Button.Visibility = Visibility.Visible;
            }
            else
            {
                //backgroundImage1.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_LockedNode;
                //topEnchantmentSymbolImage1.Source = null;

                enchantment1Button.Visibility = Visibility.Collapsed;
                enchantment2Button.Visibility = Visibility.Collapsed;
                enchantment3Button.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateSet2(bool state)
        {
            if (state)
            {
                backgroundImage2.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_Background;
                topEnchantmentSymbolImage2.Source = Save.Mapping.ImageMappings.Instance.EnchantCommonIcon;

                enchantment4Button.Visibility = Visibility.Visible;
                enchantment5Button.Visibility = Visibility.Visible;
                enchantment6Button.Visibility = Visibility.Visible;
            }
            else
            {
                backgroundImage2.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_LockedNode;
                topEnchantmentSymbolImage2.Source = null;

                enchantment4Button.Visibility = Visibility.Collapsed;
                enchantment5Button.Visibility = Visibility.Collapsed;
                enchantment6Button.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateSet3(bool state)
        {
            if (state)
            {
                backgroundImage3.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_Background;
                topEnchantmentSymbolImage3.Source = Save.Mapping.ImageMappings.Instance.EnchantCommonIcon;

                enchantment7Button.Visibility = Visibility.Visible;
                enchantment8Button.Visibility = Visibility.Visible;
                enchantment9Button.Visibility = Visibility.Visible;
            }
            else
            {
                backgroundImage3.Source = Save.Mapping.ImageMappings.Instance.EnchantmentSet_LockedNode;
                topEnchantmentSymbolImage3.Source = null;

                enchantment7Button.Visibility = Visibility.Collapsed;
                enchantment8Button.Visibility = Visibility.Collapsed;
                enchantment9Button.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateStaticImages()
        {
            if (DataContext is Item)
            {
                var item = DataContext as Item;
                var enchantments = item.Enchantments;

                if (enchantments == null)
                {
                    UpdateSet1(false);
                    UpdateSet2(false);
                    UpdateSet3(false);
                }
                else
                {
                    UpdateSet1(enchantments.Count >= 1);
                    UpdateSet2(enchantments.Count >= 4);
                    UpdateSet3(enchantments.Count >= 7);
                }
            }


        }
        */
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //UpdateStaticImages();
        }
    }
}
