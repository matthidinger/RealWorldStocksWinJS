using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;

namespace $rootnamespace$
{
    /// <summary>
    /// Utility class for dynamically resolving the hostname of the current development machine
    /// </summary>
    public static class DynamicLocalhost
    {
        /// <summary>
        /// Get the hostname of the current development machine
        /// </summary>
        public static string GetLocalHostname()
        {
            var asm = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            var file = Application.GetResourceStream(new Uri(string.Format("/{0};component/DynamicLocalhost\\LocalHostname.txt", asm.Name), UriKind.Relative));
            if (file == null)
            {
                throw new FileNotFoundException("Unable to locate the DynamicLocalhost\\LocalHostname.txt file in the XAP. Make sure it exists and the Build Action is Resource");
            }
            using (var sr = new StreamReader(file.Stream))
            {
                return sr.ReadLine();
            }
        }

        /// <summary>
        /// Replace the string "localhost" in the URL with the actual hostname of the development machine
        /// </summary>
        public static string ReplaceLocalhost(string url)
        {
            var hostname = GetLocalHostname();
            if (string.IsNullOrEmpty(hostname))
            {
                return url;
            }
            return Regex.Replace(url, "localhost", hostname, RegexOptions.IgnoreCase);
        }
    }
}