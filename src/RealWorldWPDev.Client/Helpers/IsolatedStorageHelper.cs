﻿using System.IO.IsolatedStorage;

namespace RealWorldWPDev.Client.Helpers
{
    public static class IsolatedStorageHelper
    {
        /// <summary>
        /// Safely get out a setting value, proving a default if it does not exist
        /// </summary>
        public static T GetSetting<T>(this IsolatedStorageSettings settings, string name, T defaultValue)
        {
            if (!settings.Contains(name))
                return defaultValue;

            return (T)settings[name];
        }
    }
}