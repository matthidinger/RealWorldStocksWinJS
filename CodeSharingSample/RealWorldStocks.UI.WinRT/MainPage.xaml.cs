using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RealWorldStocks.Core.Models;
using RealWorldStocks.UI.WinRT.Common;
using RealWorldStocks.UI.WinRT.Models;

namespace RealWorldStocks.UI.WinRT
{
    public sealed partial class MainPage : LayoutAwarePage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            await WatchList.InitializeAsync();

            var groups = new ObservableCollection<Group>
            {
                new Group<StockSnapshot>("Watch List")
                {
                    Items = await WatchList.Current.RefreshSnapshotsAsync()
                },
                new Group<News>("News")
                {
                    Items = await WatchList.Current.RefreshNewsAsync()
                },
            };

            DefaultViewModel["Groups"] = groups;
        }
    }
}
