using System;
using System.Windows;
#if NETFX_CORE
using Windows.UI.Xaml;
#endif

namespace RealWorldStocks.UI.Converters
{
    public class BooleanToVisibilityConverter : ConverterBase
    {
        public bool InvertValue
        {
            get;
            set;
        }

        public override object ConvertCore(object value, Type targetType, object parameter)
        {
            bool booleanValue = ((bool)value);

            if (this.InvertValue)
            {
                booleanValue = !booleanValue;
            }

            return booleanValue ? Visibility.Visible : Visibility.Collapsed; ;
        }

        public override object ConvertBackCore(object value, Type targetType, object parameter)
        {
            bool booleanValue = ((Visibility)value) == Visibility.Visible;

            return this.InvertValue ? !booleanValue : booleanValue;
        }
    }

}