using System;
using System.Windows.Data;
using System.Windows.Media;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.UI.Converters
{
    public class PriceChangeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var priceChange = value as decimal?;

            if (priceChange != null && priceChange < 0)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Green);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}