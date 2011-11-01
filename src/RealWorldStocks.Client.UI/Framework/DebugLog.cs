using System;
using System.Diagnostics;
using Caliburn.Micro;
using RealWorldStocks.Client.Core;

namespace RealWorldStocks.Client.UI.Framework
{
    public class DebugLog : ILog
    {
        [Flags]
        public enum LogThreshold
        {
            None = 0,
            Error = 1,
            Warn = 2 | Error,
            Info = 4 | Warn | Error,
            All = Error | Warn | Info
        }

        private readonly LogThreshold _logThreshold;

        public DebugLog(LogThreshold threshold = LogThreshold.All)
        {
            _logThreshold = threshold;
        }

        public void Info(string format, params object[] args)
        {
            if (_logThreshold.HasFlag(LogThreshold.Info))
            {
                Debug.WriteLine("INFO: " + format, args);
            }
        }

        public void Warn(string format, params object[] args)
        {
            if (_logThreshold.HasFlag(LogThreshold.Warn))
            {
                Debug.WriteLine("WARNING: " + format, args);
            }
        }

        public void Error(Exception exception)
        {
            if (_logThreshold.HasFlag(LogThreshold.Error))
            {
                Debug.WriteLine("ERROR: " + exception.Message);
            }
        }
    }
}