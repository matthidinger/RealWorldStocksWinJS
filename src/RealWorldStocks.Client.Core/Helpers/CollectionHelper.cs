using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RealWorldStocks.Client.Core.Helpers
{
    public static class CollectionHelper
    {
        public static void RepopulateObservableCollection<T>(this ObservableCollection<T> source, IEnumerable<T> newItemsSource)
        {
            if (source != null)
            {
                source.Clear();

                foreach (var item in newItemsSource)
                {
                    source.Add(item);
                }
            }
            else
            {
                source = new ObservableCollection<T>();
                foreach (var item in newItemsSource)
                {
                    source.Add(item);
                }
            }
        }
    }
}