using System;
using RealWorldStocks.Core.Storage;

namespace RealWorldStocks.Core
{
    public abstract class PlatformAdapter
    {
        /// <summary>
        /// Provides acess to the current platform adapter
        /// </summary>
        public static PlatformAdapter Current { get; set; }

        public abstract void NavigateTo(string sourcePageName, object parameter = null, string query = null);

        public abstract void NavigateBack();

        public abstract void DelayInvoke(Action actionToInvoke, TimeSpan timeSpan);

        public abstract void BeginInvoke(Action actionToInvoke);

        public abstract ISettingsStore Settings { get; }
    }
}
