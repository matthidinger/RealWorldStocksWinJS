using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using RealWorldStocks.Core.Storage;

namespace RealWorldStocks.UI.Storage
{
    public class PhoneSettingsStore : ISettingsStore
    {
        public Task<string> ReadAsync(string fileName)
        {
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var reader = new StreamReader(appStorage.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                return reader.ReadToEndAsync();
            }
        }

        public Task SaveAsync(string fileName, string content)
        {
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var writer = new StreamWriter(appStorage.OpenFile(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                return writer.WriteAsync(content);
            }
        }

        public Task DeleteAsync(string fileName)
        {
            using (var appStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                appStorage.DeleteFile(fileName);
            }

            return Task.FromResult(new object());
        }
    }
}