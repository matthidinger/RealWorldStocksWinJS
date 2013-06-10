using System.Threading.Tasks;
using RealWorldStocks.Core.Storage;

namespace RealWorldStocks.UI.Android
{
    public class AndroidSettingsStore : ISettingsStore
    {
        public Task<string> ReadAsync(string fileName)
        {
            return Task.FromResult("");
        }

        public Task SaveAsync(string fileName, string content)
        {
            return null;
        }

        public Task DeleteAsync(string fileName)
        {
            return null;
        }
    }
}