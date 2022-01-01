using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MCDStorageChest.Converters
{
    public sealed class BooleanToSelectionModeConverter : BooleanConverterBase<SelectionMode>
    {
        public BooleanToSelectionModeConverter() : base(SelectionMode.Multiple, SelectionMode.Single) { }
    }
}
