using System;
using System.Device.Location;
using System.IO.IsolatedStorage;
using System.Reflection;
using Caliburn.Micro;
using Microsoft.Phone.Shell;
using RealWorldWPDev.Client.Helpers;

namespace RealWorldWPDev.Client.Models
{
    public class ApplicationSettings : PropertyChangedBase
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

#if DEBUG
            // Compiling this app on MY machine, WebServiceBaseUrl will become: http://MATT-PC/RealWorldWPDev.Web/
            // The URL be automatically determined on each compile
            // Therefore no issues checking it in and getting latest on any developer machine
            WebServiceBaseUrl = DynamicLocalhost.ReplaceLocalhost("http://localhost/RealWorldWPDev.Web/");
#else
            WebServiceBaseUrl = "http://services.mydomain.com/v1/";
#endif
        }


        public string WebServiceBaseUrl { get; private set; }

        public bool AllowLocation
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.GetSetting("AllowLocation", false);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["AllowLocation"] = value;
                NotifyOfPropertyChange(() => AllowLocation);

                // If the user disables AllowLocation then clear out their current location
                if (value == false)
                {
                    GlobalData.Current.MyLocation = new GeoCoordinate();
                }
            }
        }

        public bool RunUnderLock
        {
            get
            {
                return IsolatedStorageSettings.ApplicationSettings.GetSetting("RunUnderLock", false);
            }
            set
            {
                IsolatedStorageSettings.ApplicationSettings["RunUnderLock"] = value;
                ToggleIdleDetection(value);
                NotifyOfPropertyChange(() => RunUnderLock);
            }
        }

        /// <summary>
        /// Try to toggle the OS Idle-detection but this can only be set once and may require an app restart
        /// </summary>
        private static void ToggleIdleDetection(bool enabled)
        {
            try
            {
                PhoneApplicationService.Current.ApplicationIdleDetectionMode = enabled ? IdleDetectionMode.Disabled : IdleDetectionMode.Enabled;
            }
            catch (InvalidOperationException)
            {
            }
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
            //RouteCollection.Current.Delete();
            DeleteImages();
            SettingsVersion = ClientVersion;
        }

        public void DeleteImages()
        {
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    foreach (var fileName in isoStore.GetFileNames("Images\\Stop_*"))
                    {
                        isoStore.DeleteFile("Images\\" + fileName);
                    }
                    isoStore.DeleteDirectory("Images");
                    isoStore.CreateDirectory("Images");
                }
                catch
                {

                }
            }
        }
    }
}