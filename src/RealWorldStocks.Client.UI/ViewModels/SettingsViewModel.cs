using System;
using Microsoft.Phone.Scheduler;

namespace RealWorldStocks.Client.UI.ViewModels
{
    public class SettingsViewModel
    {

        public void ActivateAgent()
        {
            var periodicTask = new PeriodicTask("StocksAgent")
            {
                Description = "Update stocks",
                ExpirationTime = DateTime.Now.AddDays(14)
            };

            // If the agent is already registered with the system
            if (ScheduledActionService.Find(periodicTask.Name) == null)
            {
                ScheduledActionService.Add(periodicTask);
            }


            ScheduledActionService.LaunchForTest(periodicTask.Name, TimeSpan.FromSeconds(1));
        } 
    }
}