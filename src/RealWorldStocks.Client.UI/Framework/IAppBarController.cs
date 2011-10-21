using System;
using Microsoft.Phone.Shell;

namespace RealWorldStocks.Client.UI.Framework
{
    /// <summary>
    /// Allows controller the Page's ApplicationBar from the view model
    /// </summary>
    public interface IAppBarController
    {
        IApplicationBar ApplicationBar { get; }
        event EventHandler AppBarChanged;
    }
}