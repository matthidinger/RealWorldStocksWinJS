using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.UI.Converters
{
    public class PriceChangeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal? priceChange;

            var snapshot = value as StockSnapshot;
            if(snapshot != null)
            {
                priceChange = snapshot.DaysChange;
            }
            else
            {
                priceChange = value as decimal?;
            }


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

    public class CollectionCountVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isVisible = (int)value == 0; return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}