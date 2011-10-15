using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RealWorldWPDev.Client.Helpers
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

        public static void RepopulateList<T>(this IList<T> source, IEnumerable<T> newItemsSource)
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
                source = new List<T>();
                foreach (var item in newItemsSource)
                {
                    source.Add(item);
                }
            }
        }
    }
}