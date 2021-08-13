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

namespace MCDSaveEditor.Controls
{
    /// <summary>
    /// Interaction logic for ItemPreview.xaml
    /// </summary>
    public partial class ItemPreview : UserControl
    {
        public ItemPreview()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = (sender as Button).ContextMenu;
            contextMenu.PlacementTarget = (sender as Button);
            contextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            contextMenu.DataContext = this.DataContext;
            contextMenu.IsOpen = true;
        }
    }
}
