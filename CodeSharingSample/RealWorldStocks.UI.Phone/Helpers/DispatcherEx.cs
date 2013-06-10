using System;
using System.Threading.Tasks;
using System.Windows;

namespace RealWorldStocks.UI.Phone
{
    /// <summary>
    /// Helper methods for Dispatcher invokes.
    /// </summary>
    public class DispatcherEx
    {
        /// <summary>
        /// Invokes the specified action on the UI Dispatcher thread.
        /// </summary>
        /// <param name="actionToInvoke">Action to invoke.</param>
        public static void BeginInvoke(Action actionToInvoke)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                actionToInvoke();
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke);
            }
        }

        /// <summary>
        /// Invokes the specified action on the UI Dispatcher thread.
        /// </summary>
        /// <param name="actionToInvoke">
        /// Action to invoke.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void BeginInvoke(Delegate actionToInvoke, object[] args)
        {
            if (Deployment.Current.Dispatcher.CheckAccess())
            {
                actionToInvoke.DynamicInvoke(args);
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke, args);
            }
        }

        /// <summary>
        /// Forces an action to be invoked as the last action in the current UI queue. 
        /// </summary>
        /// <param name="actionToInvoke">Action to invoke.</param>
        public static void BeginInvokeAtEndOfUiQueue(Action actionToInvoke)
        {
            Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke);
        }

        /// <summary>
        /// Forces an action to be invoked as the last action in the current UI queue. 
        /// </summary>
        /// <param name="actionToInvoke">
        /// Action to invoke.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void BeginInvokeAtEndOfUiQueue(Delegate actionToInvoke, object[] args)
        {
            Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionToInvoke">Action to invoke.</param>
        /// <param name="timeSpan">The time to delay the action</param>
        public static async void DelayInvoke(Action actionToInvoke, TimeSpan timeSpan)
        {
            await Task.Delay((int)timeSpan.TotalMilliseconds);
            Deployment.Current.Dispatcher.BeginInvoke(actionToInvoke);
        }
    }
}

