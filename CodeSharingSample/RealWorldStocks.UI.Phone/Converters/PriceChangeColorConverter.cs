using System;
#if NETFX_CORE
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#else
using System.Windows.Data;
using System.Windows.Media;
#endif

namespace RealWorldStocks.UI.Phone.Converters
{
    public class PriceChangeColorConverter : ConverterBase
    {
        public override object ConvertCore(object value, Type targetType, object parameter)
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

        public override object ConvertBackCore(object value, Type targetType, object parameter)
        {
            throw new NotImplementedException();
        }
    }
}