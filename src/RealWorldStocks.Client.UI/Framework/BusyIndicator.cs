using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RealWorldStocks.Client.UI.Framework
{
    public class BusyIndictator : IResult
    {
        public static BusyIndictator Show(string busyText = "")
        {
            return new BusyIndictator(true, busyText);
        }

        public static BusyIndictator Hide()
        {
            return new BusyIndictator(false);
        }


        private readonly bool _isBusy;
        private readonly string _busyText;

        public BusyIndictator(bool isBusy, string busyText = "")
        {
            _isBusy = isBusy;
            _busyText = busyText;
        }

        public void Execute(ActionExecutionContext context)
        {
            var frame = (PhoneApplicationFrame)App.Current.RootVisual;
            var page = frame.Content as PhoneApplicationPage;
            if(page != null)
            {
                var progressIndicator = new ProgressIndicator
                                            {
                                                IsVisible = _isBusy,
                                                IsIndeterminate = true,
                                                Text = _busyText
                                            };

                page.SetValue(SystemTray.ProgressIndicatorProperty, progressIndicator);
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate {};
    }
}