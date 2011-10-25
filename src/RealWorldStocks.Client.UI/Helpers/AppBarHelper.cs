using System;
using System.Linq;
using System.Threading;
using System.Windows.Interactivity;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.UI.Framework;

namespace RealWorldStocks.Client.UI.Helpers
{
    // TODO: Make CM Issue to get this trigger change in the master
    public class AppBarButtonTrigger : TriggerBase<PhoneApplicationPage>
    {
        private readonly IApplicationBarMenuItem _button;

        public AppBarButtonTrigger(IApplicationBarMenuItem button)
        {
            _button = button;
        }

        protected override void OnAttached()
        {
            _button.Click += ButtonClicked;
        }

        protected override void OnDetaching()
        {
            _button.Click -= ButtonClicked;
        }

        void ButtonClicked(object sender, EventArgs e)
        {
            InvokeActions(e);
        }
    }

    public static class AppBarHelper
    {
        public static void BindAppBar(PhoneApplicationPage page)
        {
            var appBarController = page.DataContext as IAppBarController;
            var pageAppBar = page.ApplicationBar;

            if (appBarController != null)
            {
                appBarController.
                    AppBarChanged += (s2, e2) =>
                                         {
                                             // TODO: See if I can cache the app bars for each view model, perf is currently suffering a little when flipping through panorama
                                             var controllerAppBar = appBarController.ApplicationBar;
                                             var triggers = Interaction.GetTriggers(page);

                                             pageAppBar.Buttons.Clear();
                                             triggers.OfType<AppBarButtonTrigger>().ToList().ForEach(trigger => triggers.Remove(trigger));

                                             foreach (var button in controllerAppBar.Buttons)
                                             {
                                                 // TODO: Is there a built-in Caliburn helper to do this? Maybe in ViewModelBinder?
                                                 var caliburnButton = button as AppBarButton;
                                                 if (caliburnButton != null)
                                                 {
                                                     var parsedTrigger = Parser.Parse(page, caliburnButton.Message).First();
                                                     var trigger = new AppBarButtonTrigger(caliburnButton);
                                                     var actionMessages = parsedTrigger.Actions.OfType<ActionMessage>().ToList();
                                                     actionMessages.Apply(x =>
                                                     {
                                                         //x.buttonSource = caliburnButton;
                                                         parsedTrigger.Actions.Remove(x);
                                                         trigger.Actions.Add(x);
                                                     });

                                                     triggers.Add(trigger);
                                                 }

                                                 pageAppBar.Buttons.Add(button);
                                             }

                                             pageAppBar.IsVisible = controllerAppBar.IsVisible;
                                             pageAppBar.Mode = controllerAppBar.Mode;
                                             pageAppBar.IsMenuEnabled = controllerAppBar.IsMenuEnabled;

                                            
                                         };

                page.ApplicationBar.StateChanged += (s3, e3)
                                                    =>
                                                        {
                                                            pageAppBar.Opacity = e3.IsMenuVisible
                                                                                     ? 0.99
                                                                                     : 0;
                                                        };
            }
        }


        public static AppBarButton AddButton =
            new AppBarButton
                {
                    IconUri = new Uri("/Assets/Icons/appbar.add.rest.png", UriKind.Relative),
                    Text = "add",
                    Message = "Add"
                };

        public static AppBarButton RefreshButton =
            new AppBarButton
                {
                    IconUri = new Uri("/Assets/Icons/appbar.refresh.rest.png", UriKind.Relative),
                    Text = "refresh",
                    Message = "RefreshData"
                };
    }
}