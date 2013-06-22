using System.Threading.Tasks;
using RealWorldStocks.Core.Storage;
using Windows.Storage;

namespace RealWorldStocks.UI.Storage
{
    public class WindowsSettingsStore : ISettingsStore
    {
        public Task<string> ReadAsync(string fileName)
        {
            return Task.FromResult(ApplicationData.Current.RoamingSettings.Values[fileName] as string);
        }

        public Task SaveAsync(string fileName, string content)
        {
            ApplicationData.Current.RoamingSettings.Values[fileName] = content;
            return Task.FromResult(new object());
        }

        public Task DeleteAsync(string fileName)
        {
            ApplicationData.Current.RoamingSettings.Values.Remove(fileName);
            return Task.FromResult(new object());            
        }
    }
}