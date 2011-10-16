using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RealWorldStocks.Client.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool InvertValue
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = ((bool)value);

            if (this.InvertValue)
            {
                booleanValue = !booleanValue;
            }

            return booleanValue ? Visibility.Visible : Visibility.Collapsed; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = ((Visibility)value) == Visibility.Visible;

            return this.InvertValue ? !booleanValue : booleanValue;
        }
    }

}