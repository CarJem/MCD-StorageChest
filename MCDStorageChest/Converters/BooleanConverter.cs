using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MCDStorageChest.Converters
{
    public sealed class BooleanConverter : BooleanConverterBase<bool>
    {
        public BooleanConverter() : base(true, false) { }
    }
}
