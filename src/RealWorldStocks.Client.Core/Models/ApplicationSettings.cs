using System.IO.IsolatedStorage;
using System.Reflection;
using RealWorldStocks.Client.Core.Helpers;

namespace RealWorldStocks.Client.Core.Models
{
    public class ApplicationSettings : NotifyObject
    {
        private static ApplicationSettings _current;
        public static ApplicationSettings Current
        {
            get
            {
                if (_current == null)
                    _current = new ApplicationSettings();

                return _current;
            }
            set { _current = value; }
        }

        private ApplicationSettings()
        {
            string name = Assembly.GetExecutingAssembly().FullName;
            var version = new AssemblyName(name).Version;
            ClientVersion = version.ToString();
        }


        private bool _isTrial;
        public bool IsTrial
        {
            get { return _isTrial; }
            set
            {
                _isTrial = value;
                NotifyOfPropertyChange(() => IsTrial);
            }
        }

        private string _clientVersion;

        /// <summary>
        /// The version number of the application - used to compare against their SettingsVersion to handle settings upgrades
        /// </summary>
        public string ClientVersion
        {
            get { return _clientVersion; }
            set
            {
                _clientVersion = value;
                NotifyOfPropertyChange(() => ClientVersion);
            }
        }

        /// <summary>
        /// The version number of the user's Settings file  - used to compare against their ClientVersion to handle settings upgrades
        /// </summary>
        public string SettingsVersion
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.GetSetting("SettingsVersion", ClientVersion);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["SettingsVersion"] = value;
                NotifyOfPropertyChange(() => SettingsVersion);
            }
        }


        public void DeleteAllSettings()
        {
            SettingsVersion = ClientVersion;
        }
    }
}