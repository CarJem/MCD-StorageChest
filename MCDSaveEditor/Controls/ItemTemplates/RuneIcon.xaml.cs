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

namespace MCDStorageChest.Controls.ItemTemplates
{
    /// <summary>
    /// Interaction logic for RuneIcon.xaml
    /// </summary>
    public partial class RuneIcon : UserControl
    {
        private static readonly BitmapImage ArchetypeA_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeA.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeC_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeC.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeI_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeI.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeO_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeO.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeP_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeP.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeR_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeR.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeS_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeS.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeT_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeT.png", UriKind.Relative));
        private static readonly BitmapImage ArchetypeU_Image = new BitmapImage(new Uri("/MCDStorageChest;component/Images/Runes/ArchetypeU.png", UriKind.Relative));
                 
        private static readonly SolidColorBrush ArchetypeU_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#EEBAB0");
        private static readonly SolidColorBrush ArchetypeC_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#8B60DE");
        private static readonly SolidColorBrush ArchetypeO_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#C7A2F5");
        private static readonly SolidColorBrush ArchetypeS_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#F3B8ED");
        private static readonly SolidColorBrush ArchetypeR_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#A25E90");
        private static readonly SolidColorBrush ArchetypeI_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#D76E96");
        private static readonly SolidColorBrush ArchetypeP_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#B0C7FB");
        private static readonly SolidColorBrush ArchetypeT_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#9472F4");
        private static readonly SolidColorBrush ArchetypeA_Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#F093AB");

        public RuneIcon()
        {
            InitializeComponent();
        }


        public enum Archetype { A, C, I, O, P, R, S, T, U }



        #region Properties

        public Archetype CurrentArchetype
        {
            get { return (Archetype)GetValue(CurrentArchetypeProperty); }
            set { SetValue(CurrentArchetypeProperty, value); }
        }

        #endregion

        #region Dependency Property Bindings

        public static readonly DependencyProperty CurrentArchetypeProperty = DependencyProperty.Register("CurrentArchetype", typeof(Archetype), typeof(RuneIcon), new PropertyMetadata(Archetype.A, OnArchetypeCallbackChanged));
        private static void OnArchetypeCallbackChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            RuneIcon c = sender as RuneIcon;
            if (c != null)
            {
                c.OnArchetypeChanged();
            }
        }

        #endregion

        #region Events

        protected virtual void OnArchetypeChanged()
        {
            switch (CurrentArchetype)
            {
                case Archetype.A:
                    SetIcon(ArchetypeA_Color, ArchetypeA_Image);
                    break;
                case Archetype.C:
                    SetIcon(ArchetypeC_Color, ArchetypeC_Image);
                    break;
                case Archetype.I:
                    SetIcon(ArchetypeI_Color, ArchetypeI_Image);
                    break;
                case Archetype.O:
                    SetIcon(ArchetypeO_Color, ArchetypeO_Image);
                    break;
                case Archetype.P:
                    SetIcon(ArchetypeP_Color, ArchetypeP_Image);
                    break;
                case Archetype.R:
                    SetIcon(ArchetypeR_Color, ArchetypeR_Image);
                    break;
                case Archetype.S:
                    SetIcon(ArchetypeS_Color, ArchetypeS_Image);
                    break;
                case Archetype.T:
                    SetIcon(ArchetypeT_Color, ArchetypeT_Image);
                    break;
                case Archetype.U:
                    SetIcon(ArchetypeU_Color, ArchetypeU_Image);
                    break;
            }

            void SetIcon(SolidColorBrush color, BitmapImage img)
            {
                RuneBox.Background = color;
                RuneImage.Source = img;
            }
        }


        #endregion

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            RuneBox.Background = ArchetypeA_Color;
            RuneImage.Source = ArchetypeA_Image;
        }
    }
}
