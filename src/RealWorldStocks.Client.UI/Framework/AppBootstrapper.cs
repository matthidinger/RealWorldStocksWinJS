using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Web.Services;
using RealWorldStocks.Client.Core.Models;
using RealWorldStocks.Client.UI.Controls;
using RealWorldStocks.Client.UI.ViewModels;
using RealWorldStocks.Client.UI.ViewModels.Home;
using RealWorldStocks.Client.UI.ViewModels.StockDetails;

namespace RealWorldStocks.Client.UI.Framework
{
	public class AppBootstrapper : PhoneBootstrapper
	{
        PhoneContainer _container;

        protected override void Configure()
        {
            LogManager.GetLog = type => new DebugLog();

            _container = new PhoneContainer(RootFrame);
            _container.RegisterPhoneServices();

            _container.Singleton<IStocksWebService, StocksWebService>();


            _container.Singleton<HomeViewModel>();
            _container.Singleton<HomeNewsViewModel>();
            _container.Singleton<HomeWatchListViewModel>();
            _container.Singleton<HomeQuoteViewModel>();


            _container.PerRequest<StockDetailsViewModel>();
            _container.PerRequest<InfoViewModel>();
            _container.PerRequest<NewsViewModel>();
            _container.PerRequest<TrendsViewModel>();


            _container.Singleton<SettingsViewModel>();
            _container.PerRequest<BasicHttpViewModel>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            // TODO: Make issue for CM to use Tap conventions by default for WP7 apps
            ConventionManager.AddElementConvention<HyperlinkButton>(HyperlinkButton.ContentProperty, "DataContext", "Tap");
            ConventionManager.AddElementConvention<WatermarkedTextBox>(WatermarkedTextBox.TextProperty, "DataContext", "TextChanged");

            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
             (viewModelType, path, property, element, convention) =>
             {
                 if (ConventionManager
                     .GetElementConvention(typeof(ItemsControl))
                     .ApplyBinding(viewModelType, path, property, element, convention))
                 {
                     ConventionManager
                         .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                     ConventionManager
                         .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, viewModelType);
                     return true;
                 }

                 return false;
             };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, viewModelType);
                        return true;
                    }

                    return false;
                };
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("An unexpected error occured, sorry about the troubles.", "Oops...", MessageBoxButton.OK);
                e.Handled = true;
            }

            base.OnUnhandledException(sender, e);
        }

        protected override void OnLaunch(object sender, LaunchingEventArgs e)
        {
            base.OnLaunch(sender, e);
            WatchList.Initialize();
        }

        protected override void OnActivate(object sender, ActivatedEventArgs e)
        {
            base.OnActivate(sender, e);
            if (!e.IsApplicationInstancePreserved)
            {
                WatchList.Initialize();
            }
        }

        protected override void OnDeactivate(object sender, DeactivatedEventArgs e)
        {
            WatchList.Current.PersistData();
            base.OnDeactivate(sender, e);
        }

        protected override void OnClose(object sender, ClosingEventArgs e)
        {
            WatchList.Current.PersistData();
            base.OnClose(sender, e);
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new TransitionFrame();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }  
	}
}
