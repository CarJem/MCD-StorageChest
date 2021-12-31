using MCDStorageChest.Json.Classes;
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

namespace MCDStorageChest.Controls.ItemViews
{
    /// <summary>
    /// Interaction logic for ItemEnchantmentPreview.xaml
    /// </summary>
    public partial class ItemEnchantmentPreview : UserControl
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

        public ItemEnchantmentPreview()
        {
            InitializeComponent();
        }

        private void enchantment3Button_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null) this.Opacity = 0.5;
            else this.Opacity = 1;
        }
    }
}
