using System;
using Microsoft.Phone.Shell;

namespace RealWorldStocks.Client.UI.Framework
{
    public interface IAppBarController
    {
        IApplicationBar ApplicationBar { get; }
        event EventHandler AppBarChanged;
    }
}