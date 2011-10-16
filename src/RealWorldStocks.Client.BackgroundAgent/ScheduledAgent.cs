using System;
using System.Linq;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using RealWorldStocks.Client.Core.Data;
using RealWorldStocks.Client.Core.Data.Services;
using RealWorldStocks.Client.Core.Models;

namespace RealWorldStocks.Client.BackgroundAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            var stocksService = new StocksWebService();

            var tile = ShellTile.ActiveTiles.FirstOrDefault();

            if (tile == null)
            {
                NotifyComplete();
                return;
            }

            HttpClient.BeginGetRequest(stocksService.GetWatchListSnapshots(),
                                       response =>
                                           {
                                               if(!response.HasError)
                                               {
                                                   tile.Update(new StandardTileData
                                                   {
                                                       Count = new Random().Next(1, 99)
                                                   });
                                               }
                                        
                                               NotifyComplete();
                                           });
        }
    }
}
