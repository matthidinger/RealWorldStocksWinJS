﻿using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Web;
using RealWorldStocks.Client.Core.Web.Services;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Framework;

namespace RealWorldStocks.Client.UI.ViewModels.Home
{
    public class HomeQuoteViewModel : Screen, IAppBarController
    {
        private readonly IStocksWebService _stocksWebService;

        public HomeQuoteViewModel(IStocksWebService stocksWebService)
        {
            DisplayName = "lookup";
            _stocksWebService = stocksWebService;
        }


        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                NotifyOfPropertyChange(() => Symbol);
                NotifyOfPropertyChange(() => CanGetSnapshot);
            }
        }

        public bool CanGetSnapshot
        {
            get { return !string.IsNullOrEmpty(Symbol); }
        }

        public IEnumerable<IResult> GetSnapshot()
        {
            var result = _stocksWebService.GetSnapshot(Symbol).Execute();
            yield return result;

            Snapshot = result.Response.Model;
        }

        private StockSnapshot _snapshot;
        public StockSnapshot Snapshot
        {
            get { return _snapshot; }
            set
            {
                _snapshot = value;
                NotifyOfPropertyChange(() => Snapshot);
            }
        }

        public IApplicationBar ApplicationBar
        {
            get
            {
                return new ApplicationBar
                           {
                               Mode = ApplicationBarMode.Minimized
                           };
            }
        }

        public event EventHandler AppBarChanged = delegate { };
    }
}